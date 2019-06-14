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
    public partial class ViewGenres : Form {
        public ViewGenres()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            List<List<string>> authorData;
            List<Tuple<bool, string, string>> args = new List<Tuple<bool, string, string>>();
            if (checkBox1.Checked)
            {
                args.Add(new Tuple<bool, string, string>(false, "name", textBox1.Text));
            }
            if(checkBox2.Checked)
            {
                args.Add(new Tuple<bool, string, string>(false, "description", richTextBox1.Text));
            }
            if (Ascending.Checked)
            {
                args.Add(new Tuple<bool, string, string>(false, "orderBy", "ASC"));
            }
            if (Descending.Checked)
            {
                args.Add(new Tuple<bool, string, string>(false, "orderBy", "DESC"));
            }
            authorData = Program.Select("genre", args);
            foreach (List<string> author in authorData)
            {
                Button b = new Button();
                b.Size = new Size(300, b.Size.Height);
                b.Click += B_Click;
                b.Text = author[1];
                b.Location = new Point(0, 20 * panel1.Controls.Count);
                panel1.Controls.Add(b);
            }
        }

        private void B_Click(object sender, EventArgs e)
        {
            Program.formParams = new Dictionary<string, string> { { "name", ((Button)sender).Text } };
            this.Hide();
            Program.LoadNewForm(new InspectGenre());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.LoadForm("Main");
        }

        private void ViewGenres_Load(object sender, EventArgs e)
        {
            button1_Click(this, null);
        }
    }
}
