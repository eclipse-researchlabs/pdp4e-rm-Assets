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
using System.Text;
using System.Threading.Tasks;
using Core.Assets.Implementation.Commands.Risks;
using Core.Database.Tables;

namespace Core.Assets.Interfaces.Services
{
    public interface IRiskService
    {
        Task<Risk> Create(CreateRiskCommand command);
        bool Update(UpdateRiskCommand command);
        bool UpdateStatus(UpdateRiskStatusCommand command);
    }
}