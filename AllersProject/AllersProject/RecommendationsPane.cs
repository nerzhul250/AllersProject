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
        public void setRecommendations(string text) {
            richTextBox1.Text = text;
        }
    }
}
