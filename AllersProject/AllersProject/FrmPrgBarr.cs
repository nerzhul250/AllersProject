using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AllersProject
{
    public partial class FrmPrgBarr : Form
    {
        public FrmPrgBarr()
        {
            InitializeComponent();
        }
        // expose a method to set the ProgressBar #value
        public void SetProgressBarValue(int pbValue)
        {
            progressBar1.Value = pbValue;
        }
        public void SetLabelText(string text)
        {
            label1.Text = text;
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {

        }
    }
}
