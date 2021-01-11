﻿// /********************************************************************************
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
using MediatR;

namespace Core.Assets.Implementation.Commands.Assets
{
    public class UpdateAssetDfdTypeCommand : IRequest<bool>
    {
        public Guid AssetId { get; set; }

        public string Shape { get; set; }
        public string Icon { get; set; }
        public string Src { get; set; }
        public string Color { get; set; }
    }
}