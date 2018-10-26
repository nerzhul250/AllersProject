using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace AllersProject
{
    public partial class MenuPane : UserControl
    {
        public MenuPane()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                button1.Text = folderBrowserDialog1.SelectedPath;
                toolTip1.SetToolTip(this.button1,button1.Text);
            }
        }
    }
}
