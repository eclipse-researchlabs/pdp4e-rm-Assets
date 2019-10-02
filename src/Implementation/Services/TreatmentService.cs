using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Assets.Implementation.Commands.Treatments;
using Core.Assets.Interfaces.Services;
using Core.Database.Tables;
using MediatR;

namespace Core.Assets.Implementation.Services
{
    public class TreatmentService : ITreatmentService
    {
        private IMediator _mediator;

        public TreatmentService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Treatment> Create(CreateTreatmentCommand command) => await _mediator.Send(command);
    }
}
