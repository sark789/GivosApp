using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GivosCalc
{
    public partial class CustomMessageForm : Form
    {
        bool _isYesBtnClicked = false;
        public CustomMessageForm()
        {
            InitializeComponent();

        }
        public bool isBtnYesClickedMethod()
        {
            return _isYesBtnClicked;

        }
        private void button1_Click(object sender, EventArgs e)
        {
            _isYesBtnClicked = true;
            this.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
