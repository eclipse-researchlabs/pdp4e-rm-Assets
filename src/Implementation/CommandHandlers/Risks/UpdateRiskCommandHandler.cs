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
    public class UpdateRiskCommandHandler : IRequestHandler<UpdateRiskCommand, bool>
    {
        private IBeawreContext _beawreContext;
        private IMapper _mapper;

        public UpdateRiskCommandHandler(IBeawreContext beawreContext, IMapper mapper)
        {
            _beawreContext = beawreContext;
            _mapper = mapper;
        }

        public Task<bool> Handle(UpdateRiskCommand request, CancellationToken cancellationToken)
        {
            var payload = JsonConvert.SerializeObject(request.PayloadData);

            var item = _beawreContext.Risk.FirstOrDefault(x => x.Id == request.Id && !x.IsDeleted);
            var riskPayloadId = _beawreContext.Relationship.OrderByDescending(x => x.CreatedDateTime)
                .FirstOrDefault(x =>
                    !x.IsDeleted && x.FromType == ObjectType.Risk && x.ToType == ObjectType.RiskPayload &&
                    x.FromId == item.RootId).ToId;

            var riskPayload = _beawreContext.RiskPayload.FirstOrDefault(x => x.Id == riskPayloadId && !x.IsDeleted);
            riskPayload.Id = Guid.NewGuid();
            riskPayload.Payload = payload;
            riskPayload.Version += 1;
            riskPayload.CreatedDateTime = DateTime.Now;
            _beawreContext.RiskPayload.Add(riskPayload);

            item.Description = request.Description;
            item.Name = request.Name;
            item.Version += 1;
            item.CreatedDateTime = DateTime.Now;
            _beawreContext.Risk.Add(item);

            _beawreContext.SaveChanges();

            return Task.FromResult(true);
        }
    }
}
