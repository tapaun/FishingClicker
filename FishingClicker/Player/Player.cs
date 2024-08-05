using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using FishingClicker.Equipment;
using System.Runtime.Serialization;

namespace FishingClicker.Player
{
    [XmlRoot("Player")]
    public class Player
    {
        #region Variables and Constructor
        private string playerName;
        private string playerPassword;
        private int playerLevel;
        private int playerXP;
        private int playerGold;
        private List<PlayerMaterials> playerMaterials;
        private List<FishingRod> fishingRod;
        //Player constructor
        public Player(string playerName, string playerPassword, int playerLevel, int playerXP, int playerGold, List<PlayerMaterials> playerMaterials, List<FishingRod> fishingRod)
        {
            this.playerName = playerName;
            this.playerPassword = playerPassword;
            this.playerLevel = playerLevel;
            this.playerXP = playerXP;
            this.playerGold = playerGold;
            this.playerMaterials = playerMaterials;
            this.fishingRod = fishingRod;
        }
        public Player() { }
        #endregion
        #region Getters and Setters
        public string PlayerName { get => playerName; set => playerName = value; }
        public string PlayerPassword { get => playerPassword; set => playerPassword = value; }
        public int PlayerLevel { get => playerLevel; set => playerLevel = value; }
        public int PlayerXP { get => playerXP; set => playerXP = value; }
        public int PlayerGold { get => playerGold; set => playerGold = value; }
        [XmlArray("PlayerMaterials")]
        [XmlArrayItem("PlayerMaterial")]
        public List<PlayerMaterials> PlayerMaterials { get => playerMaterials; set => playerMaterials = value; }
        [XmlElement("FishingRod")]
        public List<FishingRod> FishingRod { get => fishingRod; set => fishingRod = value; }
        #endregion
        #region Methods
        public string displayPlayerInfo()
        {
            return $"Player name: {PlayerName}\n  Player Level: {PlayerLevel}\n Player XP: {PlayerXP}\n Player Gold: {PlayerGold} \n Player Rods: {FishingRod}\n Player Password: {PlayerPassword}\n";
        }
        #endregion
    }
    #region PlayerXML
    public class PlayerManager
    {
        public void SaveToFile(List<Player> players)
        {
            var serializer = new XmlSerializer(typeof(List<Player>));
            using (var writer = new StreamWriter("playersData.xml"))
            {
                serializer.Serialize(writer, players);
            }
        }

        public List<Player> LoadFromFile()
        {
            var serializer = new XmlSerializer(typeof(List<Player>));
            using (var reader = new StreamReader("playersData.xml"))
            {
                return (List<Player>)serializer.Deserialize(reader);
            }
        }
    }
    #endregion
    #region PlayerMaterials
    public class PlayerMaterials
    {
        private Material materials;
        private int materialsAmount;

        public PlayerMaterials(Material materials, int materialsAmount)
        {
            this.materials = materials;
            this.materialsAmount = materialsAmount;
        }
        public PlayerMaterials() { }
        [XmlElement("MaterialsAmount")]
        public int MaterialsAmount { get => materialsAmount; set => materialsAmount = value; }
        [XmlElement("Materials")]
        public Material Materials { get => materials; set => materials = value; }
        public string MaterialsInfo()
        {
            return $"{Materials} : {MaterialsAmount}";
        }
    }
    #endregion
}
