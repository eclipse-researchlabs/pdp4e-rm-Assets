using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Assets.Implementation.Commands;
using Core.Assets.Implementation.Commands.Assets;
using Core.Assets.Interfaces.Services;
using Core.Database;
using Core.Database.Tables;
using MediatR;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Core.Assets.Implementation.Services
{
    public class AssetService : IAssetService
    {
        private IMediator _mediator;
        private IBeawreContext _beawreContext;

        public AssetService(IMediator mediator, IBeawreContext beawreContext)
        {
            _mediator = mediator;
            _beawreContext = beawreContext;
        }

        public async Task<Asset> Create(CreateAssetCommand command) => await _mediator.Send(command);

        public async Task<bool> MovePosition(UpdateAssetPositionCommand command) => await _mediator.Send(command);

        public void ChangeName(ChangeAssetNameCommand command)
        {
            _beawreContext.Assets.FirstOrDefault(x => x.Id == command.AssetId).Name = command.Name;
            _beawreContext.SaveChanges();
        }

        public void UpdateDfdQuestionaire(UpdateDfdQuestionaireCommand command)
        {
            var item = _beawreContext.Assets.FirstOrDefault(x => x.Id == command.AssetId);
            var payload = JObject.Parse(item.Payload ?? "{}");
            if(payload.ContainsKey("DfdQuestionaire")) payload.SelectToken("DfdQuestionaire").Replace(JArray.Parse(command.Payload));
            else payload.Add(new JProperty("DfdQuestionaire", JArray.Parse(command.Payload)));
            item.Payload = JsonConvert.SerializeObject(payload);
            _beawreContext.SaveChanges();
        }

        public void Delete(Guid id)
        {
            _beawreContext.Assets.FirstOrDefault(x => x.Id == id).IsDeleted = true;
            foreach (var relation in _beawreContext.Relationship.Where(x => x.FromId == id || x.ToId == id))
                relation.IsDeleted = true;
            _beawreContext.SaveChanges();
        }
    }
}
