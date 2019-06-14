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
    public partial class ViewAuthors : Form {
        public ViewAuthors()
        {
            InitializeComponent();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void Ascending_CheckedChanged(object sender, EventArgs e)
        {
            
        }

        private void Descending_CheckedChanged(object sender, EventArgs e)
        {
            
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
            if (Ascending.Checked)
            {
                args.Add(new Tuple<bool, string, string>(false, "orderBy", "ASC"));
            }
            if (Descending.Checked)
            {
                args.Add(new Tuple<bool, string, string>(false, "orderBy", "DESC"));
            }
            authorData = Program.Select("author", args);
            foreach(List<string> author in authorData)
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
            Program.formParams = new Dictionary<string, string>{ { "name", ((Button)sender).Text } };
            this.Hide();
            Program.LoadNewForm(new InspectAuthor());
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.LoadForm("Main");
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void ViewAuthors_Load(object sender, EventArgs e)
        {
            button1_Click(this, null);
        }
    }
}
