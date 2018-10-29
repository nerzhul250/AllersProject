using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace AllersProject
{
    public partial class MenuPane : UserControl
    {
        public Form1 main;
        public MenuPane()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                button1.Text = folderBrowserDialog1.SelectedPath;
                toolTip1.SetToolTip(this.button1,button1.Text);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            main.initializeServiceProvider(button1.Text);
            try
            {
                main.modifyGeneralPredictions(Double.Parse(textBox1.Text), Double.Parse(textBox2.Text));
            }catch(Exception ex)
            {
                MessageBox.Show("Minsup o Minconfidence erróneos");
                MessageBox.Show(ex.Message);
            }
            //try
            //{
            main.modifyGroupOfCLients(textBox3.Text);
               
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Número de grupos inválidos");
            //}
        }
    }
}
