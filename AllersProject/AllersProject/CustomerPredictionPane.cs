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
        public CustomerPredictionPane()
        {
            InitializeComponent();
        }

        private void CustomerPredictionPane_Load(object sender, EventArgs e)
        {
            richTextBox1.Font = new Font("Consolas", 18f, FontStyle.Bold);
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
    }
}
