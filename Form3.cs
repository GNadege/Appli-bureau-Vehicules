﻿using MySql.Data.MySqlClient;
using System.Data;
using System.Windows.Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Reflection;

namespace ConnexionTable
{
   

    public partial class Form3 : Form
    {
        private MySqlConnection conn;//informations de conexion a la base de données
        private string server = "172.16.0.9";
        private string database = "bdd_sn";
        private string user = "User1";
        private string password = "Prevert-77";
        private string statut="0";
        

        public Form3()
        {
            InitializeComponent();//initialisation des composants
            conn = new MySqlConnection($"Server={server};Database={database};User={user};Pwd={password};Convert Zero Datetime=True"); //création de la connection avec requette sql
            try // Permet l'affichage automatique de la base
            {
                conn.Open();
                String query = "Select * from vehicules";//Récupéraion du contenu de la table

                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur de connexion à la base de données : {ex.Message}"); //Message d'erreur en cas d'impossibilité de récuperer le contenu
            }
            finally
            {
                conn.Close();
            }
        }

        private void afficherBTN_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                String query = "Select * from vehicules";
                
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                dataGridView1.DataSource = dt;

                /*MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0].DefaultView;*/

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
            Form4 form4 = new Form4();
            form4.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
            this.Hide();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                string query = "DELETE FROM vehicules WHERE idvehicule=" + dataGridView1.SelectedCells[0].Value.ToString();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                int i = cmd.ExecuteNonQuery();
                if (i >= -1)
                {
                    MessageBox.Show("Donnée supprimée avec succes!");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur de connexion à la base de données : {ex.Message}");
            }
            finally
            {

                conn.Close();
                this.afficherBTN_Click(sender, e);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                statut = "1";
            }
            else { statut = "0"; }
            DateTime dateDebut = DateTime.Parse(dateTimePicker1.Text);
            string debutForMySql = dateDebut.ToString("yyyy-MM-dd ");
            try
            {
                conn.Open();
                string query = "insert into vehicules (modele, carburant, plaque, type, statut, Anneecontrole, Description, emplacement) values ('"+textBox1.Text+"','"+textBox2.Text+"','"+ textBox3.Text+"','"+textBox4.Text+"','"+statut+"','"+debutForMySql+ "','"+textBox6.Text+"','"+textBox7.Text+"')";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                int i = cmd.ExecuteNonQuery();
                if (i >= -1)
                {
                    MessageBox.Show("Données insérées avec succes!");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur de connexion à la base de données : {ex.Message}");
            }
            finally
            {
                conn.Close();
                this.afficherBTN_Click(sender, e);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {

            if (checkBox1.Checked)
            {
                statut = "1";
            }
            else { statut = "0"; }
            DateTime dateDebut = DateTime.Parse(dateTimePicker1.Text);
            string debutForMySql = dateDebut.ToString("yyyy-MM-dd ");
            try
            {
                conn.Open();

                string query = "UPDATE vehicules SET modele='" + textBox1.Text + "', carburant='" + textBox2.Text + "', plaque='" + textBox3.Text + "', type='" + textBox4.Text + "', Anneecontrole='" + debutForMySql + "', Description='" + textBox6.Text + "', emplacement='" + textBox7.Text + "' WHERE idvehicule=" + dataGridView1.SelectedCells[0].Value.ToString();

                MySqlCommand cmd = new MySqlCommand(query, conn);
                int i = cmd.ExecuteNonQuery();
                if (i >= -1)
                {
                    MessageBox.Show("Donnée modifiée avec succes!");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur de connexion à la base de données : {ex.Message}");
            }
            finally
            {

                conn.Close();
                this.afficherBTN_Click(sender, e);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            try
            {
                this.Hide();
                Form1 form1 = new Form1();

                form1.Show();

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
    }
}
