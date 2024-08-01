using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
            BeginnerRod fishingRod1 = new BeginnerRod("Johnathan", Rarity.Common, 2, RodAction.Light, Level.LevelOne, Material.Wood);
            label1.Text = fishingRod1.DisplayInfo();
            fishingRod1.Upgrade();
            fishingRod1.Evolve();
            label1.Text += "\n \n \n" + fishingRod1.DisplayInfo();
            label1.Text += "\n \n \n" + fishingRod1.CastLine();
        }
    }
}
