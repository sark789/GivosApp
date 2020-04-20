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
        List<Item> _items = new List<Item>();
        List<Item> _itemsOnSecondTab = new List<Item>();
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
        private void izracunajBtn_Click(object sender, EventArgs e)
        {
            CalcLogic calc = new CalcLogic();
            _items = calc.Izracun(vodoravniProfili ,profilCb.SelectedItem.ToString(), float.Parse(dolzinaTb.Text), 
                float.Parse(visinaTb.Text),
                float.Parse(razmakSpodnjiTb.Text), 
                float.Parse(razmakZgornjiTb.Text),
                listBox1, (razmaki,cenaStebrovZMontazo, cenaStebrovBrezMontaze));
                dodajVKosaricoBtn.Enabled = true;
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            razmakStebriTb.Enabled = false;
            groupBox2.Visible = false;
            radioButton1.Visible = false;
            radioButton2.Visible = false;
            radioButton3.Visible = false;
            radioButton4.Visible = false;

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

                listBox2.Items.Add(item._itemNapis);
                _itemsOnSecondTab.Add(item);
            }
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

            listBox2.Items.RemoveAt(selectedItem);
            _itemsOnSecondTab.RemoveAt(selectedItem);

            zbrisiVnosBtn.Enabled = false;
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
                listBox2.Items.Clear();
                _itemsOnSecondTab.Clear();
            }
                
        }
    }
}
