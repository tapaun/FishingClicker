using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingClicker.Player
{
    internal interface IDataLoadSave
    {
        public void SaveToFile(List<Player> players, string filePath);
        public List<Player> ReadFromFile(string filePath);
    }
}
