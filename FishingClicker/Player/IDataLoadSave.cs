using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FishingClicker.User
{
    internal interface IDataLoadSave<T>
    {
        public void SaveToFile(List<T> list, string filePath);
        public List<T> ReadFromFile(string filePath);
    }
}
