using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Assets.Implementation.Commands.Risks;
using Core.Database;
using Core.Database.Enums;
using MediatR;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Core.Assets.Implementation.CommandHandlers.Risks
{
    public class UpdateRiskStatusCommandHandler : IRequestHandler<UpdateRiskStatusCommand, bool>
    {
        private IBeawreContext _beawreContext;

        public UpdateRiskStatusCommandHandler(IBeawreContext beawreContext)
        {
            _beawreContext = beawreContext;
        }

        public Task<bool> Handle(UpdateRiskStatusCommand request, CancellationToken cancellationToken)
        {
            var riskPayloadId = _beawreContext.Relationship.FirstOrDefault(x => x.ToType == ObjectType.RiskPayload && x.FromType == ObjectType.Risk && x.FromId == request.RiskId && !x.IsDeleted)?.ToId;
            if (!riskPayloadId.HasValue) throw new Exception("NO_RISK_FOUND");

            var riskPayload = _beawreContext.RiskPayload.Where(x => x.RootId == riskPayloadId && !x.IsDeleted).OrderByDescending(x => x.Version).FirstOrDefault();
            if (riskPayload == null) throw new Exception("NO_RISK_PAYLOAD_FOUND");

            var payload = JObject.Parse(riskPayload.Payload ?? "{}");
            if (payload.ContainsKey("Status")) payload.SelectToken("Status").Replace(request.Status);
            else payload.Add(new JProperty("Status", request.Status));

            if (payload.ContainsKey("StatusAdditionalData1")) payload.SelectToken("StatusAdditionalData1").Replace(request.AdditionalValue1);
            else payload.Add(new JProperty("StatusAdditionalData1", request.AdditionalValue1));

            riskPayload.Payload = JsonConvert.SerializeObject(payload);
            _beawreContext.SaveChanges();

            return Task.FromResult(true);
        }
    }
}
