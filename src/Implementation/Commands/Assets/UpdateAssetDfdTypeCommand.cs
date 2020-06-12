using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Core.Assets.Implementation.Commands.Assets
{
    public class UpdateAssetDfdTypeCommand : IRequest<bool>
    {
        public Guid AssetId { get; set; }

        public string Shape { get; set; }
        public string Icon { get; set; }
        public string Src { get; set; }
        public string Color { get; set; }

    }
}
