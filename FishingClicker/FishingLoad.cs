using FishingClicker.Equipment;
using FishingClicker.Fish;
using FishingClicker.User;
using static FishingClicker.PlayerLogIn;

namespace FishingClicker
{
    public partial class FishingLoad : Form
    {
        private Player player;
        private Dictionary<string, TabPage> tabPageDictionary = [];
        public FishingLoad()
        {
            InitializeComponent();
            // event subs
            playerLogIn.LoginSuccessful += PlayerLogIn_LoginSuccessful;
            pictureBox1.MouseClick      += tabPage1_MouseClick;

            textBoxWood.TextChanged     += materialButtons_TextChanged;
            textBoxStone.TextChanged    += materialButtons_TextChanged;
            textBoxIron.TextChanged     += materialButtons_TextChanged;
            textBoxGold.TextChanged     += materialButtons_TextChanged;
            textBoxDiamond.TextChanged  += materialButtons_TextChanged;

            buttonULCase.MouseClick     += caseButton_Click;
            buttonLCase.MouseClick      += caseButton_Click;
            buttonMCase.MouseClick      += caseButton_Click;
            buttonMHCase.MouseClick     += caseButton_Click;
            buttonHCase.MouseClick      += caseButton_Click;
            buttonEHCase.MouseClick     += caseButton_Click;

            // tag assignments
            buttonULCase.Tag = new UltraLightCase();
            buttonLCase.Tag  = new LightCase();
            buttonMCase.Tag  = new MediumCase();
            buttonMHCase.Tag = new MediumHeavyCase();
            buttonHCase.Tag  = new HeavyCase();
            buttonEHCase.Tag = new ExtraHeavyCase();

            // TabControl set up
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
        }
        #region UI cleaners and LogIn event
        // Switch tab depending on button text
        private void ShowTabPageByButtonText(string buttonText)
        {
            if (tabPageDictionary.TryGetValue(buttonText, out TabPage? value))
            {
                tabControl1.TabPages.Clear();
                tabControl1.TabPages.Add(value);
            }
        }
        // PlayerLogIn event from the UserControl
        private void PlayerLogIn_LoginSuccessful(object sender, PlayerEventArgs e)
        {
            player = e.Player;
            progressBarInfo.Maximum = player.PlayerLevel * 1000;
            playerLogIn.Visible = false;
            player.EquippedRod = player.FishingRod.ElementAt(1);
            MessageBox.Show($"Welcome, {player.PlayerName}!");
            playerLogIn.Dispose();
        }
        // label Removers on button press
        private void labelsDisable(Label labelInUse)
        {
            Label labelToKeep = labelInUse;
            foreach (Control control in this.Controls)
            {
                if   (control is Label && control != labelToKeep) { control.Hide(); }
                else { control.Show(); }
            }
        }
        // textBox cleaner on
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

            //Image loader and cleaner
            Bitmap bitmap = new Bitmap("runecircle.png");
            bitmap.MakeTransparent(Color.White);
            for (int x = 0; x < bitmap.Width; x++)
            {
                for (int y = 0; y < bitmap.Height; y++)
                {
                    Color currentColor = bitmap.GetPixel(x, y);
                    if (currentColor.R >= 220 && currentColor.G >= 220 && currentColor.B >= 220)
                    {
                        bitmap.SetPixel(x, y, Color.Transparent);
                    }
                }
            }
            pictureBox2.Image = bitmap;
            pictureBox3.Image = bitmap;
            pictureBox2.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox3.SizeMode = PictureBoxSizeMode.Zoom;
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
        // +1 On mouse click
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
        //Saving player data
        private void FishingLoad_FormClosed(object sender, FormClosedEventArgs e)
        {
            if(player!=null) player.FormClosing(); 
        }
        //Open case button.MouseClick
        private void caseButton_Click(object sender, EventArgs e)
        {
            Button clickedButton = sender as Button;
            var caseClass = clickedButton.Tag as Case;
            if(clickedButton!=null)
            {
                caseClass.FishingRodOpened(player);
                textBoxAvailableGold.Text = player.PlayerGold.ToString();
            }
        }
    }
}
