using FishingClicker.Equipment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingClicker.Fish
{
    public class Fishies
    {
        #region Variables and Constructor
        private string name;
        private Rarity rarity;
        private decimal weight;
        //private Image fishImage;
        public Fishies(string name, Rarity rarity, decimal weight)
        {
            this.name = name;
            this.rarity = rarity;
            this.weight = weight;
            //this.fishImage = fishImage;
        }
        public Fishies() { }
        #endregion
        #region Getters and Setters
        public string Name { get => name; set => name = value; }
        public Rarity Rarity { get => rarity; set => rarity = value; }
        public decimal Weight { get => weight; set => weight = value; }
        //public Image FishImage { get => fishImage; set => fishImage = value; }
        #endregion
        #region Methods
        public virtual decimal SellFish(decimal weightMultiplier, decimal rarityMultiplier)
        {
            CEquipment rarityTransfer = new CEquipment(null, rarity);
            return weight * 1.2m + weightMultiplier + rarityTransfer.RarityDecimal() * rarityMultiplier;
        }
        #endregion
    }
    #region Other fishes
    public class HeavyFish : Fishies
    {
        public HeavyFish(string name, Rarity rarity, decimal weight) : base(name, rarity, weight)
        {
        }
        public override decimal SellFish(decimal weightMultiplier=3m, decimal rarityMultiplier=1.5m)
        {
            CEquipment rarityTransfer = new CEquipment(null, Rarity);
            return Weight * 1.5m + weightMultiplier + rarityTransfer.RarityDecimal() * rarityMultiplier;
        }
    }
    public class ExoticFish : Fishies
    {
        public ExoticFish(string name, Rarity rarity, decimal weight) : base(name, rarity, weight)
        {
        }
        public override decimal SellFish(decimal weightMultiplier = 1.6m, decimal rarityMultiplier = 4.5m)
        {
            CEquipment rarityTransfer = new CEquipment(null, Rarity);
            return Weight * 1.5m + weightMultiplier + rarityTransfer.RarityDecimal() * rarityMultiplier;
        }
    }
    #endregion
}
