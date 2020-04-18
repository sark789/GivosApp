namespace GivosCalc
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.dodajVKosaricoBtn = new System.Windows.Forms.Button();
            this.razmakSpodnjiTb = new System.Windows.Forms.TextBox();
            this.profilCb = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dolzinaTb = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.izracunajBtn = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.visinaTb = new System.Windows.Forms.TextBox();
            this.razmakZgornjiTb = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.razmakStebriTb = new System.Windows.Forms.TextBox();
            this.radioButton6 = new System.Windows.Forms.RadioButton();
            this.radioButton5 = new System.Windows.Forms.RadioButton();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton3 = new System.Windows.Forms.RadioButton();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.cenaPrevozaTb = new System.Windows.Forms.TextBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.radioButton4 = new System.Windows.Forms.RadioButton();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 20;
            this.listBox1.Location = new System.Drawing.Point(23, 336);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(1452, 224);
            this.listBox1.TabIndex = 14;
            // 
            // dodajVKosaricoBtn
            // 
            this.dodajVKosaricoBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.25F);
            this.dodajVKosaricoBtn.Location = new System.Drawing.Point(684, 565);
            this.dodajVKosaricoBtn.Name = "dodajVKosaricoBtn";
            this.dodajVKosaricoBtn.Size = new System.Drawing.Size(186, 39);
            this.dodajVKosaricoBtn.TabIndex = 28;
            this.dodajVKosaricoBtn.Text = "Dodaj v košarico";
            this.dodajVKosaricoBtn.UseVisualStyleBackColor = true;
            // 
            // razmakSpodnjiTb
            // 
            this.razmakSpodnjiTb.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.25F);
            this.razmakSpodnjiTb.Location = new System.Drawing.Point(264, 73);
            this.razmakSpodnjiTb.Name = "razmakSpodnjiTb";
            this.razmakSpodnjiTb.Size = new System.Drawing.Size(64, 32);
            this.razmakSpodnjiTb.TabIndex = 9;
            this.razmakSpodnjiTb.Text = "0";
            this.razmakSpodnjiTb.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dolzinaTb_KeyPress);
            // 
            // profilCb
            // 
            this.profilCb.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.25F);
            this.profilCb.FormattingEnabled = true;
            this.profilCb.Location = new System.Drawing.Point(95, 20);
            this.profilCb.Name = "profilCb";
            this.profilCb.Size = new System.Drawing.Size(142, 34);
            this.profilCb.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.25F);
            this.label2.Location = new System.Drawing.Point(381, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(128, 26);
            this.label2.TabIndex = 3;
            this.label2.Text = "Dolžina [m]:";
            // 
            // dolzinaTb
            // 
            this.dolzinaTb.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.25F);
            this.dolzinaTb.Location = new System.Drawing.Point(515, 20);
            this.dolzinaTb.Name = "dolzinaTb";
            this.dolzinaTb.Size = new System.Drawing.Size(100, 32);
            this.dolzinaTb.TabIndex = 4;
            this.dolzinaTb.Text = "0";
            this.dolzinaTb.TextChanged += new System.EventHandler(this.dolzinaTb_TextChanged);
            this.dolzinaTb.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dolzinaTb_KeyPress);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.25F);
            this.label5.Location = new System.Drawing.Point(155, 76);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(55, 26);
            this.label5.TabIndex = 8;
            this.label5.Text = "med";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.25F);
            this.label1.Location = new System.Drawing.Point(21, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 26);
            this.label1.TabIndex = 1;
            this.label1.Text = "Profil:";
            // 
            // izracunajBtn
            // 
            this.izracunajBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.25F);
            this.izracunajBtn.Location = new System.Drawing.Point(684, 290);
            this.izracunajBtn.Name = "izracunajBtn";
            this.izracunajBtn.Size = new System.Drawing.Size(186, 39);
            this.izracunajBtn.TabIndex = 2;
            this.izracunajBtn.Text = "Izračunaj predel";
            this.izracunajBtn.UseVisualStyleBackColor = true;
            this.izracunajBtn.Click += new System.EventHandler(this.izracunajBtn_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.25F);
            this.label3.Location = new System.Drawing.Point(738, 28);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(115, 26);
            this.label3.TabIndex = 5;
            this.label3.Text = "Višina [m]:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.25F);
            this.label4.Location = new System.Drawing.Point(21, 76);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 26);
            this.label4.TabIndex = 7;
            this.label4.Text = "Razmak:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.25F);
            this.label6.Location = new System.Drawing.Point(382, 76);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 26);
            this.label6.TabIndex = 10;
            this.label6.Text = "in";
            // 
            // visinaTb
            // 
            this.visinaTb.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.25F);
            this.visinaTb.Location = new System.Drawing.Point(856, 25);
            this.visinaTb.Name = "visinaTb";
            this.visinaTb.Size = new System.Drawing.Size(100, 32);
            this.visinaTb.TabIndex = 6;
            this.visinaTb.Text = "0";
            this.visinaTb.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dolzinaTb_KeyPress);
            // 
            // razmakZgornjiTb
            // 
            this.razmakZgornjiTb.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.25F);
            this.razmakZgornjiTb.Location = new System.Drawing.Point(434, 73);
            this.razmakZgornjiTb.Name = "razmakZgornjiTb";
            this.razmakZgornjiTb.Size = new System.Drawing.Size(64, 32);
            this.razmakZgornjiTb.TabIndex = 11;
            this.razmakZgornjiTb.Text = "0";
            this.razmakZgornjiTb.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dolzinaTb_KeyPress);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.25F);
            this.label10.Location = new System.Drawing.Point(18, 231);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(202, 26);
            this.label10.TabIndex = 25;
            this.label10.Text = "Cena prevoza [eur]:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.25F);
            this.label7.Location = new System.Drawing.Point(334, 76);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(42, 26);
            this.label7.TabIndex = 12;
            this.label7.Text = "cm";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.25F);
            this.label8.Location = new System.Drawing.Point(504, 76);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(42, 26);
            this.label8.TabIndex = 13;
            this.label8.Text = "cm";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.25F);
            this.label9.Location = new System.Drawing.Point(21, 161);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(206, 26);
            this.label9.TabIndex = 15;
            this.label9.Text = "Razmak stebri [cm]:";
            // 
            // razmakStebriTb
            // 
            this.razmakStebriTb.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.25F);
            this.razmakStebriTb.Location = new System.Drawing.Point(264, 161);
            this.razmakStebriTb.Name = "razmakStebriTb";
            this.razmakStebriTb.Size = new System.Drawing.Size(100, 32);
            this.razmakStebriTb.TabIndex = 16;
            this.razmakStebriTb.Text = "145";
            this.razmakStebriTb.TextChanged += new System.EventHandler(this.razmakStebriTb_TextChanged);
            this.razmakStebriTb.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dolzinaTb_KeyPress);
            // 
            // radioButton6
            // 
            this.radioButton6.AutoSize = true;
            this.radioButton6.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.radioButton6.Location = new System.Drawing.Point(139, 7);
            this.radioButton6.Name = "radioButton6";
            this.radioButton6.Size = new System.Drawing.Size(85, 24);
            this.radioButton6.TabIndex = 22;
            this.radioButton6.TabStop = true;
            this.radioButton6.Text = "S stebri";
            this.radioButton6.UseVisualStyleBackColor = true;
            this.radioButton6.CheckedChanged += new System.EventHandler(this.radioButton6_CheckedChanged);
            // 
            // radioButton5
            // 
            this.radioButton5.AutoSize = true;
            this.radioButton5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.radioButton5.Location = new System.Drawing.Point(6, 7);
            this.radioButton5.Name = "radioButton5";
            this.radioButton5.Size = new System.Drawing.Size(123, 24);
            this.radioButton5.TabIndex = 21;
            this.radioButton5.TabStop = true;
            this.radioButton5.Text = "Brez stebrov";
            this.radioButton5.UseVisualStyleBackColor = true;
            this.radioButton5.CheckedChanged += new System.EventHandler(this.radioButton5_CheckedChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.radioButton5);
            this.groupBox1.Controls.Add(this.radioButton6);
            this.groupBox1.Location = new System.Drawing.Point(26, 111);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(247, 37);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            // 
            // radioButton2
            // 
            this.radioButton2.AutoSize = true;
            this.radioButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.radioButton2.Location = new System.Drawing.Point(64, 73);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(122, 24);
            this.radioButton2.TabIndex = 18;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "radioButton2";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton2_CheckedChanged);
            // 
            // radioButton1
            // 
            this.radioButton1.AutoSize = true;
            this.radioButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.radioButton1.Location = new System.Drawing.Point(64, 103);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(122, 24);
            this.radioButton1.TabIndex = 17;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "radioButton1";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton1_CheckedChanged);
            // 
            // radioButton3
            // 
            this.radioButton3.AutoSize = true;
            this.radioButton3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.radioButton3.Location = new System.Drawing.Point(64, 43);
            this.radioButton3.Name = "radioButton3";
            this.radioButton3.Size = new System.Drawing.Size(122, 24);
            this.radioButton3.TabIndex = 19;
            this.radioButton3.TabStop = true;
            this.radioButton3.Text = "radioButton3";
            this.radioButton3.UseVisualStyleBackColor = true;
            this.radioButton3.CheckedChanged += new System.EventHandler(this.radioButton3_CheckedChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.radioButton3);
            this.groupBox2.Controls.Add(this.radioButton1);
            this.groupBox2.Controls.Add(this.radioButton4);
            this.groupBox2.Controls.Add(this.radioButton2);
            this.groupBox2.Location = new System.Drawing.Point(370, 131);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(586, 140);
            this.groupBox2.TabIndex = 24;
            this.groupBox2.TabStop = false;
            // 
            // cenaPrevozaTb
            // 
            this.cenaPrevozaTb.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.25F);
            this.cenaPrevozaTb.Location = new System.Drawing.Point(261, 228);
            this.cenaPrevozaTb.Name = "cenaPrevozaTb";
            this.cenaPrevozaTb.Size = new System.Drawing.Size(100, 32);
            this.cenaPrevozaTb.TabIndex = 26;
            this.cenaPrevozaTb.Text = "0";
            this.cenaPrevozaTb.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.dolzinaTb_KeyPress);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::GivosCalc.Properties.Resources.givosPic;
            this.pictureBox1.Location = new System.Drawing.Point(1099, 76);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(315, 136);
            this.pictureBox1.TabIndex = 27;
            this.pictureBox1.TabStop = false;
            // 
            // radioButton4
            // 
            this.radioButton4.AutoSize = true;
            this.radioButton4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.radioButton4.Location = new System.Drawing.Point(64, 13);
            this.radioButton4.Name = "radioButton4";
            this.radioButton4.Size = new System.Drawing.Size(122, 24);
            this.radioButton4.TabIndex = 20;
            this.radioButton4.TabStop = true;
            this.radioButton4.Text = "radioButton4";
            this.radioButton4.UseVisualStyleBackColor = true;
            this.radioButton4.CheckedChanged += new System.EventHandler(this.radioButton4_CheckedChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12.25F);
            this.tabControl1.Location = new System.Drawing.Point(12, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(1501, 643);
            this.tabControl1.TabIndex = 21;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.listBox1);
            this.tabPage1.Controls.Add(this.dodajVKosaricoBtn);
            this.tabPage1.Controls.Add(this.profilCb);
            this.tabPage1.Controls.Add(this.pictureBox1);
            this.tabPage1.Controls.Add(this.label1);
            this.tabPage1.Controls.Add(this.cenaPrevozaTb);
            this.tabPage1.Controls.Add(this.izracunajBtn);
            this.tabPage1.Controls.Add(this.label10);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.groupBox2);
            this.tabPage1.Controls.Add(this.dolzinaTb);
            this.tabPage1.Controls.Add(this.groupBox1);
            this.tabPage1.Controls.Add(this.label3);
            this.tabPage1.Controls.Add(this.razmakStebriTb);
            this.tabPage1.Controls.Add(this.visinaTb);
            this.tabPage1.Controls.Add(this.label9);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.label8);
            this.tabPage1.Controls.Add(this.razmakSpodnjiTb);
            this.tabPage1.Controls.Add(this.label7);
            this.tabPage1.Controls.Add(this.label6);
            this.tabPage1.Controls.Add(this.razmakZgornjiTb);
            this.tabPage1.Location = new System.Drawing.Point(4, 29);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(1493, 610);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(192, 74);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1525, 667);
            this.Controls.Add(this.tabControl1);
            this.Name = "Form1";
            this.Text = "Form1";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.Button dodajVKosaricoBtn;
        private System.Windows.Forms.TextBox razmakSpodnjiTb;
        private System.Windows.Forms.ComboBox profilCb;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox dolzinaTb;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button izracunajBtn;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox visinaTb;
        private System.Windows.Forms.TextBox razmakZgornjiTb;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.TextBox razmakStebriTb;
        private System.Windows.Forms.RadioButton radioButton6;
        private System.Windows.Forms.RadioButton radioButton5;
        public System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RadioButton radioButton2;
        private System.Windows.Forms.RadioButton radioButton1;
        private System.Windows.Forms.RadioButton radioButton3;
        public System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.RadioButton radioButton4;
        private System.Windows.Forms.TextBox cenaPrevozaTb;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
    }
}

