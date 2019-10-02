using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Assets.Implementation.Commands.Risks;
using Core.Database.Tables;

namespace Core.Assets.Interfaces.Services
{
    public interface IRiskService
    {
        Task<Risk> Create(CreateRiskCommand command);
    }
}
