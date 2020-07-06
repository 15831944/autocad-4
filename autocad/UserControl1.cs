using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace autocad
{
    public partial class UserControl1 : UserControl
    {
        public UserControl1()
        {
            InitializeComponent();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            // input lunghezze
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Prendo punti coordinate click

            Console.WriteLine(sender);
            Console.WriteLine(e);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // input spessore
        }
    }
}
