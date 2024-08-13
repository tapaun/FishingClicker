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
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;
using FishingClicker.User;

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
    public class FishingRod : Equipment
    {
        #region Variables
        [JsonInclude]
        public required decimal Strength { get; set; }
        [JsonInclude]
        public required RodAction Category { get; set; }
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
            return base.ToString() + $" Name: {EquipmentName} Strength: {Strength} Rod Action: {Category}";
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
    public class BeginnerRod : FishingRod
    {
        protected override decimal StrengthMultiplier => 1.2m;
        protected override decimal RarityMultiplier   => 1.25m;
        protected override decimal LevelMultiplier    => 1.1m;
        protected override decimal MaterialMultiplier => 1.1m;
        public BeginnerRod() { }
    }
    #endregion

    #region Intermediate Rod class
    public class IntermediateRod : FishingRod
    {
        protected override decimal StrengthMultiplier => 1.5m;
        protected override decimal RarityMultiplier   => 1.35m;
        protected override decimal LevelMultiplier    => 1.2m;
        protected override decimal MaterialMultiplier => 1.2m;
        public IntermediateRod() { }
    }
    #endregion

    #region Expert Rod class
    public class ExpertRod : FishingRod
    {
        protected override decimal StrengthMultiplier => 1.7m;
        protected override decimal RarityMultiplier   => 1.4m;
        protected override decimal LevelMultiplier    => 1.3m;
        protected override decimal MaterialMultiplier => 1.3m;
        public ExpertRod() { }
    }
    #endregion

    #region Case class for the shop
    public abstract class Case()
    {
        public List<FishingRod> ReadFromFile(string filePath)
        {
            var options = new JsonSerializerOptions
            {
                Converters = {new JsonStringEnumConverter() },
                WriteIndented = true
            };
            string jsonString = File.ReadAllText(filePath);
            List<FishingRod> fRods = JsonSerializer.Deserialize<List<FishingRod>>(jsonString, options);
            return fRods;
        }
        protected static Random rnd = new();
        protected virtual int DiceRoll { get; set; }
        protected virtual int Price { get; set; }
        protected virtual RodAction CaseCategory { get; set; }
        public void FishingRodOpened(Player player)
        {
            if (player.PlayerGold >= Price)
            {
                var rollingRods = new List<FishingRod>();
                if (player.FishingRod.All(x => rollingRods.Any(y => x.EquipmentName == y.EquipmentName))) return;  
                rollingRods = DiceRoll switch
                {
                    (<= 50) => ReadFromFile("beginnerRods.json"),
                    (<= 90) => ReadFromFile("intermediateRods.json"),
                    (> 90)  => ReadFromFile("expertRods.json")
                };
                player.PlayerGold -= Price;
                int rodCount = rollingRods.Count;
                FishingRod selectedRod;
                int rndRod;
                do
                {
                    rndRod = rnd.Next(0, rodCount);
                    selectedRod = rollingRods.ElementAt(rndRod);
                    selectedRod.Category = CaseCategory;
                } while (player.FishingRod.FindIndex(x=>x.EquipmentName==selectedRod.EquipmentName)!=-1);
                player.FishingRod.Add(selectedRod);
                MessageBox.Show($"Congrats you've obtained {selectedRod.Category} {selectedRod.EquipmentName}");
            }
            else{ return; }
        }
    }
    public class UltraLightCase() : Case
    {
        protected override int DiceRoll => rnd.Next(1, 100);
        protected override int Price => 500;
        protected override RodAction CaseCategory => RodAction.Ultralight;

    }
    public class LightCase() : Case
    {
        protected override int DiceRoll => rnd.Next(1, 100);
        protected override int Price => 500;
        protected override RodAction CaseCategory => RodAction.Light;
    }
    public class MediumCase() : Case
    {
        protected override int DiceRoll => rnd.Next(1, 100);
        protected override int Price => 500;
        protected override RodAction CaseCategory => RodAction.Medium;
    }
    public class MediumHeavyCase() : Case
    {
        protected override int DiceRoll => rnd.Next(20, 100);
        protected override int Price => 600;
        protected override RodAction CaseCategory => RodAction.MediumHeavy;
    }
    public class HeavyCase() : Case
    {
        protected override int DiceRoll => rnd.Next(30, 100);
        protected override int Price => 700;
        protected override RodAction CaseCategory => RodAction.Heavy;
    }
    public class ExtraHeavyCase() : Case
    {
        protected override int DiceRoll => rnd.Next(45, 100);
        protected override int Price    => 800;
        protected override RodAction CaseCategory => RodAction.ExtraHeavy;
    }
    #endregion
}
