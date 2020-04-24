using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GivosCalc
{
    class ExcelNames
    {
        public float sdP;
        public float sdA;
        public float sdB;
        public float sd7006;
        public float stStebrov;
        public float stMaliNosilec;
        public float stVelikiNosilec;
        public float montaza;
        public float vijaki;
        public float prevoz;
        public float cenaStebrovNaMeter;
        public float stStebrovBolcni;


        public ExcelNames(List<Item> _itemsOnSecondTab)
        {
           
            foreach(var item in _itemsOnSecondTab)
            {
                switch (item._profilName)
                {
                    case "SD-8006P":
                        this.sdP += item._celotnaDolzinaProfilov;
                        break;
                    case "SD-8006A":
                        this.sdA += item._celotnaDolzinaProfilov;
                        break;
                    case "SD-8006B":
                        this.sdB += item._celotnaDolzinaProfilov;
                        break;
                    case "SD-7006":
                        this.sd7006 += item._celotnaDolzinaProfilov;
                        break;
                }

                this.cenaStebrovNaMeter += item._stStebrov * item._visina;
                this.stStebrov += item._stStebrov;
                this.stStebrovBolcni = item._stStebrov;
                var k = (item._isDolgNosilec) ? this.stVelikiNosilec += stStebrovBolcni : this.stMaliNosilec += stStebrovBolcni;

                this.montaza += item._dolzinaProfilov;

                this.vijaki += item._stVijakovPriMontazi;

                this.prevoz += item._cenaPrevoza;
            }
        }

        public Dictionary<string, float> dictCalc()
        {
        Dictionary<string, float> dict = new Dictionary<string, float>()
            {
                {"SD-8006 P", sdP },
                {"SD-8006 A", sdA },
                {"SD-8006 B", sdB },
                {"SD-7006",  sd7006},
                {"SD-8005A", cenaStebrovNaMeter},
                { "SD-8002 N", stStebrov },
                { "SD8013 N", stStebrov },
                { "SD-5003M", stMaliNosilec },
                { "SD-5003V", stVelikiNosilec },
                {"MONTAZA", montaza },
                {"VIJAKI", vijaki },
                {"MONT.STEBROV", stStebrov },
                {"LEPILO", stStebrov },
                {"PREVOZ", prevoz }
            };
            return dict;
    }
    }
}
