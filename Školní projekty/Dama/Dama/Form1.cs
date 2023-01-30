using System;
using System.Drawing;
using System.Windows.Forms;

namespace Dama
{
    public partial class Form1 : Form
    {
        Color barva1 = Color.White;
        Color barva2 = Color.Black;
        HraciDeska hraciDeska;
        public Form1()
        {
            InitializeComponent();
            button1.BackColor = barva1;
            button2.BackColor = barva2;

            hraciDeska = new HraciDeska((int)numericUpDown1.Value, barva1, barva2, pictureBox1);
        }

        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            hraciDeska.Vykresli(e.Graphics, labelTah);
        }

        //Ovládání barvy 1
        private void button1_Click(object sender, EventArgs e)
        {
            var vysledek = colorDialog1.ShowDialog();
            if (vysledek == DialogResult.OK)
            {
                barva1 = colorDialog1.Color;
            }
            button1.BackColor = barva1;
            hraciDeska.barva1 = barva1;
            pictureBox1.Invalidate();
        }

        //Ovládání barvy 2
        private void button2_Click(object sender, EventArgs e)
        {
            var vysledek = colorDialog2.ShowDialog();
            if (vysledek == DialogResult.OK)
            {
                barva2 = colorDialog2.Color;
            }
            button2.BackColor = barva2;
            hraciDeska.barva2 = barva2;
            pictureBox1.Invalidate();
        }

        //Ošetření změny okna
        private void pictureBox1_Resize(object sender, EventArgs e)
        {
            pictureBox1.Invalidate();
        }

        //Ovládání velikosti
        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {
            hraciDeska = new HraciDeska((int)numericUpDown1.Value, barva1, barva2, pictureBox1);
            pictureBox1.Invalidate();
        }

        //Zjištění lokace kliku
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            var me = e as MouseEventArgs; //me = mouseEvent
            Point klikPozice = me.Location;
            hraciDeska.Pohyb(klikPozice);
            pictureBox1.Invalidate();
        }

        //Tlačítko pro Reset Hry
        private void buttonResetHry_Click(object sender, EventArgs e)
        {
            hraciDeska = new HraciDeska((int)numericUpDown1.Value, barva1, barva2, pictureBox1);
            pictureBox1.Invalidate();
        }

        //Tlačítko Reset Nastavení
        private void buttonResetNastaveni_Click(object sender, EventArgs e)
        {
            //Všude nastavíme výchozí hodnoty
            numericUpDown1.Value = 8; 
            barva1 = Color.White;
            button1.BackColor = barva1;
            barva2 = Color.Black;
            button2.BackColor = barva2;
            hraciDeska.velikostDesky = 8;
            hraciDeska.barva1 = barva1;
            hraciDeska.barva2 = barva2;
            pictureBox1.Invalidate();
        }
    }
}
