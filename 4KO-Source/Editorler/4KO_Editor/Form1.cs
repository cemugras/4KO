using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace _4KO_Editor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            LoadConfig(); // Form açılırken ayarları yükle
        }

        // Dosya yolu uygulamanın olduğu klasörde 4KO.ini olacak
        IniFile ini = new IniFile(Application.StartupPath + "\\4KO.ini");

        private void LoadConfig()
        {
            txtIP.Text = ini.Read("Settings", "IP");
            txtDBName.Text = ini.Read("Settings", "DBName");
            txtUser.Text = ini.Read("Settings", "User");
            txtPass.Text = ini.Read("Settings", "Pass");
            if (!string.IsNullOrEmpty(txtIP.Text) &&
                !string.IsNullOrEmpty(txtDBName.Text) &&
                !string.IsNullOrEmpty(txtUser.Text) &&
                !string.IsNullOrEmpty(txtPass.Text))
            {
                textBoxInfo.SelectionColor = System.Drawing.Color.Green;
                textBoxInfo.AppendText("Settings loaded successfuly.\n");
            } else {
                textBoxInfo.SelectionColor = System.Drawing.Color.Red;
                textBoxInfo.AppendText("Settings load error.\n");
                textBoxInfo.SelectionColor = System.Drawing.Color.Black;
                textBoxInfo.AppendText("Fill the blanks for database connection.\n");
            }
        }

        private void connectButton_Click(object sender, EventArgs e)
        {
            textBoxInfo.Clear();
            textBoxInfo.AppendText("Connection trying...\n");

            // Ayrı textBox'lardaki verileri alıp birleştiriyoruz
            string ip = txtIP.Text;
            string dbName = txtDBName.Text;
            string user = txtUser.Text;
            string pass = txtPass.Text;

            // Bağlantı dizesini oluşturuyoruz
            string connectionString = $"Server={ip},1433;Database={dbName};User Id={user};Password={pass};";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    textBoxInfo.SelectionColor = System.Drawing.Color.Green;
                    textBoxInfo.AppendText("Connection Success!\n");
                    ini.Write("Settings", "IP", txtIP.Text);
                    ini.Write("Settings", "DBName", txtDBName.Text);
                    ini.Write("Settings", "User", txtUser.Text);
                    ini.Write("Settings", "Pass", txtPass.Text);
                }
                catch (Exception ex)
                {
                    textBoxInfo.SelectionColor = System.Drawing.Color.Red;
                    textBoxInfo.AppendText("Connection Failed!\n");
                    textBoxInfo.AppendText("Error: " + ex.Message + "\n");
                }
                textBoxInfo.SelectionColor = System.Drawing.Color.Black;
            }
        }
    }
}
        