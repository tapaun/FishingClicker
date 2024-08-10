using FishingClicker.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;
using FishingClicker.Player;

namespace FishingClicker.Fish
{
    #region enums
    public enum FishDepth
    {
        Shallow,
        Midwater,
        Deepwater,
        UltraDeepwater
    }
    public enum FishRarity
    {
        Puddle,
        Lake,
        Creek,
        River,
        Sea,
        Ocean,
        Exotic,
        Tropical
    }
    #endregion
    public class Fishies : IDataManager
    {
        #region Variables and Constructor
        private string name;
        private FishRarity fishRarity;
        private FishDepth fishDepth;
        private decimal weight;
        protected virtual decimal WeightMultiplier { get; set; }
        protected virtual decimal RarityMultiplier { get; set; }
        protected virtual decimal DepthMultiplier { get; set; }
        //private Image fishImage;
        public Fishies(string name, FishRarity fishRarity, FishDepth fishDepth, decimal weight)
        {
            this.name       = name;
            this.fishRarity = fishRarity;
            this.fishDepth  = fishDepth;
            this.weight     = weight;
            //this.fishImage = fishImage;
        }
        public Fishies() { }

        #endregion
        #region Getters and Setters
        public string Name { get => name; set => name = value; }
        public FishRarity FishiesRarity { get => fishRarity; set => fishRarity = value; }
        public FishDepth FishiesDeptyh { get => fishDepth; set => fishDepth = value; }
        public decimal Weight { get => weight; set => weight = value; }
        //public Image FishImage { get => fishImage; set => fishImage = value; }
        #endregion
        #region Methods
        public decimal FishRarityDecimal()
        {
            return 0m;
        }
        public decimal DepthRarityDecimal()
        {
            return 0m;
        }
        public decimal SellFish() =>
            Weight * WeightMultiplier + FishRarityDecimal() * RarityMultiplier + DepthMultiplier * DepthRarityDecimal();
        #endregion
    }
    #region DataManager
    public class FishiesManager : IDataLoadSave<Fishies>
    {
        public void SaveToFile(List<Fishies> players, string filePath)
        {
            var options = new JsonSerializerOptions
            {
                Converters = { new JsonStringEnumConverter() },
                WriteIndented = true
            };
            var jsonString = JsonSerializer.Serialize(players, options);
            File.WriteAllText(filePath, jsonString);
        }

        public List<Fishies> ReadFromFile(string filePath)
        {
            var options = new JsonSerializerOptions
            {
                Converters = { new JsonStringEnumConverter() },
                WriteIndented = true
            };
            string jsonString = File.ReadAllText(filePath);
            List<Fishies> fish = JsonSerializer.Deserialize<List<Fishies>>(jsonString, options);
            return fish;
        }
    }
    #endregion
    #region Other fishes
    public class HeavyFish : Fishies
    {
        protected override decimal WeightMultiplier => 1.5m;
        protected override decimal RarityMultiplier => 1.5m;
        protected override decimal DepthMultiplier  => 1.5m;
        public HeavyFish(string name, FishRarity fishRarity, FishDepth fishDepth, decimal weight) : base(name, fishRarity, fishDepth, weight)
        {
        }
    }
    public class ExoticFish : Fishies
    {
        protected override decimal WeightMultiplier => 2m;
        protected override decimal RarityMultiplier => 1.75m;
        protected override decimal DepthMultiplier  => 2m;
        public ExoticFish(string name, FishRarity fishRarity, FishDepth fishDepth, decimal weight) : base(name, fishRarity, fishDepth, weight)
        {
        }
    }
    #endregion
}
