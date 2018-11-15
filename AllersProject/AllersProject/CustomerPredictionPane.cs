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
    public partial class CustomerPredictionPane : UserControl
    {
        public Form1 main { get; set; }

        public char tipoPanel { get; set; }

        public string customerId { get; set; }

        public double minConf { get; set; }

        public double minSup { get; set; }

        public CustomerPredictionPane()
        {
            InitializeComponent();
        }

        private void CustomerPredictionPane_Load(object sender, EventArgs e)
        {

        }
        delegate void SetTextCallback(string text);
        public void setText(String text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.richTextBox1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(setText);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                richTextBox1.ResetText();
                richTextBox1.AppendText(text);
            }
        }

        public string getSpecificCodes()
        {
            return textCodigosProductos.Text;
        }


        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (tipoPanel == Form1.PESTANHA_PRED)
            {
                try
                {
                    main.modifyGeneralPredictions(0, 0, true,"");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }

            }
            else if (tipoPanel == Form1.PESTANHA_CLIENTE)
            {
                try
                {
                    main.predictionsByCostumer(customerId, minSup, minConf, true,"");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tipoPanel == Form1.PESTANHA_PRED)
            {
                try
                {
                    main.modifyGeneralPredictions(main.minSGeneral, main.minCGeneral, false, comboBox1.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }else if (tipoPanel == Form1.PESTANHA_CLIENTE)
            {
                try
                {
                    main.predictionsByCostumer(customerId, minSup, minConf, false,comboBox1.Text);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
