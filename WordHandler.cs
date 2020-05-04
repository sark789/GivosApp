using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;

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
            WriteValuesInWord(_itemsOnSecondTab, word, vodoravniProfili);
            workDocument.Save();
        }

        public void WriteValuesInWord(List<Item> _itemsOnSecondTab, Microsoft.Office.Interop.Word.Application word, ProfiliCollection vodoravniProfili) 
        {
            TabControl tab = System.Windows.Forms.Application.OpenForms["Form1"].Controls["tabControl1"] as TabControl;
            var imeInPriimekTb = tab.SelectedTab.Controls["imeInPriimekTb"];
            var naslovTb = tab.SelectedTab.Controls["naslovTb"];
            var stPonudbeTb = tab.SelectedTab.Controls["stPonudbeTb"];
            var numericUpDown1 = tab.SelectedTab.Controls["numericUpDown1"];

            DateTime thisDay = DateTime.Today;
            string date = thisDay.ToString("d");
            float dolzina = 0;
            float cena_z = 0;
            float dim = 0;
            float cena_brez = 0;

            foreach(var item in vodoravniProfili._profili)
            {
                if(_itemsOnSecondTab[0]._profilName == item._name)
                {
                    dim = item._width;
                }
            }

            foreach(var item in _itemsOnSecondTab)
            {
                dolzina += item._dolzinaProfilov;
                cena_z += item._cenaSkupajZMontazo;
                cena_brez += item._cenaSkupajBrezMontaze;
            }

            float cena_brez_in_popust = cena_brez * ((100 - int.Parse(numericUpDown1.Text)) * 0.01f);

            Dictionary<string, string> dict = new Dictionary<string, string> {  {"<ime>", imeInPriimekTb.Text},
                                                                                {"<naslov>",  naslovTb.Text},
                                                                                {"<st.ponudbe>", stPonudbeTb.Text },
                                                                                {"<datum>", date },
                                                                                {"<model_ograje>", _itemsOnSecondTab[0]._profilName},
                                                                                {"<st_prof>", _itemsOnSecondTab[0]._kolikoProfilovVVisino.ToString() },
                                                                                {"<dolzina>",  dolzina.ToString(".00")},
                                                                                {"<visina>", (_itemsOnSecondTab[0]._visina * 100).ToString(".00") },
                                                                                {"<cena_z>", cena_z.ToString(".00") },
                                                                                {"<dim>", (dim * 1000).ToString() },
                                                                                {"<cena_brez>", cena_brez.ToString(".00") },
                                                                                {"<popust_brez>", numericUpDown1.Text},
                                                                                {"<cena_brez_in_popust>",  cena_brez_in_popust.ToString(".00")} };

            foreach (var item in dict)
            {
                FindAndReplace(word, item.Key,item.Value);
            }

        }
        private void FindAndReplace(Microsoft.Office.Interop.Word.Application doc, object findText, object replaceWithText)
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
            doc.Selection.Find.Execute(ref findText, ref matchCase, ref matchWholeWord,
                ref matchWildCards, ref matchSoundsLike, ref matchAllWordForms, ref forward, ref wrap, ref format, ref replaceWithText, ref replace,
                ref matchKashida, ref matchDiacritics, ref matchAlefHamza, ref matchControl);
        }
    }
}
