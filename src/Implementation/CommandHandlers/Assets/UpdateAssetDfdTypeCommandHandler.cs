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
    public class UpdateAssetDfdTypeCommandHandler : IRequestHandler<UpdateAssetDfdTypeCommand, bool>
    {
        private IDatabaseContext _databaseContext;

        public UpdateAssetDfdTypeCommandHandler(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public Task<bool> Handle(UpdateAssetDfdTypeCommand request, CancellationToken cancellationToken)
        {
            var asset = _databaseContext.Assets.FirstOrDefault(x => x.Id == request.AssetId);
            if (asset == null) return Task.FromResult(false);

            if (string.IsNullOrEmpty(asset.Payload) || asset.Payload == "null") asset.Payload = "{}";
            var payload = JObject.Parse(asset.Payload ?? "{}");

            if (payload.ContainsKey("Shape")) payload.SelectToken("Shape").Replace(request.Shape);
            else if (!payload.ContainsKey("Shape")) payload.Add(new JProperty("Shape", request.Shape));

            if (payload.ContainsKey("Icon")) payload.SelectToken("Icon").Replace(request.Icon);
            else if (!payload.ContainsKey("Icon")) payload.Add(new JProperty("Icon", request.Icon));

            if (payload.ContainsKey("Src")) payload.SelectToken("Src").Replace(request.Src);
            else if (!payload.ContainsKey("Src")) payload.Add(new JProperty("Src", request.Src));

            if (payload.ContainsKey("Color")) payload.SelectToken("Color").Replace(request.Color);
            else if (!payload.ContainsKey("Color")) payload.Add(new JProperty("Color", request.Color));

            asset.Payload = JsonConvert.SerializeObject(payload);
            _databaseContext.SaveChanges();

            return Task.FromResult(true);
        }
    }
}