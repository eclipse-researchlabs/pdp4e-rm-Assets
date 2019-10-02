using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Core.Assets.Implementation.Commands.Risks;
using Core.Database;
using Core.Database.Enums;
using Core.Database.Tables;
using MediatR;
using Newtonsoft.Json;

namespace Core.Assets.Implementation.CommandHandlers.Risks
{
    public class CreateRiskCommandHandler : IRequestHandler<CreateRiskCommand, Risk>
    {
        private IBeawreContext _beawreContext;
        private IMapper _mapper;

        public CreateRiskCommandHandler(IBeawreContext beawreContext, IMapper mapper)
        {
            _beawreContext = beawreContext;
            _mapper = mapper;
        }

        public Task<Risk> Handle(CreateRiskCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Risk>(request);

            entity.Payload = JsonConvert.SerializeObject(request.PayloadData);

            var riskPayload = new RiskPayload(){ Payload = entity.Payload };
            _beawreContext.RiskPayload.Add(riskPayload);
            _beawreContext.SaveChanges();

            var risk = _beawreContext.Risk.FirstOrDefault(x => x.Name.ToLower() == request.Name.ToLower());
            if (risk == null)
            {
                risk = new Risk(){ Description = request.Description, Name = request.Name };
                _beawreContext.Risk.Add(risk);
                _beawreContext.SaveChanges();
            }
            else
            {
                risk.Description = request.Description;
                _beawreContext.SaveChanges();
            }

            _beawreContext.Relationship.Add(new Relationship()
            {
                FromType = ObjectType.Risk, 
                FromId = risk.Id, 
                ToType = ObjectType.RiskPayload, 
                ToId = riskPayload.Id
            });

            return Task.FromResult(risk);
        }
    }
}
