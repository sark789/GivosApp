using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GivosCalc
{
    class CalcLogic
    {
        public float price;
        public float capPrice;
        public float width;




        public List<Item> Izracun(ProfiliCollection profili, string selectedProfil, float dolzina, float visina, float rspodnji, float rzgornji, ListBox listbox, (int,float, float) stebri)
        {
            float razmak, razmakGor, razmakDol;
            int pribGor, pribDol;
            List<string> list = new List<string>();
            string res;

            TabControl tab = Application.OpenForms["Form1"].Controls["tabControl1"] as TabControl;
            var cenaPrevozaTb = tab.SelectedTab.Controls["cenaPrevozaTb"];

            float cenaPrevoza = float.Parse(cenaPrevozaTb.Text);

            JArray jsonCeneZaSteber = JArray.Parse(File.ReadAllText("CeneStebri.json"));
            List<float> temp = new List<float>();
            List<Item> items = new List<Item>();

            foreach (JObject obj in jsonCeneZaSteber.Children<JObject>())
            {
                foreach (JProperty singleProp in obj.Properties())
                {
                    float value = float.Parse(singleProp.Value.ToString());
                    temp.Add(value);
                }
            }
            Cene cene = new Cene(temp[6], temp[7], temp[9]);
            float cenaSrauba = cene._enSraub;
            float montaza = cene._montaza;
            float cenaRazreza = cene._razrez;
            Cene lepiu =  new Cene(temp[5]);
            float lepilo = lepiu._lepilo;



            foreach(var obj in profili._profili)
            {
                if(obj._name == selectedProfil)
                {
                    price = obj._price;
                    capPrice = obj._capPrice;
                    width = obj._width;

                    int prib = (int)(visina / (width + ((rspodnji + rzgornji) / 2) * 0.01f));
                    razmak = (visina - prib * width) / prib;

                    float cenaVijakov; 
                    string cenaBrezMontaze, cenaZMontazo;
                    float razrez;

                    //racunanje letvic za razmak, dolzino, visino in končni izpis
                    pribDol = prib;
                    pribGor = prib;
                    do
                    {
                        pribDol -= 1;
                        razmakDol = (visina - pribDol * width) / pribDol;
                        cenaVijakov = pribDol * stebri.Item1 * 4 * cenaSrauba;
                        razrez = cenaRazreza * ((stebri.Item1 + 1) + (stebri.Item1 * pribDol));
                        cenaBrezMontaze = Math.Round((pribDol * obj._price * dolzina + cenaVijakov / 2 + stebri.Item3 + razrez), 2).ToString(".00");
                        cenaZMontazo = Math.Round((pribDol * obj._price * dolzina + cenaVijakov + stebri.Item2 + cenaPrevoza + dolzina * montaza), 2).ToString(".00");

                        res = "razmak: " + (Math.Round(razmakDol * 100, 3)).ToString("00.000") + "cm      stevilo letvic: " + pribDol.ToString("00")  + "   cena letvic: " + (pribDol * obj._price * dolzina).ToString(".00") + " eur" +
                            "    cena vijakov: " + cenaVijakov.ToString(".00") + " eur    cena stebrov: " + stebri.Item2.ToString(".00") +  " eur    skupna cena brez montaže: " 
                            + cenaBrezMontaze + " eur" + "    skupna cena z montažo: " + cenaZMontazo + " eur";
                        
                        list.Add(res);
                        items.Add(new Item(selectedProfil, dolzina, Math.Round(razmakDol * 100, 3), res));
                    }
                    while ((rspodnji <= razmakDol * 100 && razmakDol * 100 <= rzgornji));
                    list.Reverse();
                    items.Reverse();

                    cenaVijakov = prib * stebri.Item1 * 4 * cenaSrauba;
                    razrez = cenaRazreza * ((stebri.Item1 + 1) + (stebri.Item1 * prib));
                    cenaBrezMontaze = Math.Round((prib * obj._price * dolzina + cenaVijakov / 2 + stebri.Item3 + razrez), 2).ToString(".00");
                    cenaZMontazo = Math.Round((prib * obj._price * dolzina + cenaVijakov + stebri.Item2 + cenaPrevoza+ dolzina * montaza), 2).ToString(".00");

                    res = "razmak: " + (Math.Round(razmak * 100, 3)).ToString("00.000") + "cm      stevilo letvic: " + prib.ToString("00") + "   cena letvic: " + (prib * obj._price * dolzina).ToString(".00") + " eur" +
                        "    cena vijakov: " + cenaVijakov.ToString(".00") + " eur    cena stebrov: " + stebri.Item2.ToString(".00") + 
                        " eur    skupna cena brez montaže: " + cenaBrezMontaze + " eur" + "    skupna cena z montažo: "+ cenaZMontazo + " eur";

                    list.Add(res);
                    items.Add(new Item(selectedProfil, dolzina, Math.Round(razmak * 100, 3), res));
                    do
                    {
                        pribGor += 1;
                        razmakGor = (visina - pribGor * width) / pribGor;
                        cenaVijakov = pribGor * stebri.Item1 * 4 * cenaSrauba;
                        razrez = cenaRazreza * ((stebri.Item1 + 1) + (stebri.Item1 * pribGor));
                        cenaBrezMontaze = Math.Round((pribGor * obj._price * dolzina + cenaVijakov / 2 + stebri.Item3 + razrez), 2).ToString(".00");
                        cenaZMontazo = Math.Round((pribGor * obj._price * dolzina + cenaVijakov + stebri.Item2 + cenaPrevoza + dolzina * montaza), 2).ToString(".00");
                        res = "razmak: " + (Math.Round(razmakGor * 100, 3)).ToString("00.000") + "cm      stevilo letvic: " + pribGor.ToString("00") + "   cena letvic: " + (pribGor * obj._price * dolzina).ToString(".00") + " eur" +
                            "    cena vijakov: " + cenaVijakov.ToString(".00") + " eur    cena stebrov: " + stebri.Item2.ToString(".00") + 
                            " eur    skupna cena brez montaže: " + cenaBrezMontaze + " eur" + "    skupna cena z montažo: " + cenaZMontazo + " eur";
                        list.Add(res);
                        items.Add(new Item(selectedProfil, dolzina, Math.Round(razmakGor * 100, 3), res));

                    }
                    while ((rspodnji <= razmakGor * 100 && razmakGor * 100 <= rzgornji));
                    list.Reverse();
                    items.Reverse();
                    foreach (var item in list)
                    {
                        listbox.DataSource = list;
                    }
                   

                }
            }
            return items;
        }

        public (int,float, float) racunStebrov(RadioButton radioButton1, RadioButton radioButton2, RadioButton radioButton3, RadioButton radioButton4)
        {
            TabControl tab = Application.OpenForms["Form1"].Controls["tabControl1"] as TabControl;
            var dolzinaTb = tab.SelectedTab.Controls["dolzinaTb"];
            var razmakStebriTb = tab.SelectedTab.Controls["razmakStebriTb"];
            var visinaTb = tab.SelectedTab.Controls["visinaTb"];
   
            int koncnoStebrov;
            float dolzina;
            try {
                dolzina = float.Parse(dolzinaTb.Text);
            }
            catch
            {
                dolzina = 0f;
            }
            float razmak = 145f;
            try
            {
                razmak = float.Parse(razmakStebriTb.Text);
            }
            catch
            {
                razmak = 145f;
            }
            float enSteberMn, enSteberVec, dvaStebraVec;
            int pribStRaz = (int)(dolzina / (razmak / 100));


            float razmakSteber = dolzina / pribStRaz;
            enSteberMn = dolzina / (pribStRaz - 1);
            enSteberVec = dolzina / (pribStRaz + 1);
            dvaStebraVec = dolzina / (pribStRaz + 2);

            radioButton1.Text = "Razmak: " + (Math.Round(dvaStebraVec * 100, 2)) + "cm Št. stebrov: " + (pribStRaz+3);
            radioButton2.Text = "Razmak: " + (Math.Round(enSteberVec * 100, 2)) + "cm Št. stebrov: " + (pribStRaz + 2);
            radioButton3.Text = "Razmak: " + (Math.Round(razmakSteber * 100, 2)) + "cm Št. stebrov: " + (pribStRaz + 1);
            radioButton4.Text = "Razmak: " + (Math.Round(enSteberMn * 100, 2)) + "cm Št. stebrov: " + (pribStRaz);
           

            JArray jsonCeneZaSteber = JArray.Parse(File.ReadAllText("CeneStebri.json"));
            List<float> temp = new List<float>();

            foreach (JObject obj in jsonCeneZaSteber.Children<JObject>())
            {
                foreach (JProperty singleProp in obj.Properties())
                {
                    float value = float.Parse(singleProp.Value.ToString());
                    temp.Add(value);
                }
            }

            if (radioButton1.Checked)
            {
                koncnoStebrov = (pribStRaz + 3);
            }
            else if (radioButton2.Checked)
            {
                koncnoStebrov = (pribStRaz + 2);
            }
            else if (radioButton3.Checked)
            {
                koncnoStebrov = (pribStRaz + 1);
            }
            else if (radioButton4.Checked)
            {
                koncnoStebrov = (pribStRaz);
            }
            else
            {
                koncnoStebrov = 0;
            }

            Cene cene = new Cene(temp[0], temp[1], temp[2], temp[3], temp[4], temp[5], temp[8]);
            float visina = (float.Parse(visinaTb.Text));
            float kateriNosilec = (visina >= 1.5f) ? cene._dolgNosilec : cene._kratekNosilec;
            float skupnaCenaNaVseStebreZmontaze = koncnoStebrov * (kateriNosilec + (cene._cenaStebraNaMeter * visina) + cene._rozeta  + cene._pokrov + cene._lepilo + cene._montazaStebra);
            float skupnaCenaNaVseStebreBrezmontaze = koncnoStebrov * (kateriNosilec + (cene._cenaStebraNaMeter * visina) + cene._rozeta + cene._pokrov);

            var res = (razmaki: koncnoStebrov - 1, cenaZmontazo: skupnaCenaNaVseStebreZmontaze, cenaBrezMontaze: skupnaCenaNaVseStebreBrezmontaze);
            return res;

        }
    }
}
