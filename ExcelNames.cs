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
        public string date;
        public string strankaInfo;
        public string letvica;
        public float popustZMontazo;
        public float popustBrezMontaze;
        public float razrez;
        public float pokrovckiNaStebru;
        public float pokrovRocaj;
        public float rocaj;
        public float rozeta;


        public ExcelNames(List<Item> _itemsOnSecondTab)
        {

            TabControl tab = Application.OpenForms["Form1"].Controls["tabControl1"] as TabControl;
            var popustBrezMontaze2 = tab.SelectedTab.Controls["numericUpDown1"];
            var popustZMontazo2 = tab.SelectedTab.Controls["numericUpDown2"];
            string naVrh = tab.TabPages[0].Controls["groupBox3"].Controls["naVrhRb"].Text;
            string naBok = tab.TabPages[0].Controls["groupBox3"].Controls["naBokRb"].Text;


            foreach (var item in _itemsOnSecondTab)
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
                this.pokrovckiNaStebru += item._stStebrov;
                this.stStebrov += item._stStebrov;
                this.rozeta += item._stStebrov;


                this.stStebrovBolcni = item._stStebrov;
                var k = (item._visina >= 1.5f) ? this.stVelikiNosilec += stStebrovBolcni : this.stMaliNosilec += stStebrovBolcni;

                this.montaza += item._dolzinaProfilov;


                this.razrez += (item._stStebrov + (item._kolikoProfilovVVisino * (item._stStebrov - 1)) >= 0) ? (item._stStebrov + (item._kolikoProfilovVVisino * (item._stStebrov - 1))) : 0;

                this.vijaki += item._stVijakovPriMontazi;

                this.prevoz += item._cenaPrevoza;

                this.letvica += item._profilName + " št. v višino: " + item._kolikoProfilovVVisino + "  /  ";

                if(item._vrstaOgraje == naVrh)
                {
                    this.pokrovckiNaStebru -= item._stStebrov;
                    this.pokrovRocaj += item._stRocajPokrovov;
                    this.rocaj += item._dolzinaProfilov;
                }

                if (item._vrstaOgraje == naBok)
                {
                    this.rozeta -= item._stStebrov;
                    this.pokrovRocaj += item._stRocajPokrovov;
                    this.rocaj += item._dolzinaProfilov;
                }

            }

            this.letvica = this.letvica.Substring(0, this.letvica.Length - 5);

            //datum
            DateTime thisDay = DateTime.Today;
            this.date = thisDay.ToString("d");

            //strankainfo
            var imeInPriimekTb = tab.SelectedTab.Controls["imeInPriimekTb"];
            var telefonTb = tab.SelectedTab.Controls["telefonTb"];
            var naslovTb = tab.SelectedTab.Controls["naslovTb"];
            List<string> info = new List<string> { imeInPriimekTb.Text,
                                                   naslovTb.Text,
                                                   telefonTb.Text};

            strankaInfo = "";
            foreach(var tekst in info)
            {
                strankaInfo += (tekst != "") ? tekst + ", " : "/" + ", ";  
            }
            strankaInfo = strankaInfo.Substring(0, strankaInfo.Length - 2);


            this.popustZMontazo = float.Parse(popustZMontazo2.Text) / 100;
            this.popustBrezMontaze = float.Parse(popustBrezMontaze2.Text) / 100;



        }

        public Dictionary<string, dynamic> dictCalc()
        {
            Dictionary<string, dynamic> dict = new Dictionary<string, dynamic>()
            {
                {"SD-8006 P", sdP },
                {"SD-8006 A", sdA },
                {"SD-8006 B", sdB },
                {"SD-7006",  sd7006},
                {"SD-8005A", cenaStebrovNaMeter},
                { "SD-8002 N", rozeta },
                { "SD8013 N", pokrovckiNaStebru },
                { "SD-5003M", stMaliNosilec },
                { "SD-5003V", stVelikiNosilec },
                {"MONTAZA", montaza },
                {"VIJAKI", vijaki },
                {"MONT.STEBROV", stStebrov },
                {"LEPILO", stStebrov },
                {"PREVOZ", prevoz },
                {"DATUM :",date },
                {"STRANKA :", strankaInfo },
                {"LETVICA :", letvica},
                {"POPUST", popustZMontazo },
                {"P802", rocaj },
                {"POKROV", pokrovRocaj }
            };
            return dict;
        }

        public Dictionary<string, dynamic> dictCalc2()
        {
            Dictionary<string, dynamic> dict = new Dictionary<string, dynamic>()
            {
                {"SD-8006 P", sdP },
                {"SD-8006 A", sdA },
                {"SD-8006 B", sdB },
                {"SD-7006",  sd7006},
                {"SD-8005A", cenaStebrovNaMeter},
                { "SD-8002 N", rozeta },
                { "SD8013 N", pokrovckiNaStebru },
                { "SD-5003M", stMaliNosilec },
                { "SD-5003V", stVelikiNosilec },
                {"MONTAZA", 0 },
                {"VIJAKI", vijaki / 2 },
                {"MONT.STEBROV", 0 },
                {"LEPILO", 0 },
                {"PREVOZ", 0 },
                {"DATUM :",date },
                {"STRANKA :", strankaInfo },
                {"LETVICA :", letvica},
                {"POPUST", popustBrezMontaze },
                {"RAZREZ", razrez },
                {"P802", rocaj },
                {"POKROV", pokrovRocaj }
            };
            return dict;
        }
    }
}
