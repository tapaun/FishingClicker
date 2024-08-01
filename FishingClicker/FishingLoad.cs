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
            ExpertRod fishingRod1 = new ExpertRod("Johnathan", Rarity.Common, 2, RodAction.Light, Level.LevelOne, Material.Wood);
            label1.Text = fishingRod1.DisplayInfo();
            fishingRod1.Upgrade();
            label1.Text += "\n" + fishingRod1.CastLine();
            fishingRod1.Evolve();
            label1.Text += "\n" + fishingRod1.DisplayInfo();
            label1.Text += "\n" + fishingRod1.CastLine();
        }
    }
}
