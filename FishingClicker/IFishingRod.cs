using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingClicker
{
    internal interface IRod
    {
        public decimal CastLine(IRod rod);
    }
    internal enum RodAction
    {
        Ultralight,
        Light,
        Medium,
        MediumHeavy,
        Heavy,
        ExtraHeavy
    }
}