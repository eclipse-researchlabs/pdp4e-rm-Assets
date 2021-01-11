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
using Core.Assets.Interfaces.Services;
using Core.Database.Tables;
using MediatR;

namespace Core.Assets.Implementation.Services
{
    public class RiskService : IRiskService
    {
        private IMediator _mediator;

        public RiskService(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task<Risk> Create(CreateRiskCommand command) => await _mediator.Send(command);

        public bool Update(UpdateRiskCommand command)
            => _mediator.Send(command).Result;

        public bool UpdateStatus(UpdateRiskStatusCommand command)
            => _mediator.Send(command).Result;
    }
}