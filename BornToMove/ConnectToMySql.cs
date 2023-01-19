using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;


namespace BornToMove
{

    public class ConnectToMySql
    {
        private string? server;
        private string? database;
        private string? username;
        private string? password;
        private MySqlConnection? conn;

        public ConnectToMySql() { 
            server = "127.0.0.1";
            database = "born2move";
            username = "Kevins_WebShopUser";
            password = "GebruikerWebShop";


            string connectstring = "SERVER=" + server + ";" + "DATABASE=" + database + ";"
                + "UID=" + username + ";" + "PASSWORD=" + password + ";";

            conn = new MySqlConnection(connectstring);
            //conn.Open();
            //try
            //{
            //    this.conn.Open();
                
            //}
            //catch (MySqlException ex)
            //{
            //    //When handling errors, you can your application's response based 
            //    //on the error number.
            //    //The two most common error numbers when connecting are as follows:
            //    //0: Cannot connect to server.
            //    //1045: Invalid user name and/or password.
            //    Console.WriteLine("good");
            //    switch (ex.Number)
            //    {
            //        case 0:
            //            Console.WriteLine("Cannot connect to server.  Contact administrator");
            //            break;

            //        case 1045:
            //            Console.WriteLine("Invalid username/password, please try again");
            //            break;
            //    }
                
            //}
        }

        private bool OpenConnection()
        {
            try
            {
                conn.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                //When handling errors, you can your application's response based 
                //on the error number.
                //The two most common error numbers when connecting are as follows:
                //0: Cannot connect to server.
                //1045: Invalid user name and/or password.
                switch (ex.Number)
                {
                    case 0:
                        Console.WriteLine("Cannot connect to server.  Contact administrator");
                        break;

                    case 1045:
                        Console.WriteLine("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        private bool CloseConnection()
        {
            try
            {
                conn.Close();
                return true;
            }
            catch (MySqlException ex) 
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public void ReadAllData()
        {
            string query = "SELECT * FROM move";
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine("name: " + reader.GetString("name"));
                    Console.WriteLine("description: " + reader.GetString("description"));
                    Console.WriteLine("sweatRate: " + reader.GetInt32("sweatRate"));
                }

                reader.Close();
                this.CloseConnection();
            }
        }

        public void ReadAllIds()
        {
            string query = "SELECT id FROM move";
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    Console.WriteLine("name: " + reader.GetString("name"));
                    Console.WriteLine("description: " + reader.GetString("description"));
                    Console.WriteLine("sweatRate: " + reader.GetInt32("sweatRate"));
                }

                reader.Close();
                this.CloseConnection();
            }
        }
    }
}
