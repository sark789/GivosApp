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
        public float _cenaLetvic;
        public float _cenaVijakov;
        public float _cenaPrevoza;
        public float _cenaStebrovBrezMontaze;
        public float _cenaSkupajBrezMontaze;
        public float _cenaStebrovZMontazo; 
        public float _cenaSkupajZMontazo;
        public bool _isDolgNosilec;
        public float _stStebrov;
        public float _celotnaDolzinaProfilov;
        public float _stVijakovPriMontazi;
        public float _visina;
      public Item(string profilName, float dolzinaProfilov, double razmakMedProfili, string itemNapis, float cenaLetvic, float cenaVijakov,
          float cenaPrevoza, float cenaStebrovBrezMontaze, float cenaSkupajBrezMontaze, float cenaStebrovZMontazo, float cenaSkupajZMontazo, 
          bool isDolgNosilec, float stStebrov, float celotnaDolzinaProfilov, float stVijakovPriMontazi, float visina)
        {
            this._profilName = profilName;
            this._dolzinaProfilov = dolzinaProfilov;
            this._razmakMedProfili = razmakMedProfili;
            this._itemNapis = itemNapis;
            this._cenaLetvic = cenaLetvic;
            this._cenaVijakov = cenaVijakov;
            this._cenaPrevoza = cenaPrevoza;
            this._cenaStebrovBrezMontaze = cenaStebrovBrezMontaze;
            this._cenaSkupajBrezMontaze = cenaSkupajBrezMontaze;
            this._cenaStebrovZMontazo = cenaStebrovZMontazo;
            this._cenaSkupajZMontazo = cenaSkupajZMontazo;
            this._isDolgNosilec = isDolgNosilec;
            this._stStebrov = stStebrov;
            this._celotnaDolzinaProfilov = celotnaDolzinaProfilov;
            this._stVijakovPriMontazi = stVijakovPriMontazi;
            this._visina = visina;
        }
    }
}
