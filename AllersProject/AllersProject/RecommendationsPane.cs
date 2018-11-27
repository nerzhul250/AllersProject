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
    public partial class RecommendationsPane : UserControl
    {
        public RecommendationsPane()
        {
            InitializeComponent();
        }

        private void RecommendationsPane_Load(object sender, EventArgs e)
        {
        }
        delegate void SetTextCallback(string text);

        public void setRecommendations(string text) {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.dataGridView1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(setRecommendations);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                string[]recom = text.Split('\n');
                int customersNum = int.Parse(recom[0]);
                int recomNum = int.Parse(recom[1]);
                dataGridView1.Columns.Clear();
                dataGridView1.Columns.Add("Cliente","Cliente");
                for (int i = 0; i < recomNum; i++)
                {
                    dataGridView1.Columns.Add("Recomendacion" + (i + 1), "Recomendacion"+(i+1));
                    dataGridView1.Columns.Add("CantidadMenosQueCompraDelPromedio" + (i + 1), "CantidadMenosQueCompraDelPromedio" + (i + 1));
                }
                int num = (recom.Length - 2) / (2+recomNum*2);
                for (int i = 0; i < num; i++)
                {
                    int pos = i * (2+recomNum*2) + 2;
                    int rowId = dataGridView1.Rows.Add();
                    DataGridViewRow row = dataGridView1.Rows[rowId];
                    row.Cells["Cliente"].Value = recom[pos++];
                    for (int j = pos; j <recomNum*2+pos ; j+=2)
                    {
                        row.Cells["Recomendacion" + (((j-pos)/2)+1)].Value = recom[j];
                        row.Cells["CantidadMenosQueCompraDelPromedio" + (((j - pos)/ 2)+1)].Value = recom[j+1];
                    }
                }
            }
        }
    }
}
