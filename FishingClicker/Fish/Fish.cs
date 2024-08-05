using FishingClicker.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

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
    public class Fishies
    {
        #region Variables and Constructor
        private string name;
        private FishRarity fishRarity;
        private FishDepth fishDepth;
        private decimal weight;
        //private Image fishImage;
        public Fishies(string name, FishRarity fishRarity, FishDepth fishDepth, decimal weight)
        {
            this.name = name;
            this.fishRarity = fishRarity;
            this.weight = weight;
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
        public virtual decimal SellFish(decimal weightMultiplier, decimal rarityMultiplier, decimal depthMultiplier)
        {
            return weight * weightMultiplier + FishRarityDecimal() * rarityMultiplier + depthMultiplier * DepthRarityDecimal();
        }
        public void WriteToFile(List<Fishies> fishies)
        {
            var serializer = new XmlSerializer(typeof(List<Fishies>));
            using (var writer = new StreamWriter("availableFish.xml"))
            {
                serializer.Serialize(writer, fishies);
            }
        }
        #endregion
    }
    #region Other fishes
    public class HeavyFish : Fishies
    {
        public HeavyFish(string name, FishRarity fishRarity, FishDepth fishDepth, decimal weight) : base(name, fishRarity, fishDepth, weight)
        {
        }
        public override decimal SellFish(decimal weightMultiplier=3m, decimal rarityMultiplier=1.5m, decimal depthMultiplier = 2m)
        {
            return base.SellFish(weightMultiplier, rarityMultiplier, depthMultiplier);
        }
    }
    public class ExoticFish : Fishies
    {
        public ExoticFish(string name, FishRarity fishRarity, FishDepth fishDepth, decimal weight) : base(name, fishRarity, fishDepth, weight)
        {
        }
        public override decimal SellFish(decimal weightMultiplier = 1.6m, decimal rarityMultiplier = 4.5m, decimal depthMultiplier = 2.5m)
        {
            return base.SellFish(weightMultiplier, rarityMultiplier, depthMultiplier);
        }
    }
    #endregion
}
