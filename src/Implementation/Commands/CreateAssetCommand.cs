using System;
using System.Collections.Generic;
using System.Text;
using Core.Database.Tables;
using MediatR;

namespace Core.Assets.Implementation.Commands
{
    public class CreateAssetCommand : Asset, IRequest<Asset>
    {
        public AssetPayloadModel PayloadData { get; set; }
        public List<Guid> Assets { get; set; }
        public Guid? ContainerRootId { get; set; }
    }
}
