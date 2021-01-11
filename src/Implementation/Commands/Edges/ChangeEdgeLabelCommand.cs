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

namespace Core.Assets.Implementation.Commands.Edges
{
    public class ChangeEdgeLabelCommand
    {
        public Guid EdgeId { get; set; }
        public string Label { get; set; }
        public string Shape { get; set; }
    }
}