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
