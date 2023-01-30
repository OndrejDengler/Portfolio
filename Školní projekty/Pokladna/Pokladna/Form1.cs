using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.IO;

namespace Pokladna
{
    public partial class Form1 : Form
    {
        string kod = "";
        int celkovaCena = 0;
        bool storno = false;
        public Form1()
        {
            InitializeComponent();
            //UpravaDatabaze();
            Setup();

        }

        private void Setup()
        {
            TvorbaDatabaze();
            listView1.View = View.Details;
            listView1.Columns.Add("Položka", 85, HorizontalAlignment.Center);
            listView1.Columns.Add("Počet");
            listView1.Columns.Add("Cena za kus", 85, HorizontalAlignment.Center);
            listView1.Columns.Add("Cena");
            listView2.View = View.Details;
            listView2.Columns.Add("Položka", 85, HorizontalAlignment.Center);
            listView2.Columns.Add("Cena");
            listView2.Columns.Add("Počet", 85, HorizontalAlignment.Center);
            listView2.Columns.Add("Kod", 85, HorizontalAlignment.Center);
            VypisSkladu();
            vymazaniKosiku();
        }
        private string ZiskaniPolozky(string kod)
        {
            using (SQLiteConnection c = new SQLiteConnection("Data Source=databaze.sqlite;Version=3;"))
            {
                c.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(c))
                {
                    cmd.CommandText =
                    @"
                    SELECT polozka
                    FROM sklad
                    WHERE kod = $kod
                    ";

                    cmd.Parameters.AddWithValue("$kod", kod);
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            var polozka = rdr.GetString(0);

                            return polozka;
                        }
                    }
                }
            }
            return "";
        }
        private int ZiskaniIntu(string kod, string select) //Pocet kusu, cena
        {
            //connection.Open();

            using (SQLiteConnection c = new SQLiteConnection("Data Source=databaze.sqlite;Version=3;"))
            {
                c.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(c))
                {
                    if (select == "pocet")
                    {
                        cmd.CommandText =
                        @"
                        SELECT pocet
                        FROM sklad
                        WHERE kod = $kod
                        ";
                    }
                    else if (select == "cena")
                    {
                        cmd.CommandText =
                        @"
                        SELECT cena
                        FROM sklad
                        WHERE kod = $kod
                        ";
                    }
                    cmd.Parameters.AddWithValue("$kod", kod);
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            int pocet = (int)rdr.GetInt32(0);

                            c.Close();
                            return pocet;
                        }
                    }
                }
            }
            return 0;
        }

        private void SpravaKosiku(int pocet, string polozka, int cena)
        {
            int staryPocet = 0;
            using (SQLiteConnection c = new SQLiteConnection("Data Source=databaze.sqlite;Version=3;"))
            {
                c.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(c))
                {
                    cmd.CommandText =
                    @"
                    SELECT pocet
                    FROM kosik
                    WHERE polozka = @polozka
                    ";

                    cmd.Parameters.AddWithValue("@polozka", polozka);
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            staryPocet = (int)rdr.GetInt32(0);
                        }

                    }
                    pocet += staryPocet;
                    if (staryPocet == 0)
                    {
                        cmd.CommandText = @"insert into kosik (polozka, cena, pocet, id_pokladny, id_pracovnika) values (@polozka,@cena, @pocet,1,1)";
                        cmd.Parameters.AddWithValue("@polozka", polozka);
                        cmd.Parameters.AddWithValue("@cena", cena);
                        cmd.Parameters.AddWithValue("@pocet", pocet);
                        cmd.ExecuteNonQuery();
                    }
                    else
                    {
                        cmd.CommandText = @"update kosik set pocet = @pocet where polozka = @polozka and id_pokladny = 1 and id_pracovnika = 1";
                        cmd.Parameters.AddWithValue("@pocet", pocet);
                        cmd.Parameters.AddWithValue("@polozka", polozka);
                        cmd.ExecuteNonQuery();
                    }
                }

            }
        }
        private void Storno()
        {
            string polozka = ZiskaniPolozky(kod);
            int pocetStorno = (int)numKusy.Value;
            int skladPocet = 0;
            int staryPocet = 0;
            int cena = ZiskaniIntu(kod, "cena");
            int pocet = 0;
            label2.Text = "Storno " + polozka;
            using (SQLiteConnection c = new SQLiteConnection("Data Source=databaze.sqlite;Version=3;"))
            {
                c.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(c))
                {
                    cmd.CommandText =
                    @"
                    SELECT pocet
                    FROM kosik
                    WHERE polozka = @polozka
                    ";

                    cmd.Parameters.AddWithValue("@polozka", polozka);
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            staryPocet = (int)rdr.GetInt32(0);
                        }

                    }
                    cmd.CommandText =
                    @"
                    SELECT pocet
                    FROM sklad
                    WHERE polozka = @polozka
                    ";
                    cmd.Parameters.AddWithValue("@polozka", polozka);
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            skladPocet = (int)rdr.GetInt32(0);
                        }

                    }
                    if (staryPocet>=pocetStorno)
                    {
                        pocet = staryPocet - pocetStorno;
                        cmd.CommandText = @"update kosik set pocet = @pocet where polozka = @polozka and id_pokladny = 1 and id_pracovnika = 1";
                        cmd.Parameters.AddWithValue("@pocet", pocet);
                        cmd.Parameters.AddWithValue("@polozka", polozka);
                        cmd.ExecuteNonQuery();
                        pocet = skladPocet + pocetStorno;
                        cmd.CommandText = @"update sklad set pocet = @pocet where polozka = @polozka";
                        cmd.Parameters.AddWithValue("@pocet", pocet);
                        cmd.Parameters.AddWithValue("@polozka", polozka);
                        cmd.ExecuteNonQuery();
                        cmd.CommandText = @"delete from kosik where pocet = 0";
                        cmd.Parameters.AddWithValue("@pocet", pocet);
                        cmd.Parameters.AddWithValue("@polozka", polozka);
                        cmd.ExecuteNonQuery();
                        celkovaCena -= cena * pocetStorno;
                        labelCelkCena.Text = "Celková cena: " + celkovaCena.ToString() + " Kč";
                        label2.Text = "";
                    }
                    else
                    {
                        MessageBox.Show("Špatný počet kusů");
                    }
                    AktualizaceKosiku();
                    VypisSkladu();
                    kod = "";
                    textBox1.Select();
                    textBox1.Select(0, numKusy.Text.Length);

                }

            }
        }
        private void PotvrzeniKusu()
        {
            int pocet = (int)numKusy.Value;
            int maxPocet = ZiskaniIntu(kod, "pocet");
            if (pocet == 0)
            {
                label2.Text = "";
                kod = "";
                textBox1.Focus();
            }
            else if (pocet > maxPocet)
            {
                MessageBox.Show("Nedostatek kusů na skladě");
                numKusy.Value = 1;
                numKusy.Select(0, numKusy.Text.Length);
            }
            else if(pocet <= maxPocet)
            {
                string polozka = ZiskaniPolozky(kod);
                int cena = ZiskaniIntu(kod, "cena");
                
                label2.Text = "";
                celkovaCena += cena * pocet;
                labelCelkCena.Text = "Celková cena: " + celkovaCena + " Kč";
                OdecitaniKusu(pocet,maxPocet);
                SpravaKosiku(pocet, polozka, cena);
                AktualizaceKosiku();
                VypisSkladu();
                kod = "";
                textBox1.Focus();
            }
        }

        private void AktualizaceKosiku()
        {
            listView1.Items.Clear();
            using (SQLiteConnection c = new SQLiteConnection("Data Source=databaze.sqlite;Version=3;"))
            {
                c.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(c))
                {
                    cmd.CommandText = @"select * from kosik";
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            string polozka = rdr.GetString(0);
                            int cenaPolozky = (int)rdr.GetInt32(1);
                            int pocet = (int)rdr.GetInt32(2);
                            var radek = new ListViewItem(new[] { polozka, pocet.ToString(), cenaPolozky.ToString() + " Kč", (pocet * cenaPolozky).ToString() + " Kč" });
                            listView1.Items.Add(radek);
                        }
                    }
                }

            }
        }

        private void OdecitaniKusu(int pocet,int maxPocet)
        {
            int celkovyPocet = maxPocet - pocet;
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=databaze.sqlite;Version=3;"))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(conn))
                {
                    cmd.CommandText = @"update sklad set pocet = @celkovyPocet where kod = @kod";
                    cmd.Parameters.AddWithValue("@celkovyPocet", celkovyPocet);
                    cmd.Parameters.AddWithValue("@kod", kod);
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }
        public void VypisSkladu()
        {
            listView2.Items.Clear();
            using (SQLiteConnection c = new SQLiteConnection("Data Source=databaze.sqlite;Version=3;"))
            {
                c.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(c))
                {
                    cmd.CommandText = @"select * from sklad";
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            string polozka = rdr.GetString(0);
                            int cenaPolozky = (int)rdr.GetInt32(1);
                            int pocet = (int)rdr.GetInt32(2);
                            string kodSklad = rdr.GetString(3);
                            var radek = new ListViewItem(new[] { polozka, cenaPolozky.ToString() + " Kč", pocet.ToString(), kodSklad});
                            listView2.Items.Add(radek);
                        }
                    }
                }

            }
        }
        private void prekladCisel(KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D1 || e.KeyCode == Keys.NumPad1) //Klávesnice
            {
                kod += "1";
            }
            else if (e.KeyCode == Keys.D2 || e.KeyCode == Keys.NumPad2)
            {
                kod += "2";
            }
            else if (e.KeyCode == Keys.D3 || e.KeyCode == Keys.NumPad3)
            {
                kod += "3";
            }
            else if (e.KeyCode == Keys.D4 || e.KeyCode == Keys.NumPad4)
            {
                kod += "4";
            }
            else if (e.KeyCode == Keys.D5 || e.KeyCode == Keys.NumPad5)
            {
                kod += "5";
            }
            else if (e.KeyCode == Keys.D6 || e.KeyCode == Keys.NumPad6)
            {
                kod += "6";
            }
            else if (e.KeyCode == Keys.D7 || e.KeyCode == Keys.NumPad7)
            {
                kod += "7";
            }
            else if (e.KeyCode == Keys.D8 || e.KeyCode == Keys.NumPad8)
            {
                kod += "8";
            }
            else if (e.KeyCode == Keys.D9 || e.KeyCode == Keys.NumPad9)
            {
                kod += "9";
            }
            else if (e.KeyCode == Keys.D0 || e.KeyCode == Keys.NumPad0)
            {
                kod += "0";
            }
        }

        private void TextBox1_KeyDown(object sender, KeyEventArgs e)
        {
            prekladCisel(e);
            //textBox1.Text = "";
            if (e.KeyCode == Keys.Enter)
            {

                textBox1.Text = kod;
                //kod = textBox2.Text;
                if(ZiskaniPolozky(kod) != "")
                {
                    if (!storno)
                    {
                        label2.Text = ZiskaniPolozky(kod) + " - " + ZiskaniIntu(kod, "cena") + " Kč";
                    }
                    else
                    {
                        label2.Text = "Storno " + ZiskaniPolozky(kod).ToString();
                    }
                    textBox1.Text = "";
                    numKusy.Value = 1;
                    numKusy.Refresh();
                    numKusy.Select();
                    numKusy.Select(0, numKusy.Text.Length);
                }
                else
                {
                    MessageBox.Show("Neznámá položka");
                    textBox1.Text = "";
                    kod = "";
                }
                
            }
        }

        private void numKusy_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.D1) //Klávesnice
            {
                PotvrzeniKusu();
                kod = "";
                kod += "1";
                textBox1.Focus();
            }
            else if (e.KeyCode == Keys.D2)
            {
                PotvrzeniKusu();
                kod = "";
                kod += "2";
                textBox1.Focus();
            }
            else if (e.KeyCode == Keys.D3)
            {
                PotvrzeniKusu();
                kod = "";
                kod += "3";
                textBox1.Focus();
            }
            else if (e.KeyCode == Keys.D4)
            {
                PotvrzeniKusu();
                kod = "";
                kod += "4";
                textBox1.Focus();
            }
            else if (e.KeyCode == Keys.D5)
            {
                PotvrzeniKusu();
                kod = "";
                kod += "5";
                textBox1.Focus();
            }
            else if (e.KeyCode == Keys.D6)
            {
                PotvrzeniKusu();
                kod = "";
                kod += "6";
                textBox1.Focus();
            }
            else if (e.KeyCode == Keys.D7)
            {
                PotvrzeniKusu();
                kod = "";
                kod += "7";
                textBox1.Focus();
            }
            else if (e.KeyCode == Keys.D8)
            {
                PotvrzeniKusu();
                kod = "";
                kod += "8";
                textBox1.Focus();
            }
            else if (e.KeyCode == Keys.D9)
            {
                PotvrzeniKusu();
                kod = "";
                kod += "9";
                textBox1.Focus();
            }
            else if (e.KeyCode == Keys.D0)
            {
                PotvrzeniKusu();
                kod = "";
                kod += "0";
                textBox1.Focus();
            }
            if(e.KeyCode == Keys.Subtract)
            {
                kod = "";
                label2.Text = "";
                numKusy.Value = 1;
                textBox1.Focus();
            }
            if (e.KeyCode == Keys.Enter)
            {
                if (storno)
                {
                    Storno();
                    storno = false;
                }
                else
                {
                    PotvrzeniKusu();
                }
            }
            numKusy.Value = 1;
        }

        /*private void Vraceni() //work in progress
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=databaze.sqlite;Version=3;"))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(conn))
                {
                    List<string[]> polozkyNaVraceni = new List<string[]>();
                    cmd.CommandText = "select polozka, pocet from kosik";
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            cmd.CommandText.
                            polozkyNaVraceni.Add([rdr.GetString(0), rdr.GetInt32(1).ToString()]);
                        }
                    }
                }
                conn.Close();
            }
        }*/

        private void vymazaniKosiku()
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=databaze.sqlite;Version=3;"))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(conn))
                {
                    cmd.CommandText = @"delete from kosik";
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=databaze.sqlite;Version=3;"))
            {
                conn.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(conn))
                {
                    cmd.CommandText = @"update sklad set pocet = 4 where kod = 6273615362";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = @"update sklad set pocet = 5 where kod = 6374836271";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = @"update sklad set pocet = 40 where kod = 1284939927";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = @"update sklad set pocet = 9 where kod = 1264736485";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = @"update sklad set pocet = 12 where kod = 3127382937";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = @"update sklad set pocet = 7 where kod = 8261528492";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = @"update sklad set pocet = 8 where kod = 1528940372";
                    cmd.ExecuteNonQuery();
                    cmd.CommandText = @"update sklad set pocet = 33 where kod = 6273840263";
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
                VypisSkladu();
            }
        }

        private void TvorbaDatabaze()
        {
            if (!File.Exists("databaze.sqlite"))
            {
                SQLiteConnection.CreateFile("databaze.sqlite");
                SQLiteConnection connection = new SQLiteConnection("Data Source=databaze.sqlite;Version=3;");
                connection.Open();
                string sql;
                SQLiteCommand command = new SQLiteCommand();
                sql = "create table sklad (polozka varchar(20), cena int, pocet int, kod varchar(20))";
                command = new SQLiteCommand(sql, connection);
                command.ExecuteNonQuery();

                sql = "create table kosik (polozka varchar(20), cena int, pocet int, id_pokladny int, id_pracovnika int)";
                command = new SQLiteCommand(sql, connection);
                command.ExecuteNonQuery();

                sql = "create table historie (id int AUTO_INCREMENT PRIMARY KEY, celkova_cena int, datum date)";
                command = new SQLiteCommand(sql, connection);
                command.ExecuteNonQuery();

                sql = "create table historie_polozky (historie_id int, polozka varchar(20), pocet int, cena int)";  
                command = new SQLiteCommand(sql, connection);
                command.ExecuteNonQuery();

                sql = "insert into sklad (polozka, cena, pocet, kod) values ('Minerálka','12','9','1264736485')";
                command = new SQLiteCommand(sql, connection);
                command.ExecuteNonQuery();
                sql = "insert into sklad (polozka, cena, pocet, kod) values ('Knedlík','30','4','6273615362')";
                command = new SQLiteCommand(sql, connection);
                command.ExecuteNonQuery();

                sql = "insert into sklad (polozka, cena, pocet, kod) values ('Rohlík','40','2','1284939927')";
                command = new SQLiteCommand(sql, connection);
                command.ExecuteNonQuery();
                sql = "insert into sklad (polozka, cena, pocet, kod) values ('Cigarety','100','5','6374836271')";
                command = new SQLiteCommand(sql, connection);
                command.ExecuteNonQuery();

                connection.Close();
            }
        }

        private void Form1_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Multiply)
            {
                textBox1.Clear();
                if (celkovaCena != 0)
                {
                    Form2 form2 = new Form2(celkovaCena);
                    form2.ShowDialog();
                    if(form2.DialogResult == DialogResult.OK)
                    {
                        vymazaniKosiku();
                        listView1.Items.Clear();
                    }
                }
            }
            else if(e.KeyCode == Keys.Divide)
            {
                label2.Text = "Storno";
                storno = true;
                textBox1.Select();
                textBox1.Select(0, numKusy.Text.Length);
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            vymazaniKosiku();
            
        }
    }
}
