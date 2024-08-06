using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingClicker.Equipment
{
    internal interface IEquipment
    {
        public void Upgrade();
        public decimal Recycle();
        public void Evolve();
        public Rarity RarityValue { get; }
        public Level ItemLevel { get; }
        public Materials MaterialVar { get;  }
        public decimal RarityDecimal();
        public decimal LevelDecimal();
    }
}
