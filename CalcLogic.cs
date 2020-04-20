﻿using Newtonsoft.Json.Linq;
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
                    razmak = (visina - 0.04f - prib * width) / prib;

                    float cenaVijakov; 
                    double cenaBrezMontaze, cenaZMontazo;
                    float razrez;
                    float cenaLetvic;

                    //racunanje letvic za razmak, dolzino, visino in končni izpis
                    pribDol = prib;
                    pribGor = prib;
                    do
                    {
                        pribDol -= 1;
                        razmakDol = (visina - 0.04f - pribDol * width) / pribDol;
                        cenaVijakov = pribDol * stebri.Item1 * 4 * cenaSrauba;
                        cenaLetvic = (pribDol * obj._price * dolzina);
                        razrez = cenaRazreza * ((stebri.Item1 + 1) + (stebri.Item1 * pribDol));
                        cenaBrezMontaze = Math.Round((pribDol * obj._price * dolzina + cenaVijakov / 2 + stebri.Item3 + razrez), 2);
                        cenaZMontazo = Math.Round((pribDol * obj._price * dolzina + cenaVijakov + stebri.Item2 + cenaPrevoza + dolzina * montaza), 2);

                        res = "razmak: " + (Math.Round(razmakDol * 100, 3)).ToString("00.000") + "cm      stevilo letvic: " + pribDol.ToString("00")  + "   cena letvic: " + cenaLetvic.ToString(".00") + " eur" +
                            "    cena vijakov: " + cenaVijakov.ToString(".00") + " eur    cena stebrov: " + stebri.Item2.ToString(".00") +  " eur    skupna cena brez montaže: " 
                            + cenaBrezMontaze.ToString(".00") + " eur" + "    skupna cena z montažo: " + cenaZMontazo.ToString(".00") + " eur";
                        
                        list.Add(res);
                        items.Add(new Item(selectedProfil, dolzina, Math.Round(razmakDol * 100, 3), res, cenaLetvic, cenaVijakov, cenaPrevoza,
                            stebri.Item3, (float)cenaBrezMontaze, stebri.Item2, (float)cenaZMontazo));
                    }
                    while ((rspodnji <= razmakDol * 100 && razmakDol * 100 <= rzgornji));
                    list.Reverse();
                    items.Reverse();

                    cenaVijakov = prib * stebri.Item1 * 4 * cenaSrauba;
                    razrez = cenaRazreza * ((stebri.Item1 + 1) + (stebri.Item1 * prib));
                    cenaLetvic = (prib * obj._price * dolzina);
                    cenaBrezMontaze = Math.Round((prib * obj._price * dolzina + cenaVijakov / 2 + stebri.Item3 + razrez), 2);
                    cenaZMontazo = Math.Round((prib * obj._price * dolzina + cenaVijakov + stebri.Item2 + cenaPrevoza+ dolzina * montaza), 2);

                    res = "razmak: " + (Math.Round(razmak * 100, 3)).ToString("00.000") + "cm      stevilo letvic: " + prib.ToString("00") + "   cena letvic: " + cenaLetvic.ToString(".00") + " eur" +
                        "    cena vijakov: " + cenaVijakov.ToString(".00") + " eur    cena stebrov: " + stebri.Item2.ToString(".00") + 
                        " eur    skupna cena brez montaže: " + cenaBrezMontaze.ToString(".00") + " eur" + "    skupna cena z montažo: "+ cenaZMontazo.ToString(".00") + " eur";

                    list.Add(res);
                    items.Add(new Item(selectedProfil, dolzina, Math.Round(razmak * 100, 3), res, cenaLetvic, cenaVijakov, cenaPrevoza,
                            stebri.Item3, (float)cenaBrezMontaze, stebri.Item2, (float)cenaZMontazo));
                    do
                    {
                        pribGor += 1;
                        razmakGor = (visina - 0.04f - pribGor * width) / pribGor;
                        cenaVijakov = pribGor * stebri.Item1 * 4 * cenaSrauba;
                        cenaLetvic = (pribGor * obj._price * dolzina);
                        razrez = cenaRazreza * ((stebri.Item1 + 1) + (stebri.Item1 * pribGor));
                        cenaBrezMontaze = Math.Round((pribGor * obj._price * dolzina + cenaVijakov / 2 + stebri.Item3 + razrez), 2);
                        cenaZMontazo = Math.Round((pribGor * obj._price * dolzina + cenaVijakov + stebri.Item2 + cenaPrevoza + dolzina * montaza), 2);
                        res = "razmak: " + (Math.Round(razmakGor * 100, 3)).ToString("00.000") + "cm      stevilo letvic: " + pribGor.ToString("00") + "   cena letvic: " + cenaLetvic.ToString(".00") + " eur" +
                            "    cena vijakov: " + cenaVijakov.ToString(".00") + " eur    cena stebrov: " + stebri.Item2.ToString(".00") + 
                            " eur    skupna cena brez montaže: " + cenaBrezMontaze.ToString(".00") + " eur" + "    skupna cena z montažo: " + cenaZMontazo.ToString(".00") + " eur";
                        list.Add(res);
                        items.Add(new Item(selectedProfil, dolzina, Math.Round(razmakGor * 100, 3), res, cenaLetvic, cenaVijakov, cenaPrevoza,
                            stebri.Item3, (float)cenaBrezMontaze, stebri.Item2, (float)cenaZMontazo));

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

            var res = (razmaki: koncnoStebrov - 1, cenaZmontazo: skupnaCenaNaVseStebreZmontaze, cenaBrezMontaze: skupnaCenaNaVseStebreBrezmontaze);
            return res;

        }
    }
}