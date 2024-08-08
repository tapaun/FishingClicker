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
using FishingClicker.Fish;
using System.IO;
using System.Runtime.Serialization;
using System.Xml.Serialization;
using System.Numerics;
using FishingClicker.Player;
using static FishingClicker.PlayerLogIn;
using System.Runtime.InteropServices;

namespace FishingClicker
{
    public partial class FishingLoad : Form
    {
        private Player.Player player;
        private Dictionary<string, TabPage> tabPageDictionary = new Dictionary<string, TabPage>();
        private FishingRod equippedRod;
        private List<Fishies> phish;
        public FishingLoad()
        {
            InitializeComponent();
            playerLogIn.LoginSuccessful += PlayerLogIn_LoginSuccessful;
            pictureBox1.MouseClick += tabPage1_MouseClick;
            phish = new List<Fishies>();
            #region TabControl set up
            foreach (TabPage tabPage in tabControl1.TabPages)
            {
                tabPageDictionary.Add(tabPage.Text, tabPage);
            }
            tabControl1.TabPages.Clear();
            if (tabPageDictionary.Count > 0)
            {
                var firstTabPage = tabPageDictionary.First().Value;
                tabControl1.TabPages.Add(firstTabPage);
            }
            #endregion
        }
        #region some methods
        private void ShowTabPageByButtonText(string buttonText)
        {
            if (tabPageDictionary.ContainsKey(buttonText))
            {
                tabControl1.TabPages.Clear();
                tabControl1.TabPages.Add(tabPageDictionary[buttonText]);
            }
        }
        private void PlayerLogIn_LoginSuccessful(object sender, PlayerEventArgs e)
        {
            // Receive the Player object from the login user control
            player = e.Player;
            progressBarInfo.Minimum = 0;
            progressBarInfo.Maximum = player.PlayerLevel * 1000;
            // Hide the login user control and show the main form content
            playerLogIn.Visible = false;
            equippedRod = player.FishingRod.ElementAt(1);
            MessageBox.Show($"Welcome, {player.PlayerName}!");
            playerLogIn.Dispose();
        }
        private void labelsDisable(Label labelInUse)
        {
            Label labelToKeep = labelInUse;
            foreach (Control control in this.Controls)
            {
                if (control is Label && control != labelToKeep)
                {
                    control.Hide();
                }
                else
                {
                    control.Show();
                }
            }
        }
        #endregion
        private async void FishingLoad_Load(object sender, EventArgs e)
        {
            labelsDisable(label1);
            var phishControl = new FishiesManager();
            phish = phishControl.ReadFromFile("availableFish.json");
        }
        #region Navigation Buttons
        private void theWater_Click(object sender, EventArgs e)
        {
            labelsDisable(label1); ShowTabPageByButtonText(((Button)sender).Text);
        }
        private void theInventory_Click(object sender, EventArgs e)
        {
            comboBoxFishingRods.Items.Clear();
            comboBoxMaterials.Items.Clear();
            labelsDisable(label2); ShowTabPageByButtonText(((Button)sender).Text);
            foreach (PlayerMaterials playersMaterials in player.PlayerMaterials)
            {
                comboBoxMaterials.Items.Add(playersMaterials.MaterialVar.ToString());
            }
            foreach (FishingRod fishingRods in player.FishingRod)
            {
                comboBoxFishingRods.Items.Add(fishingRods.EquipmentName);
            }
        }
        private void theShop_Click(object sender, EventArgs e)
        {
            labelsDisable(label3); ShowTabPageByButtonText(((Button)sender).Text);
        }
        private void theShrine_Click(object sender, EventArgs e)
        {
            labelsDisable(label4); ShowTabPageByButtonText(((Button)sender).Text);
        }
        private void userInfo_Click(object sender, EventArgs e)
        {
            labelsDisable(label5); ShowTabPageByButtonText(((Button)sender).Text);
        }
        #endregion
        #region ComboBoxes
        private void comboBoxMaterials_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (PlayerMaterials playersMaterials in player.PlayerMaterials)
            {
                if (comboBoxMaterials.Text == playersMaterials.MaterialVar.ToString())
                {
                    textBoxMAmount.Text = playersMaterials.MaterialsAmount.ToString();
                    textBoxMValue.Text = (playersMaterials.MaterialDecimal() * 100 * playersMaterials.MaterialsAmount).ToString();
                    break;
                }
            }
        }
        private void comboBoxFishingRods_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (FishingRod fishingRod in player.FishingRod)
            {
                if (fishingRod.EquipmentName == comboBoxFishingRods.Text)
                {
                    textBoxFRodRarity.Text = fishingRod.RarityValue.ToString();
                    textBoxFRodMaterial.Text = fishingRod.MaterialVar.ToString();
                    textBoxFRodLevel.Text = fishingRod.ItemLevel.ToString();
                    textBoxFRodCategory.Text = fishingRod.Category.ToString();
                }
            }
        }
        #endregion
        #region mouse clicked 
        private async void tabPage1_MouseClick(object sender, MouseEventArgs e)
        {
            Random random = new();
            List<Fishies> fishableFish = phish.Where(ph => ph.Weight < equippedRod.CastLine()).ToList();
            int randomFish = random.Next(fishableFish.Count);
            labelNotification.Text = $"Congrats you've caught a {fishableFish.ElementAt(randomFish).Name} - {fishableFish.ElementAt(randomFish).Weight}gold - {(fishableFish.ElementAt(randomFish).Weight * 12)}XP!";
            player.PlayerGold += (fishableFish.ElementAt(randomFish).Weight);
            player.LevelUp((int)(fishableFish.ElementAt(randomFish).Weight * 12));
            progressBarInfo.Maximum = player.PlayerLevel * 1000;
            progressBarInfo.Value = player.PlayerXP;
            labelInfo.Text = $"XP Bar - Player name: {player.PlayerName} - Player Level: {player.PlayerLevel} - Player Gold: {player.PlayerGold}";
            int coordX = e.X;
            int coordY = e.Y;
            Label labelClick = new()
            {
                AutoSize = true,
                BackColor = Color.Violet,
                Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0),
                ForeColor = Color.White,
                BorderStyle = (BorderStyle)FlatStyle.Flat,
                Location = new Point(coordX, coordY),
                Name = "labelClick",
                Size = new Size(10, 10),
                TabIndex = 100,
                Text = "+1"
            };
            pictureBox1.Controls.Add(labelClick);
            pictureBox1.Enabled = false;
            await Task.Delay(50);
            pictureBox1.Controls.Remove(labelClick);
            pictureBox1.Enabled = true;
        }
        #endregion

        private void FishingLoad_FormClosed(object sender, FormClosedEventArgs e)
        {
            var dataManager = new DataManager();
            var players = dataManager.ReadFromFile("playersData.json");
            if (player != null)
            {
                foreach (var playerFile in players)
                {
                    if (playerFile.PlayerName == player.PlayerName)
                    {
                        players.Remove(playerFile);
                        players.Add(player);
                        break;
                    }
                }
            }
            dataManager.SaveToFile(players, "playersData.json");
        }

        private void buttonEquipRod_Click(object sender, EventArgs e)
        {
            foreach(FishingRod fRod in player.FishingRod)
            {
                if(comboBoxFishingRods.Text == fRod.EquipmentName)
                {
                    equippedRod= fRod;
                }
            }
        }
    }
}
