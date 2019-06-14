using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Reflection;

namespace SubdForms {
    public partial class InspectBook : Form {
        public InspectBook()
        {
            InitializeComponent();
        }

        private void InspectBook_Load(object sender, EventArgs e)
        {
            if (Program.formParams != null)
            {
                foreach (KeyValuePair<string, string> t in Program.formParams)
                {
                    RichTextBox rt = (RichTextBox)GetType().GetField(t.Key, BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this);
                    rt.Text = t.Value;
                }
            }
            List<List<string>> bookData = Program.Select("book", new List<Tuple<bool, string, string>>() { new Tuple<bool, string, string>(false, "name", name.Text) });
            richTextBox1.Text = bookData[0][3];
            richTextBox2.Text = bookData[0][2];
            richTextBox3.Text = bookData[0][4];
            List<List<string>> authors = Program.GetBookJoined(int.Parse(bookData[0][0]), "author");
            StringBuilder authorText = new StringBuilder();
            for(int i = 0; i < authors.Count; i++)
            {
                authorText.Append(authors[i][0]);
                if(i < authors.Count - 1)
                {
                    authorText.Append('\n');
                }
            }
            this.authors.Text = authorText.ToString();
            List<List<string>> genres = Program.GetBookJoined(int.Parse(bookData[0][0]), "genre");
            StringBuilder genresText = new StringBuilder();
            for (int i = 0; i < genres.Count; i++)
            {
                genresText.Append(genres[i][0]);
                if (i < genres.Count - 1)
                {
                    genresText.Append('\n');
                }
            }
            this.genres.Text = genresText.ToString();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void authors_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            string s;
            Program.formParams.TryGetValue("name", out s);
            Program.DeleteBook(s);
            Program.formParams = null;
            Program.LoadForm("Main");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            string s;
            Program.formParams.TryGetValue("name", out s);
            Program.UpdateBook(s, name.Text, richTextBox2.Text, richTextBox1.Text, richTextBox3.Text);
            Program.formParams = null;
            Program.LoadForm("Main");
        }

        private void name_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
