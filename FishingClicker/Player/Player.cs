using System.Text.Json;
using System.Text.Json.Serialization;
using FishingClicker.Equipment;
using System.Diagnostics;
using FishingClicker.Fish;
using FishingClicker.Mats;

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
            PlayerName      = playerName;
            PlayerPassword  = playerPassword;
            PlayerLevel     = playerLevel;
            PlayerXP        = playerXP;
            PlayerGold      = playerGold;
            PlayerMaterials = playerMaterials;
            FishingRod      = fishingRod;
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
        public FishingRod EquippedRod { get; set; }
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
        public FishingRod EquipRod(string fishingRodName)
        {
            return FishingRod.First(fishingRod => fishingRod.EquipmentName == fishingRodName);   
        }
        public string[] RodData(string fishingRodName)
        {
            var fRod = FishingRod.First(fishingRod => fishingRod.ToString() == fishingRodName);
            return [fRod.RarityValue.ToString(),
                    fRod.MaterialVar.ToString(),
                    fRod.ItemLevel.ToString(),
                    fRod.Category.ToString()];
        }
        public string[] MaterialsData(string materialVar)
        {
            var mat = PlayerMaterials.First(material => material.ToString() == materialVar);
            return [ mat.MaterialsAmount.ToString(),
                    (mat.SingleMaterialWorth(mat.MaterialVar)*mat.MaterialsAmount).ToString()];
        }
        public void MaterialsBought(decimal totalPrice, string[] textBoxValues)
        {
            var materialsRef = new Mats.MaterialReference();
            var materialsList = Mats.MaterialReference.MaterialsList;
            var materialsWorth = materialsRef.DisplayPrices(PlayerLevel);
            var textBoxParsed = materialsRef.TextBoxParser(textBoxValues);
            Dictionary<Mats.Materials, int> keyValuePairs = new()
            {
                { Mats.Materials.Wood,    textBoxParsed[0]},
                { Mats.Materials.Stone,   textBoxParsed[1]},
                { Mats.Materials.Iron,    textBoxParsed[2]},
                { Mats.Materials.Gold,    textBoxParsed[3]},
                { Mats.Materials.Diamond, textBoxParsed[4]},
            };
            foreach (var material in materialsList)
            {
                int index = PlayerMaterials.FindIndex(mat => mat.MaterialVar == material.MaterialVar);
                keyValuePairs.TryGetValue(material.MaterialVar, out int value);
                if (index != -1) { PlayerMaterials.ElementAt(index).MaterialsAmount += value; }
                else
                {
                    if (value > 0)
                    {
                        PlayerMaterials newMaterial = new()
                        {
                            MaterialVar = material.MaterialVar,
                            MaterialsAmount = value
                            };
                         PlayerMaterials.Add(newMaterial);
                    }
                }
            }
        }
        public Fishies CatchFish()
        {
            var phishControl = new FishiesManager();
            var phish = phishControl.ReadFromFile("availableFish.json");
            Random random = new();
            List<Fishies> fishableFish = phish.Where(ph => ph.Weight < this.EquippedRod.CastLine()).ToList();
            int randomFish = random.Next(fishableFish.Count);
            Fishies phishy = fishableFish.ElementAt(randomFish);
            PlayerGold += (phishy.Weight);
            LevelUp((int)(phishy.Weight * 12));
            return phishy;
        }
        public void FormClosing()
        {
            var dataManager = new DataManager();
            var players = dataManager.ReadFromFile("playersData.json");
            if (this != null)
            {
                foreach (var playerFile in players)
                {
                    if (playerFile.PlayerName == PlayerName)
                    {
                        players.Remove(playerFile);
                        players.Add(this);
                        break;
                    }
                }
            }
            dataManager.SaveToFile(players, "playersData.json");
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
        public required int MaterialsAmount { get; set; }
        public PlayerMaterials() { }
        public decimal SingleMaterialWorth(Mats.Materials material)
        {
            var materials = MaterialReference.MaterialsList;
            return materials.First(mat => mat.MaterialVar == material).MaterialDecimal() * 100;
        }
        public override string ToString()
        {
            return $"{MaterialVar} : {MaterialsAmount}";
        }
    }
    #endregion
}
