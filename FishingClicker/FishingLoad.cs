using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FishingClicker.Equipment;
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Numerics;

namespace FishingClicker
{
    public partial class FishingLoad : Form
    {
        public FishingLoad()
        {
            InitializeComponent();
        }

        private void FishingLoad_Load(object sender, EventArgs e)
        {
            PlayerMaterials playerMaterialsArray = new PlayerMaterials(Material.Diamond, 1);
            var playerMaterials = new List<PlayerMaterials>
            {
                playerMaterialsArray,
                playerMaterialsArray,
                playerMaterialsArray
            };
            BeginnerRod fishingRod = new BeginnerRod("Beginner Rod", Rarity.Uncommon, 2.5m, RodAction.Light, Level.LevelOne, Material.Diamond);
            Player player1 = new Player("Nickolas Prochevsky", 1, 100, 150, playerMaterials, fishingRod);
            Player player2 = new Player("Nickolas", 5, 110, 1540, playerMaterials, fishingRod);
            Player player3 = new Player("Prochevsky", 2, 2100, 1350, playerMaterials, fishingRod);
            Player player4 = new Player("Provsky", 3, 1003, 1250, playerMaterials, fishingRod);
            Player player5 = new Player("Nicky", 4, 1050, 1150, playerMaterials, fishingRod);
            var playersList = new List<Player>
            {
                player1,
                player2,
                player3,
                player4,
                player5
            };
            var playerManager = new PlayerManager();
            playerManager.SaveToFile(playersList);
            var loadedPlayers = playerManager.LoadFromFile();
            label1.Text ="";
            foreach (var player in loadedPlayers)
            {
                label1.Text += player.displayPlayerInfo();
            }
        }
    }
}
