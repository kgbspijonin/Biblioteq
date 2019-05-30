using System;
using System.Collections.Generic;
using System.Data.Common;
using MySql.Data.MySqlClient;
using System.IO;
using System.Collections;
using System.Text;

namespace Subd
{
    class Author
    {
        int id;
        string name;
    }

    class Book
    {
        int id;
        string name;
        int publishYear;
        decimal price;
        string description;
    }

    class Genre
    {
        int id;
        string name;
        string description;
    }

    class DBConnect
    {
        private MySqlConnection connection;
        private string server;
        private string database;
        private string uid;
        private string password;

        //Constructor
        public DBConnect()
        {
            Initialize();
        }

        //Initialize values
        public void Initialize()
        {
            server = "localhost";
            database = "library";
            uid = "root";
            Console.Write("DB password: ");
            password = Console.ReadLine();
            Console.Clear();
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

        public void Insert(string query)
        {
            //open connection
            if (this.OpenConnection() == true)
            {
                //create command and assign the query and connection from the constructor
                MySqlCommand cmd = new MySqlCommand(query, connection);

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

        public List<string>[] Select(string query)
        {
            //Create a list to store the result
            List<string>[] list = new List<string>[3];
            list[0] = new List<string>();
            list[1] = new List<string>();
            list[2] = new List<string>();

            //Open connection
            if (this.OpenConnection() == true)
            {
                //Create Command
                MySqlCommand cmd = new MySqlCommand(query, connection);
                //Create a data reader and Execute the command
                MySqlDataReader dataReader = cmd.ExecuteReader();

                //Read the data and store them in the list
                while (dataReader.Read())
                {
                    list[0].Add(dataReader["id"] + "");
                    list[1].Add(dataReader["name"] + "");
                    list[2].Add(dataReader["age"] + "");
                }

                //close Data Reader
                dataReader.Close();

                //close Connection
                this.CloseConnection();

                //return list to be displayed
                return list;
            }
            else
            {
                return list;
            }
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
    }

    class Program
    {
        static DBConnect db;

        static void Main(string[] args)
        {
            db = new DBConnect();
            while(true)
            {
                int input = GetInput("What would you like to do? ", "Create", "Read", "Update", "Delete", "Exit");
                if(input == 0)
                {
                    input = GetInput("What would you like to create", "Book", "Author", "Genre");
                    if(input == 0)
                    {
                        Insert("book", "name", "publish year", "price", "description");
                    }
                    if(input == 1)
                    {
                        Insert("author", "name");
                    }
                    if (input == 2)
                    {
                        Insert("genre", "name", "description");
                    }
                }
                else if(input == 4)
                {
                    return;
                }
            }
        }

        static void Insert(string tableName, params string[] options)
        {
            Dictionary<string, string> queryEntries  = GetEntry("Provide some information about the new " + tableName + ":", options);
            StringBuilder query = new StringBuilder();
            query.Append("INSERT INTO ");
            query.Append(tableName);
            query.Append(" VALUES(null, ");
            int i = 0;
            foreach(string s in queryEntries.Values)
            {
                float ?f = null;
                float.TryParse(s, out f);
                if(f == null)
                {
                    query.Append('\'');
                    query.Append(s);
                    query.Append('\'');
                }
                else
                {
                    query.Append(s);
                }
                if (i <= queryEntries.Count - 2)
                {
                    query.Append(',');
                }
                i++;
            }
            query.Append(");");
            db.Insert(query.ToString());
        }

        static void Read(string tableName, params string[] options)
        {

        }

        static void Update(string entryName, string tableName, params string[] options)
        {
            Delete(entryName, tableName);
            Insert(tableName, options);
        }

        static void Delete(string entryName, string tableName)
        {
            StringBuilder query = new StringBuilder();
            Append(query, "DELETE FROM ", tableName, " WHERE 'name' = ", entryName, ";");
            db.Delete(query.ToString());
        }

        static void Append(StringBuilder sb, params string[] what)
        {
            foreach(string s in what)
            {
                sb.Append(s);
            }
        }

        static Dictionary<string, string> GetEntry(string top, params string[] options)
        {
            int option = 0;
            StringBuilder[] results = new StringBuilder[options.Length];
            for(int i = 0; i < options.Length; i++)
            {
                results[i] = new StringBuilder();
            }
            while (true)
            {
                Console.Clear();
                for (int i = 0; i < options.Length; i++)
                {
                    Console.Write(option == i ? "> " : "  ");
                    Console.Write(options[i]);
                    Console.Write(": ");
                    Console.Write(results[i]);
                    Console.WriteLine();
                }
                ConsoleKeyInfo key = Console.ReadKey();
                if(key.Key == ConsoleKey.Enter && option == options.Length - 1)
                {
                    Dictionary<string, string> entry = new Dictionary<string, string>();
                    for(int i = 0; i < options.Length; i++)
                    {
                        entry.Add(options[i], results[i].ToString());
                    }
                    return entry;
                }
                else if (key.Key == ConsoleKey.DownArrow || key.Key == ConsoleKey.Enter)
                {
                    option++;
                    option = option % options.Length;
                }
                else if (key.Key == ConsoleKey.UpArrow)
                {
                    option = option > 0 ? --option : options.Length - 1;
                }
                else if(key.Key == ConsoleKey.Backspace)
                {
                    if (results[option].Length > 0)
                    {
                        results[option].Remove(results[option].Length - 1, 1);
                    }
                }
                else
                {
                    results[option].Append(key.KeyChar);
                }
            }
        }

        static int GetInput(string top, params string[] options)
        {
            int option = 0;
            while(true)
            {
                Console.Clear();
                for (int i = 0; i < options.Length; i++)
                {
                    Console.Write(option == i ? "> " : "  ");
                    Console.Write(options[i]);
                    Console.WriteLine();
                }
                ConsoleKeyInfo key = Console.ReadKey();
                if(key.Key == ConsoleKey.DownArrow)
                {
                    option++;
                    option = option % options.Length;
                }
                else if(key.Key == ConsoleKey.UpArrow)
                {
                    option = option > 0 ? --option : options.Length - 1;
                }
                else if(key.Key == ConsoleKey.Enter)
                {
                    return option;
                }
            }
        }
    }
}
