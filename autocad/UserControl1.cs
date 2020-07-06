using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Private.InfoCenter;

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

            this.checkIfNumber(Lunghezza_input);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Controllo che non ci siano campi vuoti e che siano numeri
            string spessore = Spessore_input.Text;
            string lunghezza = Lunghezza_input.Text;

            // * System.Diagnostics.Debug.WriteLine(e);
            if (string.IsNullOrEmpty(spessore) || string.IsNullOrEmpty(lunghezza))
            {
                // richiedi l'inserimento del punto base
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            // input spessore

            this.checkIfNumber(Spessore_input);
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void checkIfNumber(TextBox box)
        {
            string tString = box.Text;
            if (tString.Trim() == "") return;
            for (int i = 0; i < tString.Length; i++)
            {
                if (!char.IsNumber(tString[i]))
                {
                    MessageBox.Show("inserisci un numero");
                    box.Text = "";
                    return;
                }
            }
        }
    }
}
