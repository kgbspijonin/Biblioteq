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
    public partial class InspectGenre : Form {
        public InspectGenre()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            string s;
            Program.formParams.TryGetValue("name", out s);
            Program.DeleteGenre(s);
            Program.formParams = null;
            Program.LoadForm("Main");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            string s;
            Program.formParams.TryGetValue("name", out s);
            Program.UpdateGenre(s, name.Text, richTextBox1.Text);
            Program.formParams = null;
            Program.LoadForm("Main");
        }

        private void InspectGenre_Load(object sender, EventArgs e)
        {
            if (Program.formParams != null)
            {
                foreach (KeyValuePair<string, string> t in Program.formParams)
                {
                    RichTextBox rt = (RichTextBox)GetType().GetField(t.Key, BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this);
                    rt.Text = t.Value;
                }
            }
            List<List<string>> genreData = Program.Select("genre", new List<Tuple<bool, string, string>>() { new Tuple<bool, string, string>(false, "name", name.Text) });
            richTextBox1.Text = genreData[0][2];
        }

        private void name_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
