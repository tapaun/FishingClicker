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
            var playerManager = new PlayerManager();
            var loadedPlayers = playerManager.LoadFromFile();
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
        public string PlayerUsernameTextbox
        {
            get => usernameTxtbox.Text;
            set => usernameTxtbox.Text = value;
        }
        private void signupButton_Click(object sender, EventArgs e)
        {
            #region variables and file loader
            bool playerExists = false;
            var fishingRod = new List<FishingRod>
            {
                new BeginnerRod("Basic Rod", Rarity.Common, 2m, RodAction.Light, Level.LevelOne,Material.Wood),
                new BeginnerRod("Common Rod", Rarity.Common, 3m, RodAction.Heavy, Level.LevelOne,Material.Wood)
            };
            PlayerMaterials materials1 = new PlayerMaterials(Material.Iron, 20);
            PlayerMaterials materials2 = new PlayerMaterials(Material.Stone, 20);
            var materials = new List<PlayerMaterials>
            {
                materials1,
                materials2
            };
            var playerManager = new PlayerManager();
            var loadedPlayers = playerManager.LoadFromFile();
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
                playerManager.SaveToFile(loadedPlayers);
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
