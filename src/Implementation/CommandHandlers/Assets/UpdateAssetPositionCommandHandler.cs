﻿using System;
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
        private IDatabaseContext _databaseContext;

        public UpdateAssetPositionCommandHandler(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public Task<bool> Handle(UpdateAssetPositionCommand request, CancellationToken cancellationToken)
        {
            var relationship =
                _databaseContext.Relationship.FirstOrDefault(x =>
                    x.FromId == request.ContainerId && x.ToId == request.AssetId);

            var payload = JsonConvert.DeserializeObject<AssetPayloadModel>(relationship?.Payload ?? "{}");

            payload.X = request.X;
            payload.Y = request.Y;

            if (relationship == null) return Task.FromResult(false);
            relationship.Payload = JsonConvert.SerializeObject(payload);
            _databaseContext.SaveChanges();

            return Task.FromResult(true);
        }
    }
}
