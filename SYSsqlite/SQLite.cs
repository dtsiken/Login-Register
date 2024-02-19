    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.IO;
    using System.Data.SQLite;
    using System.Diagnostics.Eventing.Reader;

    namespace SYSsqlite
    {
        internal class SQLite
        {
            public string connectionString { get; set; }
            string connection;

            public void getConnection()
            {
                connection = @"Data Source=Userdata.db; Version=3;";
                connectionString = connection;
            }
        public void createDatabase()
        {
            try
            {
                if (!File.Exists("Userdata.db"))
                {
                    SQLiteConnection.CreateFile("Userdata.db");
                    createTable();
                }
                else
                {
                    createTable();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void createTable()
            {
                try
                {
                    getConnection();
                    using (SQLiteConnection con = new SQLiteConnection(connection))
                    {
                        con.Open();
                        SQLiteCommand cmd = new SQLiteCommand();

                    string query = @"CREATE TABLE Account (ID INTEGER PRIMARY KEY AUTOINCREMENT, Name TEXT(50), Email TEXT(50), Username TEXT(50), Password TEXT(50))";
                    cmd.CommandText = query;
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }
    }
