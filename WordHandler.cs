using System;
using System.Collections.Generic;
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
            List<string> temp_list = new List<string>();
            List<string> model_ograj = new List<string>();
            List<string> allInfoText = new List<string>();
            Dictionary<string, string> mainBody = new Dictionary<string, string>();
            

            foreach (var item in _itemsOnSecondTab)
            {
                temp_list.Add(item._profilName);
            }
            model_ograj = temp_list.Distinct().ToList();
            string modeli_ograj = string.Join(" + ", model_ograj.ToArray());
           
            
            foreach (var item in model_ograj)
            {
                foreach (var i in vodoravniProfili._profili)
                {
                    if (item == i._name)
                    {
                        dim = i._width;
                    }
                }

                string predInfo = File.ReadAllText(System.AppDomain.CurrentDomain.BaseDirectory + "predInfo.txt");
                predInfo = predInfo.Replace("<model_ograje>", item);
                predInfo = predInfo.Replace("<dim>", (dim * 1000).ToString());
                mainBody.Add(item, predInfo + "\n\n");
            }


            foreach (var item in _itemsOnSecondTab)
            {
                dolzina += item._dolzinaProfilov;
                cena_z += item._cenaSkupajZMontazo;
                cena_brez += item._cenaSkupajBrezMontaze;
                stebri += item._stStebrov + " kos  x  V = " + (item._visina * 100).ToString("0.00") + "cm" + "  +  ";
                visina += item._stStebrov * item._visina;
                
                info = (item._razmakMedProfili < 0) ? File.ReadAllText(System.AppDomain.CurrentDomain.BaseDirectory + "infoProfiliSePrekrivajo.txt") :
                File.ReadAllText(System.AppDomain.CurrentDomain.BaseDirectory + "infoProfiliSeNePrekrivajo.txt");
                if (mainBody.ContainsKey(item._profilName))
                {
                    try
                    {
                        info = info.Replace("<st_prof>", item._kolikoProfilovVVisino.ToString());
                        info = info.Replace("<posamezna_dolzina>", item._dolzinaProfilov.ToString("0.00"));
                        info = info.Replace("<visina>", (item._visina * 100).ToString("0.00"));
                        info = info.Replace("<razmak>", item._razmakMedProfili.ToString("0.00"));
                    }
                    catch (Exception e) { }
                    //allInfoText.Add(info);
                    mainBody[item._profilName] += info + "\n\n";

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
            if (model_ograj.Contains("SD-8006P")) {
                if(model_ograj.Contains("SD-8006A") || model_ograj.Contains("SD-8006B") || model_ograj.Contains("SD-7006"))
                {
                    ref_slik = temp[0] + "  ter  " + temp[1];
                }
                else
                {
                    ref_slik = temp[0];
                }             
            }
            else
            {
                ref_slik = temp[1];
            }

            //open text file and read it
            str = (model_ograj.Contains("SD-8006P")) ? File.ReadAllText(System.AppDomain.CurrentDomain.BaseDirectory + "SD-8006PinfoTemplate.txt") :
                File.ReadAllText(System.AppDomain.CurrentDomain.BaseDirectory + "OtherProfilesinfoTemplate.txt");

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
                kolPopustString = "Glede na količino  ( L = " + dolzina + "m ), vam na omenjeni znesek nudimo dodatno še " + kol_popust + "% popusta  ( količisnki popust ).";
            }
            

            Dictionary<string, string> dict = new Dictionary<string, string> {  {"<opis>", str },
                                                                                {"<kol_popust>", kolPopustString },
                                                                                {"<info>",   string.Join("\n", mainBody.Values.ToArray())},
                                                                                {"<ime>", imeInPriimekTb.Text},
                                                                                {"<naslov>",  naslov},
                                                                                {"<st.ponudbe>", stPonudbeTb.Text },
                                                                                {"<datum>", date },
                                                                                {"<dolzina>",  dolzina.ToString(".00")},
                                                                                {"<visina>", (visina * 100).ToString(".00") },
                                                                                {"<cena_z>", cena_z.ToString(".00") },
                                                                                {"<cena_brez>", cena_brez.ToString(".00") },
                                                                                {"<popust_brez>", numericUpDown1.Text},
                                                                                {"<cena_brez_in_popust>",  cena_brez_in_popust.ToString(".00")},
                                                                                {"<stebri>", stebri},
                                                                                {"<ref_slik>", ref_slik },
                                                                                {"<mozni_modeli>", modeli_ograj } };

            foreach (var item in dict)
            {
                FindAndReplace(word, item.Key,item.Value, workDocument);
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
