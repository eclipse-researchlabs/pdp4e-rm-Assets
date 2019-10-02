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
