using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms.VisualStyles;
using System.Xml.Serialization;

namespace FishingClicker.Mats
{
    public enum Materials
    {
        Wood,
        Stone,
        Iron,
        Gold,
        Diamond
    }
    public class Material()
    {
        [JsonInclude]
        public required Materials MaterialVar { get; set; }
        public virtual decimal MaterialDecimal()
        {
            return MaterialVar switch
            {
                Materials.Wood    => 1,
                Materials.Stone   => 1.5m,
                Materials.Iron    => 2,
                Materials.Gold    => 2.5m,
                Materials.Diamond => 3,
                _ => 1
            };
        }
    }
    public class MaterialReference()
    {
        public static List<Material> materials =
        [
            new() { MaterialVar = Materials.Wood },
            new() { MaterialVar = Materials.Stone },
            new() { MaterialVar = Materials.Iron },
            new() { MaterialVar = Materials.Gold },
            new() { MaterialVar = Materials.Diamond },
        ];
        public static decimal[] materialsWorth = materials.Select(mat => mat.MaterialDecimal()).ToArray();
        public static List<Material> MaterialsList { get { return materials; } } 
        public static decimal[] MaterialsWorth { get { return materialsWorth; } }
        public decimal[] DisplayPrices(int playerLevel)
        {

            if (playerLevel % 5 == 0)
            {
                return [(materialsWorth[0] * 80),
                        (materialsWorth[1] * 80),
                        (materialsWorth[2] * 80),
                        (materialsWorth[3] * 80),
                        (materialsWorth[4] * 80)];
            }
            return [(materialsWorth[0] * 100),
                    (materialsWorth[1] * 100),
                    (materialsWorth[2] * 100),
                    (materialsWorth[3] * 100),
                    (materialsWorth[4] * 100)];
        }
        public int[] TextBoxParser(string[] textBoxValues)
        {
            int[] values = new int[5];
            if (int.TryParse(textBoxValues[0], out int wood))    values[0] = wood;
            if (int.TryParse(textBoxValues[1], out int stone))   values[1] = stone;
            if (int.TryParse(textBoxValues[2], out int iron))    values[2] = iron;
            if (int.TryParse(textBoxValues[3], out int gold))    values[3] = gold;
            if (int.TryParse(textBoxValues[4], out int diamond)) values[4] = diamond;
            return values;
        }
        public decimal TotalPrice(string[] textBoxValues, int playerLevel)
        {
            decimal[] prices = DisplayPrices(playerLevel);
            int[] textBoxParsed = TextBoxParser(textBoxValues);
            return (textBoxParsed[0] * prices[0] +
                    textBoxParsed[1] * prices[1] +
                    textBoxParsed[2] * prices[2] +
                    textBoxParsed[3] * prices[3] +
                    textBoxParsed[4] * prices[4]);
        }
    }
}
