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
using System.Security.Cryptography.X509Certificates;
using FishingClicker.Equipment;

namespace FishingClicker
{
    public partial class FishingLoad : Form
    {
        private Player.Player player;
        private Dictionary<string, TabPage> tabPageDictionary = [];
        public FishingLoad()
        {
            InitializeComponent();
            #region event subs
            playerLogIn.LoginSuccessful += PlayerLogIn_LoginSuccessful;
            pictureBox1.MouseClick += tabPage1_MouseClick;
            textBoxWood.TextChanged += materialButtons_TextChanged;
            textBoxStone.TextChanged += materialButtons_TextChanged;
            textBoxIron.TextChanged += materialButtons_TextChanged;
            textBoxGold.TextChanged += materialButtons_TextChanged;
            textBoxDiamond.TextChanged += materialButtons_TextChanged;
            #endregion
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
        #region UI cleaners and LogIn event
        private void ShowTabPageByButtonText(string buttonText)
        {
            if (tabPageDictionary.TryGetValue(buttonText, out TabPage? value))
            {
                tabControl1.TabPages.Clear();
                tabControl1.TabPages.Add(value);
            }
        }
        private void PlayerLogIn_LoginSuccessful(object sender, PlayerEventArgs e)
        {
            player = e.Player;
            progressBarInfo.Maximum = player.PlayerLevel * 1000;
            playerLogIn.Visible = false;
            player.EquippedRod = player.FishingRod.ElementAt(1);
            MessageBox.Show($"Welcome, {player.PlayerName}!");
            playerLogIn.Dispose();
        }
        private void labelsDisable(Label labelInUse)
        {
            Label labelToKeep = labelInUse;
            foreach (Control control in this.Controls)
            {
                if   (control is Label && control != labelToKeep) { control.Hide(); }
                else { control.Show(); }
            }
        }
        private void textBoxCleaner(TabPage tabPage)
        {
            foreach (Control control in tabPage.Controls)
            {
                if (control is TextBox) { control.Text = ""; }
            }
        }
        #endregion
        private void FishingLoad_Load(object sender, EventArgs e)
        {
            labelsDisable(label1);
        }
        #region Navigation Buttons
        //Fishing tab
        private void theWater_Click(object sender, EventArgs e)
        {
            labelsDisable(label1);
            ShowTabPageByButtonText(((Button)sender).Text);
        }
        //Inventory tab
        private void theInventory_Click(object sender, EventArgs e)
        {
            textBoxCleaner(tabPage2);
            labelsDisable(label2);
            ShowTabPageByButtonText(((Button)sender).Text);
            comboBoxMaterials.DataSource = player.PlayerMaterials;
            comboBoxMaterials.DisplayMember = "MaterialVar";
            comboBoxFishingRods.DataSource = player.FishingRod;
            comboBoxFishingRods.DisplayMember = "EquipmentName";
        }
        private void buttonEquipRod_Click(object sender, EventArgs e)
        {
            if (comboBoxFishingRods.TabIndex != -1)
            {
                player.EquippedRod = player.EquipRod(comboBoxFishingRods.SelectedValue!.ToString()!);
                MessageBox.Show($"You've equipped {player.EquippedRod.EquipmentName}!");
            }
        }
        //Shop tab
        private void theShop_Click(object sender, EventArgs e)
        {
            textBoxCleaner(tabPage3);
            labelsDisable(label3);
            textBoxAvailableGold.Text = player.PlayerGold.ToString();
            ShowTabPageByButtonText(((Button)sender).Text);
            Mats.MaterialReference materialReference = new();
            decimal[] materialWorth = materialReference.DisplayPrices(player.PlayerLevel);
            labelWoodValue.Text    = $"{materialWorth[0]}g/piece";
            labelStoneValue.Text   = $"{materialWorth[1]}g/piece";
            labelIronValue.Text    = $"{materialWorth[2]}g/piece";
            labelGoldValue.Text    = $"{materialWorth[3]}g/piece";
            labelDiamondValue.Text = $"{materialWorth[4]}g/piece";
        }
        public void materialButtons_TextChanged(object sender, EventArgs e)
        {
            string[] textBoxValues =
            [
                textBoxWood.Text,
                textBoxStone.Text,  
                textBoxIron.Text,
                textBoxGold.Text,
                textBoxDiamond.Text,
            ];
            Mats.MaterialReference materialReference = new();
            textBoxTotalPrice.Text = ($"{materialReference.TotalPrice(textBoxValues, player.PlayerLevel)}");
        }
        private void buttonPurchase_Click(object sender, EventArgs e)
        {
            string[] textBoxValues =
            [
                textBoxWood.Text,
                textBoxStone.Text,
                textBoxIron.Text,
                textBoxGold.Text,
                textBoxDiamond.Text,
            ];
            #region TryParses
            decimal totalPrice = 0;
            if (decimal.TryParse(textBoxTotalPrice.Text, out decimal totalPriceTrue)) totalPrice = totalPriceTrue;
            #endregion
            if (totalPrice <= player.PlayerGold)
            {
                player.PlayerGold -= totalPrice;
                textBoxAvailableGold.Text = player.PlayerGold.ToString();
                player.MaterialsBought(totalPrice, textBoxValues);
            }
            else { MessageBox.Show("Insufficient funds :( !"); }
        }
        //Upgrade tab
        private void theShrine_Click(object sender, EventArgs e)
        {
            labelsDisable(label4);
            ShowTabPageByButtonText(((Button)sender).Text);
        }
        //UserInfo tab
        private void userInfo_Click(object sender, EventArgs e)
        {
            labelsDisable(label5);
            ShowTabPageByButtonText(((Button)sender).Text);
        }
        #endregion
        #region ComboBoxes
        private void comboBoxMaterials_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] materialsAbout = new string[2];
            if (comboBoxMaterials.SelectedIndex != -1)  materialsAbout = player.MaterialsData(comboBoxMaterials.SelectedValue!.ToString()!);
            textBoxMAmount.Text = materialsAbout[0];
            textBoxMValue.Text  = materialsAbout[1];
        }
        private void comboBoxFishingRods_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] fRodAbout = new string[4];
            if (comboBoxFishingRods.SelectedIndex != -1) fRodAbout = player.RodData(comboBoxFishingRods.SelectedValue!.ToString()!);
            textBoxFRodRarity.Text   = fRodAbout[0];
            textBoxFRodMaterial.Text = fRodAbout[1];
            textBoxFRodLevel.Text    = fRodAbout[2];
            textBoxFRodCategory.Text = fRodAbout[3];
        }
        #endregion
        #region mouse clicked 
        private async void tabPage1_MouseClick(object sender, MouseEventArgs e)
        {
            Fishies phishy = player.CatchFish();
            labelNotification.Text = $"Congrats you've caught a {phishy.Name} - " +
                                     $"{phishy.Weight}gold - " +
                                     $"{phishy.Weight * 12}XP!";

            progressBarInfo.Maximum = player.PlayerLevel * 1000;
            progressBarInfo.Value = player.PlayerXP;

            labelInfo.Text = $"XP Bar - Player name: {player.PlayerName} - " +
                             $"Player Level: {player.PlayerLevel} - " +
                             $"Player Gold: {player.PlayerGold}";

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
        //Saving player data
        private void FishingLoad_FormClosed(object sender, FormClosedEventArgs e)
        {
            player.FormClosing();
        }
    }
}
