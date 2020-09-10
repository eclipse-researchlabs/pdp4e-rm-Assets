using System;
using System.Collections.Generic;
using System.Text;
using Core.Database.Models;
using Core.Database.Tables;
using MediatR;

namespace Core.Assets.Implementation.Commands.Treatments
{
    public class CreateTreatmentCommand : Treatment, IRequest<TreatmentModel>
    {
        public Guid AssetId { get; set; }
        public Guid? RiskId { get; set; }
    }
}
