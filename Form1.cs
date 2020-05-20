using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GivosCalc
{
    public partial class Form1 : Form
    {
        public ProfiliCollection vodoravniProfili;
        private static Form1 form = null;
        int razmaki;
        float cenaStebrovZMontazo;
        float cenaStebrovBrezMontaze;
        bool isDolgiNosilec;
        bool isKombinirana = false;
        float _skupnaBrez;
        float _skupnaZ;
        List<Item> _items = new List<Item>();
        List<Item> _itemsOnSecondTab = new List<Item>();
        List<string> _stringToWriteOnSecondTab = new List<string>();
        List<string> _finalStringOnSecondTab = new List<string>();
        public Form1()
        {
            InitializeComponent();
            form = this;
            //
            radioButton5.Checked = true;
            razmakStebriTb.Enabled = false;
            radioButton1.Visible = false;
            radioButton2.Visible = false;
            radioButton3.Visible = false;
            radioButton4.Visible = false;
            dodajVKosaricoBtn.Enabled = false;
            dodanoLb.Visible = false;
            zbrisiVnosBtn.Enabled = false;


            JArray jsonProfili = JArray.Parse(File.ReadAllText("profiliDb.json"));
            List<string> temp = new List<string>();
            ProfiliCollection profiliCol = new ProfiliCollection();
            string name;
            float capPrice, price, width;

            this.profilCb.DropDownStyle = ComboBoxStyle.DropDownList; //disabling writing in cb
            this.profilCb2.DropDownStyle = ComboBoxStyle.DropDownList;
            this.profilCb3.DropDownStyle = ComboBoxStyle.DropDownList;
            this.profilCb4.DropDownStyle = ComboBoxStyle.DropDownList;
            //this.dolzinaTb.

            foreach (JObject obj in jsonProfili.Children<JObject>())
            {
                foreach (JProperty singleProp in obj.Properties())
                {
                    string value = singleProp.Value.ToString();
                    temp.Add(value);
                }
                Profil profil = new Profil(temp[0], float.Parse(temp[1]), float.Parse(temp[2]), float.Parse(temp[3]));
                temp.Clear();
                profiliCol._profili.Add(profil);
            }
            AddProfilsToComboBox(profilCb, profiliCol._profili);
            vodoravniProfili = profiliCol;
        }

        public void AddProfilsToComboBox(ComboBox box, List<Profil> arr) {
            foreach (var obj in arr)
            {
                var item = obj._name;
                box.Items.Add(item);
            }
            box.SelectedIndex = 0; //combobox selected value to first item in list
        }

        //this method handles the dolzina textbox, acceptc float numbers only
        private void dolzinaTb_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != ',') && (e.KeyChar != '-'))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == ',') && ((sender as TextBox).Text.IndexOf(',') > -1))
            {
                e.Handled = true;
            }

            if ((e.KeyChar == '-') && ((sender as TextBox).Text.IndexOf('-') > -1))
            {
                e.Handled = true;
            }

        }
        private void dolzinaTbNoMinus_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) &&
                (e.KeyChar != ','))
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if ((e.KeyChar == ',') && ((sender as TextBox).Text.IndexOf(',') > -1))
            {
                e.Handled = true;
            }
        }


        private void izracunajBtn_Click(object sender, EventArgs e)
        {

            CalcLogic calc = new CalcLogic();
            var acc = calc.racunStebrov(radioButton1, radioButton2, radioButton3, radioButton4);
            razmaki = acc.Item1;
            cenaStebrovZMontazo = acc.Item2;
            cenaStebrovBrezMontaze = acc.Item3;
            (List<Item>, List<string>) result = (_items, _stringToWriteOnSecondTab);
            result = calc.Izracun(vodoravniProfili ,profilCb.SelectedItem.ToString(), float.Parse(dolzinaTb.Text), 
                float.Parse(visinaTb.Text),
                float.Parse(razmakSpodnjiTb.Text), 
                float.Parse(razmakZgornjiTb.Text),
                listBox1, (razmaki,cenaStebrovZMontazo, cenaStebrovBrezMontaze), isKombinirana);
                dodajVKosaricoBtn.Enabled = true;
            _items = result.Item1;
            _stringToWriteOnSecondTab = result.Item2;           
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            razmakStebriTb.Enabled = false;
            groupBox2.Visible = false;
            radioButton1.Visible = false;
            radioButton2.Visible = false;
            radioButton3.Visible = false;
            radioButton4.Visible = false;
            radioButton1.Checked = false;
            radioButton2.Checked = false;
            radioButton3.Checked = false;
            radioButton4.Checked = false;

            razmaki = 0;
            cenaStebrovZMontazo = 0;
            cenaStebrovBrezMontaze = 0;
        }

        private void radioButton6_CheckedChanged(object sender, EventArgs e)
        {
            razmakStebriTb.Enabled = true;
            groupBox2.Visible = true;
            radioButton1.Visible = true;
            radioButton1.Enabled = true;
            radioButton2.Visible = true;
            radioButton3.Visible = true;
            radioButton3.Checked = true;
            radioButton4.Visible = true;

            CalcLogic logic = new CalcLogic();
            var acc = logic.racunStebrov(radioButton1, radioButton2, radioButton3, radioButton4);
            razmaki = acc.Item1;
            cenaStebrovZMontazo = acc.Item2;
            cenaStebrovBrezMontaze = acc.Item3;

        }

        private void razmakStebriTb_TextChanged(object sender, EventArgs e)
        {

            CalcLogic logic = new CalcLogic();
            var acc = logic.racunStebrov(radioButton1, radioButton2, radioButton3, radioButton4);
            razmaki = acc.Item1;
            cenaStebrovZMontazo = acc.Item2;
            cenaStebrovBrezMontaze = acc.Item3;

        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            CalcLogic logic = new CalcLogic();
            var acc = logic.racunStebrov(radioButton1, radioButton2, radioButton3, radioButton4);
            razmaki = acc.Item1;
            cenaStebrovZMontazo = acc.Item2;
            cenaStebrovBrezMontaze = acc.Item3;
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            CalcLogic logic = new CalcLogic();
            var acc = logic.racunStebrov(radioButton1, radioButton2, radioButton3, radioButton4);
            razmaki = acc.Item1;
            cenaStebrovZMontazo = acc.Item2;
            cenaStebrovBrezMontaze = acc.Item3;
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            CalcLogic logic = new CalcLogic();
            var acc = logic.racunStebrov(radioButton1, radioButton2, radioButton3, radioButton4);
            razmaki = acc.Item1;
            cenaStebrovZMontazo = acc.Item2;
            cenaStebrovBrezMontaze = acc.Item3;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            CalcLogic logic = new CalcLogic();
            var acc = logic.racunStebrov(radioButton1, radioButton2, radioButton3, radioButton4);
            razmaki = acc.Item1;
            cenaStebrovZMontazo = acc.Item2;
            cenaStebrovBrezMontaze = acc.Item3;
        }

        private void dolzinaTb_TextChanged(object sender, EventArgs e)
        {
            CalcLogic logic = new CalcLogic();
            var acc = logic.racunStebrov(radioButton1, radioButton2, radioButton3, radioButton4);
            razmaki = acc.Item1;
            cenaStebrovZMontazo = acc.Item2;
            cenaStebrovBrezMontaze = acc.Item3;
        }

        private void dodajVKosaricoBtn_Click(object sender, EventArgs e)
        {
            int selectedItem = listBox1.SelectedIndex;

            if (selectedItem == -1)
            {
                btnOnClickTekst(dodanoLb, "Napaka pri dodajanju v košarico!", Color.Red);
            }
            else
            {
                Item item = _items[selectedItem];
                btnOnClickTekst(dodanoLb, "Dodano v košarico!", Color.LightGreen);

                _finalStringOnSecondTab.Add(_items[selectedItem]._itemNapis);
                CalcLogic calc = new CalcLogic();
                var k = calc.FancierString(_finalStringOnSecondTab);

                listBox2.DataSource = null;
                listBox2.DataSource = k;
                _itemsOnSecondTab.Add(item);
            }
            SetFinalPrices();
            shraniExcelBtn.Enabled = true;
            shraniWordBtn.Enabled = true;
        }

        public void btnOnClickTekst(Label label, string text, Color color)
        {
            label.Visible = true;
            label.ForeColor = color;
            label.Text = text;
            var t = new Timer();
            t.Interval = 2000; 
            t.Tick += (s, g) =>
            {
                dodanoLb.Visible = false;
                t.Stop();
            };
            t.Start();
        }

        private void visinaTb_TextChanged(object sender, EventArgs e)
        {
            CalcLogic logic = new CalcLogic();
            var acc = logic.racunStebrov(radioButton1, radioButton2, radioButton3, radioButton4);
            razmaki = acc.Item1;
            cenaStebrovZMontazo = acc.Item2;
            cenaStebrovBrezMontaze = acc.Item3;
        }

        private void zbrisiVnosBtn_Click(object sender, EventArgs e)
        {
            int selectedItem = listBox2.SelectedIndex;
            Item item = _itemsOnSecondTab[selectedItem];

            listBox2.DataSource = null;
            //listBox2.Items.RemoveAt(selectedItem);
            _itemsOnSecondTab.RemoveAt(selectedItem);
            _finalStringOnSecondTab.RemoveAt(selectedItem);
            CalcLogic calc = new CalcLogic();
            listBox2.DataSource = calc.FancierString(_finalStringOnSecondTab);
            listBox2.Update();

            if(listBox2.Items.Count == 0 || selectedItem == -1)
            {
                zbrisiVnosBtn.Enabled = false;
            }
            if(listBox2.Items.Count == 0)
            {
                shraniExcelBtn.Enabled = false;
                shraniWordBtn.Enabled = false;
            }
            listBox2.SelectedIndex = selectedItem-1;
            SetFinalPrices();
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectedItem = listBox2.SelectedIndex;
            if (listBox2.Items.Count > 0 && selectedItem != -1)
            {
                zbrisiVnosBtn.Enabled = true;
            }
            else
            {
                zbrisiVnosBtn.Enabled = false;
            }
        }

        private void zbrisiVseVnoseBtn_Click(object sender, EventArgs e)
        {
            CustomMessageForm messageBox = new CustomMessageForm();
            messageBox.ShowDialog();
            if(messageBox.isBtnYesClickedMethod() == true)
            {
                zbrisiVnosBtn.Enabled = false;
                listBox2.DataSource = null;
                //listBox2.Items.Clear();
                _itemsOnSecondTab.Clear();
                _finalStringOnSecondTab.Clear();
                listBox2.DataSource = _finalStringOnSecondTab;
                listBox2.Update();
                shraniExcelBtn.Enabled = false;
                shraniWordBtn.Enabled = false;
            }
            SetFinalPrices();
        }

        private void SetFinalPrices()
        {

            Dictionary<dynamic, Label> dict = new Dictionary<dynamic, Label> { { cenaLetvicLb, label11}, { CenaLetvic2Lb, label27 }, { vijakiBrezLb, label13 }, { VijakiZLb, label25 },
                                                                           { stebriBrezLb, label18}, { StebriZLb, label28 }, { PrevozZLb, label16 }, { SkupnaBrezLb, label20 }, { SkupnaZLb, label14 },
                                                                           { razrezLb, label24 }, { montazaLb, label26}, { rocajBrezLb, label35},{ rocajLb, label34} };

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

            Cene cene1 = new Cene(temp[6], temp[7], temp[9]);
            Cene cene2 = new Cene(temp[10], temp[11], temp[12],temp[13]);
            float cenaMontaze = cene1._montaza;
            float cenaRazreza = cene1._razrez;

            float cenaLetvic = 0;
            float cenaVijakov = 0;
            float stebriBrez = 0;
            float StebriZ = 0;
            float prevoz = 0;
            float skupnaBrez = 0;
            float skupnaZ = 0;
            float montaza = 0;
            float razrez = 0;
            float rocaj = 0;
            int i = 0;
            TabControl tab = Application.OpenForms["Form1"].Controls["tabControl1"] as TabControl;           
            string naVrh = tab.TabPages[0].Controls["groupBox3"].Controls["naVrhRb"].Text;
            string naBok = tab.TabPages[0].Controls["groupBox3"].Controls["naBokRb"].Text;

            foreach (var item in dict)
            {
                item.Key.Text = "0 €";
                item.Key.ForeColor = SystemColors.ControlDark;
                item.Value.ForeColor = SystemColors.ControlDark;
            }

                foreach(var item in _itemsOnSecondTab)
                {
                    cenaLetvic += item._cenaLetvic;
                    cenaVijakov += item._cenaVijakov;
                    stebriBrez += item._cenaStebrovBrezMontaze;
                    StebriZ += item._cenaStebrovZMontazo;
                    prevoz += item._cenaPrevoza;
                    skupnaBrez += item._cenaSkupajBrezMontaze;
                    skupnaZ += item._cenaSkupajZMontazo;
                    montaza += item._dolzinaProfilov * cenaMontaze;
                    razrez += item._kolikoProfilovVVisino * item._stStebrov * cenaRazreza;
                    if(item._vrstaOgraje == naVrh)
                    {
                        rocaj = item._stRocajPokrovov * cene2._pokrovZaRocaj + item._dolzinaProfilov * cene2._rocaj;
                    }
                    if (item._vrstaOgraje == naBok)
                    {
                        rocaj = item._stRocajPokrovov * cene2._pokrovZaRocaj + item._dolzinaProfilov * cene2._rocaj + 4*item._visina*cene2._lprofil;
                    }

            }
                List<float> cene = new List<float> { cenaLetvic, cenaLetvic, cenaVijakov / 2, cenaVijakov, stebriBrez, StebriZ,
                                                     prevoz, skupnaBrez, skupnaZ, razrez, montaza, rocaj, rocaj,};

                foreach(var item in dict)
                {
                    if(cene[i] != 0)
                    {
                        item.Key.Text = Math.Round(cene[i], 2).ToString() + " €";
                        item.Key.ForeColor = SystemColors.ControlText;
                        item.Value.ForeColor = SystemColors.ControlText;
                    }
                    i++;
                }

            if(listBox2.Items.Count > 0 && SkupnaZLb.Text != "0 €")
            {
                numericUpDown1.ForeColor = SystemColors.ControlText;
                numericUpDown1.Enabled = true;
                label29.ForeColor = SystemColors.ControlText;
                SkupnaBrezLb.Text = Math.Round(((100 - (float)numericUpDown1.Value) * 0.01f) * skupnaBrez, 2).ToString() + " €";
                numericUpDown2.ForeColor = SystemColors.ControlText;
                numericUpDown2.Enabled = true;
                label30.ForeColor = SystemColors.ControlText;
                SkupnaZLb.Text = Math.Round(((100 - (float)numericUpDown2.Value) * 0.01f) * skupnaZ, 2).ToString() + " €";
            }
            else
            {
                numericUpDown1.ForeColor = SystemColors.ControlDark;
                numericUpDown1.Enabled = false;
                numericUpDown1.Value = 0;
                label29.ForeColor = SystemColors.ControlDark;
                SkupnaBrezLb.Text = Math.Round(skupnaBrez, 2).ToString() + " €";
                numericUpDown2.ForeColor = SystemColors.ControlDark;
                numericUpDown2.Enabled = false;
                numericUpDown2.Value = 0;
                label30.ForeColor = SystemColors.ControlDark;
                SkupnaZLb.Text = Math.Round(skupnaZ, 2).ToString() + " €";
            }

            _skupnaBrez = skupnaBrez;
            _skupnaZ = skupnaZ;
            
        }

        private void shraniBtn_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Excel file |*.xlsx";
            saveFileDialog1.Title = "Shrani Excel datoteko";

                string path;
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                        path = Path.GetFullPath(saveFileDialog1.FileName);
                        ExcelHandler handler = new ExcelHandler();
                        handler.SaveAndOpenExcel("TemplateVodoravne.xlsx", path, _itemsOnSecondTab);
                }                              
            }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {           
            if (listBox2.Items.Count > 0)
            {
                numericUpDown1.ForeColor = SystemColors.ControlText;
                numericUpDown1.Enabled = true;
                label29.ForeColor = SystemColors.ControlText;
                SkupnaBrezLb.Text = Math.Round(((100 - (float)numericUpDown1.Value) * 0.01f) * _skupnaBrez, 2).ToString() + " €";
            }
            else
            {
                numericUpDown1.ForeColor = SystemColors.ControlDark;
                numericUpDown1.Enabled = false;
                numericUpDown1.Value = 0;
                label29.ForeColor = SystemColors.ControlDark;
                SkupnaBrezLb.Text = Math.Round(_skupnaBrez, 2).ToString() + " €";
            }
        }

        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            if (listBox2.Items.Count > 0)
            {
                numericUpDown2.ForeColor = SystemColors.ControlText;
                numericUpDown2.Enabled = true;
                label30.ForeColor = SystemColors.ControlText;
                SkupnaZLb.Text = Math.Round(((100 - (float)numericUpDown2.Value) * 0.01f) * _skupnaZ, 2).ToString() + " €";
            }
            else
            {
                numericUpDown2.ForeColor = SystemColors.ControlDark;
                numericUpDown2.Enabled = false;
                numericUpDown2.Value = 0;
                label30.ForeColor = SystemColors.ControlDark;
                SkupnaZLb.Text = Math.Round(_skupnaZ, 2).ToString() + " €";
            }
        }

        private void shraniWordBtn_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Word file |*.doc";
            saveFileDialog1.Title = "Shrani Word datoteko";

            string path;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                path = Path.GetFullPath(saveFileDialog1.FileName);
                WordHandler handler = new WordHandler();
                handler.SaveAndOpenWord("wordTemplate.doc", path, _itemsOnSecondTab, vodoravniProfili);
            }
        }

        private void vrtnaRb_CheckedChanged(object sender, EventArgs e)
        {
            if(naBokRb.Checked || naVrhRb.Checked)
            {
                pokrovLb.Visible = false;
                pokrovUpDown.Value = 0;
                pokrovUpDown.Enabled = false;
                pokrovUpDown.Visible = false;
            }
        }

        private void naVrhRb_CheckedChanged(object sender, EventArgs e)
        {
            if (naBokRb.Checked || naVrhRb.Checked)
            {
                pokrovLb.Visible = true;
                pokrovUpDown.Enabled = true;
                pokrovUpDown.Visible = true;
            }
            else
            {
                pokrovLb.Visible = false;
                pokrovUpDown.Value = 0;
                pokrovUpDown.Enabled = false;
                pokrovUpDown.Visible = false;
            }
        }

        private void naBokRb_CheckedChanged(object sender, EventArgs e)
        {
            if (naBokRb.Checked || naVrhRb.Checked)
            {
                pokrovLb.Visible = true;
                pokrovUpDown.Enabled = true;
                pokrovUpDown.Visible = true;
            }
            else
            {
                pokrovLb.Visible = false;
                pokrovUpDown.Value = 0;
                pokrovUpDown.Enabled = false;
                pokrovUpDown.Visible = false;
            }
        }

        private void enostavnaRb_CheckedChanged(object sender, EventArgs e)
        {
            profilCb2.Visible = false;
            label36.Visible = false;
            profilCb3.Visible = false;
            label37.Visible = false;
            profilCb4.Visible = false;
            label38.Visible = false;

            label39.Visible = false;
            label40.Visible = false;
            label41.Visible = false;
            label42.Visible = false;

            numericUpDown3.Value = 0;
            numericUpDown3.Enabled = false;
            numericUpDown3.Visible = false;
            numericUpDown4.Value = 0;
            numericUpDown4.Enabled = false;
            numericUpDown4.Visible = false;
            numericUpDown5.Value = 0;
            numericUpDown5.Enabled = false;
            numericUpDown5.Visible = false;
            numericUpDown6.Value = 0;
            numericUpDown6.Enabled = false;
            numericUpDown6.Visible = false;

            profilCb.Items.Clear();
            AddProfilsToComboBox(profilCb, vodoravniProfili._profili);

            isKombinirana = false;
        }

        private void kombiniranaRb_CheckedChanged(object sender, EventArgs e)
        {
            profilCb.Visible = true;
            label1.Visible = true;
            profilCb.Items.Clear();
            profilCb.Items.Add(vodoravniProfili._profili.ElementAt(0)._name);
            profilCb.SelectedIndex = 0;

            profilCb2.Visible = true;
            label36.Visible = true;
            profilCb2.Items.Clear();
            profilCb2.Items.Add(vodoravniProfili._profili.ElementAt(1)._name);
            profilCb2.SelectedIndex = 0;

            profilCb3.Visible = true;
            label37.Visible = true;
            profilCb3.Items.Clear();
            profilCb3.Items.Add(vodoravniProfili._profili.ElementAt(2)._name);
            profilCb3.SelectedIndex = 0;

            profilCb4.Visible = true;
            label38.Visible = true;
            profilCb4.Items.Clear();
            profilCb4.Items.Add(vodoravniProfili._profili.ElementAt(3)._name);
            profilCb4.SelectedIndex = 0;

            //
            label39.Visible = true;
            label40.Visible = true;
            label41.Visible = true;
            label42.Visible = true;

            numericUpDown3.Enabled = true;
            numericUpDown3.Visible = true;
            numericUpDown4.Enabled = true;
            numericUpDown4.Visible = true;
            numericUpDown5.Enabled = true;
            numericUpDown5.Visible = true;
            numericUpDown6.Enabled = true;
            numericUpDown6.Visible = true;

            isKombinirana = true;
        }
    }
}
