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
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Core.Assets.Implementation.Commands.Treatments;
using Core.Database;
using Core.Database.Enums;
using Core.Database.Models;
using Core.Database.Payloads;
using Core.Database.Tables;
using MediatR;
using Newtonsoft.Json;

namespace Core.Assets.Implementation.CommandHandlers.Treatments
{
    public class UpdateTreatmentCommandHandler : IRequestHandler<UpdateTreatmentCommand, Guid>
    {
        private IDatabaseContext _databaseContext;
        private IMapper _mapper;

        public UpdateTreatmentCommandHandler(IDatabaseContext databaseContext, IMapper mapper)
        {
            _databaseContext = databaseContext;
            _mapper = mapper;
        }

        public Task<Guid> Handle(UpdateTreatmentCommand request, CancellationToken cancellationToken)
        {
            var currentTreatment = _databaseContext.Treatment.FirstOrDefault(x => x.Id == request.Id);

            var treatments = _mapper.Map<List<TreatmentModel>>(_databaseContext.Treatment.ToList());
            Fastenshtein.Levenshtein lev = new Fastenshtein.Levenshtein(request.Description);
            foreach (var treatmentItem in treatments)
                treatmentItem.ClosedDescriptionProbability = lev.DistanceFrom(treatmentItem.Description);

            var treatment = treatments.Where(x => x.ClosedDescriptionProbability <= 7).OrderByDescending(x => x.ClosedDescriptionProbability).FirstOrDefault();
            if (treatment == null)
            {
                treatment = new TreatmentModel() {Type = request.Type, Description = request.Description, Name = request.Name,};
                _databaseContext.Treatment.Add(treatment);
                _databaseContext.SaveChanges();
            }

            //else
            //    _databaseContext.Relationship.Add(new Relationship()
            //    {
            //        FromType = ObjectType.Treatment,
            //        FromId = treatment.Id,
            //        ToType = ObjectType.TreatmentPayload,
            //        ToId = treatmentPayload.Id
            //    });
            _databaseContext.SaveChanges();
            return Task.FromResult(treatment.Id);
        }
    }
}