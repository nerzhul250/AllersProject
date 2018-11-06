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
            richTextBox1.Font = new Font("Consolas", 10f, FontStyle.Bold);
            richTextBox1.BackColor = Color.AliceBlue;
            string[] words =
            {
                "Relevancia promedio de las predicciones: 30%",
                "Confianza promedio de las predicciones: 73%",
                "Si el cliente compra los productos:",
                "-Jeringa bestial",
                "-Bata japonesa",
                "Compraria los productos:",
                "-Bata samurai, minima cantidad: 4, maxima cantidad: 5",
                "-Jeringa apaciguante, minima cantidad: 65, maxima cantidad: 66",
                "Ganancia minima: $63.000",
                "Ganancia mazima: $70.000",
                "Confianza de la prediccion: 40%",
                "Relevancia de la prediccion: 83.33%"
            };
            Color[] colors =
            {
                Color.Aqua,
                Color.Aqua,
                Color.CadetBlue,
                Color.Cornsilk,
                Color.Cornsilk,
                Color.Gold,
                Color.HotPink,
                Color.HotPink,
                Color.Lavender,
                Color.Lavender,
                Color.Lavender,
                Color.Lavender
            };
            for (int i = 0; i < words.Length; i++)
            {
                string word = words[i];
                Color color = colors[i];
                {
                    richTextBox1.SelectionBackColor = color;
                    richTextBox1.AppendText(word);
                    richTextBox1.SelectionBackColor = Color.AliceBlue;
                    richTextBox1.AppendText("\n");
                }
            }
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
