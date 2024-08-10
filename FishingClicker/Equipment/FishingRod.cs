using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

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
    [Serializable]
    [JsonDerivedType(typeof(BeginnerRod), "BeginnerRod")]
    [JsonDerivedType(typeof(IntermediateRod), "IntermediateRod")]
    [JsonDerivedType(typeof(ExpertRod), "ExpertRod")]
    public record class FishingRod : Equipment
    {
        #region Variables
        [JsonInclude]
        public required decimal Strength { get; init; }
        [JsonInclude]
        public required RodAction Category { get; init; }
        protected virtual decimal StrengthMultiplier { get; }
        protected virtual decimal RarityMultiplier { get; }
        protected virtual decimal LevelMultiplier { get; }
        protected virtual decimal MaterialMultiplier { get; }

        public FishingRod()  { }
        #endregion

        #region Methods
        //Method for displaying a fishing rod's information
        public override string ToString()
        {
            return base.ToString() + $" Name: {EquipmentName} \n Strength: {Strength} \n Rod Action: {Category} \n";
        }

        //Method for deciding how far a CastLine would go depending on variables below, used to determine the type of fish you'd be able to get
        public decimal CastLine() =>
              Strength          * StrengthMultiplier +
              RarityDecimal()   * RarityMultiplier +
              LevelDecimal()    * LevelMultiplier +
              MaterialDecimal() * MaterialMultiplier;
        #endregion
    }

    #region Beginner Rod class
    public record class BeginnerRod : FishingRod
    {
        protected override decimal StrengthMultiplier => 1.2m;
        protected override decimal RarityMultiplier   => 1.25m;
        protected override decimal LevelMultiplier    => 1.1m;
        protected override decimal MaterialMultiplier => 1.1m;
        public BeginnerRod() { }
    }
    #endregion

    #region Intermediate Rod class
    public record class IntermediateRod : FishingRod
    {
        protected override decimal StrengthMultiplier => 1.5m;
        protected override decimal RarityMultiplier   => 1.35m;
        protected override decimal LevelMultiplier    => 1.2m;
        protected override decimal MaterialMultiplier => 1.2m;
        public IntermediateRod() { }
    }
    #endregion

    #region Expert Rod class
    public record class ExpertRod : FishingRod
    {
        protected override decimal StrengthMultiplier => 1.7m;
        protected override decimal RarityMultiplier   => 1.4m;
        protected override decimal LevelMultiplier    => 1.3m;
        protected override decimal MaterialMultiplier => 1.3m;
        public ExpertRod() { }
    }
    #endregion
}
