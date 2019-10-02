using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Assets.Implementation.Commands.Assets;
using Core.Database;
using Core.Database.Tables;
using MediatR;
using Newtonsoft.Json;

namespace Core.Assets.Implementation.CommandHandlers.Assets
{
    public class UpdateAssetPositionCommandHandler : IRequestHandler<UpdateAssetPositionCommand, bool>
    {
        private IBeawreContext _beawreContext;

        public UpdateAssetPositionCommandHandler(IBeawreContext beawreContext)
        {
            _beawreContext = beawreContext;
        }

        public Task<bool> Handle(UpdateAssetPositionCommand request, CancellationToken cancellationToken)
        {
            var entity = _beawreContext.Assets.FirstOrDefault(x => x.Id == request.AssetId);

            var payloadData = JsonConvert.DeserializeObject<AssetPayloadModel>(entity.Payload);
            payloadData.X = request.X;
            payloadData.Y = request.Y;

            entity.Payload = JsonConvert.SerializeObject(payloadData);
            _beawreContext.SaveChanges();

            return Task.FromResult(true);
        }
    }
}
