using System;
using System.Collections.Generic;
using System.Text;
using Core.Assets.Implementation.Commands.Edges;

namespace Core.Assets.Interfaces.Services
{
    public interface IAssetEdgeService
    {
        void Update(ChangeEdgeLabelCommand command);
        bool Delete(Guid id);
    }
}
