using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms.Design;
using System.Xml.Serialization;
using System.Xml;
using System.IO;

namespace FishingClicker.Equipment
{
    #region Enums 
    //Rarity enum for each item we will be using
    public enum Rarity
    {
        Common,
        Uncommon,
        Rare,
        Epic,
        Legendary
    }
    //Level enum for each item we will be using
    public enum Level
    {
        LevelOne,
        LevelTwo,
        LevelThree,
        LevelFour,
        LevelFive
    }
    public enum Material
    {
        Wood,
        Stone,
        Iron,
        Gold,
        Diamond
    }
    #endregion
    public class Equipment : IEquipment
    {
        #region Variables and Constructor
        private string name;
        private Rarity rarity;
        private Level level;
        private Material material;

        public Equipment(string name, Rarity rarity=default, Level level=default, Material material=default)
        {
            this.name = name;
            this.rarity = rarity;
            this.level = level;
            this.material = material;
        }
        public Equipment() { }
        #endregion

        #region Getters and Setters
        [XmlElement("RarityValue")]
        public Rarity RarityValue { get => rarity; set => rarity = value; }
        [XmlElement("ItemLevel")]
        public Level ItemLevel { get => level; set => level = value; }
        [XmlElement("Material")]
        public Material Material { get => material; set => material = value; }
        [XmlElement("EquipmentName")]
        public string EquipmentName { get => name; set => name = value; }
        #endregion

        #region Methods
        public virtual string DisplayInfo()
        {
            return $"Material: {Material} \n Level: {level} \n Rarity: {rarity} \n";
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
            if (ItemLevel == Level.LevelFive)
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
        public virtual decimal MaterialDecimal()
        {
            switch (Material)
            {
                case Material.Wood:
                    return 1;
                case Material.Stone:
                    return 1.5m;
                case Material.Iron:
                    return 2;
                case Material.Gold:
                    return 2.5m;
                case Material.Diamond:
                    return 3;
                default:
                    return 1;
            }
        }
        #endregion
    }
}
