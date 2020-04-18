using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GivosCalc
{
   public class Profil
    {
        public string _name;
        public float _width;
        public float _price;
        public float _capPrice;
        public Profil(string name, float width, float price, float capPrice)
        {
            this._name = name;
            this._width = width;
            this._price = price;
            this._capPrice = capPrice;
        }
        
    }
}
