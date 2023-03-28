using System;
using System.Data;
using System.Runtime.Intrinsics.Arm;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Security.Cryptography;
using System.Text;

namespace ConnexionTable
{
    public partial class Form1 : Form
    {
       private MySqlConnection conn;
        private string server = "localhost";
        private string database = "bdd_sn";
        private string user = "root";
        private string password = "";
        private string salt = "pepper"; // Chaines de caractères pour connection a la base de sonnées

        public Form1()
        {
            InitializeComponent(); //initialisation des composants
            PasswordTextbox.PasswordChar = '*';// cache caractères
            conn = new MySqlConnection($"Server={server};Database={database};User={user};Pwd={password};"); //création de la connection avec requette sql
        }

        private void ShowPasswordCheckbox_CheckedChanged(object sender, EventArgs e)// Evenement si case cochée
        {
            if (ShowPasswordCheckbox.Checked)
            {
                PasswordTextbox.PasswordChar = '\0';
            }
            else
            {
                PasswordTextbox.PasswordChar = '*';
            }
        }

        private void ConnexionButton_Click(object sender, EventArgs e)
        {
            string mdp = PasswordTextbox.Text + salt; //cryptage 
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

            try //essai pour attraper les erreurs
            { 
                conn.Open(); //ouverture de la connection a la base de donnée
                MySqlCommand cmd = new MySqlCommand($"SELECT * FROM admin WHERE login='{UsernameTextbox.Text}' AND pwd= ('{mdp}' )", conn); // récupértion login et mdp
                MySqlDataAdapter adapter = new MySqlDataAdapter(cmd);
                DataTable dataTable = new DataTable(); //création de table pour test de connection
                adapter.Fill(dataTable); //test de la table
                if (dataTable.Rows.Count == 1) // Si une colone dans la table
                {
                    Form2 form2 = new Form2(); //ouverture du fomrs 2
                    form2.Show();
                    this.Hide(); //cacher forms 1
                    //MessageBox.Show("Vous êtes connecté.");
                }
                else
                {
                    MessageBox.Show("Nom d'utilisateur ou mot de passe incorrect."); //en cas de login ou mdp incorrect
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur : {ex.Message}"); //en cas d'erreur de la base de donnée
            }
            finally // fermeture de la connection
            {
                conn.Close();
            }
        }
    }
}