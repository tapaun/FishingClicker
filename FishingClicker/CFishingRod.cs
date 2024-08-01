using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingClicker
{
    //Rod action is basically a rod's durability/weight capacity
    internal enum RodAction
    {
        Ultralight,
        Light,
        Medium,
        MediumHeavy,
        Heavy,
        ExtraHeavy
    }
    internal class CFishingRod : CEquipment
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
        public virtual decimal CastLine(decimal strengthMultiplier, decimal rarityMultiplier, decimal levelMultiplier)
        {
            decimal castLine = Strength * strengthMultiplier + RarityDecimal() * rarityMultiplier + LevelDecimal() * levelMultiplier;
            return castLine;
        }
    }
    #region Beginner Rod class
    internal class BeginnerRod : CFishingRod
    {

        public BeginnerRod(string name, Rarity rarityValue, decimal strength, RodAction category, Level itemLevel, Material material) : base(name, rarityValue, strength, category, itemLevel, material)
        {
        }
        public override decimal CastLine(decimal strengthMultiplier = 1.2m, decimal rarityMultiplier = 1.25m, decimal levelMultiplier = 1.1m)
        {
            return base.CastLine(strengthMultiplier, rarityMultiplier, levelMultiplier);
        }
    }
    #endregion
    #region Intermediate Rod class
    internal class IntermediateRod : CFishingRod
    {

        public IntermediateRod(string name, Rarity rarityValue, decimal strength, RodAction category, Level itemLevel, Material material) : base(name, rarityValue, strength, category, itemLevel, material)
        {
        }
        public override decimal CastLine(decimal strengthMultiplier = 1.4m, decimal rarityMultiplier = 1.5m, decimal levelMultiplier = 1.2m)
        {
            return base.CastLine(strengthMultiplier, rarityMultiplier, levelMultiplier);
        }
    }
    #endregion
    #region Expert Rod class
    internal class ExpertRod : CFishingRod
    {

        public ExpertRod(string name, Rarity rarityValue, decimal strength, RodAction category, Level itemLevel, Material material) : base(name, rarityValue, strength, category, itemLevel, material)
        {
        }
        public override decimal CastLine(decimal strengthMultiplier = 1.6m, decimal rarityMultiplier = 1.8m, decimal levelMultiplier = 1.4m)
        {
            return base.CastLine(strengthMultiplier, rarityMultiplier, levelMultiplier);
        }
    }
    #endregion
}
