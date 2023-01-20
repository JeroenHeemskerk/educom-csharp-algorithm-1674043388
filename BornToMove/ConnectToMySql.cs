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
                    var move = new Move(reader.GetInt32("id"), reader.GetString("name"), reader.GetString("description"), reader.GetInt32("sweatRate"));
                    //Console.WriteLine("Name: " + reader.GetString("name"));
                    //Console.WriteLine("Description: " + reader.GetString("description"));
                    //Console.WriteLine("SweatRate: " + reader.GetInt32("sweatRate"));
                    //Console.WriteLine();
                }

                reader.Close();
                this.CloseConnection();
            }
        }

        public void GetRandomMove()
        {
            List<int> allIds = new List<int>();
            string query = "SELECT id FROM move";
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    allIds.Add(reader.GetInt32("id"));
                }
                reader.Close();
                var rand = new Random();
                var randomId = rand.Next(1, (allIds.Count + 1));

                //string query2 = $"SELECT * FROM move WHERE id={randomId}";
                //MySqlCommand cmd2 = new MySqlCommand(query2, conn);
                //MySqlDataReader reader2 = cmd2.ExecuteReader();
                //while (reader2.Read())
                //{
                //    var move = new Move(reader2.GetInt32("id"), reader2.GetString("name"), reader2.GetString("description"), reader2.GetInt32("sweatRate"));
                //    Console.WriteLine();
                //    move.ShowAll();

                //    //Console.WriteLine("Name: " + reader2.GetString("name"));
                //    //Console.WriteLine("Description: " + reader2.GetString("description"));
                //    //Console.WriteLine("SweatRate: " + reader2.GetInt32("sweatRate"));
                //    //Console.WriteLine();
                //}

                //reader2.Close();
                var move = GetMoveById(randomId);
                move.ShowAll();
                this.CloseConnection();
            }
        }

        public Move GetMoveById(int id)
        {
            Move move = null;
            string query = $"SELECT * FROM move WHERE id={id}";
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataReader reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                move = new Move(reader.GetInt32("id"), reader.GetString("name"), reader.GetString("description"), reader.GetInt32("sweatRate"));

                //Console.WriteLine("Name: " + reader2.GetString("name"));
                //Console.WriteLine("Description: " + reader2.GetString("description"));
                //Console.WriteLine("SweatRate: " + reader2.GetInt32("sweatRate"));
                //Console.WriteLine();
            }

            reader.Close();

            return move;
        }

        public Dictionary<int, Move> ReadAllOptions()
        {
            Dictionary<int, Move> moves = new Dictionary<int, Move>();
            string query = "SELECT id, name, sweatRate FROM move";
            if (this.OpenConnection() == true)
            {
                int number = 0;
                MySqlCommand cmd = new MySqlCommand(query, conn);
                MySqlDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    number++;
                    var move = new Move(reader.GetInt32("id"), reader.GetString("name"), null, reader.GetInt32("sweatRate"));
                    moves.Add(number, move);
                    
                    //Console.WriteLine("Name: " + reader.GetString("name"));
                    //Console.WriteLine("Description: " + reader.GetString("description"));
                    //Console.WriteLine("SweatRate: " + reader.GetInt32("sweatRate"));
                    //Console.WriteLine();
                }

                reader.Close();
                this.CloseConnection();
            }
            foreach (KeyValuePair<int, Move> element in moves)
            {
                Console.WriteLine("Number: " + element.Key);
                element.Value.ShowMoveName();
                element.Value.ShowMoveSweatRate();
                Console.WriteLine();
            }

            return moves;
        }

        public void GetChosenMoveById(int id)
        {
            Move move = null;
            if (this.OpenConnection() == true)
            {
                move = GetMoveById(id);
                this.CloseConnection();
                Console.WriteLine();
                move.ShowAll();
            }
        }

        public void InsertNewMove(string moveName, string moveDescription, int sweatRateNumber)
        {
            string query = $"INSERT INTO move(name, description, sweatRate) VALUES ('{moveName}', '{moveDescription}', {sweatRateNumber})";
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }
    }
}
