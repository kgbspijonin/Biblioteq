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
    public partial class Login : Form {
        public Login()
        {
            InitializeComponent();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void Connect_Click(object sender, EventArgs e)
        {
            this.Hide();
            System.Security.SecureString secure = new System.Security.SecureString();
            foreach (char c in Password.Text.ToCharArray())
            {
                secure.AppendChar(c);
            }
            Program.connection = new DBConnect(Username.Text, secure);
            Program.LoadForm("Main");
        }
    }
}
