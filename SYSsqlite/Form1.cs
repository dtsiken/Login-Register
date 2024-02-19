using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using System.Data.SQLite;

namespace SYSsqlite
{
    public partial class Form1 : KryptonForm
    {
        public Form1()
        {
            InitializeComponent();
        }
        public string usernames;
        SQLite sqlite = new SQLite();

        private void Form1_Load(object sender, EventArgs e)
        {
            ControlBox = false;
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 form2 = new Form2();
            form2.Show();
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            if (kryptonTextBox1.Text != string.Empty && kryptonTextBox2.Text != string.Empty)
            {
                checkAccount(kryptonTextBox1.Text, kryptonTextBox2.Text);
            }
        }

        private void checkAccount(string username, string password)
        {
            sqlite = new SQLite();
            sqlite.getConnection();

            try
            {
                using (SQLiteConnection con = new SQLiteConnection(sqlite.connectionString))
                {
                    con.Open();
                    SQLiteCommand cmd = new SQLiteCommand();
                    string query = @"SELECT * FROM  Account WHERE Username='" + username + "' and Password='" + password + "'";

                    int count = 0;
                    cmd.CommandText = query;
                    cmd.Connection = con;

                    SQLiteDataReader read = cmd.ExecuteReader();
                    while (read.Read())
                    {
                        count++;
                    }
                    if (count == 1)
                    {
                        MessageBox.Show("Log in successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        usernames = username;
                        this.Hide();
                        Form3 form3 = new Form3();
                        form3.Show();
                    }
                    else
                    {
                        MessageBox.Show("Username or password is incorrect", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch(Exception ex) 
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Exit the Application?", "Exit",MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (dialog == DialogResult.Yes) 
            {
                Application.Exit();
            }
        }
    }
}
