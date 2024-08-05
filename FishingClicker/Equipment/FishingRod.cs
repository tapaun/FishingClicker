using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization;

namespace FishingClicker.Equipment
{
    #region Enum
    //Rod action is basically a rod's durability/weight capacity
    public enum RodAction
    {
        Ultralight,
        Light,
        Medium,
        MediumHeavy,
        Heavy,
        ExtraHeavy
    }
    #endregion
    [XmlInclude(typeof(BeginnerRod))]
    [XmlInclude(typeof(IntermediateRod))]
    [XmlInclude(typeof(ExpertRod))]
    [Serializable]
    public class FishingRod : Equipment
    {
        #region Variables and Sonstructor
        private decimal strength;
        private RodAction category;

        public FishingRod(string equipmentName, Rarity rarityValue, decimal strength, RodAction category, Level itemLevel, Material material) : base(equipmentName, rarityValue, itemLevel, material)
        {
            this.strength = strength;
            this.category = category;
        }
        public FishingRod() { }
        #endregion

        #region Getters and Setters
        //this should be used when applying maybe potions or random strength boost
        [XmlElement("Strength")]
        public decimal Strength { get => strength; set => strength = value; }
        [XmlElement("Category")]
        public RodAction Category { get => category; set => category = value; }
        #endregion

        #region Override methods - DecimalValue(Rarity,Level,Material)
        public override decimal RarityDecimal()
        {
            return base.RarityDecimal();
        }
        public override decimal LevelDecimal()
        {
            return base.LevelDecimal();
        }
        public override decimal MaterialDecimal()
        {
            return base.MaterialDecimal();
        }
        #endregion

        #region Methods
        //Method for displaying a fishing rod's information
        public override string DisplayInfo()
        {
            return base.DisplayInfo() + $" Name: {EquipmentName} \n Strength: {Strength} \n Rod Action: {Category} \n";
        }

        //Method for deciding how far a CastLine would go depending on variables below, used to determine the type of fish you'd be able to get
        public virtual decimal CastLine(decimal strengthMultiplier, decimal rarityMultiplier, decimal levelMultiplier, decimal materialMultiplier)
        {
            decimal castLine = Strength * strengthMultiplier + RarityDecimal() * rarityMultiplier + LevelDecimal() * levelMultiplier + MaterialDecimal() * materialMultiplier;
            return castLine;
        }
        #endregion
    }

    #region Beginner Rod class
    public class BeginnerRod : FishingRod
    {
        #region Constructor
        public BeginnerRod(string equipmentName, Rarity rarityValue, decimal strength, RodAction category, Level itemLevel, Material material) : base(equipmentName, rarityValue, strength, category, itemLevel, material)
        {
        }
        public BeginnerRod() { }
        #endregion
        #region Override method
        public override decimal CastLine(decimal strengthMultiplier = 1.2m, decimal rarityMultiplier = 1.25m, decimal levelMultiplier = 1.1m, decimal materialMultiplier = 1.1m)
        {
            return base.CastLine(strengthMultiplier, rarityMultiplier, levelMultiplier, materialMultiplier);
        }
        #endregion
    }
    #endregion

    #region Intermediate Rod class
    public class IntermediateRod : FishingRod
    {
        #region Constructor
        public IntermediateRod(string equipmentName, Rarity rarityValue, decimal strength, RodAction category, Level itemLevel, Material material) : base(equipmentName, rarityValue, strength, category, itemLevel, material)
        {
        }
        public IntermediateRod() { }
        #endregion
        #region Override method
        public override decimal CastLine(decimal strengthMultiplier = 1.4m, decimal rarityMultiplier = 1.5m, decimal levelMultiplier = 1.2m, decimal materialMultiplier = 1.3m)
        {
            return base.CastLine(strengthMultiplier, rarityMultiplier, levelMultiplier, materialMultiplier);
        }
        #endregion
    }
    #endregion

    #region Expert Rod class
    public class ExpertRod : FishingRod
    {
        #region Constructor
        public ExpertRod(string equipmentName, Rarity rarityValue, decimal strength, RodAction category, Level itemLevel, Material material) : base(equipmentName, rarityValue, strength, category, itemLevel, material)
        {
        }
        public ExpertRod() { }
        #endregion
        #region Override method
        public override decimal CastLine(decimal strengthMultiplier = 1.6m, decimal rarityMultiplier = 1.8m, decimal levelMultiplier = 1.4m, decimal materialMultiplier = 1.1m)
        {
            return base.CastLine(strengthMultiplier, rarityMultiplier, levelMultiplier, materialMultiplier);
        }
        #endregion
    }
    #endregion
}
