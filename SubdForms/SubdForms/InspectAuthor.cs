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
    public partial class InspectAuthor : Form {
        public InspectAuthor()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void InspectAuthor_Load(object sender, EventArgs e)
        {
            if(Program.formParams != null)
            {
                foreach(KeyValuePair<string, string> t in Program.formParams)
                {
                    RichTextBox rt = (RichTextBox) GetType().GetField(t.Key, BindingFlags.NonPublic | BindingFlags.Instance).GetValue(this);
                    rt.Text = t.Value;
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            string s;
            Program.formParams.TryGetValue("name", out s);
            Program.UpdateAuthor(s, name.Text);
            Program.formParams = null;
            Program.LoadForm("Main");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            string s;
            Program.formParams.TryGetValue("name", out s);
            Program.DeleteAuthor(s);
            Program.formParams = null;
            Program.LoadForm("Main");
        }
    }
}
