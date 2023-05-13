using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace ConnexionTable
{
    public partial class Form4 : Form
    {
        private MySqlConnection conn;//informations de conexion a la base de données
        private string server = "172.16.0.9";
        private string database = "bdd_sn";
        private string user = "User1";
        private string password = "Prevert-77";

        public Form4()
        {
            InitializeComponent();//initialisation des composants
            conn = new MySqlConnection($"Server={server};Database={database};User={user};Pwd={password};Convert Zero Datetime=True"); //création de la connection avec requette sql
            try //Permet l'affichage automatique de la base
            {
                conn.Open();
                String query = "Select * from dispo";//Récupéraion du contenu de la table
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(reader);
                dataGridView1.DataSource = dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur de connexion à la base de données : {ex.Message}");//Message d'erreur en cas d'impossibilité de récuperer le contenu
            }
        }
private void AffichBTN_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                String query = "Select * from dispo";
                MySqlCommand cmd = new MySqlCommand(query, conn);
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
        private void button1_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
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
            //Conversion des dates au format MySQL :
            DateTime dateDebut = DateTime.Parse(dateTimePicker1.Text);
            string debutForMySql = dateDebut.ToString("yyyy-MM-dd ");
            DateTime dateFin = DateTime.Parse(dateTimePicker2.Text);
            string finForMySql = dateFin.ToString("yyyy-MM-dd HH:mm");
            try
            {
                conn.Open();
                string query = "insert into dispo (idvehicule, idadmin, date_emprunt, date_retour, commentaire) values('" + textBox1.Text + "','" + textBox2.Text + "','" + debutForMySql + "','" + finForMySql + "','" + textBox3.Text + "')";
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
                this.AffichBTN_Click(sender, e);
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                string query = "DELETE FROM dispo WHERE iddispo=" + dataGridView1.SelectedCells[2].Value.ToString();
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
                this.AffichBTN_Click(sender, e);
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //Conversion des dates au format MySQL :
            DateTime dateDebut = DateTime.Parse(dateTimePicker1.Text);
            string debutForMySql = dateDebut.ToString("yyyy-MM-dd ");
            DateTime dateFin = DateTime.Parse(dateTimePicker2.Text);
            string finForMySql = dateFin.ToString("yyyy-MM-dd HH:mm");

            try
            {
                conn.Open();
                String query = "SELECT * from dispo WHERE '" + debutForMySql + "' < date_emprunt AND '" + finForMySql + "' > date_retour";
                MySqlCommand cmd = new MySqlCommand(query, conn);
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

        private void button6_Click(object sender, EventArgs e)
        {
            //Conversion des dates au format MySQL :
            DateTime dateDebut = DateTime.Parse(dateTimePicker1.Text);
            string debutForMySql = dateDebut.ToString("yyyy-MM-dd ");
            DateTime dateFin = DateTime.Parse(dateTimePicker2.Text);
            string finForMySql = dateFin.ToString("yyyy-MM-dd HH:mm");
            try
            {
                conn.Open();

                string query = "UPDATE dispo  SET idvehicule='" + textBox1.Text + "', idadmin='" + textBox2.Text + "', iddispo='" + textBox4.Text + "', date_emprunt='" + debutForMySql + "', date_retour='" + finForMySql + "', Commentaire='" + textBox3.Text + "' WHERE iddispo=" + dataGridView1.SelectedCells[2].Value.ToString();

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
                this.AffichBTN_Click(sender, e);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            MessageBox.Show(dataGridView1.SelectedCells[2].Value.ToString());
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
    }
}
