using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Assets.Implementation.Commands.Risks;
using Core.Assets.Interfaces.Services;
using Core.Database.Tables;
using MediatR;

namespace Core.Assets.Implementation.Services
{
    public class RiskService : IRiskService
    {
        private IMediator _mediator;

        public RiskService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Risk> Create(CreateRiskCommand command) => await _mediator.Send(command);
        public bool Update(UpdateRiskCommand command) => _mediator.Send(command).Result;

    }
}
