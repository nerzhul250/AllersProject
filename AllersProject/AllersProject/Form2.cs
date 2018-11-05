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
    public partial class Form2 : Form
    {
        public Form1 main { get; set; }


        public Form2()
        {
            InitializeComponent();
        }

        private void expandCollapsePanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            customerPredictionPane1.main = this.main;
            customerPredictionPane1.tipoPanel = Form1.PESTANHA_CLIENTE;
        }

        private void customerPredictionPane1_Load(object sender, EventArgs e)
        {

        }

        private void txtClientInfo_TextChanged(object sender, EventArgs e)
        {

        }

        public string GetSpecificClients()
        {
            return customerPredictionPane1.getSpecificCodes();
        }

        public void SetCustomerId(string id)
        {
            customerPredictionPane1.customerId = id;
        }

        public void SetConfSup (double conf, double sup)
        {
            customerPredictionPane1.minConf = conf;
            customerPredictionPane1.minSup = sup;
        }
    }
}
