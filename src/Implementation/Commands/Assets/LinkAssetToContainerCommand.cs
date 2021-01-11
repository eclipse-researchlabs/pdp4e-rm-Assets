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
using MediatR;

namespace Core.Assets.Implementation.Commands.Assets
{
    public class LinkAssetToContainerCommand
    {
        public Guid AssetId { get; set; }
        public Guid ContainerId { get; set; }

        public string X { get; set; }
        public string Y { get; set; }

        public Guid CreateByUserId { get; set; }
    }
}