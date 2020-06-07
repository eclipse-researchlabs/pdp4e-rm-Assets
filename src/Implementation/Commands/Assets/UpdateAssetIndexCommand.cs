using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Core.Assets.Implementation.Commands.Assets
{
    public class UpdateAssetIndexCommand : IRequest<bool>
    {
        public Guid AssetId { get; set; }
        public string Direction { get; set; } // up || down
    }
}
