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
    public partial class CreateAuthor : Form {
        public CreateAuthor()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void AuthorName_TextChanged(object sender, EventArgs e)
        {

        }

        private void Create_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.CreateAuthor(AuthorName.Text);
            Program.LoadForm("Main");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.LoadForm("Main");
        }
    }
}
