using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingClicker
{
    internal enum RodAction
    {
        Ultralight,
        Light,
        Medium,
        MediumHeavy,
        Heavy,
        ExtraHeavy
    }
    internal class CFishingRod : CEquipment, IRod
    {
        private string name;
        private decimal strength;
        private RodAction category;
        public Rarity rarityValue;
        public Level itemLevel;
        public Material material;

        public CFishingRod(string name, Rarity rarityValue, decimal strength, RodAction category, Level itemLevel, Material material) : base(rarityValue, itemLevel, material)
        {
            this.name = name;
            this.rarityValue = rarityValue;
            this.strength = strength;
            this.category = category;
            this.itemLevel = itemLevel;
            this.material = material;
        }

        public string Name { get => name; set => name = value; }
        public decimal Strength { get => strength; set => strength = value; }
        internal RodAction Category { get => category; set => category = value; }
        public override decimal RarityDecimal()
        {
            return base.RarityDecimal();
        }
        public override decimal LevelDecimal()
        {
            return base.LevelDecimal();
        }
        public override string DisplayInfo()
        {
            return base.DisplayInfo() + ($" Name: {name} \n Strength: {strength} \n Rod Action: {category} \n");
        }
        public decimal CastLine(IRod beginnerRod)
        {
            Random random = new Random(10);
            decimal minDistance = 0.5m;
            decimal maxDistance = 10m;
            decimal distanceCast = (decimal)random.NextDouble() * (maxDistance - minDistance) + minDistance;
            return Strength * 0.1m * distanceCast + RarityDecimal()*0.75m + LevelDecimal()*0.2m;
        }

    }
}
