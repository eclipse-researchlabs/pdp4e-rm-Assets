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
        private IDatabaseContext _databaseContext;
        private IMapper _mapper;

        public UpdateRiskCommandHandler(IDatabaseContext databaseContext, IMapper mapper)
        {
            _databaseContext = databaseContext;
            _mapper = mapper;
        }

        public Task<bool> Handle(UpdateRiskCommand request, CancellationToken cancellationToken)
        {
            var payload = JsonConvert.SerializeObject(request.PayloadData);

            var item = _databaseContext.Risk.FirstOrDefault(x => x.Id == request.Id && !x.IsDeleted);
            var riskPayloadId = _databaseContext.Relationship.OrderByDescending(x => x.CreatedDateTime)
                .FirstOrDefault(x =>
                    !x.IsDeleted && x.FromType == ObjectType.Risk && x.ToType == ObjectType.RiskPayload &&
                    x.FromId == item.RootId).ToId;

            var riskPayload = _databaseContext.RiskPayload.Where(x => x.RootId == riskPayloadId && !x.IsDeleted).OrderByDescending(x => x.Version).FirstOrDefault();
            riskPayload.Id = Guid.NewGuid();
            riskPayload.Payload = payload;
            riskPayload.Version += 1;
            riskPayload.CreatedDateTime = DateTime.Now;
            _databaseContext.RiskPayload.Add(riskPayload);

            item.Description = request.Description;
            item.Name = request.Name;
            item.Version += 1;
            item.CreatedDateTime = DateTime.Now;
            _databaseContext.Risk.Add(item);

            _databaseContext.SaveChanges();

            return Task.FromResult(true);
        }
    }
}
