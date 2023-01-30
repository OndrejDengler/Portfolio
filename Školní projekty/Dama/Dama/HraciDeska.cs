using System;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Drawing;

namespace Dama
{
    class HraciDeska
    {
        private int sirkaPolicka;
        public int velikostDesky;
        private int[,] poziceFigurek;
        public Color barva1;
        public Color barva2;
        private PictureBox pictureBox;
        private List<Point> moznostiPohybu;
        private Point poziceVybraneFigurky = new Point(-2,-2);
        private Point posledniPozice;
        bool figurkaVybrana = false;
        bool hrajeHrac1 = true;

        public HraciDeska(int velikostDesky, Color barva1, Color barva2, PictureBox pictureBox)
        {
            this.velikostDesky = velikostDesky;
            this.barva1 = barva1;
            this.barva2 = barva2;
            this.pictureBox = pictureBox;
            poziceFigurek = new int[velikostDesky, velikostDesky];
            moznostiPohybu = new List<Point>();
            for (int x = 0; x < velikostDesky; x++) //Vyplní pole základní formací figurek
            {
                for (int y = 0; y < velikostDesky; y++)
                {
                    if ((y == 0 && (x + y) % 2 != 0) || (y == 1 && (x + y) % 2 != 0))
                    {
                        poziceFigurek[x, y] = 1;
                    }

                    if ((y == velikostDesky - 1 && (x + y) % 2 != 0) || (y == velikostDesky - 2 && (x + y) % 2 != 0))
                    {
                        poziceFigurek[x, y] = 2;
                    }
                }
            }
        }
        /// <summary>
        /// Vykreslí desku s aktuální polohou figurek
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="label"></param>
        public void Vykresli(Graphics graphics, Label label)
        {
            int xPosun;
            int yPosun;
            sirkaPolicka = Math.Min(pictureBox.Width, pictureBox.Height) / velikostDesky;
            float sirkaZvyrazneni = sirkaPolicka * 0.1f;
            var stetka1 = new SolidBrush(barva1);
            var stetka2 = new SolidBrush(barva2);
            var barvaFigurka1 = Color.Gold;
            var barvaFigurka2 = Color.SaddleBrown;

            var stetkaFigurka1 = new SolidBrush(barvaFigurka1);
            var stetkaFigurka2 = new SolidBrush(barvaFigurka2);


            for (int x = 0; x < velikostDesky; x++) //Vykreslí desku
            {
                for (int y = 0; y < velikostDesky; y++)
                {
                    xPosun = x * sirkaPolicka;
                    yPosun = y * sirkaPolicka;
                    var stetka = (x + y) % 2 == 0 ? stetka1 : stetka2;
                    graphics.FillRectangle(stetka, new Rectangle(xPosun, yPosun, sirkaPolicka, sirkaPolicka));
                    
                    if(poziceFigurek[x, y] == 1)
                    {
                        graphics.FillEllipse(stetkaFigurka1, new Rectangle(xPosun, yPosun, sirkaPolicka, sirkaPolicka));
                    }

                    if (poziceFigurek[x, y] == 2)
                    {
                        graphics.FillEllipse(stetkaFigurka2, new Rectangle(xPosun, yPosun, sirkaPolicka, sirkaPolicka));
                    }
                }
            }

            Pen peroPohyb = new Pen(Color.Green, sirkaZvyrazneni); //Vykreslí možné pohyby
            foreach (var policko in moznostiPohybu)
            {
                xPosun = policko.X * sirkaPolicka;
                yPosun = policko.Y * sirkaPolicka;
                graphics.DrawRectangle(peroPohyb, new Rectangle(xPosun, yPosun, sirkaPolicka, sirkaPolicka));
            }

            if (figurkaVybrana) //Zvýrazní právě nakliknutou figurku
            {
                Pen peroPozice = new Pen(Color.Red, sirkaZvyrazneni);
                xPosun = poziceVybraneFigurky.X * sirkaPolicka;
                yPosun = poziceVybraneFigurky.Y * sirkaPolicka;
                graphics.DrawRectangle(peroPozice, new Rectangle(xPosun, yPosun, sirkaPolicka, sirkaPolicka));
            }

            if (hrajeHrac1)
                label.Text = "Hraje hráč 1";
            else
                label.Text = "Hraje hráč 2";
        }

