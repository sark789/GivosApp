using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GivosCalc
{
    public class Item
    {
        public string _profilName;
        public float _dolzinaProfilov;
        public double _razmakMedProfili;
        public string _itemNapis;
      public Item(string profilName, float dolzinaProfilov, double razmakMedProfili, string itemNapis)
        {
            this._profilName = profilName;
            this._dolzinaProfilov = dolzinaProfilov;
            this._razmakMedProfili = razmakMedProfili;
            this._itemNapis = itemNapis;
        }
    }
}
