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

namespace Core.Assets.Implementation.CommandHandlers.Treatments
{
    public class UpdateTreatmentCommandHandler : IRequestHandler<UpdateTreatmentCommand, Guid>
    {
        private IBeawreContext _beawreContext;
        private IMapper _mapper;

        public UpdateTreatmentCommandHandler(IBeawreContext beawreContext, IMapper mapper)
        {
            _beawreContext = beawreContext;
            _mapper = mapper;
        }

        public Task<Guid> Handle(UpdateTreatmentCommand request, CancellationToken cancellationToken)
        {
            var currentTreatment = _beawreContext.Treatment.FirstOrDefault(x => x.Id == request.Id);

            var treatments = _mapper.Map<List<TreatmentModel>>(_beawreContext.Treatment.ToList());
            Fastenshtein.Levenshtein lev = new Fastenshtein.Levenshtein(request.Description);
            foreach (var treatmentItem in treatments)
                treatmentItem.ClosedDescriptionProbability = lev.DistanceFrom(treatmentItem.Description);

            var treatment = treatments.Where(x => x.ClosedDescriptionProbability <= 7).OrderByDescending(x => x.ClosedDescriptionProbability).FirstOrDefault();
            if (treatment == null)
            {
                treatment = new TreatmentModel() { Type = request.Type, Description = request.Description, Name = request.Name, };
                _beawreContext.Treatment.Add(treatment);
                _beawreContext.SaveChanges();
            }
            //else
            //    _beawreContext.Relationship.Add(new Relationship()
            //    {
            //        FromType = ObjectType.Treatment,
            //        FromId = treatment.Id,
            //        ToType = ObjectType.TreatmentPayload,
            //        ToId = treatmentPayload.Id
            //    });
            _beawreContext.SaveChanges();
            return Task.FromResult(treatment.Id);
        }
    }
}
