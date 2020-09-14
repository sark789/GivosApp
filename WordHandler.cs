using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;
using Newtonsoft.Json.Linq;

namespace GivosCalc
{
    class WordHandler
    {
        public void SaveAndOpenWord(string path, string outputPath, List<Item> _itemsOnSecondTab, ProfiliCollection vodoravniProfili) 
        {
            Microsoft.Office.Interop.Word.Application word = new Microsoft.Office.Interop.Word.Application();
            Document document = word.Documents.Open(System.AppDomain.CurrentDomain.BaseDirectory + path);
            document.SaveAs(outputPath);
            document.Close(0);

            word.Visible = true;
            Document workDocument = word.Documents.Open(outputPath);
            WriteValuesInWord(_itemsOnSecondTab, word, vodoravniProfili, workDocument);
            workDocument.Save();
        }

        public void WriteValuesInWord(List<Item> _itemsOnSecondTab, Microsoft.Office.Interop.Word.Application word, ProfiliCollection vodoravniProfili, Document workDocument) 
        {
            TabControl tab = System.Windows.Forms.Application.OpenForms["Form1"].Controls["tabControl1"] as TabControl;
            string vrtna = tab.TabPages[0].Controls["groupBox3"].Controls["vrtnaRb"].Text;
            string naVrh = tab.TabPages[0].Controls["groupBox3"].Controls["naVrhRb"].Text;
            var imeInPriimekTb = tab.SelectedTab.Controls["imeInPriimekTb"];
            string naslovTb = tab.SelectedTab.Controls["naslovTb"].Text;
            string naslov = "";
            try
            {
                string cleanedNaslov = System.Text.RegularExpressions.Regex.Replace(naslovTb, @"\s+", " ");
                naslov = cleanedNaslov.Replace(", ", "^l").Trim();              
            }
            catch (Exception e) { naslov = naslovTb; }
            var stPonudbeTb = tab.SelectedTab.Controls["stPonudbeTb"];
            var numericUpDown1 = tab.SelectedTab.Controls["numericUpDown1"];

            DateTime thisDay = DateTime.Today;
            string date = thisDay.ToString("d");
            float dolzina = 0;
            float cena_z = 0;
            float dim = 0;
            float cena_brez = 0;
            float st_stebrov = 0;
            float visina = 0;
            var str = "";
            var info = "";
            string stebri = "";
            string kam = "";
            string prevoz = "";
            bool containsBalkonskaOgraja = false;
            bool containsVrtnaOgraja = false;
            List<string> temp_list = new List<string>();
            List<string> vrste = new List<string>();
            List<string> model_ograj = new List<string>();
            List<string> allInfoText = new List<string>();
            Dictionary<string, string> mainBody = new Dictionary<string, string>();
            

            foreach (var item in _itemsOnSecondTab)
            {
                if (!item._isKombinirana)
                {
                    temp_list.Add(item._profilName);
                }
                else
                {
                    string komb = "kombinacija (";
                    foreach(var val in item._dict)
                    {
                        komb += val.Key._name + " + ";
                    }
                    komb = komb.Substring(0, komb.Length - 3) + ")";
                    temp_list.Add(komb);
                }
            }
            model_ograj = temp_list.Distinct().ToList();
            string modeli_ograj = string.Join(" + ", model_ograj.ToArray());
           
            
            foreach (var item in model_ograj)
            {
                bool komb = false;
                foreach (var i in vodoravniProfili._profili)
                {
                    if (item == i._name)
                    {
                        dim = i._width;
                        komb = false;
                        break;
                    }
                    else
                    {
                        komb = true;
                    }
                }
                if (!komb)
                {
                    string predInfo = File.ReadAllText(System.AppDomain.CurrentDomain.BaseDirectory + "predInfo.txt");
                    predInfo = predInfo.Replace("<model_ograje>", item);
                    predInfo = predInfo.Replace("<dim>", (dim * 1000).ToString());
                    mainBody.Add(item, predInfo + "\n\n\n");
                }
                else
                {
                    string s = "Kombinirani ALU ograjni profili, ki se med ALU stebre ( v utore stebrov ) vstavljajo v :";
                    mainBody.Add(item, s + "\n\n\n");
                }
                komb = false;
            }

            List<double> vsiRazmaki = new List<double>();
            foreach (var item in _itemsOnSecondTab)
            {
                dolzina += item._dolzinaProfilov;
                cena_z += item._cenaSkupajZMontazo;
                cena_brez += item._cenaSkupajBrezMontaze;
                st_stebrov += item._stStebrov;
                stebri += item._stStebrov + " kos"+"  +  ";
                visina += item._stStebrov * item._visina;
                vsiRazmaki.Add(item._razmakMedProfili);
                if(item._cenaPrevoza == 0) { prevoz = " (velja za lokacijo  Ljubaljana – Kranj z bližnjo okolico )"; }
                else
                {
                    prevoz = string.Empty;
                }
                if (item._vrstaOgraje == vrtna)
                {
                    containsVrtnaOgraja = true;
                    info = (item._razmakMedProfili <= 0) ? File.ReadAllText(System.AppDomain.CurrentDomain.BaseDirectory + "infoProfiliSePrekrivajo.txt") :
                    File.ReadAllText(System.AppDomain.CurrentDomain.BaseDirectory + "infoProfiliSeNePrekrivajo.txt");
                    if (item._isKombinirana)
                    {                        
                        string komb1 = " \n             Na omenjeni dolžini je kombinacija ograjnih profilov sledeča :";
                        foreach (var val in item._dict)
                        {
                            komb1 +=  "\n             "+ val.Value + " kos  x  vodoravno ALU profili z oznako " + val.Key._name + " ( dim.: " + val.Key._width * 1000+ "x14mm )" + ", ";
                            
                        }
                        komb1 = komb1.Substring(0, komb1.Length - 2);

                        info += komb1;
                    }

                }
                else
                {
                    containsBalkonskaOgraja = true;                   
                    info = (item._razmakMedProfili <= 0) ? File.ReadAllText(System.AppDomain.CurrentDomain.BaseDirectory + "infoProfiliBalkonSePrekrivajo.txt") :
                    File.ReadAllText(System.AppDomain.CurrentDomain.BaseDirectory + "infoProfiliBalkonSeNePrekrivajo.txt");

                    if(item._vrstaOgraje == naVrh)
                    {
                        kam = "vrh";
                    }
                    else { kam = "bok"; }
                    if (item._isKombinirana)
                    {
                        string komb1 = " \n             Na omenjeni dolžini je kombinacija ograjnih profilov sledeča :";
                        foreach (var val in item._dict)
                        {
                            komb1 += "\n             " + val.Value + " kos  x  vodoravno ALU profili z oznako " + val.Key._name + " ( dim.: " + val.Key._width * 1000 + "x14mm )" + ", ";

                        }
                        komb1 = komb1.Substring(0, komb1.Length - 2);

                        info += komb1;
                    }
                }               

                string komb = "kombinacija (";
                foreach (var val in item._dict)
                {
                    komb += val.Key._name + " + ";
                }
                komb = komb.Substring(0, komb.Length - 3) + ")";

                if (mainBody.ContainsKey(item._profilName) ||mainBody.ContainsKey(komb))
                {
                    try
                    {
                        info = info.Replace("<st_prof>", item._kolikoProfilovVVisino.ToString());
                        info = info.Replace("<posamezna_dolzina>", item._dolzinaProfilov.ToString("0.00"));
                        info = info.Replace("<visina>", (item._visina * 100).ToString("0.00"));
                        info = info.Replace("<razmak>", item._razmakMedProfili.ToString("0.00"));
                    }
                    catch (Exception e) { }

                    if (item._isKombinirana)
                    {
                        
                        mainBody[komb] += info + "\n\n";
                    }
                    else
                    {
                        mainBody[item._profilName] += info + "\n\n";
                    }
                   
                }


                
            }

            stebri = stebri.Remove(stebri.Length - 5);
            visina = visina / st_stebrov;
            

            //reference slik
            JArray jsonReferenc = JArray.Parse(File.ReadAllText("referenceSlikString.json"));
            List<string> temp = new List<string>();
            foreach (JObject obj in jsonReferenc.Children<JObject>())
            {
                foreach (JProperty singleProp in obj.Properties())
                {
                    string value = singleProp.Value.ToString();
                    temp.Add(value);
                }
            }

            //reference za SD-8006P = temp[0];
            //reference za ostale = temp[1];

            string ref_slik = "";
            if (model_ograj.Contains("SD-8006P") && containsVrtnaOgraja)
            {
                ref_slik += temp[0] + "  in  ";
            }
            if(model_ograj.Contains("SD-8006A") || model_ograj.Contains("SD-8006B") || model_ograj.Contains("SD-7006") && containsVrtnaOgraja)
            {
                ref_slik += temp[1]+ "  in  ";
            }
            foreach(var item in _itemsOnSecondTab)
            {
                if(item._isKombinirana && item._vrstaOgraje == vrtna)
                {
                    ref_slik += temp[2] + "  in  ";
                    break;
                }
                
            }
            
            foreach (var item in _itemsOnSecondTab)
            {
                if (item._vrstaOgraje != vrtna)
                {
                    ref_slik += temp[4] + "  in  ";
                    break;
                }
            }

            ref_slik = ref_slik.Substring(0, ref_slik.Length - 6);


            bool allNeg = vsiRazmaki.All(a => a <= 0);
            string ps = "";
            //open text file and read it
            if (!containsBalkonskaOgraja)
            {
                str = (allNeg) ? File.ReadAllText(System.AppDomain.CurrentDomain.BaseDirectory + "SD-8006PinfoTemplate.txt") :
                File.ReadAllText(System.AppDomain.CurrentDomain.BaseDirectory + "OtherProfilesinfoTemplate.txt");
                ps = File.ReadAllText(System.AppDomain.CurrentDomain.BaseDirectory + "PS.txt");
            }
            else
            {
                modeli_ograj = modeli_ograj + " + P802-ročaj";
                str = File.ReadAllText(System.AppDomain.CurrentDomain.BaseDirectory + "balkonskaInfoTemplate.txt");
                ps = "";
            }

            float cena_brez_in_popust = cena_brez * ((100 - int.Parse(numericUpDown1.Text)) * 0.01f);
            string kolPopustString = "";
            int kol_popust = 0;
            switch (dolzina)
            {
                case var expression when dolzina < 20:
                    kol_popust = 0;
                    break;
                case var expression when (dolzina >= 20 && dolzina < 30):
                    kol_popust = 2;
                    break;
                case var expression when (dolzina >= 30 && dolzina < 40):
                    kol_popust = 3;
                    break;
                case var expression when (dolzina >= 40 && dolzina < 50):
                    kol_popust = 4;
                    break;

                default:
                    kol_popust = 5;
                    break;
            }
            if(kol_popust == 0)
            {
                kolPopustString = "";
            }
            else
            {
                kolPopustString = "Glede na količino  ( L = " + dolzina + "m ), vam na omenjeni znesek nudimo dodatno še " + kol_popust + "% popusta  ( količinski popust ).";
            }

            string vrstaOgraje = "";
            if (containsBalkonskaOgraja) { vrstaOgraje = "BALKONSKE"; }
            if (containsVrtnaOgraja) { vrstaOgraje = "VRTNE"; }
            if (containsVrtnaOgraja && containsBalkonskaOgraja) { vrstaOgraje = "BALKONSKE IN VRTNE"; }                        


            Dictionary<string, string> dict = new Dictionary<string, string> {  {"<opis>", str },
                                                                                {"<kol_popust>", kolPopustString },
                                                                                {"<info>",   string.Join("\n", mainBody.Values.ToArray())},
                                                                                {"<ime>", imeInPriimekTb.Text},
                                                                                {"<naslov>",  naslov},
                                                                                {"<st.ponudbe>", stPonudbeTb.Text },
                                                                                {"<datum>", date },
                                                                                {"<dolzina>",  dolzina.ToString(".00")},
                                                                                {"<visina>", (visina * 100).ToString(".00") },
                                                                                {"<cena_z>", cena_z.ToString("N",new CultureInfo("is-IS")) },
                                                                                {"<cena_brez>", cena_brez.ToString("N",new CultureInfo("is-IS")) },
                                                                                {"<popust_brez>", numericUpDown1.Text},
                                                                                {"<cena_brez_in_popust>",  cena_brez_in_popust.ToString("N",new CultureInfo("is-IS"))},
                                                                                {"<stebri>", stebri},
                                                                                {"<ref_slik>", ref_slik },
                                                                                {"<mozni_modeli>", modeli_ograj },
                                                                                {"<vrsta_ograje>", vrstaOgraje },
                                                                                {"<kam>", kam },
                                                                                {"<prevoz>", prevoz },
                                                                                {"<PS>", ps } };

            foreach (var item in dict)
            {
                FindAndReplace(word, item.Key,item.Value, workDocument);
            }
            foreach (Shape shape in workDocument.Shapes)
            {
                if (shape.Name == "Text Box 2")
                {
                    if (shape.AlternativeText.Contains("<mozni_modeli>"))
                    {
                        modeli_ograj = "OKVIRNA PONUDBA " + vrstaOgraje + " ALU OGRAJE\n\n" + "MODEL :  " + modeli_ograj + " + ALU stebri SD-8005A \n\nBARVA  :    ANTRACIT  ( tovarniška barva )";
                        shape.TextFrame.TextRange.Text = modeli_ograj;
                    }

                }
            }

        }
        private void FindAndReplace(Microsoft.Office.Interop.Word.Application doc, object findText, object replaceWithText, Document workDocument)
        {
            //options
            object matchCase = false;
            object matchWholeWord = true;
            object matchWildCards = false;
            object matchSoundsLike = false;
            object matchAllWordForms = false;
            object forward = true;
            object format = false;
            object matchKashida = false;
            object matchDiacritics = false;
            object matchAlefHamza = false;
            object matchControl = false;
            object read_only = false;
            object visible = true;
            object replace = 2;
            object wrap = 1;
            //execute find and replace

            try
            {
                doc.Selection.Find.Execute(ref findText, ref matchCase, ref matchWholeWord,
                    ref matchWildCards, ref matchSoundsLike, ref matchAllWordForms, ref forward, ref wrap, ref format, ref replaceWithText, ref replace,
                    ref matchKashida, ref matchDiacritics, ref matchAlefHamza, ref matchControl);
            }
            catch(Exception e)
            {
                Range range = workDocument.Content;
                range.Find.Execute(findText);
                range.Text = replaceWithText.ToString();
            }

        }
    }
}
