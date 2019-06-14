using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SubdForms {
    public partial class CreateBook : Form {
        public CreateBook()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void AuthorName_TextChanged(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void Create_Click(object sender, EventArgs e)
        {
            this.Hide();
            string[] authors = richTextBox2.Text.Split('\n');
            string[] genres = richTextBox3.Text.Split('\n');
            Program.CreateBook(AuthorName.Text, textBox1.Text, textBox2.Text, richTextBox1.Text, new List<string>(authors), new List<string>(genres));
            List<List<string>> bookData = Program.Select("book", new List<Tuple<bool, string, string>>() { new Tuple<bool, string, string>(false, "name", AuthorName.Text) });
            int id = int.Parse(bookData[0][0]);
            foreach (string author in authors)
            {
                List<List<string>> authorData = Program.Select("author", new List<Tuple<bool, string, string>>() { new Tuple<bool, string, string>(false, "name", author) });
                int aId = -1;
                if(authorData.Count > 0)
                {
                    int.TryParse(authorData[0][0], out aId);
                    if (aId > 0)
                    {
                        Program.CreateAuthorBookConnection(aId, id);
                    }
                }
            }
            foreach(string genre in genres)
            {
                List<List<string>> genreData = Program.Select("genre", new List<Tuple<bool, string, string>>() { new Tuple<bool, string, string>(false, "name", genre) });
                int gId = int.Parse(genreData[0][0]);
                if(genreData.Count > 0)
                {
                    int.TryParse(genreData[0][0], out gId);
                    if(gId > 0)
                    {
                        Program.CreateBookGenreConnection(id, gId);
                    }
                }
            }
            Program.LoadForm("Main");
        }

        private void CreateBook_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.LoadForm("Main");
        }
    }
}
