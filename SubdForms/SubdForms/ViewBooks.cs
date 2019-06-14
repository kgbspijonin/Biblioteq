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
    public partial class ViewBooks : Form {
        public ViewBooks()
        {
            InitializeComponent();
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void ViewBooks_Load(object sender, EventArgs e)
        {
            button1_Click(this, null);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Controls.Clear();
            List<List<string>> bookData;
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
            bookData = Program.Select("book", args);
            foreach (List<string> author in bookData)
            {
                Button b = new Button();
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
            Program.LoadNewForm(new InspectBook());
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.LoadForm("Main");
        }
    }
}
