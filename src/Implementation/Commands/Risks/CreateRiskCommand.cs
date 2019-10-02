using System;
using System.Collections.Generic;
using System.Text;
using Core.Assets.Models;
using Core.Database.Payloads;
using Core.Database.Tables;
using MediatR;

namespace Core.Assets.Implementation.Commands.Risks
{
    public class CreateRiskCommand : Risk, IRequest<Risk>
    {
        public Guid AssetId { get; set; }

        public List<Guid> Vulnerabilities { get; set; }
        public List<Guid> Risks { get; set; }
        public List<Treatment> Treatments { get; set; }
        public RiskPayloadModel PayloadData { get; set; }
    }
}
