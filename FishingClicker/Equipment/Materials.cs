using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FishingClicker.Equipment
{
    public enum Materials
    {
        Wood,
        Stone,
        Iron,
        Gold,
        Diamond
    }
    public record class Material()
    {
        [JsonInclude]
        public required Materials MaterialVar { get; set; }
        public virtual decimal MaterialDecimal()
        {
            return MaterialVar switch
            {
                Materials.Wood => 1,
                Materials.Stone => 1.5m,
                Materials.Iron => 2,
                Materials.Gold => 2.5m,
                Materials.Diamond => 3,
                _ => 1
            };
        }
    }
}
