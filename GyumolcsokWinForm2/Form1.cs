using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace GyumolcsokWinForm2
{
    public partial class Form1 : Form
    {
        MySqlConnection conn;
        MySqlCommand cmd;

        public Form1()
        {
            InitializeComponent();

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            builder.Server = "localhost";
            builder.UserID = "root";
            builder.Password = "";
            builder.Database = "gyumolcsok";
            conn = new MySqlConnection(builder.ConnectionString);

            try
            {
                conn.Open();
                cmd = conn.CreateCommand();
            }
            catch (MySqlException ex)
            {
                MessageBox.Show(ex.Message + Environment.NewLine + "A program leáll!");
                Environment.Exit(0);
            }
            finally
            {
                conn.Close();
            }
            gyumolcsok_update();
        }
        private void gyumolcsok_update()
        {
            listBoxGyumolcsok.Items.Clear();
            cmd.CommandText = "SELECT `id`, `nev`, `egysegar`, `mennyiseg` FROM `gyumolcsok` WHERE 1;";
            conn.Open();
            using (MySqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {

                    gyumolcsok uj = new gyumolcsok(dr.GetInt32("id"), dr.GetString("nev"), dr.GetInt32("egysegar"), dr.GetInt32("mennyiseg"));
                    listBoxGyumolcsok.Items.Add(uj);

                }

            }
            conn.Close();
        }
        private bool nincsenek_adatok()
        {
            if (string.IsNullOrEmpty(textBoxNev.Text))
            {
                MessageBox.Show("Adjon meg gyumolcs nevet");
                textBoxNev.Focus();
                return true;
            }
            if (numericUpDownMennyiseg.Value > 1000)
            {
                MessageBox.Show("Ervenytelen mennyiseg");
                numericUpDownMennyiseg.Value = 1000;
                numericUpDownMennyiseg.Focus();
                return true;
            }
            if (string.IsNullOrEmpty(numericUpDownEgysegar.Text))
            {
                MessageBox.Show("Nem adott meg egysegarat");
                numericUpDownEgysegar.Focus();
                return true;
            }
            return false;
        }


        private void ujGyumolcsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (nincsenek_adatok())
            {
                return;
            }

            cmd.CommandText = "INSERT INTO `gyumolcsok` (`id`, `nev`, `egysegar`, `mennyiseg`) VALUES (NULL, @nev, @egysegar, @mennyiseg)";
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@nev", textBoxNev.Text);
            cmd.Parameters.AddWithValue("@egysegar", numericUpDownEgysegar.Value);
            cmd.Parameters.AddWithValue("@mennyiseg", numericUpDownMennyiseg.Value);
            try
            {
                conn.Open();

                if (cmd.ExecuteNonQuery() == 1)
                {
                    MessageBox.Show("Sikeresen rogzitve");
                    textBoxID.Text = "";
                    textBoxNev.Text = "";
                    numericUpDownEgysegar.Text = "";
                }
                else
                {
                    MessageBox.Show("Sikertelen rogzites");
                }

            }
            catch (MySqlException ex)
            {

                MessageBox.Show(ex.Message, "sikertelen!");

            }
            conn.Close();

        }

        private void gyumolcsTorleseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (listBoxGyumolcsok.SelectedIndex<0)
            {
                MessageBox.Show("nincs kijelolve gyumolcs");
            }


        }
    }
}
    

    
