using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingClicker
{
    internal class BeginnerRod : CFishingRod
    {

        public BeginnerRod(string name, Rarity rarityValue, decimal strength, RodAction category, Level itemLevel, Material material) : base(name, rarityValue, strength, category, itemLevel, material)
        {
        }
        public override decimal CastLine(decimal strengthMultiplier=1.2m, decimal rarityMultiplier=1.25m, decimal levelMultiplier=1.1m)
        {
            return base.CastLine(strengthMultiplier, rarityMultiplier, levelMultiplier);
        }
    }
}
