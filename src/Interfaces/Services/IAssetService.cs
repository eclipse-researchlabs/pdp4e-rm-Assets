using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Core.Assets.Implementation.Commands;
using Core.Assets.Implementation.Commands.Assets;
using Core.Database.Tables;

namespace Core.Assets.Interfaces.Services
{
    public interface IAssetService
    {
        Task<Asset> Create(CreateAssetCommand command);
        Task<bool> MovePosition(UpdateAssetPositionCommand command);
        void ChangeName(ChangeAssetNameCommand command);
        void Delete(Guid id);
        void UpdateDfdQuestionaire(UpdateDfdQuestionaireCommand command);
    }
}
