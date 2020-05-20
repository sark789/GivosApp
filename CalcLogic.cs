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
        int num1, num2, num3, num4;




        public (List<Item>, List<string>) Izracun(ProfiliCollection profili, string selectedProfil, float dolzina, float visina, 
            float rspodnji, float rzgornji, ListBox listbox, (int,float, float) stebri, bool isKombinirana)
        {
            float razmak, razmakGor, razmakDol;
            int pribGor, pribDol;
            List<string> list = new List<string>();
            List<string> resList = new List<string>();
            string res;

            TabControl tab = Application.OpenForms["Form1"].Controls["tabControl1"] as TabControl;
            num1 = int.Parse(tab.TabPages[0].Controls["numericUpDown3"].Text);
            num2 = int.Parse(tab.TabPages[0].Controls["numericUpDown4"].Text);
            num3 = int.Parse(tab.TabPages[0].Controls["numericUpDown5"].Text);
            num4 = int.Parse(tab.TabPages[0].Controls["numericUpDown6"].Text);

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
          

            foreach (var obj in profili._profili)
            {
                if(obj._name == selectedProfil)
                {
                    price = obj._price;
                    capPrice = obj._capPrice;
                    width = obj._width;

                    int prib = (int)(visina / (width + ((rspodnji + rzgornji) / 2) * 0.01f));
                    int pribKomb = num1 + num2 + num3 + num4;

                    //racunanje letvic za razmak, dolzino, visino in končni izpis
                    pribDol = prib;
                    pribGor = prib;
                    if (!isKombinirana)
                    {
                        do
                        {
                            pribDol -= 1;

                            razmakDol = Izpis(obj, selectedProfil, visina, dolzina, pribDol, stebri, list, items, cenaSrauba, cenaRazreza, montaza, cenaPrevoza, isKombinirana,  profili);
                        }
                        while ((rspodnji <= razmakDol * 100 && razmakDol * 100 <= rzgornji));
                        list.Reverse();
                        items.Reverse();

                        razmak = razmakDol = Izpis(obj, selectedProfil, visina, dolzina, prib, stebri, list, items, cenaSrauba, cenaRazreza, montaza, cenaPrevoza, isKombinirana,  profili);
                        do
                        {
                            pribGor += 1;

                            razmakGor = Izpis(obj, selectedProfil, visina, dolzina, pribGor, stebri, list, items, cenaSrauba, cenaRazreza, montaza, cenaPrevoza, isKombinirana,  profili);

                        }
                        while ((rspodnji <= razmakGor * 100 && razmakGor * 100 <= rzgornji));
                    }
                    else
                    {
                        razmak = razmakDol = Izpis(obj, selectedProfil, visina, dolzina, pribKomb, stebri, list, items, cenaSrauba, cenaRazreza, montaza, cenaPrevoza, isKombinirana, profili);
                    }
                    list.Reverse();
                    items.Reverse();                    
                    resList = FancierString(list);
             
                    listbox.DataSource = resList;                        
                
                }
            }
            return (items,resList);
        }

        private float Izpis(Profil obj, string selectedProfil, float visina, float dolzina, int prib, (int, float, float) stebri,
            List<string> list, List<Item> items, float cenaSrauba, float cenaRazreza, float montaza, float cenaPrevoza, bool isKombinirana, ProfiliCollection profiliCollection)
        {

            price = obj._price;
            capPrice = obj._capPrice;
            width = obj._width;

            float razmak = (visina - 0.04f - prib * width) / prib;
            float cenaLetvic;
            double cenaBrezMontaze, cenaZMontazo;
            float stVijakov;
            float cenaVijakov;
            float razrez;
            double pisiRazmak;
            int stStebrov = stebri.Item1 + 1;

            //vrsta ograje
            TabControl tab = Application.OpenForms["Form1"].Controls["tabControl1"] as TabControl;
            var naBok = tab.SelectedTab.Controls["groupBox3"];
            RadioButton isNaVrh = (RadioButton)naBok.Controls["naVrhRb"];
            RadioButton isNaBok = (RadioButton)naBok.Controls["naBokRb"];
            RadioButton isVrtna = (RadioButton)naBok.Controls["vrtnaRb"];
            string vrstaOgraje = "";
            if (isVrtna.Checked) { vrstaOgraje = isVrtna.Text; }
            if (isNaBok.Checked) { vrstaOgraje = isNaBok.Text; }
            if (isNaVrh.Checked) { vrstaOgraje = isNaVrh.Text; }


            JArray jsonCeneZaSteber = JArray.Parse(File.ReadAllText("CeneStebri.json"));
            List<float> temp = new List<float>();
            foreach (JObject itm in jsonCeneZaSteber.Children<JObject>())
            {
                foreach (JProperty singleProp in itm.Properties())
                {
                    float value = float.Parse(singleProp.Value.ToString());
                    temp.Add(value);
                }
            }

            Cene cene = new Cene(temp[10], temp[11], temp[12], temp[13]);
            float cenaPokrovaZaRocaj = cene._pokrovZaRocaj;
            float rocaj = cene._rocaj;
            float lprofil = cene._lprofil;

            //st pokrovov pri rocaju
            NumericUpDown pokrovi = (NumericUpDown)tab.SelectedTab.Controls["pokrovUpDown"];
            float stRocajPokrovov = (float)pokrovi.Value;
            float cenaRocaja;
            if (isNaBok.Checked || isNaVrh.Checked)
            {
                cenaRocaja = cenaPokrovaZaRocaj * stRocajPokrovov + dolzina * rocaj;
                if (isNaBok.Checked)
                {
                    cenaRocaja = cenaPokrovaZaRocaj * stRocajPokrovov + dolzina * rocaj + 4 * visina * lprofil;
                }
            }
            else { cenaRocaja = 0; }


            stVijakov = prib * stebri.Item1 * 4;
            cenaVijakov = stVijakov * cenaSrauba;
            if (cenaVijakov < 0)
            {
                cenaVijakov = 0;
                stVijakov = 0;
            }

            razrez = cenaRazreza * ((stStebrov) + (stebri.Item1 * prib));
            if (stStebrov <= 1)
            {
                razrez = 0;
            }

            pisiRazmak = (Math.Round(razmak * 100, 3));
            string predznak = "";
            if (pisiRazmak >= 0)
            {
                predznak = "+";
            }
            else
            {
                pisiRazmak *= -1;
                predznak = "‒";
            }

            //katera vrsta ograje - enostavna, kombinirana

            Dictionary<Profil, int> dictProfilov = new Dictionary<Profil, int>();
            num1 = int.Parse(tab.TabPages[0].Controls["numericUpDown3"].Text);
            num2 = int.Parse(tab.TabPages[0].Controls["numericUpDown4"].Text);
            num3 = int.Parse(tab.TabPages[0].Controls["numericUpDown5"].Text);
            num4 = int.Parse(tab.TabPages[0].Controls["numericUpDown6"].Text);
           
            string vrsta = "";
            string letvice = "";
            if (isKombinirana)
            {
                ComboBox profilCb = (ComboBox)tab.TabPages[0].Controls["profilCb"];
                string cb = profilCb.SelectedItem.ToString();
                ComboBox profilCb2 = (ComboBox)tab.TabPages[0].Controls["profilCb2"];
                string cb2 = profilCb2.SelectedItem.ToString();
                ComboBox profilCb3 = (ComboBox)tab.TabPages[0].Controls["profilCb3"];
                string cb3 = profilCb3.SelectedItem.ToString();
                ComboBox profilCb4 = (ComboBox)tab.TabPages[0].Controls["profilCb4"];
                string cb4 = profilCb4.SelectedItem.ToString();

                float wid1 = 0;
                float wid2 = 0;
                float wid3 = 0;
                float wid4 = 0;
                float price1 = 0;
                float price2 = 0;
                float price3 = 0;
                float price4 = 0;
                if (num1 != 0)
                {
                    letvice += cb + " x" + num1 + ", ";
                    dictProfilov.Add(profiliCollection._profili[0], num1);
                    wid1 = profiliCollection._profili[0]._width;
                    price1 = profiliCollection._profili[0]._price;
                }
                if (num2 != 0)
                {
                    letvice += cb2 + " x" + num2 + ", ";
                    dictProfilov.Add(profiliCollection._profili[1], num2);
                    wid2 = profiliCollection._profili[1]._width;
                    price2 = profiliCollection._profili[1]._price;
                }
                if (num3 != 0)
                {
                    letvice += cb3 + " x" + num3 + ", ";
                    dictProfilov.Add(profiliCollection._profili[2], num3);
                    wid3 = profiliCollection._profili[2]._width;
                    price3 = profiliCollection._profili[2]._price;
                }
                if (num4 != 0)
                {
                    letvice += cb4 + " x" + num4 + ", ";
                    dictProfilov.Add(profiliCollection._profili[3], num4);
                    wid4 = profiliCollection._profili[3]._width;
                    price4 = profiliCollection._profili[3]._price;
                }
                try
                {
                    letvice = letvice.Substring(0, letvice.Length - 2);
                }catch(Exception e) { letvice = "Noben model"; }
                
                vrsta = "Kombinacija";

                razmak = (visina - 0.04f - (wid1 * num1 + wid2 * num2 + wid3 * num3 + wid4 * num4)) / prib;
                cenaLetvic = ((price1 * num1 + price2 * num2 + price3 * num3 + price4 * num4) * dolzina);
                cenaBrezMontaze = Math.Round((cenaLetvic + cenaVijakov / 2 + stebri.Item3 + razrez + cenaRocaja), 2);
                cenaZMontazo = Math.Round((cenaLetvic + cenaVijakov + stebri.Item2 + cenaPrevoza + dolzina * montaza + cenaRocaja), 2);
            }
            else
            {
                vrsta = "Enostavna";
                letvice = selectedProfil;
                cenaLetvic = (prib * obj._price * dolzina);
                cenaBrezMontaze = Math.Round((prib * obj._price * dolzina + cenaVijakov / 2 + stebri.Item3 + razrez + cenaRocaja), 2);
                cenaZMontazo = Math.Round((prib * obj._price * dolzina + cenaVijakov + stebri.Item2 + cenaPrevoza + dolzina * montaza + cenaRocaja), 2);
            }

          
                               

            string res = "Vrsta ograje: " + vrstaOgraje + " / " + vrsta + " (" + letvice + ")   "+"Razmak: " + predznak + pisiRazmak.ToString("0.000") + "cm   Stevilo letvic: " + prib.ToString("0") + "   Cena letvic: " + cenaLetvic.ToString("0.00") + " eur" +
               "   Cena stebrov brez montaže: " + stebri.Item3.ToString("0.00") + " eur   Cena stebrov z montažo: " + stebri.Item2.ToString("0.00") +
            " eur   Skupna cena brez montaže: " + cenaBrezMontaze.ToString("0.00") + " eur" + "   Skupna cena z montažo: " + cenaZMontazo.ToString("0.00") + " eur";

            list.Add(res);
            items.Add(new Item(selectedProfil, dolzina, Math.Round(razmak * 100, 3), res, cenaLetvic, cenaVijakov, cenaPrevoza,
                    stebri.Item3, (float)cenaBrezMontaze, stebri.Item2, (float)cenaZMontazo, stStebrov, dolzina * prib,
                    stVijakov, visina, prib, vrstaOgraje, stRocajPokrovov, isKombinirana, dictProfilov));
            return razmak;
 
        }

        public List<string> FancierString(List<string> input)
        {
            string[] stop = new string[] { "   " };
            List<int[]> dolzinaNiza = new List<int[]>();
            List<string[]> besede = new List<string[]>();
            List<string> resList = new List<string>();
            int st = 8;
            int[] max = new int[st];
            
            int i = 0;
            foreach (var niz in input)
            {
                string[] substring1 = niz.Split(stop, StringSplitOptions.None);
                int[] stevila = new int[st];
                foreach (var item in substring1)
                {
                    stevila[i] = item.Length;
                    i++;
                }
                i = 0;
                dolzinaNiza.Add(stevila);
                besede.Add(substring1);
            }
            for (var j = 0; j < st; j++) {
                int[] temp = new int[100000];
                foreach (var item in dolzinaNiza)
                {
                    temp[i] = item[j];
                    i++;
                }
                i = 0;
                max[j] = temp.Max();
            }

            System.Text.StringBuilder res = new System.Text.StringBuilder();
            foreach (var item in besede)
            {
                res.Clear();
                int c;
                foreach(var el in item)
                {

                    c = max[i] - el.Length;
                    int d = 4;
                    if (c != 0)
                    {
                        d = 4 + c; 
                    }
                    
                    i++;
                    res.Append(el);
                    res.Append("".PadRight(d, ' '));
                }
                i = 0;
                resList.Add(res.ToString());
            }
            return resList;
        }

        public (int,float, float) racunStebrov(RadioButton radioButton1, RadioButton radioButton2, RadioButton radioButton3, RadioButton radioButton4)
        {
            TabControl tab = Application.OpenForms["Form1"].Controls["tabControl1"] as TabControl;
            var dolzinaTb = tab.SelectedTab.Controls["dolzinaTb"];
            var razmakStebriTb = tab.SelectedTab.Controls["razmakStebriTb"];
            var visinaTb = tab.SelectedTab.Controls["visinaTb"];
            var naBok = tab.SelectedTab.Controls["groupBox3"];
            RadioButton isNaVrh = (RadioButton)naBok.Controls["naVrhRb"];
            RadioButton isNaBok = (RadioButton)naBok.Controls["naBokRb"];
            
            
   
            int koncnoStebrov;
            float dolzina;
            float razmak;
            float visina;
            try {
                dolzina = float.Parse(dolzinaTb.Text);
                razmak = float.Parse(razmakStebriTb.Text);
                visina = (float.Parse(visinaTb.Text));
            }
            catch
            {
                dolzina = 0f;
                razmak = 145f;
                visina = 0f;
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
            float kateriNosilec = (visina >= 1.5f) ? cene._dolgNosilec : cene._kratekNosilec;
            float skupnaCenaNaVseStebreZmontaze = koncnoStebrov * (kateriNosilec + (cene._cenaStebraNaMeter * visina) + cene._rozeta  + cene._pokrov + cene._lepilo + cene._montazaStebra);
            float skupnaCenaNaVseStebreBrezmontaze = koncnoStebrov * (kateriNosilec + (cene._cenaStebraNaMeter * visina) + cene._rozeta + cene._pokrov);

               if (isNaBok.Checked) 
            {
                skupnaCenaNaVseStebreZmontaze = koncnoStebrov * (kateriNosilec + (cene._cenaStebraNaMeter * visina) + cene._pokrov + cene._lepilo + cene._montazaStebra);
                skupnaCenaNaVseStebreBrezmontaze = koncnoStebrov * (kateriNosilec + (cene._cenaStebraNaMeter * visina) + cene._pokrov);
            }
            if (isNaVrh.Checked)
            {
                skupnaCenaNaVseStebreZmontaze = koncnoStebrov * (kateriNosilec + (cene._cenaStebraNaMeter * visina) + cene._lepilo + cene._rozeta + cene._montazaStebra);
                skupnaCenaNaVseStebreBrezmontaze = koncnoStebrov * (kateriNosilec + (cene._cenaStebraNaMeter * visina)+ cene._rozeta);
            }

            var res = (razmaki: koncnoStebrov - 1, cenaZmontazo: skupnaCenaNaVseStebreZmontaze, cenaBrezMontaze: skupnaCenaNaVseStebreBrezmontaze);
            return res;

        }
    }
}
