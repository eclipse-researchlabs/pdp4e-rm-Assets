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
using Core.Assets.Implementation.Commands.Treatments;
using Core.Database.Models;
using Core.Database.Tables;

namespace Core.Assets.Interfaces.Services
{
    public interface ITreatmentService
    {
        Task<TreatmentModel> Create(CreateTreatmentCommand command);
        Guid Update(UpdateTreatmentCommand command);
        bool Delete(Guid id);
    }
}