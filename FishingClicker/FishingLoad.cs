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
            var playerManager = new PlayerManager();
            var loadedPlayers = playerManager.LoadFromFile();
            label1.Text ="";
            foreach (var player in loadedPlayers)
            {
                label1.Text += player.displayPlayerInfo();
            }
        }
    }
}
