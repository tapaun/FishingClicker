using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using FishingClicker.Equipment;
using System.Runtime.Serialization;
using System.Diagnostics;
using static System.Windows.Forms.Design.AxImporter;
using System.Collections;
using System.Net.Http.Json;

namespace FishingClicker.Player 
{
    [DebuggerDisplay("Player: {PlayerName} : {PlayerPassword}, Level: {PlayerLevel}-{PlayerXP}XP, {PlayerGold}g")]
    [Serializable]
    public class Player : IDataManager
    {
        #region Variables and Constructor
        //Player constructor
        public Player(string playerName, string playerPassword, int playerLevel, int playerXP, decimal playerGold, List<PlayerMaterials> playerMaterials, List<FishingRod> fishingRod)
        {
            PlayerName = playerName;
            PlayerPassword = playerPassword;
            PlayerLevel = playerLevel;
            PlayerXP = playerXP;
            PlayerGold = playerGold;
            PlayerMaterials = playerMaterials;
            FishingRod = fishingRod;
        }
        public Player() { }
        [JsonInclude]
        public string PlayerName { get;  set; }
        [JsonInclude]
        public string PlayerPassword { get; set; }
        [JsonInclude]
        public int  PlayerLevel { get; set; }
        [JsonInclude]
        public int PlayerXP { get; set; }
        [JsonInclude]
        public decimal PlayerGold { get; set; }
        [JsonInclude]
        public List<PlayerMaterials> PlayerMaterials { get; set; }
        [JsonInclude]
        public List<FishingRod> FishingRod { get; set; }
        #endregion
        public void LevelUp(int fishWeight)
        {
            PlayerXP += fishWeight;
            if(PlayerXP>=PlayerLevel*1000)
            {
                PlayerXP = PlayerXP-PlayerLevel * 1000;
                PlayerLevel++;
                MessageBox.Show("You've leveled up!");
                PlayerGold += PlayerLevel * 120;
            }
        }
    }
    #region PlayerManager
    public class DataManager : IDataLoadSave<Player>
    {
        public void SaveToFile(List<Player> players, string filePath)
        {
            var options = new JsonSerializerOptions
            {
                Converters = { new JsonStringEnumConverter() },
                WriteIndented = true,
            };
            var jsonString = JsonSerializer.Serialize(players, options);
            File.WriteAllText(filePath, jsonString);
        }

        public List<Player> ReadFromFile(string filePath)
        {
            var options = new JsonSerializerOptions
            {
                Converters = { new JsonStringEnumConverter() },
                WriteIndented = true
            };
            string jsonString = File.ReadAllText(filePath);
            List<Player> players = JsonSerializer.Deserialize<List<Player>>(jsonString, options);
            return players;
        }
    }
    #endregion
    #region PlayerMaterials
    [DebuggerDisplay("{Materials} : {MaterialsAmount}")]
    public record class PlayerMaterials : Material
    {
        public required int MaterialsAmount { get; init; }

        public PlayerMaterials() { }
        public override string ToString()
        {
            return $"{MaterialVar} : {MaterialsAmount}";
        }
    }
    #endregion
}
