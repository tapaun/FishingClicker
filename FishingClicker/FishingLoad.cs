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

namespace FishingClicker
{
    public partial class FishingLoad : Form
    {
        private Player.Player player;
        private Dictionary<string, TabPage> tabPageDictionary = new Dictionary<string, TabPage>();
        private FishingRod equippedRod;
        public FishingLoad()
        {
            InitializeComponent();
            playerLogIn.LoginSuccessful += PlayerLogIn_LoginSuccessful;
            #region TabControl set up
            // Hide all tab pages
            foreach (TabPage tabPage in tabControl1.TabPages)
            {
                tabPageDictionary.Add(tabPage.Text, tabPage);
            }
            tabControl1.TabPages.Clear();
            // Optionally, show the first tab page
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
                // Hide all TabPages
                tabControl1.TabPages.Clear();

                // Show the target TabPage
                tabControl1.TabPages.Add(tabPageDictionary[buttonText]);
            }
        }
        private void PlayerLogIn_LoginSuccessful(object sender, PlayerEventArgs e)
        {
            // Receive the Player object from the login user control
            player = e.Player;

            // Hide the login user control and show the main form content
            playerLogIn.Visible = false;
            equippedRod = player.FishingRod.ElementAt(0);
            // Display player information or proceed with main application logic
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
        private void FishingLoad_Load(object sender, EventArgs e)
        {
            labelsDisable(label1);
        }
        #region Navigation Buttons
        private void theWater_Click(object sender, EventArgs e)
        {
            labelsDisable(label1); ShowTabPageByButtonText((sender as Button).Text);
        }
        private void theInventory_Click(object sender, EventArgs e)
        {
            labelsDisable(label2); ShowTabPageByButtonText((sender as Button).Text);
            if (comboBoxFishingRods.SelectedIndex == -1)
            {
                foreach (PlayerMaterials playersMaterials in player.PlayerMaterials)
                {
                    comboBoxMaterials.Items.Add(playersMaterials.Materials.ToString());
                }
                foreach (FishingRod fishingRods in player.FishingRod)
                {
                    comboBoxFishingRods.Items.Add(fishingRods.EquipmentName);
                }
            }
        }
        private void theShop_Click(object sender, EventArgs e)
        {
            labelsDisable(label3); ShowTabPageByButtonText((sender as Button).Text);
        }
        private void theShrine_Click(object sender, EventArgs e)
        {
            labelsDisable(label4); ShowTabPageByButtonText((sender as Button).Text);
        }
        private void userInfo_Click(object sender, EventArgs e)
        {
            labelsDisable(label5); ShowTabPageByButtonText((sender as Button).Text);
        }
        #endregion
        private void comboBoxMaterials_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (PlayerMaterials playersMaterials in player.PlayerMaterials)
            {
                if (comboBoxMaterials.Text == playersMaterials.Materials.ToString())
                {
                    Equipment.Equipment material = new Equipment.Equipment("Substitute", default, default, playersMaterials.Materials);
                    textBoxMAmount.Text = playersMaterials.MaterialsAmount.ToString();
                    textBoxMValue.Text = (material.MaterialDecimal() * 100).ToString();
                    break;
                }
            }
        }

        private void comboBoxFishingRods_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach(FishingRod fishingRod in player.FishingRod)
            {
                if(fishingRod.EquipmentName == comboBoxFishingRods.Text)
                {
                    textBoxFRodRarity.Text = fishingRod.RarityValue.ToString();
                    textBoxFRodMaterial.Text = fishingRod.Material.ToString();
                    textBoxFRodLevel.Text = fishingRod.ItemLevel.ToString();
                    textBoxFRodCategory.Text = fishingRod.Category.ToString();
                }
            }
        }
    }
}
