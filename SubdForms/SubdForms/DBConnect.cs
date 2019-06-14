using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Security;
using System.Net;

namespace SubdForms {
    class DBConnect {
        public MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        //Constructor
        public DBConnect(string username, SecureString password)
        {
            Initialize(username, password);
        }

        //Initialize values
        private void Initialize(string u, SecureString p)
        {
            server = "localhost";
            database = "library";
            uid = u;
            password = new NetworkCredential("", p).Password;
            string connectionString;
            connectionString = "SERVER=" + server + ";" + "DATABASE=" +
            database + ";" + "UID=" + uid + ";" + "PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionString);
        }

        //open connection to database
        public bool OpenConnection()
        {
            try
            {
                connection.Open();
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

        //Close connection
        public bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        /*
        public void Insert(string query)
        {
            //open connection
            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.Prepare();

                //Execute command
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }

        //Update statement
        public void Update(string query)
        {
            //Open connection
            if (this.OpenConnection() == true)
            {
                //create mysql command
                MySqlCommand cmd = new MySqlCommand();
                //Assign the query using CommandText
                cmd.CommandText = query;
                //Assign the connection using Connection
                cmd.Connection = connection;

                //Execute query
                cmd.ExecuteNonQuery();

                //close connection
                this.CloseConnection();
            }
        }
        
        //Delete statement
        public void Delete(string query)
        {
            if (this.OpenConnection() == true)
            {
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                this.CloseConnection();
            }
        }
        */
        public List<List<string>> Select(string query, List<Tuple<string, string>> args)
        {
            if(args == null)
            {
                args = new List<Tuple<string, string>>();
            }
            List<List<string>> result = new List<List<string>>();
            if(OpenConnection())
            {
                var cmd = new MySqlCommand(query, connection);
                foreach (Tuple<string, string> t in args)
                {
                    cmd.Parameters.AddWithValue(t.Item1, t.Item2);
                }
                cmd.Prepare();
                var reader = cmd.ExecuteReader();
                while(reader.Read())
                {
                    object[] values = new object[10];
                    reader.GetValues(values);
                    object[] vals = values.Where(v => v != null).ToArray();
                    List<string> s = new List<string>();
                    foreach(object o in vals)
                    {
                        s.Add(o.ToString());
                    }
                    result.Add(s);
                }
            }
            CloseConnection();
            return result;
        }

        //Count statement
        public int Count(string query)
        {
            int Count = -1;

            //Open Connection
            if (this.OpenConnection() == true)
            {
                //Create Mysql Command
                MySqlCommand cmd = new MySqlCommand(query, connection);

                //ExecuteScalar will return one value
                Count = int.Parse(cmd.ExecuteScalar() + "");

                //close Connection
                this.CloseConnection();

                return Count;
            }
            else
            {
                return Count;
            }
        }

        public void Prepared(string query, List<Tuple<string, string>> args)
        {
            if (this.OpenConnection() == true)
            {
                var cmd = new MySqlCommand(query, connection);
                foreach (Tuple<string, string> t in args)
                {
                    cmd.Parameters.AddWithValue(t.Item1, t.Item2);
                }
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                CloseConnection();
            }
        }
    }
}
