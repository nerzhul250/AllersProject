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
            richTextBox1.Font = new Font("Consolas", 18f, FontStyle.Bold);
        }
        delegate void SetTextCallback(string text);

        public void setRecommendations(string text) {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.richTextBox1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(setRecommendations);
                this.Invoke(d, new object[] { text });
            }
            else
            {
                this.richTextBox1.Text = text;
            }
        }
    }
}
