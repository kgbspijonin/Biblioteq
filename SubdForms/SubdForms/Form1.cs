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
    public partial class Form1 : Form {
        public Form1()
        {
            InitializeComponent();
        }

        private void CreateAuthor_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.LoadForm("CreateAuthor");
        }

        private void CreateGenre_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.LoadForm("CreateGenre");
        }

        private void CreateBook_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.LoadForm("CreateBook");
        }

        private void ViewAuthors_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.LoadNewForm(new ViewAuthors());
        }

        private void ViewGenres_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.LoadNewForm(new ViewGenres());
        }

        private void ViewBooks_Click(object sender, EventArgs e)
        {
            this.Hide();
            Program.LoadNewForm(new ViewBooks());
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
    }
}
