using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Assets.Implementation.Commands.Edges
{
    public class CreateEdgeCommand
    {
        public Guid? ContainerId { get; set; }

        public string Name { get; set; }

        public Guid Asset1Guid { get; set; }
        public int Asset1Anchor { get; set; }

        public Guid Asset2Guid { get; set; }
        public int Asset2Anchor { get; set; }

    }
}
