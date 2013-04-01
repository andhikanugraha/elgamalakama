using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ElGamalApplication
{
    public partial class UserInterface : Form
    {
        public UserInterface()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (!validate_generate_key())
            {
                MessageBox.Show("X and G must be less than p");
                return;
            }

            // Generate Key Button
            SaveFileDialog fileBrowser = new SaveFileDialog();

            if (fileBrowser.ShowDialog() == DialogResult.OK)
            {
                string fileName = fileBrowser.FileName;
                Key key = new Key((long)key_generate_p.Value, (long)key_generate_g.Value, 
                    (long)key_generate_x.Value);

                key.saveToFile(fileName);
            }
        }

        private bool validate_generate_key()
        {
            return key_generate_g.Value < key_generate_p.Value && key_generate_x.Value < key_generate_p.Value;
        }
    }
}
