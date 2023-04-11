using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConnexionTable
{
    public partial class Form2 : Form
    {
        private MySqlConnection conn;

        private string server = "localhost";
        private string database = "bdd-sn";
        private string uid = "root";
        private string password = "";

        public Form2(/*MySqlConnection conn*/)
        {
            InitializeComponent();
            /*conn = new MySqlConnection($"Server={server};Database={database};Uid={uid};Pwd={password};");
            this.conn = conn;*/
        }


   

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                Form3 form3 = new Form3();

                form3.Show();

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur de connexion à la base de données : {ex.Message}");
            }
            finally
            {
                conn.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                //string query = $"INSERT INTO user (nom, prenom, adresse) VALUES ('{nom}', '{prenom}', '{adresse}');";
                // MySqlCommand cmd = new MySqlCommand(query, conn);
                //int rowsAffected = cmd.ExecuteNonQuery();
                //MessageBox.Show($"Enregistrement effectué, {rowsAffected} ligne(s) affectée(s).", "Succès");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'enregistrement des données : {ex.Message}", "Erreur");
            }
            finally
            {
                conn.Close();
            }
        }
        public Form2()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                conne.Open();
                String query = "Select * from user";
                MySqlCommand cmd = new MySqlCommand(query, conne);
                MySqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                dataGridView1.DataSource = dt;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur de connexion à la base de données : {ex.Message}");
            }
            finally
            {
                conn.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                string query = "DELETE FROM user WHERE id=" + dataGridView1.SelectedCells[0].Value.ToString();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                int i = cmd.ExecuteNonQuery();
                if (i >= -1)
                {
                    MessageBox.Show("Données supprimées avec succes!");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur de connexion à la base de données : {ex.Message}");
            }
            finally
            {

                conne.Close();
                this.affichBTN_Click(sender, e);
            }
        }
    }
}
