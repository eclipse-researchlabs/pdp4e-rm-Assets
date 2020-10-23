using MediatR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Assets.Implementation.Commands.Risks
{
    public class UpdateRiskStatusCommand : IRequest<bool>
    {
        public Guid RiskId { get; set; }

        public string Status { get; set; }
        public string AdditionalValue1 { get; set; }
    }
}
