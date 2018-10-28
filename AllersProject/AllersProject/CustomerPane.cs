using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AllersProject
{
    public partial class CustomerPane : UserControl
    {
        public Form1 main;
        public CustomerPane()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void CustomerPane_Load(object sender, EventArgs e)
        {
            advancedFlowLayoutPanel1.AutoScroll = true;
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try {
            main.predictionsByCostumer(textBox1.Text);

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void expandCollapsePanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void customerPredictionPane1_Load(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
