using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design;

namespace FishingClicker
{
    #region enums 
    //Rarity enum for each item we will be using
    internal enum Rarity
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary
    }
    //Level enum for each item we will be using
    internal enum Level
    {
        LevelOne,
        LevelTwo,
        LevelThree,
        LevelFour,
        LevelFive
    }
    internal enum Material
    {
        Wood,
        Stone,
        Iron,
        Gold,
        Diamond
    }
    #endregion
    internal class CEquipment : IEquipment
    {
        private Rarity rarity;
        private Level level;
        private Material material;

        public CEquipment(Rarity rarity, Level level, Material material)
        {
            this.rarity = rarity;
            this.level = level;
            this.Material = material;
        }

        public Rarity RarityValue { get => rarity; set => rarity = value;  }
        public Level ItemLevel { get => level; set => level = value; }
        public Material Material { get => material; set => material = value; }

        public virtual string DisplayInfo()
        {
            return ($"Material: {Material} \n Level: {level} \n Rarity: {rarity} \n");
        }
        public void Evolve()
        {
            
            if (RarityValue == Rarity.Legendary)
            {
                throw new InvalidOperationException("Cannot evolve beyond Legendary.");
            }
            RarityValue++;
        }

        public void Upgrade()
        {
            if(ItemLevel == Level.LevelFive)
            {
                throw new InvalidOperationException("Cannot upgrade beyond Level 5.");
            }
            ItemLevel++;
        }
        public decimal Recycle()
        {
            switch (RarityValue)
            {
                case Rarity.Common:
                    return 100;
                case Rarity.Uncommon:
                    return 150;
                case Rarity.Rare:
                    return 200;
                case Rarity.Epic:
                    return 250;
                case Rarity.Legendary:
                    return 300;
                default:
                    return 100;
            }
        }
        public virtual decimal RarityDecimal()
        {
            switch (RarityValue)
            {
                case Rarity.Common:
                    return 1;
                case Rarity.Uncommon:
                    return 1.5m;
                case Rarity.Rare:
                    return 2;
                case Rarity.Epic:
                    return 2.5m;
                case Rarity.Legendary:
                    return 3;
                default:
                    return 1;
            }
        }
        public virtual decimal LevelDecimal()
        {
            switch (ItemLevel)
            {
                case Level.LevelOne:
                    return 1;
                case Level.LevelTwo:
                    return 1.5m;
                case Level.LevelThree:
                    return 2;
                case Level.LevelFour:
                    return 2.5m;
                case Level.LevelFive:
                    return 3;
                default:
                    return 1;
            }
        }
    }
}
