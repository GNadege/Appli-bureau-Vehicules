using Microsoft.VisualBasic.ApplicationServices;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Security.Cryptography;
using System.Runtime.Intrinsics.Arm;
using System.Reflection;

namespace ConnexionTable
{
    public partial class Form2 : Form
    {
        private MySqlConnection conn; //informations de conexion a la base de données

        private string server = "172.16.0.9";
        private string database = "bdd_sn";
        private string user = "User1";
        private string password = "Prevert-77";

        public Form2()
        {
            InitializeComponent();//initialisation des composants
            conn = new MySqlConnection($"Server={server};Database={database};User={user};Pwd={password};");//création de la connection avec requette sql
            try  //Permet l'affichage automatique de la base
            {
                conn.Open();
                String query = "Select idadmin, login, pwd, fonction, Description, nom, prenom from admin"; //Récupéraion du contenu de la table
                MySqlCommand cmd = new MySqlCommand(query, conn);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0].DefaultView;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur de connexion à la base de données : {ex.Message}"); //Message d'erreur en cas d'impossibilité de récuperer le contenu
            }
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
            string mdp = mdpTXT.Text + "pepper"; //cryptage 
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(mdp));
                var sb = new StringBuilder(hash.Length * 2);
                foreach (byte b in hash)
                {
                    sb.Append(b.ToString("x2"));
                }
                mdp = sb.ToString();
            }
            try
            {
                conn.Open();
                string query = $"INSERT INTO admin (login, pwd, fonction, Description, nom, prenom) VALUES ('"+loginTXT.Text+"','"+mdp+"','"+fonctionTXT.Text+"','"+descriptionTXT.Text+"','"+textBox1.Text+"','"+textBox2.Text+"')";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                int rowsAffected = cmd.ExecuteNonQuery();
                MessageBox.Show($"Ajout éffectué avec succès");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'enregistrement des données : {ex.Message}", "Erreur");
            }
            finally
            {
                conn.Close();
                this.button3_Click(sender, e);
            }
            
        }
        

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                String query = "Select idadmin, login, pwd, fonction, Description, nom, prenom from admin";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                MySqlDataAdapter da = new MySqlDataAdapter(cmd);
                DataSet ds = new DataSet();
                da.Fill(ds);
                dataGridView1.DataSource = ds.Tables[0].DefaultView;

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
                string query = "DELETE FROM admin WHERE idadmin=" + dataGridView1.SelectedCells[0].Value.ToString();
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

                conn.Close();
                this.button3_Click(sender, e);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show();
            this.Hide();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string mdp = mdpTXT.Text + "pepper"; //cryptage 
            using (SHA1Managed sha1 = new SHA1Managed())
            {
                var hash = sha1.ComputeHash(Encoding.UTF8.GetBytes(mdp));
                var sb = new StringBuilder(hash.Length * 2);
                foreach (byte b in hash)
                {
                    sb.Append(b.ToString("x2"));
                }
                mdp = sb.ToString();
            }
            try
            {
                conn.Open();
                
                string query = "UPDATE admin SET login='" + loginTXT.Text + "', pwd='" + mdp + "', fonction='"+ fonctionTXT.Text+ "', description='"+ descriptionTXT.Text+ "', nom='"+textBox1.Text+"', prenom='"+textBox2.Text+"' WHERE idadmin=" + dataGridView1.SelectedCells[0].Value.ToString();
                
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
                this.button3_Click(sender, e);
            }
        }

        private void button7_Click(object sender, EventArgs e)
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count > 0) // make sure user select at least 1 row 
            {
                //textBox1.Text = dataGridView1.SelectedRows[0].Cells[5].Value + string.Empty;
                //textBox2.Text = dataGridView1.SelectedRows[0].Cells[6].Value + string.Empty;
             //textBox1.Text= Convert.ToString(dataGridView1[5, dataGridView1.SelectedRows].Value);
            }
            
        }
    }
}
