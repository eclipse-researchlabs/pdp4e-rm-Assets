using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Assets.Implementation.Commands.Treatments;
using Core.Database.Models;
using Core.Database.Tables;

namespace Core.Assets.Interfaces.Services
{
    public interface ITreatmentService
    {
        Task<TreatmentModel> Create(CreateTreatmentCommand command);
        Guid Update(UpdateTreatmentCommand command);
        bool Delete(Guid id);
    }
}
