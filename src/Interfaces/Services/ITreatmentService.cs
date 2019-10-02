using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Assets.Implementation.Commands.Treatments;
using Core.Database.Tables;

namespace Core.Assets.Interfaces.Services
{
    public interface ITreatmentService
    {
        Task<Treatment> Create(CreateTreatmentCommand command);
    }
}
