using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MakarovDev.ExpandCollapsePanel;
using System.Diagnostics;

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
        public void modifyPredictions(string text)
        {
            throw new NotImplementedException();
        }
        private void button1_Click_1(object sender, EventArgs e)
        {
            double conf;
            double sup;
            try
            {
                try
                {
                    sup = Double.Parse(textBox2.Text);
                } catch (Exception ex)
                {
                    throw new Exception("El soporte no tiene el formato adecuado (Decimal)");
                }

                try
                {
                    conf = Double.Parse(textBox3.Text);
                }
                catch (Exception ex)
                {
                    throw new Exception("La confianza no tiene el formato adecuado (Decimal)");
                }
                main.predictionsByCostumer(textBox1.Text, sup, conf, false,"");

            } catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void addControlToTheAdvanceControl(ExpandCollapsePanel ad)
        {
            advancedFlowLayoutPanel1.Controls.Add(ad);
        }
        public void clearAdvancedLayout()
        {
            advancedFlowLayoutPanel1.Controls.Clear();
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

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void combQuantity_SelectedIndexChanged(object sender, EventArgs e)
        {
            main.ChangeRange(Int32.Parse(combQuantity.SelectedItem.ToString()));
        }
    }
}
