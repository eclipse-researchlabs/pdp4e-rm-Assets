using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Core.Assets.Implementation.Commands.Treatments;
using Core.Database;
using Core.Database.Enums;
using Core.Database.Models;
using Core.Database.Payloads;
using Core.Database.Tables;
using MediatR;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Core.Assets.Implementation.CommandHandlers.Treatments
{
    public class CreateTreatmentCommandHandler : IRequestHandler<CreateTreatmentCommand, TreatmentModel>
    {
        private IMapper _mapper;
        private IBeawreContext _beawreContext;

        public CreateTreatmentCommandHandler(IMapper mapper, IBeawreContext beawreContext)
        {
            _mapper = mapper;
            _beawreContext = beawreContext;
        }

        public Task<TreatmentModel> Handle(CreateTreatmentCommand request, CancellationToken cancellationToken)
        {
            var requestPayload = JObject.Parse(request.Payload ?? "{}");
            requestPayload.Add(new JProperty("Status", "pending-approval"));

            var treatmentPayload = new TreatmentPayload() { Payload = JsonConvert.SerializeObject(requestPayload) };
            _beawreContext.TreatmentPayload.Add(treatmentPayload);
            _beawreContext.SaveChanges();

            var treatments = _mapper.Map<List<TreatmentModel>>(_beawreContext.Treatment.ToList());
            Fastenshtein.Levenshtein lev = new Fastenshtein.Levenshtein(request.Description);
            foreach (var treatmentItem in treatments)
                treatmentItem.ClosedDescriptionProbability = lev.DistanceFrom(treatmentItem.Description);

            var treatment = treatments.Where(x => x.ClosedDescriptionProbability <= 7 && x.Type?.ToLower() == request.Type.ToLower()).OrderByDescending(x => x.ClosedDescriptionProbability).FirstOrDefault();
            if (treatment == null)
            {
                treatment = new TreatmentModel() { Type = request.Type, Description = request.Description, Name = request.Name };
                _beawreContext.Treatment.Add(treatment);
                _beawreContext.SaveChanges();
            }

            var treatmentModel = _mapper.Map<TreatmentModel>(treatment);
            treatmentModel.Payload = treatmentPayload;

            if (request.RiskId.HasValue)
                _beawreContext.Relationship.Add(new Relationship()
                {
                    FromType = ObjectType.Risk,
                    FromId = request.RiskId.Value,
                    ToType = ObjectType.TreatmentPayload,
                    ToId = treatmentPayload.Id
                });

            _beawreContext.Relationship.Add(new Relationship()
            {
                FromType = ObjectType.Treatment,
                FromId = treatment.Id,
                ToType = ObjectType.TreatmentPayload,
                ToId = treatmentPayload.Id
            });
            _beawreContext.SaveChanges();
            return Task.FromResult(treatmentModel);
        }
    }
}
