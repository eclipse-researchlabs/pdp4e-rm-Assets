﻿using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace Core.Assets.Implementation.Commands.Assets
{
    public class LinkAssetToContainerCommand
    {
        public Guid AssetId { get; set; }
        public Guid ContainerId { get; set; }

        public string X { get; set; }
        public string Y { get; set; }

        public Guid CreateByUserId { get; set; }
    }
}
