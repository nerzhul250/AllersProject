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
        // This event handler is where the time-consuming work is done.
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            worker.ReportProgress(1);
            main.initializeServiceProvider(button1.Text);
            worker.ReportProgress(2);
            try
            {
                main.modifyGeneralPredictions(Double.Parse(textBox1.Text), Double.Parse(textBox2.Text));
            }
            catch (Exception ex)
            {
                MessageBox.Show("Minsup o Minconfidence erróneos");
                MessageBox.Show(ex.Message);
            }
            worker.ReportProgress(3);
            try
            {
                main.modifyGroupOfCLients(textBox3.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Número de grupos inválidos");
            }
            worker.ReportProgress(4);
            //try
            //{
                main.getRelevantCustomers();
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show(ex.Message);
            //}
            //if (worker.CancellationPending == true)
            //{
            //    e.Cancel = true;
            //    break;
            //}
        }
        //  This event handler updates the progress.
        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            switch (e.ProgressPercentage)
            {
                case 1:
                    label5.Text = "Cargando datos";
                    break;
                case 2:
                    label5.Text = "Generando predicciones generales";
                    break;
                case 3:
                    label5.Text = "Agrupando clientes similares";
                    break;
                case 4:
                    label5.Text = "Generando predicciones por cliente";
                    break;
                default:
                    break;
            }
        }

        // This event handler deals with the results of the background operation.
        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                label5.Text = "Canceled!";
            }
            else if (e.Error != null)
            {
                label5.Text = "Error: " + e.Error.Message;
            }
            else
            {
                label5.Text = "Estado de la aplicacion: Con datos";
                label5.ForeColor = Color.Green;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {

            if (backgroundWorker1.IsBusy != true)
            {
                // Start the asynchronous operation.
                backgroundWorker1.RunWorkerAsync();
            }

        }
    }
}
