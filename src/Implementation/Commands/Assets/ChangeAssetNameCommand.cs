using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Assets.Implementation.Commands.Assets
{
    public class ChangeAssetNameCommand
    {
        public Guid AssetId { get; set; }
        public string Name { get; set; }
    }
}
