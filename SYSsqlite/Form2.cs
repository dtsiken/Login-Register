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
    public partial class Form2 : KryptonForm
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ControlBox = false;
        }

        private void label7_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form1 form1 = new Form1();
            form1.Show();
        }

        SQLite sqlite = new SQLite();
        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(valname.Text) ||
                string.IsNullOrWhiteSpace(valemailadd.Text) ||
                string.IsNullOrWhiteSpace(valusername.Text) ||
                string.IsNullOrWhiteSpace(valpassword.Text) ||
                string.IsNullOrWhiteSpace(valconfirmpassword.Text))
            {
                MessageBox.Show("Please fill in all the required fields.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (valpassword.Text == valconfirmpassword.Text)
                {
                    try
                    {
                        sqlite.createDatabase();
                        sqlite.getConnection();
                        checkAccount(valusername.Text);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Password don't match", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void checkAccount(string username)
        {
            try
            {
                using (SQLiteConnection con = new SQLiteConnection(sqlite.connectionString))
                {
                    SQLiteCommand cmd = new SQLiteCommand();
                    con.Open();

                    int count = 0;
                    string query = @"SELECT * FROM Account WHERE Username='" + username + "'";
                    cmd.CommandText = query;
                    cmd.Connection = con;

                    SQLiteDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        count++;
                    }
                    if (count == 1)
                    {
                        MessageBox.Show("This account already existed", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else if (count == 0)
                    {
                        insertData(valname.Text, valemailadd.Text, valusername.Text, valpassword.Text);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void insertData(string Name, string Email, string Username, string Password)
        {
            try
            {
                using (SQLiteConnection con = new SQLiteConnection(sqlite.connectionString))
                {
                    con.Open();
                    SQLiteCommand cmd = new SQLiteCommand();
                    string query = @"INSERT INTO Account (Name, Email, Username, Password) VALUES(@name, @email, @username, @password)";
                    cmd.CommandText = query;
                    cmd.Connection = con;
                    cmd.Parameters.Add(new SQLiteParameter("@name", Name));
                    cmd.Parameters.Add(new SQLiteParameter("@email", Email));
                    cmd.Parameters.Add(new SQLiteParameter("@username", Username));
                    cmd.Parameters.Add(new SQLiteParameter("@password", Password));

                    cmd.ExecuteNonQuery();

                    MessageBox.Show("Account Created!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Hide();
                    Form1 form1 = new Form1();
                    form1.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
