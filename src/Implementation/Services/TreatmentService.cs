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
using System.Threading.Tasks;
using Core.Assets.Implementation.Commands.Treatments;
using Core.Assets.Interfaces.Services;
using Core.Database;
using Core.Database.Models;
using Core.Database.Tables;
using MediatR;

namespace Core.Assets.Implementation.Services
{
    public class TreatmentService : ITreatmentService
    {
        private IDatabaseContext _databaseContext;
        private IMediator _mediator;

        public TreatmentService(IMediator mediator, IDatabaseContext databaseContext)
        {
            _mediator = mediator;
            _databaseContext = databaseContext;
        }

        public async Task<TreatmentModel> Create(CreateTreatmentCommand command) => await _mediator.Send(command);
        public Guid Update(UpdateTreatmentCommand command) => _mediator.Send(command).Result;

        public bool Delete(Guid id)
        {
            _databaseContext.Treatment.FirstOrDefault(x => x.Id == id).IsDeleted = true;
            _databaseContext.SaveChanges();
            return true;
        }
    }
}