        /// <summary>
        /// Vypočítá možné pohyby a změní aktuální polohu figurek
        /// </summary>
        /// <param name="klikPozice"></param>
        public void Pohyb(Point klikPozice)
        {
            int xKlik = klikPozice.X / sirkaPolicka;
            int yKlik = klikPozice.Y / sirkaPolicka;
            Point vybranePolicko = new Point(xKlik, yKlik);

            if (hrajeHrac1) //Zjistíme jaký hráč právě hraje
            {
                if (figurkaVybrana) //Dáme do pole takové hodnoty, aby odpovídali pohybu
                {
                    if (!JeMimoMoznosti(xKlik, yKlik) && poziceFigurek[vybranePolicko.X, vybranePolicko.Y] == 0 && moznostiPohybu.Contains(vybranePolicko))
                    {
                        poziceFigurek[posledniPozice.X, posledniPozice.Y] = 0;
                        poziceFigurek[xKlik, yKlik] = 1;
                        hrajeHrac1 = false;
                    }
                        moznostiPohybu.Clear();
                        figurkaVybrana = false;
                }
                else if (!JeMimoMoznosti(xKlik, yKlik) && poziceFigurek[vybranePolicko.X, vybranePolicko.Y] == 1) //Zjistíme jaké jsou možné pohyby
                {
                    moznostiPohybu.Clear();
                    poziceVybraneFigurky = vybranePolicko;
                    if (!JeMimoMoznosti(xKlik - 1, yKlik - 1) && poziceFigurek[vybranePolicko.X - 1, vybranePolicko.Y - 1] == 0)
                    {
                        moznostiPohybu.Add(new Point(vybranePolicko.X - 1, vybranePolicko.Y - 1));
                    }
                    if (!JeMimoMoznosti(xKlik - 1, yKlik + 1) && poziceFigurek[vybranePolicko.X - 1, vybranePolicko.Y + 1] == 0)
                    {
                        moznostiPohybu.Add(new Point(vybranePolicko.X - 1, vybranePolicko.Y + 1));
                    }
                    if (!JeMimoMoznosti(xKlik + 1, yKlik + 1) && poziceFigurek[vybranePolicko.X + 1, vybranePolicko.Y + 1] == 0)
                    {
                        moznostiPohybu.Add(new Point(vybranePolicko.X + 1, vybranePolicko.Y + 1));
                    }
                    if (!JeMimoMoznosti(xKlik + 1, yKlik - 1) && poziceFigurek[vybranePolicko.X + 1, vybranePolicko.Y - 1] == 0)
                    {
                        moznostiPohybu.Add(new Point(vybranePolicko.X + 1, vybranePolicko.Y - 1));
                    }
                    posledniPozice = poziceVybraneFigurky;
                    figurkaVybrana = true;
                }
            }
            else
            {
                if (figurkaVybrana) //Dáme do pole takové hodnoty, aby odpovídali pohybu
                {
                    if (!JeMimoMoznosti(xKlik, yKlik) && poziceFigurek[vybranePolicko.X, vybranePolicko.Y] == 0 && moznostiPohybu.Contains(vybranePolicko))
                    {
                        poziceFigurek[posledniPozice.X, posledniPozice.Y] = 0;
                        poziceFigurek[xKlik, yKlik] = 2;
                        hrajeHrac1 = true;
                    }
                    figurkaVybrana = false;
                    moznostiPohybu.Clear();
                }
                else if (!JeMimoMoznosti(xKlik, yKlik) && poziceFigurek[vybranePolicko.X, vybranePolicko.Y] == 2) //Zjistíme jaké jsou možné pohyby
                {
                    moznostiPohybu.Clear();
                    poziceVybraneFigurky = vybranePolicko;
                    if (!JeMimoMoznosti(xKlik - 1, yKlik - 1) && poziceFigurek[vybranePolicko.X - 1, vybranePolicko.Y - 1] == 0)
                    {
                        moznostiPohybu.Add(new Point(vybranePolicko.X - 1, vybranePolicko.Y - 1));
                    }
                    if (!JeMimoMoznosti(xKlik - 1, yKlik + 1) && poziceFigurek[vybranePolicko.X - 1, vybranePolicko.Y + 1] == 0)
                    {
                        moznostiPohybu.Add(new Point(vybranePolicko.X - 1, vybranePolicko.Y + 1));
                    }
                    if (!JeMimoMoznosti(xKlik + 1, yKlik + 1) && poziceFigurek[vybranePolicko.X + 1, vybranePolicko.Y + 1] == 0)
                    {
                        moznostiPohybu.Add(new Point(vybranePolicko.X + 1, vybranePolicko.Y + 1));
                    }
                    if (!JeMimoMoznosti(xKlik + 1, yKlik - 1) && poziceFigurek[vybranePolicko.X + 1, vybranePolicko.Y - 1] == 0)
                    {
                        moznostiPohybu.Add(new Point(vybranePolicko.X + 1, vybranePolicko.Y - 1));
                    }
                    posledniPozice = poziceVybraneFigurky;
                    figurkaVybrana = true;
                }
            }
        }

        /// <summary>
        /// Zjistí zda cílové místo je mimo možnosti
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns>Vrátí bool hodnotu</returns>
       public bool JeMimoMoznosti(int x, int y) 
        {
            bool mimoMoznostiX = x < 0 || x > velikostDesky - 1;
            bool mimoMoznostiY = y < 0 || y > velikostDesky - 1;
            return mimoMoznostiX || mimoMoznostiY;
        }
    }
}
