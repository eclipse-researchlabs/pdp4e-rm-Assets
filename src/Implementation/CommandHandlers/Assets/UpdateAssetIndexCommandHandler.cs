using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Core.Assets.Implementation.Commands.Assets;
using Core.Database;
using MediatR;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Core.Assets.Implementation.CommandHandlers.Assets
{
    public class UpdateAssetIndexCommandHandler : IRequestHandler<UpdateAssetIndexCommand, bool>
    {
        private IDatabaseContext _databaseContext;

        public UpdateAssetIndexCommandHandler(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public Task<bool> Handle(UpdateAssetIndexCommand request, CancellationToken cancellationToken)
        {
            var asset = _databaseContext.Assets.FirstOrDefault(x => x.Id == request.AssetId);
            if (asset == null) return Task.FromResult(false);

            if (string.IsNullOrEmpty(asset.Payload) || asset.Payload == "null") asset.Payload = "{}";
            var payload = JObject.Parse(asset.Payload ?? "{}");
            if (payload.ContainsKey("Index"))
            {
                if(request.Direction.ToLower() == "up") payload.SelectToken("Index").Replace(payload.SelectToken("Index").Value<int>() + 1);
                else payload.SelectToken("Index").Replace(payload.SelectToken("Index").Value<int>() - 1);
            }
            else
            {
                if (request.Direction.ToLower() == "up") payload.Add(new JProperty("Index", 1));
                else payload.Add(new JProperty("Index", -1));
            }

            asset.Payload = JsonConvert.SerializeObject(payload);
            _databaseContext.SaveChanges();

            return Task.FromResult(true);
        }
    }
}
