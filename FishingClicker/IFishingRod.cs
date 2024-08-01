using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingClicker
{
    internal interface IRod
    {
        public decimal CastLine(decimal strengthMultiplier, decimal rarityMultiplier, decimal levelMultiplier);
    }
}