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
using Core.Assets.Models;
using Core.Database.Payloads;
using Core.Database.Tables;
using MediatR;

namespace Core.Assets.Implementation.Commands.Risks
{
    public class CreateRiskCommand : Risk, IRequest<Risk>
    {
        public Guid AssetId { get; set; }

        public List<Guid> Vulnerabilities { get; set; }
        public List<Guid> Risks { get; set; }
        public List<Treatment> Treatments { get; set; }
        public RiskPayloadModel PayloadData { get; set; }
    }
}