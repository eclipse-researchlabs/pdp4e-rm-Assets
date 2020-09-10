using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Assets.Implementation.Commands.Treatments;
using Core.Assets.Interfaces.Services;
using Core.Database;
using Core.Database.Models;
using Core.Database.Tables;
using MediatR;

namespace Core.Assets.Implementation.Services
{
    public class TreatmentService : ITreatmentService
    {
        private IMediator _mediator;
        private IBeawreContext _beawreContext;

        public TreatmentService(IMediator mediator, IBeawreContext beawreContext)
        {
            _mediator = mediator;
            _beawreContext = beawreContext;
        }

        public async Task<TreatmentModel> Create(CreateTreatmentCommand command) => await _mediator.Send(command);
        public Guid Update(UpdateTreatmentCommand command) => _mediator.Send(command).Result;

        public bool Delete(Guid id)
        {
            _beawreContext.Treatment.FirstOrDefault(x => x.Id == id).IsDeleted = true;
            _beawreContext.SaveChanges();
            return true;
        }
    }
}
