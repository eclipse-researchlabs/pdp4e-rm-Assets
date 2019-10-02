using System;
using System.Collections.Generic;
using System.Text;
using Core.Database.Tables;

namespace Core.Assets.Models
{
    public class AssetModel : Asset
    {
        public AssetPayloadModel PayloadData { get; set; }
    }
}
