using FishingClicker.Equipment;
using FishingClicker.Player;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace FishingClicker
{
    public partial class PlayerLogIn : UserControl
    {
        public event EventHandler<PlayerEventArgs> LoginSuccessful;
        public PlayerLogIn()
        {
            InitializeComponent();
        }
        protected virtual void OnLoginSuccessful(PlayerEventArgs e)
        {
            LoginSuccessful?.Invoke(this, e);
        }   
        #region Log In and Sign Up buttons
        private void loginButton_Click(object sender, EventArgs e)
        {
            #region variables and file loader
            bool realAccount = false;
            Player.Player currentPlayer = new Player.Player();
            var playerManager = new DataManager();
            var loadedPlayers = playerManager.ReadFromFile("playersData.json");
            #endregion
            #region player Log In and data checker
            foreach (var player in loadedPlayers)
            {
                if (player.PlayerName == usernameTxtbox.Text)
                {
                    currentPlayer = player;
                    realAccount = true;
                    break;
                }
            }
            if (!realAccount) { label1.Text = "No username found!"; } else { label1.Text = "✓"; }
            if (currentPlayer.PlayerPassword == passwordTxtbox.Text)
            {
                label2.Text = "✓";
                OnLoginSuccessful(new PlayerEventArgs(currentPlayer));
            }
            else
            {
                label2.Text = "Wrong password!";
            }
            #endregion
        }
        private void signupButton_Click(object sender, EventArgs e)
        {
            #region variables and file loader
            bool playerExists = false;
            var fishingRod = new List<FishingRod>
            {
                new BeginnerRod{
                    EquipmentName = "Basic Rod", 
                    RarityValue = Rarity.Common, 
                    Strength = 2m, 
                    Category = RodAction.Light, 
                    ItemLevel = Level.LevelOne,
                    MaterialVar = Mats.Materials.Wood },
                new BeginnerRod{
                    EquipmentName = "Common Rod",
                    RarityValue = Rarity.Common,
                    Strength = 2m,
                    Category = RodAction.Medium,
                    ItemLevel = Level.LevelOne,
                    MaterialVar = Mats.Materials.Wood },
            };
            PlayerMaterials materials1 = new PlayerMaterials
            { 
                MaterialsAmount = 10,
                MaterialVar = Mats.Materials.Wood
            };
            PlayerMaterials materials2 = new PlayerMaterials
            {
                MaterialsAmount = 5,
                MaterialVar = Mats.Materials.Stone
            };
            List<PlayerMaterials> materials = new List<PlayerMaterials>
            {
                materials1,
                materials2
            };
            var playerManager = new DataManager();
            var loadedPlayers = playerManager.ReadFromFile("playersData.json");
            foreach (var player in loadedPlayers)
            {
                if (player.PlayerName == usernameTxtbox.Text)
                {
                    playerExists = true;
                    break;
                }
            }
            #endregion
            #region data checker and player creator
            if (!playerExists)
            {
                Player.Player newPlayer = new Player.Player(usernameTxtbox.Text, passwordTxtbox.Text, 1, 0, 100, materials, fishingRod);
                loadedPlayers.Add(newPlayer);
                playerManager.SaveToFile(loadedPlayers, "playersData.json");
                usernameTxtbox.Text = "";
                passwordTxtbox.Text = "";
            }
            else
            {
                label1.Text = "Player already exists";
            }
            #endregion
        }
        #endregion
        public class PlayerEventArgs : EventArgs
        {
            public Player.Player Player { get; }

            public PlayerEventArgs(Player.Player player)
            {
                Player = player;
            }
        }
    }
}
