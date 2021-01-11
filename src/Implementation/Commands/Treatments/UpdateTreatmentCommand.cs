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

namespace Core.Assets.Implementation.Commands.Treatments
{
    public class UpdateTreatmentCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }

        public string Type { get; set; }
        public string Description { get; set; }

        public string Name { get; set; }
    }
}