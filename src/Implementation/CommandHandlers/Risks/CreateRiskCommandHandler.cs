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
        private IDatabaseContext _databaseContext;
        private IMapper _mapper;

        public CreateRiskCommandHandler(IDatabaseContext databaseContext, IMapper mapper)
        {
            _databaseContext = databaseContext;
            _mapper = mapper;
        }

        public Task<Risk> Handle(CreateRiskCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Risk>(request);

            entity.Payload = JsonConvert.SerializeObject(request.PayloadData);

            var riskPayload = new RiskPayload(){ Payload = entity.Payload };
            _databaseContext.RiskPayload.Add(riskPayload);
            _databaseContext.SaveChanges();

            var risk = _databaseContext.Risk.FirstOrDefault(x => x.Name.ToLower() == request.Name.ToLower());
            if (risk == null)
            {
                risk = new Risk(){ Description = request.Description, Name = request.Name };
                _databaseContext.Risk.Add(risk);
                _databaseContext.SaveChanges();
            }
            else
            {
                risk.Description = request.Description;
                _databaseContext.SaveChanges();
            }

            _databaseContext.Relationship.Add(new Relationship()
            {
                FromType = ObjectType.Risk, 
                FromId = risk.RootId, 
                ToType = ObjectType.RiskPayload, 
                ToId = riskPayload.RootId
            });

            return Task.FromResult(risk);
        }
    }
}
