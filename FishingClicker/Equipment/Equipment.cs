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
using System.Diagnostics;
using System.Text.Json.Serialization;
using FishingClicker.Equipment;

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
    #endregion
    [DebuggerDisplay("Name: {EquipmentName} Material: {MaterialVar} Level: {ItemLevel} Rarity: {RarityValue}")]
    public class Equipment : Mats.Material, IEquipment
    {
        #region Variables and Constructor
        public Equipment() { }
        #endregion

        #region Getters and Setters
        [JsonInclude]
        public required Rarity RarityValue { get; set; }
        [JsonInclude]
        public required Level ItemLevel { get; set; }
        [JsonInclude]
        public required string EquipmentName { get; set; }
        #endregion

        #region Methods
        public override string ToString()
        {
            return $"Material: {MaterialVar} Level: {ItemLevel} Rarity: {RarityValue}";
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
            return RarityValue switch
            {
                Rarity.Common    => 100,
                Rarity.Uncommon  => 150,
                Rarity.Rare      => 200,
                Rarity.Epic      => 250,
                Rarity.Legendary => 300,
                _ => 100,
            };
        }
        public virtual decimal RarityDecimal()
        {
            return RarityValue switch
            {
                Rarity.Common    => 1,
                Rarity.Uncommon  => 1.5m,
                Rarity.Rare      => 2,
                Rarity.Epic      => 2.5m,
                Rarity.Legendary => 3,
                _ => 1,
            };
        }
        public virtual decimal LevelDecimal()
        {
            return ItemLevel switch
            {
                Level.LevelOne   => 1,
                Level.LevelTwo   => 1.5m,
                Level.LevelThree => 2,
                Level.LevelFour  => 2.5m,
                Level.LevelFive  => 3,
                _ => 1,
            };
        }
        #endregion
    }
}
