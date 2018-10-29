using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Modelo.services;
using System.Diagnostics;
using ZedGraph;
using MakarovDev.ExpandCollapsePanel;
namespace AllersProject
{
    public partial class Form1 : Form
    {
        private ServiceProvider model;
        public Form1()
        {
            InitializeComponent();
        }
        private double minSGeneral;
        private double minCGeneral;
        private void Form1_Load(object sender, EventArgs e)
        {
            customerPane1.main = this;
            menuPane1.main = this;
        }
        //METHODS
        public void initializeServiceProvider(String route)
        {
            try
            {
                String r= route+"\\";
            ServiceProvider model1 = new ServiceProvider(r);
                model = model1;
            }catch(Exception e)
            {
                MessageBox.Show("Ruta no especificada correctamente");
            }
        }
        
        public void modifyGeneralPredictions(double minSup, double minConfidence)
        {
            minSGeneral = minSup;
            minCGeneral = minConfidence;
            List<Prediction> predictions = model.GetGeneralPredictions(minSGeneral, minCGeneral);
            double AverageRelevance = 0;
            double averageConfidence = 0;
            string text = "";
            MessageBox.Show(minSGeneral+"");
            MessageBox.Show(minCGeneral + "");
            MessageBox.Show(predictions.Count+"");
            foreach (var p in predictions)
            {
                averageConfidence += p.confidence;
                AverageRelevance += p.relevance;
                string antecedent = "If you buy these items:";
                string consequent = "You will probably buy those: ";
                for(int i = 0; i < p.antecedent.Length; i++)
                {
                    antecedent += ", " + p.antecedent[i].itemName;
                }
                for (int i = 0; i < p.consequent.Length; i++)
                {
                    consequent += ", " + p.consequent[i].itemName;
                }
                text += antecedent+"\n"+consequent+"\n---------------------------------------\n";
            }
            averageConfidence /= predictions.Count*100;
            AverageRelevance /= predictions.Count*100;
            text = "Average relevance: " + AverageRelevance+""+ "%" + "\n" + "Average confidence: " + averageConfidence+"" + "%\n"+text;
            customerPredictionPane1.setText(text);
        }
        //TODO
        public void modifyGroupOfCLients(String NoGroups)
        {
            int numberOfGroups = Int32.Parse(NoGroups);
            List<Recommendation>res=model.GetItemsCustomersMightBuyMoreButBuyFew(numberOfGroups,100,2,3);
            ZedGraph.ZedGraphControl zgc = zedGraphControl1;
            GraphPane myPane = zgc.GraphPane;

            // Set the titles
            myPane.Title.Text = "Clientes";
            myPane.XAxis.Title.Text = "X";
            myPane.YAxis.Title.Text = "Y";

            // Populate a PointPairList
            PointPairList list = new PointPairList();
            int control = 0;
            foreach (Recommendation re in res)
            {
                double x = re.customer2dRepresentation[0];
                double y = re.customer2dRepresentation[1];
                if (re.groupColor != control) {
                    control = re.groupColor;
                    // Add the curve
                    LineItem myCurve = myPane.AddCurve("G"+(control-1)+"", list, Color.Black, SymbolType.Diamond);
                    // Don't display the line (This makes a scatter plot)
                    myCurve.Line.IsVisible = false;
                    // Hide the symbol outline
                    myCurve.Symbol.Border.IsVisible = false;
                    // Fill the symbol interior with color
                    myCurve.Symbol.Fill = new Fill(Color.FromArgb(((control*300)%129)+127, ((control * 750)%129)+127, ((control * 400)%129)+127));
                    list = new PointPairList();
                    list.Add(x, y);
                }
                else {
                    list.Add(x, y);
                }
            }
            

            // Fill the background of the chart rect and pane
            myPane.Chart.Fill = new Fill(Color.White, Color.LightGoldenrodYellow, 45.0f);
            myPane.Fill = new Fill(Color.White, Color.SlateGray, 45.0f);

            zgc.AxisChange();
            zgc.GraphPane.Legend.IsVisible = false;
        }
        public void predictionsByCostumer(String customerId,double sop,double conf)
        {
            if (model == null)
            {
                MessageBox.Show("En la primer pestaña debe ingresar los parámetros");
                return;
            }
            List<Prediction> predictions = model.GetPredictionsOfCustomer(customerId,sop,conf);
            double AverageRelevance = 0;
            double averageConfidence = 0;   
            string text = "";
            Debug.WriteLine(predictions.Count);
            foreach (Prediction p in predictions)
            {
                averageConfidence += p.confidence;
                AverageRelevance += p.relevance;
                string antecedent = "If the client buy these items:";
                string consequent = "he will probably buy those: ";
                for (int i = 0; i < p.antecedent.Length; i++)
                {
                    antecedent += ", " + p.antecedent[i].itemName;
                }
                for (int i = 0; i < p.consequent.Length; i++)
                {
                    consequent += ", " + p.consequent[i].itemName;
                }
                text += antecedent + "\n" + consequent + "\n---------------------------------------\n";
            }
            averageConfidence /= predictions.Count*100;
            AverageRelevance /= predictions.Count*100;
            text = "Average relevance: " + AverageRelevance+ "%" + "\n" + "Average confidence: " + averageConfidence + "%\n"+text;
            customerPane1.modifyPredictions(text);
        }
        //THIS METHOD DEPENDS ON THE GENERAL SUPPORT AND CONFIDENCE
        public void getRelevantCustomers()
        {
            Dictionary<String, List<Prediction>> dic = model.getRelevantCustomersByHisAveragePurchases(minSGeneral,minCGeneral);
            MessageBox.Show("a");
            foreach (var n in dic.Keys)
            {
                List<Prediction> predictions = dic[n];
                double AverageRelevance = 0;
                double averageConfidence = 0;
                string text = "";

                foreach (Prediction p in predictions)
                {
                    averageConfidence += p.confidence;
                    AverageRelevance += p.relevance;
                    string antecedent = "If the client buy these items:";
                    string consequent = "he will probably buy those: ";
                    for (int i = 0; i < p.antecedent.Length; i++)
                    {
                        antecedent += ", " + p.antecedent[i].itemName;
                    }
                    for (int i = 0; i < p.consequent.Length; i++)
                    {
                        consequent += ", " + p.consequent[i].itemName;
                    }
                    text += antecedent + "\n" + consequent + "\n---------------------------------------\n";
                }
                averageConfidence /= predictions.Count * 100;
                AverageRelevance /= predictions.Count * 100;
                text = "Average relevance: " + AverageRelevance + "%" + "\n" + "Average confidence: " + averageConfidence + "%\n" + text;
                CustomerPredictionPane c1 = new CustomerPredictionPane();
                c1.setText(text);
                ExpandCollapsePanel ex = new ExpandCollapsePanel();
                //ex.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
                //ex.ButtonSize = MakarovDev.ExpandCollapsePanel.ExpandCollapseButton.ExpandButtonSize.Normal;
                //ex.ButtonStyle = MakarovDev.ExpandCollapsePanel.ExpandCollapseButton.ExpandButtonStyle.MagicArrow;
                ex.Controls.Add(c1);
                //ex.ExpandedHeight = 376;
                //ex.IsExpanded = true;
                //ex.Location = new System.Drawing.Point(3, 3);
                //ex.Name = n;
                //ex.Size = new System.Drawing.Size(715, 376);
                //ex.TabIndex = 1;
                ex.Text = "Codigo: " + n;
                //ex.UseAnimation = true;
                customerPane1.addControlToTheAdvanceControl(ex);

            }
        }
        //END_METHODS

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void customerPane1_Load(object sender, EventArgs e)
        {

        }

        private void customerPane1_Load_1(object sender, EventArgs e)
        {

        }

        private void customerPane1_Load_2(object sender, EventArgs e)
        {

        }

        private void customerPredictionPane1_Load(object sender, EventArgs e)
        {

        }

        private void menuPane1_Load(object sender, EventArgs e)
        {

        }

        private void gruposClientes_Click(object sender, EventArgs e)
        {

        }

        private void customerPane1_Load_3(object sender, EventArgs e)
        {

        }

        private void menu_Click(object sender, EventArgs e)
        {

        }
    }
}
