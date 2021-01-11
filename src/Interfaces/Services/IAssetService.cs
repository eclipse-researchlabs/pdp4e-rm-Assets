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
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Core.Assets.Implementation.Commands;
using Core.Assets.Implementation.Commands.Assets;
using Core.Database.Tables;

namespace Core.Assets.Interfaces.Services
{
    public interface IAssetService
    {
        Task<Asset> Create(CreateAssetCommand command);
        Task<bool> MovePosition(UpdateAssetPositionCommand command);
        void ChangeName(ChangeAssetNameCommand command);
        void Delete(Guid id);
        void UpdateDfdQuestionaire(UpdateDfdQuestionaireCommand command);
        bool UpdateIndex(UpdateAssetIndexCommand command);
        bool UpdateDfdType(UpdateAssetDfdTypeCommand command);
        Asset GetSingle(Expression<Func<Asset, bool>> func);
    }
}