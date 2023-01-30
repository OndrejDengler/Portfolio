using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pokladna
{
    public partial class Form2 : Form
    {
        int cena;
        bool zaplaceno = false;
        string filePath = "uctenky\\";
        public Form2(int cena)
        {
            InitializeComponent();
            label1.Text = "Cena k zaplacení: " + cena + " Kč";
            this.cena = cena;
            zaplaceno = false;
            numericUpDown1.Select();
            numericUpDown1.Select(0, numericUpDown1.Text.Length);
        }

        private void numericUpDown1_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                int penize = (int)numericUpDown1.Value;
                if(penize < cena)
                {
                    MessageBox.Show("Zákazník zaplatil málo peněz.");
                    numericUpDown1.Select();
                    numericUpDown1.Select(0, numericUpDown1.Text.Length);
                }
                else
                {
                    penize = penize-cena;
                    zaplaceno = true;
                    label3.Text = "Peníze k vrácení " + penize + " Kč";

                }
            }
        }
        private void historie()
        {
            using (SQLiteConnection c = new SQLiteConnection("Data Source=databaze.sqlite;Version=3;"))
            {
                c.Open();
                using (SQLiteCommand cmd = new SQLiteCommand(c))
                {
                    cmd.CommandText = @"insert into historie (celkova_cena, datum) values (@cena,@datum)";
                    cmd.Parameters.AddWithValue("@cena", cena);
                    cmd.Parameters.AddWithValue("@datum", DateTime.Now);
                    cmd.ExecuteNonQuery();
                    long id = c.LastInsertRowId;

                    cmd.CommandText = @"select * from kosik";

                    DataTable dataTable = new DataTable();

                    if (dataTable != null && dataTable.Rows != null && dataTable.Rows.Count > 0)
                    {
                        int cena = (int)dataTable.Rows[0]["cena"];
                    }
                    using (SQLiteDataReader rdr = cmd.ExecuteReader())
                    {
                        dataTable.Load(rdr);
                        
                    }
                    if (dataTable != null && dataTable.Rows != null && dataTable.Rows.Count > 0)
                    {
                        for (int i = 0; i < dataTable.Rows.Count; i++)
                        {
                            string polozka = dataTable.Rows[i]["polozka"].ToString();
                            int cenaPolozky = (int)dataTable.Rows[i]["cena"];
                            int pocet = (int)dataTable.Rows[i]["pocet"];
                            cmd.CommandText = @"insert into historie_polozky (historie_id, polozka, cena, pocet) values (@id, @polozka,@cena, @pocet)";
                            cmd.Parameters.AddWithValue("@polozka", polozka);
                            cmd.Parameters.AddWithValue("@cena", cenaPolozky);
                            cmd.Parameters.AddWithValue("@pocet", pocet);
                            cmd.Parameters.AddWithValue("@id", id);
                            cmd.ExecuteNonQuery();
                        }
                    }
                    
                }

            }
        }

        private void uctenka()
        {
            string datum = DateTime.Now.ToString().Replace(":","-");
            using (StreamWriter sw = new StreamWriter(filePath+datum+".txt", true))
            {
                using (SQLiteConnection c = new SQLiteConnection("Data Source=databaze.sqlite;Version=3;"))
                {
                    c.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand(c))
                    {
                        cmd.CommandText = @"select * from kosik";
                        using (SQLiteDataReader rdr = cmd.ExecuteReader())
                        {
                            int id_pracovnika = 1;
                            int id_pokladny = 1;
                            while (rdr.Read())
                            {
                                string polozka = rdr.GetString(0);
                                int cenaPolozky = (int)rdr.GetInt32(1);
                                int pocet = (int)rdr.GetInt32(2);
                                id_pokladny = (int)rdr.GetInt32(3);
                                id_pracovnika = (int)rdr.GetInt32(4);
                                sw.WriteLine($"{polozka} {pocet}x - {cenaPolozky}Kč/kus {cenaPolozky*pocet}Kč");
                                sw.Flush();
                            }
                            sw.WriteLine("--------------------------");
                            sw.Flush();
                            sw.WriteLine("Celková cena "+cena+"Kč");
                            sw.Flush();
                            sw.WriteLine("Obsloužil vás pracovník číslo "+id_pracovnika+ " na pokladně číslo "+id_pokladny);
                            sw.Flush();
                            sw.WriteLine(datum);
                            sw.Flush();
                        }
                    }

                }
            }

        }
        private void Form2_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Multiply)
            {
                if (zaplaceno)
                {
                    this.DialogResult = DialogResult.OK;
                    uctenka();
                    historie();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Nezaplaceno!");
                }
            }
            else if (e.KeyCode == Keys.Subtract)
            {
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }
    }
}
