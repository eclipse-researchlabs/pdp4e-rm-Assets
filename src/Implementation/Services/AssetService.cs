// /********************************************************************************
//  * Copyright (c) 2020,2021 Beawre Digital SL
//  *
//  * This program and the accompanying materials are made available under the
//  * terms of the Eclipse Public License 2.0 which is available at
//  * http://www.eclipse.org/legal/epl-2.0.
//  *
//  * SPDX-License-Identifier: EPL-2.0 3
//  *
//  ********************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        private IDatabaseContext _databaseContext;
        private IMediator _mediator;

        public AssetService(IMediator mediator, IDatabaseContext databaseContext)
        {
            _mediator = mediator;
            _databaseContext = databaseContext;
        }

        public Asset GetSingle(Expression<Func<Asset, bool>> func) =>
            _databaseContext.Assets.FirstOrDefault(func);

        public async Task<Asset> Create(CreateAssetCommand command) => await _mediator.Send(command);

        public async Task<bool> MovePosition(UpdateAssetPositionCommand command) => await _mediator.Send(command);

        public void ChangeName(ChangeAssetNameCommand command)
        {
            _databaseContext.Assets.FirstOrDefault(x => x.Id == command.AssetId).Name = command.Name;
            _databaseContext.SaveChanges();
        }

        public void UpdateDfdQuestionaire(UpdateDfdQuestionaireCommand command)
        {
            if (command.AssetId.HasValue)
            {
                var item = _databaseContext.Assets.FirstOrDefault(x => x.Id == command.AssetId);
                var payload = JObject.Parse(item.Payload ?? "{}");
                if (payload.ContainsKey("DfdQuestionaire"))
                    payload.SelectToken("DfdQuestionaire").Replace(JArray.Parse(command.Payload));
                else payload.Add(new JProperty("DfdQuestionaire", JArray.Parse(command.Payload)));

                if (payload.ContainsKey("GeneratingVRT")) payload.SelectToken("GeneratingVRT").Replace(2);
                else payload.Add(new JProperty("GeneratingVRT", 2));

                item.Payload = JsonConvert.SerializeObject(payload);
            }
            else
            {
                var item = _databaseContext.Relationship.FirstOrDefault(x => x.Id == command.EdgeId);
                var payload = JObject.Parse(item.Payload ?? "{}");
                if (payload.ContainsKey("DfdQuestionaire"))
                    payload.SelectToken("DfdQuestionaire").Replace(JArray.Parse(command.Payload));
                else payload.Add(new JProperty("DfdQuestionaire", JArray.Parse(command.Payload)));

                if (payload.ContainsKey("GeneratingVRT")) payload.SelectToken("GeneratingVRT").Replace(2);
                else payload.Add(new JProperty("GeneratingVRT", 2));

                item.Payload = JsonConvert.SerializeObject(payload);
            }

            _databaseContext.SaveChanges();
        }

        public bool UpdateIndex(UpdateAssetIndexCommand command) => _mediator.Send(command).Result;

        public bool UpdateDfdType(UpdateAssetDfdTypeCommand command) => _mediator.Send(command).Result;

        public void Delete(Guid id)
        {
            var asset = _databaseContext.Assets.FirstOrDefault(x => x.Id == id);
            if (asset == null) return;
            asset.IsDeleted = true;
            foreach (var relation in _databaseContext.Relationship.Where(x => x.FromId == id || x.ToId == id))
                relation.IsDeleted = true;
            _databaseContext.SaveChanges();
        }
    }
}