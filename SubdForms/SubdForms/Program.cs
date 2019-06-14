using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace SubdForms {
    static class Program {
        static public DBConnect connection;
        static public Dictionary<string, Form> forms;
        static public Dictionary<string, string> formParams = null;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 


        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            forms = new Dictionary<string, Form>()
                {
                    { "Login", new Login() },
                    { "Main", new Form1() },
                    { "CreateAuthor", new CreateAuthor() },
                    { "CreateGenre", new CreateGenre() },
                    { "CreateBook", new CreateBook() },
                    { "ViewAuthors", new ViewAuthors() },
                    { "ViewGenres", new ViewGenres() },
                    { "InspectAuthor", new InspectAuthor() },
                    { "InspectGenre", new InspectGenre() },
                    { "ViewBooks", new ViewBooks() },
                    { "InspectBook", new InspectBook() }
                };
            Form f;
            forms.TryGetValue("Login", out f);
            Application.Run(f);
        }

        static public Form LoadForm(string form, bool shouldLoadThings = false)
        {
            Form f;
            forms.TryGetValue(form, out f);
            if(f != null)
            {
                f.Show();
            }
            return f;
        }

        static public Form LoadNewForm(Form form)
        {
            form.Show();
            return form;
        }

        static public void CreateAuthor(string name)
        {
            connection.Prepared("INSERT INTO author(name) VALUES (@name);", new List<Tuple<string, string>>() { new Tuple<string, string>("@name", name) });
        }

        static public void CreateGenre(string name, string description)
        {
            connection.Prepared("INSERT INTO genre(name, description) VALUES (@name, @description)", new List<Tuple<string, string>>() { new Tuple<string, string>("@name", name), new Tuple<string, string>("@description", description) });
        }

        static public void CreateBook(string name, string publishYear, string price, string description, List<string> authors, List<string> genres)
        {
            connection.Prepared("INSERT INTO book(name, publishYear, price, description) VALUES (@name, @publishYear, @price, @description)",
                new List<Tuple<string, string>>() {
                    new Tuple<string, string>("@name", name),
                    new Tuple<string, string>("@publishYear", publishYear),
                    new Tuple<string, string>("@price", price),
                    new Tuple<string, string>("@description", description)
                });
        }
        
        static public void UpdateAuthor(string oldName, string newName)
        {
            connection.Prepared("UPDATE author SET author.name = @newName WHERE author.name LIKE @oldName",
                new List<Tuple<string, string>>()
                {
                    new Tuple<string, string>("oldName", oldName),
                    new Tuple<string, string>("newName", newName)
                });
        }

        static public void DeleteAuthor(string name)
        {
            connection.Prepared("DELETE FROM author WHERE author.name LIKE @name;",
                new List<Tuple<string, string>>()
                {
                    new Tuple<string, string>("name", name)
                });
        }

        static public void UpdateGenre(string oldName, string newName, string newDescription)
        {
            connection.Prepared("UPDATE genre SET genre.name = @newName, genre.description = @newDescription WHERE genre.name LIKE @oldName",
                new List<Tuple<string, string>>()
                {
                    new Tuple<string, string>("oldName", oldName),
                    new Tuple<string, string>("newName", newName),
                    new Tuple<string, string>("newDescription", newDescription)
                });
        }

        static public void DeleteGenre(string name)
        {
            connection.Prepared("DELETE FROM genre WHERE genre.name LIKE @name;",
                new List<Tuple<string, string>>()
                {
                    new Tuple<string, string>("name", name)
                });
        }

        static public void DeleteBook(string name)
        {
            connection.Prepared("DELETE FROM book WHERE book.name LIKE @name;",
            new List<Tuple<string, string>>()
                {
                    new Tuple<string, string>("name", name)
                });
        }

        static public void UpdateBook(string oldName, string newName, string publishYear, string price, string description)
        {
            connection.Prepared("UPDATE book SET book.name = @newName, book.publishYear = @publishYear, book.price = @price, book.description = @description WHERE book.name LIKE @oldName", new List<Tuple<string, string>>
            {
                new Tuple<string, string>("newName", newName),
                new Tuple<string, string>("publishYear", publishYear),
                new Tuple<string, string>("price", price),
                new Tuple<string, string>("description", description),
                new Tuple<string, string>("oldName", oldName)
            });
        }

        static public void CreateAuthorBookConnection(int authorID, int bookId)
        {
            connection.Prepared("INSERT INTO authorBookConnection(authorId, bookId) VALUES(" + authorID + ", " + bookId + ");", new List<Tuple<string, string>>());
        }

        static public void CreateBookGenreConnection(int bookId, int genreId)
        {
            connection.Prepared("INSERT INTO bookGenreConnection(bookId, genreId) VALUES(" + bookId + ", " + genreId + ");", new List<Tuple<string, string>>());
        }

        static public List<List<string>> GetBookJoined(int id, string tableName)
        {
            StringBuilder sb = new StringBuilder();
            string s = "SELECT a.name FROM book " +
                "INNER JOIN " + (tableName == "author" ? "authorBookConnection" : "bookGenreConnection") + " o on id=" + id + " AND id=o.bookId " +
                "INNER JOIN " + tableName + " a on a.id = " + (tableName == "author" ? "o.authorId;" : "o.genreId;");
            return connection.Select(s, null);
        }

        static public List<List<string>> Select(string table, List<Tuple<bool, string, string>> args, List<Tuple<string, string>> selectArgs = null)
        {
            StringBuilder sb = new StringBuilder();
            bool hasWhere = false;
            selectArgs = new List<Tuple<string, string>>();
            for (int i = 0; i < args.Count; i++)
            {
                if(args[i].Item2.Equals("orderBy"))
                {
                    sb.Append(" ORDER BY " + table + ".name " + args[i].Item3);
                    if(args.Count == 1)
                    {
                        hasWhere = false;
                    }
                }
                else {
                    hasWhere = true;
                    sb.Append(" " + table + "." + args[i].Item2 + (args[i].Item1 ? " = @" : " LIKE @") + args[i].Item2 + " ");
                    selectArgs.Add(new Tuple<string, string>(args[i].Item2 , args[i].Item1 ? "" : "%" + args[i].Item3 + (args[i].Item1 ? "" : "%")));
                    if (i < args.Count - 1)
                    {
                        sb.Append("AND");
                    }
                }
            }
            string s = "SELECT * FROM " + table + (hasWhere ? " WHERE " : " ") + sb.ToString() + ";";
            var result = connection.Select(s, selectArgs);
            return result;
        }
    }
}
