using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Core.Assets.Implementation.Commands.Edges;
using Core.Assets.Interfaces.Services;
using Core.Database;
using Core.Database.Models;
using Newtonsoft.Json;

namespace Core.Assets.Implementation.Services
{
    public class AssetEdgeService : IAssetEdgeService
    {
        private IDatabaseContext _databaseContext;

        public AssetEdgeService(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public void Update(ChangeEdgeLabelCommand command)
        {
            var edge = _databaseContext.Relationship.FirstOrDefault(x => x.Id == command.EdgeId);
            var payload = JsonConvert.DeserializeObject<AssetEdgePayloadModel>(edge.Payload ?? "{}");
            payload.Name = command.Label;
            payload.Shape = command.Shape;
            edge.Payload = JsonConvert.SerializeObject(payload);
            _databaseContext.SaveChanges();
        }

        public bool Delete(Guid id)
        {
            var item = _databaseContext.Relationship.FirstOrDefault(x => x.Id == id);
            if (item == null) return false;
            item.IsDeleted = true;
            _databaseContext.SaveChanges();
            return true;
        }
    }
}
