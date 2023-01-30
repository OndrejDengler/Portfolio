namespace Pokladna
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.numKusy = new System.Windows.Forms.NumericUpDown();
            this.listView1 = new System.Windows.Forms.ListView();
            this.labelKosik = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.labelKusy = new System.Windows.Forms.Label();
            this.labelCelkCena = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.buttonRestock = new System.Windows.Forms.Button();
            this.listView2 = new System.Windows.Forms.ListView();
            this.labelSklad = new System.Windows.Forms.Label();
            this.labelInstrukce = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numKusy)).BeginInit();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(35, 69);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(169, 20);
            this.textBox1.TabIndex = 0;
            this.textBox1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TextBox1_KeyDown);
            // 
            // numKusy
            // 
            this.numKusy.Location = new System.Drawing.Point(251, 69);
            this.numKusy.Maximum = new decimal(new int[] {
            2147483647,
            0,
            0,
            0});
            this.numKusy.Name = "numKusy";
            this.numKusy.Size = new System.Drawing.Size(120, 20);
            this.numKusy.TabIndex = 3;
            this.numKusy.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.numKusy.KeyDown += new System.Windows.Forms.KeyEventHandler(this.numKusy_KeyDown);
            // 
            // listView1
            // 
            this.listView1.HideSelection = false;
            this.listView1.Location = new System.Drawing.Point(35, 189);
            this.listView1.Name = "listView1";
            this.listView1.Size = new System.Drawing.Size(293, 207);
            this.listView1.TabIndex = 4;
            this.listView1.UseCompatibleStateImageBehavior = false;
            // 
            // labelKosik
            // 
            this.labelKosik.AutoSize = true;
            this.labelKosik.Location = new System.Drawing.Point(32, 163);
            this.labelKosik.Name = "labelKosik";
            this.labelKosik.Size = new System.Drawing.Size(35, 13);
            this.labelKosik.TabIndex = 5;
            this.labelKosik.Text = "Košík";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label2.Location = new System.Drawing.Point(32, 21);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(0, 25);
            this.label2.TabIndex = 6;
            // 
            // labelKusy
            // 
            this.labelKusy.AutoSize = true;
            this.labelKusy.Location = new System.Drawing.Point(248, 53);
            this.labelKusy.Name = "labelKusy";
            this.labelKusy.Size = new System.Drawing.Size(64, 13);
            this.labelKusy.TabIndex = 7;
            this.labelKusy.Text = "Počet kusů:";
            // 
            // labelCelkCena
            // 
            this.labelCelkCena.AutoSize = true;
            this.labelCelkCena.Location = new System.Drawing.Point(37, 403);
            this.labelCelkCena.Name = "labelCelkCena";
            this.labelCelkCena.Size = new System.Drawing.Size(101, 13);
            this.labelCelkCena.TabIndex = 8;
            this.labelCelkCena.Text = "Celková cena: 0 Kč";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(35, 106);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(100, 20);
            this.textBox2.TabIndex = 9;
            // 
            // buttonRestock
            // 
            this.buttonRestock.Location = new System.Drawing.Point(681, 130);
            this.buttonRestock.Name = "buttonRestock";
            this.buttonRestock.Size = new System.Drawing.Size(75, 23);
            this.buttonRestock.TabIndex = 10;
            this.buttonRestock.Text = "Restock";
            this.buttonRestock.UseVisualStyleBackColor = true;
            this.buttonRestock.Click += new System.EventHandler(this.button1_Click);
            // 
            // listView2
            // 
            this.listView2.HideSelection = false;
            this.listView2.Location = new System.Drawing.Point(434, 189);
            this.listView2.Name = "listView2";
            this.listView2.Size = new System.Drawing.Size(326, 207);
            this.listView2.TabIndex = 11;
            this.listView2.UseCompatibleStateImageBehavior = false;
            // 
            // labelSklad
            // 
            this.labelSklad.AutoSize = true;
            this.labelSklad.Location = new System.Drawing.Point(431, 163);
            this.labelSklad.Name = "labelSklad";
            this.labelSklad.Size = new System.Drawing.Size(34, 13);
            this.labelSklad.TabIndex = 12;
            this.labelSklad.Text = "Sklad";
            // 
            // labelInstrukce
            // 
            this.labelInstrukce.AutoSize = true;
            this.labelInstrukce.Location = new System.Drawing.Point(458, 21);
            this.labelInstrukce.Name = "labelInstrukce";
            this.labelInstrukce.Size = new System.Drawing.Size(166, 13);
            this.labelInstrukce.TabIndex = 13;
            this.labelInstrukce.Text = "Pro zaplacení platby zmáčkněte *";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(458, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(118, 13);
            this.label1.TabIndex = 14;
            this.label1.Text = "Pro storno zmáčkněte /";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelInstrukce);
            this.Controls.Add(this.labelSklad);
            this.Controls.Add(this.listView2);
            this.Controls.Add(this.buttonRestock);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.labelCelkCena);
            this.Controls.Add(this.labelKusy);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.labelKosik);
            this.Controls.Add(this.listView1);
            this.Controls.Add(this.numKusy);
            this.Controls.Add(this.textBox1);
            this.KeyPreview = true;
            this.Name = "Form1";
            this.Text = "Form1";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.numKusy)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.NumericUpDown numKusy;
        private System.Windows.Forms.ListView listView1;
        private System.Windows.Forms.Label labelKosik;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelKusy;
        private System.Windows.Forms.Label labelCelkCena;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.Button buttonRestock;
        private System.Windows.Forms.ListView listView2;
        private System.Windows.Forms.Label labelSklad;
        private System.Windows.Forms.Label labelInstrukce;
        private System.Windows.Forms.Label label1;
    }
}

