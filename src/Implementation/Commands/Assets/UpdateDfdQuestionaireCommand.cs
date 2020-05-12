using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Assets.Implementation.Commands.Assets
{
    public class UpdateDfdQuestionaireCommand
    {
        public Guid AssetId { get; set; }
        public string Payload { get; set; }
    }
}
