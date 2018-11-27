using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using ZedGraph;
using MakarovDev.ExpandCollapsePanel;
using Modelo.services;
using Modelo;

namespace AllersProject
{
    public partial class Form1 : Form
    {
        private ServiceProvider model;

        public const char PESTANHA_PRED = 'P';
        public const char PESTANHA_CLIENTE = 'C';

        public Form2 window { get; set; }

        public Form1()
        {
            InitializeComponent();
        }
        public double minSGeneral;
        public double minCGeneral;
        private void Form1_Load(object sender, EventArgs e)
        {
            customerPane1.main = this;
            menuPane1.main = this;
            customerPredictionPane1.main = this;
            customerPredictionPane1.tipoPanel = PESTANHA_PRED;
        }
        //METHODS
        public void initializeServiceProvider(String route)
        {
            try
            {
                String r = route + "\\";
                ServiceProvider model1 = new ServiceProvider(r);
                model = model1;
            }
            catch (Exception e)
            {
                throw new Exception("Ruta no especificada correctamente");
            }
        }
        public List<Prediction> filterPredictions(List<Prediction> p, String category)
        {

            List<Prediction> copy = new List<Prediction>();
            if (category.Equals("Confianza"))
            {
                p.OrderByDescending(x => x.confidence).ToList().ForEach(x => copy.Add(x));

            }
            else if (category.Equals("Relevancia"))
            {

                p.OrderByDescending(x => x.relevance).ToList().ForEach(x => copy.Add(x));

            }
            else if (category.Equals("Ingreso minimo"))
            {
                p.Select(x => new { pre = x, min = x.consequent.Select(y => y.price).Zip(x.minimumQuantity, (a, b) => a * b).Sum() }).OrderByDescending(x => x.min).ToList().ForEach(x => copy.Add(x.pre));
            }
            else if (category.Equals("Ingreso maximo"))
            {
                p.Select(x => new { pre = x, max = x.consequent.Select(y => y.price).Zip(x.maximumQuantity, (a, b) => a * b).Sum() }).OrderByDescending(x => x.max).ToList().ForEach(x => copy.Add(x.pre));
            }
            else if (category.Equals("Cantidad minima"))
            {
                p.Select(x => new { pre = x, min = x.minimumQuantity.Average() }).OrderByDescending(x => x.min).ToList().ForEach(x => copy.Add(x.pre));
            }
            else if (category.Equals("Cantidad maxima"))
            {
                p.Select(x => new { pre = x, min = x.maximumQuantity.Average() }).OrderByDescending(x => x.min).ToList().ForEach(x => copy.Add(x.pre));
            }
            else if (category.Equals(""))
            {
                return p;
            }
            return copy;
        }
        public void modifyGeneralPredictions(double minSup, double minConfidence, bool specific, string category)
        {
            minSGeneral = minSup;
            minCGeneral = minConfidence;
            try
            {
                List<Prediction> predictions;
                string specificCodes = customerPredictionPane1.getSpecificCodes();
                if (specific && !specificCodes.Equals(""))
                {
                    string[] specificCodesArray = specificCodes.Split(' ');
                    if (model == null)
                    {
                        throw new Exception("Primero se deben de cargar datos");
                    }
                    predictions = model.GetPredictionsFromCodeItems(specificCodesArray);
                    Debug.WriteLine(specificCodes.Length + " " + specificCodes[0] + ".");
                }
                else if (!specific)
                {
                    Debug.WriteLine("Aqui");
                    Debug.WriteLine(minSGeneral);
                    Debug.WriteLine(minCGeneral);
                    predictions = model.GetGeneralPredictions(minSGeneral, minCGeneral);
                    Debug.WriteLine("Joder");
                }
                else
                {
                    predictions = model.Predictions;
                    Debug.WriteLine("What's your problem");
                }
                Debug.WriteLine("x");
                predictions = filterPredictions(predictions, category);
                Debug.WriteLine("y");
                double AverageRelevance = 0;
                double averageConfidence = 0;
                string text = "";
                foreach (var p in predictions)
                {
                    Debug.WriteLine("Waaaa");
                    averageConfidence += p.confidence;
                    AverageRelevance += p.relevance;
                    string antecedent = "";
                    string consequent = "";
                    double minimaGananciaAdicional = 0;
                    double maximaGananciaAdicional = 0;
                    for (int i = 0; i < p.antecedent.Length; i++)
                    {
                        antecedent += ", " + p.antecedent[i].itemName + " (" + p.antecedent[i].ItemCode + ")";
                    }
                    for (int i = 0; i < p.consequent.Length; i++)
                    {
                        consequent += ", " + p.consequent[i].itemName + " (" + p.consequent[i].ItemCode + ")";
                        minimaGananciaAdicional += p.consequent[i].price * p.minimumQuantity[i];
                        maximaGananciaAdicional += p.consequent[i].price * p.maximumQuantity[i];
                    }
                    text += antecedent + "\n" + consequent + "\n";
                    text += (p.relevance * 100).ToString("0.##") + "%\n";
                    text += (p.confidence * 100).ToString("0.##") + "%\n";
                    text += minimaGananciaAdicional.ToString("C2") + "\n";
                    text += maximaGananciaAdicional.ToString("C2") + "\n";
                    text += "---------------------------------------\n";
                }
                averageConfidence /= predictions.Count;
                AverageRelevance /= predictions.Count;
                text = "Relevancia promedio: " + (AverageRelevance * 100).ToString("0.##") + "" + "%" + "\n" + "Confiabilidad promedio: " + (averageConfidence * 100).ToString("0.##") + "" + "%\n" + text;
                customerPredictionPane1.setText(text);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        internal void ChangeRange(int v)
        {
            getRelevantCustomers(v);
        }

        //TODO
        public void modifyGroupOfCLients(int numberOfGroups, int itemsToRecommend)
        {
            List<Recommendation> res = model.GetItemsCustomersMightBuyMoreButBuyFew(numberOfGroups, 100, 2, itemsToRecommend);
            string recom = res.Count + "\n" + res[0].recommendations.Count + "\n";
            for (int i = res.Count - 1; i >= 0; i--)
            {
                recom += res[i].customer.id + "\n";
                for (int j = 0; j < res[i].recommendations.Count; j++)
                {
                    recom += res[i].recommendations[j].Item1.itemName + "\n" + (res[i].recommendations[j].Item2).ToString("0.##") + "\n";
                }
                recom += "----------------------------------------------------------------------------------------------------\n";
            }
            recommendationsPane1.setRecommendations(recom);

            res = res.OrderBy(re => re.groupColor).ToList();

            ZedGraph.ZedGraphControl zgc = zedGraphControl1;
            zgc.GraphPane.CurveList.Clear();
            zgc.GraphPane.GraphObjList.Clear();
            GraphPane myPane = zgc.GraphPane;

            // Set the titles
            myPane.Title.Text = "Grupos de Clientes";
            myPane.XAxis.Title.Text = "X";
            myPane.YAxis.Title.Text = "Y";

            // Populate a PointPairList
            PointPairList list = new PointPairList();
            int control = 0;
            for (int i = 0; i < res.Count; i++)
            {
                double x = res[i].customer2dRepresentation[0];
                double y = res[i].customer2dRepresentation[1];
                Debug.WriteLine(res[i].groupColor + " " + control);
                if (res[i].groupColor != control)
                {
                    control = res[i].groupColor;
                    // Add the curve
                    LineItem myCurve = myPane.AddCurve("G" + (control - 1) + "", list, Color.Black, SymbolType.Diamond);
                    // Don't display the line (This makes a scatter plot)
                    myCurve.Line.IsVisible = false;
                    // Hide the symbol outline
                    myCurve.Symbol.Border.IsVisible = false;
                    // Fill the symbol interior with color
                    myCurve.Symbol.Fill = new Fill(Color.FromArgb((((control - 1) * 7) % 129) + 100, (((control - 1) * 101) % 129) + 100, (((control - 1) * 300) % 129) + 100));

                    list = new PointPairList();
                    PointPair IKnowThisVariableNameIsLongButIDontCare = new PointPair(x, y);
                    IKnowThisVariableNameIsLongButIDontCare.Tag = res[i];
                    list.Add(IKnowThisVariableNameIsLongButIDontCare);
                }
                else
                {
                    PointPair IKnowThisVariableNameIsLongButIDontCare = new PointPair(x, y);
                    IKnowThisVariableNameIsLongButIDontCare.Tag = res[i];
                    list.Add(IKnowThisVariableNameIsLongButIDontCare);
                }
            }

            LineItem myCurve2 = myPane.AddCurve("G" + (control) + "", list, Color.Black, SymbolType.Diamond);
            // Don't display the line (This makes a scatter plot)
            myCurve2.Line.IsVisible = false;
            // Hide the symbol outline
            myCurve2.Symbol.Border.IsVisible = false;
            // Fill the symbol interior with color
            myCurve2.Symbol.Fill = new Fill(Color.FromArgb((((control) * 7) % 129) + 100, (((control) * 101) % 129) + 100, (((control) * 300) % 129) + 100));



            // Fill the background of the chart rect and pane
            myPane.Chart.Fill = new Fill(Color.White, Color.LightGoldenrodYellow, 45.0f);
            myPane.Fill = new Fill(Color.White, Color.SlateGray, 45.0f);

            zgc.AxisChange();
            zgc.GraphPane.Legend.IsVisible = false;
        }

        public void predictionsByCostumer(String customerId, double sop, double conf, bool specific, String category)
        {
            //Form2 window2 = null;
            List<Prediction> predictions = model.GetPredictionsOfCustomer(customerId, sop, conf);
            if (specific)
            {
                string[] ProductsCode = window.GetSpecificClients().Split(' ');
                if (ProductsCode.Length != 1 || !ProductsCode[0].Equals(""))
                {
                    predictions = model.GetPredictionsFromCodeItemsSpecificClient(ProductsCode, customerId, sop, conf);
                    if (predictions.Count == 0)
                    {
                        throw new Exception("No se encontraron predicciones con los items especificados");
                    }

                }
            }
            else
            {

                if (window == null) 
                {
                    window = new Form2();
                    window.main = this;
                    window.Visible = true;
                    window.SetCustomerId(customerId);
                    window.SetConfSup(conf, sop);

                }
                if (model == null)
                {
                    MessageBox.Show("En la primer pestaña debe ingresar los parámetros");
                    return;
                }

            }

            Debug.WriteLine(predictions.Count);
            predictions = filterPredictions(predictions, category);
            double AverageRelevance = 0;
            double averageConfidence = 0;
            StringBuilder text = new StringBuilder();
            foreach (Prediction p in predictions)
            {
                averageConfidence += p.confidence;
                AverageRelevance += p.relevance;
                StringBuilder antecedent = new StringBuilder();
                //antecedent.Append("Si el cliente compra estos items:");
                StringBuilder consequent = new StringBuilder();
                //consequent.Append("Probablemente comprara estos: ");
                double minimaGananciaAdicional = 0;
                double maximaGananciaAdicional = 0;
                for (int i = 0; i < p.antecedent.Length; i++)
                {
                    antecedent.Append(", ");
                    antecedent.Append(p.antecedent[i].itemName + " (" + p.antecedent[i].ItemCode + ")");
                }
                for (int i = 0; i < p.consequent.Length; i++)
                {
                    consequent.Append(", ");
                    consequent.Append(p.consequent[i].itemName + " (" + p.consequent[i].ItemCode + ")");
                    minimaGananciaAdicional += p.consequent[i].price * p.minimumQuantity[i];
                    maximaGananciaAdicional += p.consequent[i].price * p.maximumQuantity[i];
                }
                text.Append(antecedent);
                text.Append("\n");
                text.Append(consequent);
                text.Append("\n");
                //text.Append("Relevancia de la prediccion: ");
                text.Append((p.relevance * 100).ToString("0.##"));
                text.Append("%\n");
                //text.Append("Confiabilidad de la prediccion: ");
                text.Append((p.confidence * 100).ToString("0.##"));
                text.Append("%\n");
                //text.Append("Ganancia minima adicional: ");
                text.Append(minimaGananciaAdicional.ToString("C2"));
                text.Append("\n");
                //text.Append("Ganancia maxima adicional: ");
                text.Append(maximaGananciaAdicional.ToString("C2"));
                text.Append("\n");
                text.Append("---------------------------------------\n");
            }
            averageConfidence /= predictions.Count;
            AverageRelevance /= predictions.Count;
            text.Insert(0, "%");
            text.Insert(0, (AverageRelevance * 100).ToString("0.##"));
            //text.Insert(0, "Relevancia promedio: ");
            text.Insert(0, "\n");
            text.Insert(0, "%");
            text.Insert(0, (averageConfidence * 100).ToString("0.##"));
            //text.Insert(0, "Confianza promedio: ");
            text.Insert(0, "\n");
            window.customerPredictionPane1.setText(text.ToString());
            if (!specific)
            {
                try
                {
                    DataManager dm = model.GetDataBy(customerId);
                    Customer cus = dm.mapFromCustomerIdToCustomer[customerId];
                    string info = "Id: " + cus.id + "\n" + "Región: " + cus.regionName + "\n" + "Ciudad: " + cus.cityName;
                    window.txtClientInfo.Text = info;
                    //if (window2 != null) window2.Close();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }

            }


        }
        //THIS METHOD DEPENDS ON THE GENERAL SUPPORT AND CONFIDENCE

        delegate void CustomerPaneClearCustomersCallback();
        public void CustomerPaneClearCustomers()
        {
            customerPane1.clearAdvancedLayout();
        }
        public void getRelevantCustomers(int q)
        {
            Debug.WriteLine("ENTRO");
            if (model != null)
            {
                Debug.WriteLine("ENTRO X2");
                CustomerPaneClearCustomersCallback kaka = new CustomerPaneClearCustomersCallback(CustomerPaneClearCustomers);
                this.Invoke(kaka, new object[] { });
                Dictionary<String, List<Prediction>> dic = model.getRelevantCustomersByHisAveragePurchases(minSGeneral, minCGeneral, q);
                int counter = 0;
                Debug.WriteLine("ENTRO X3");
                foreach (var n in dic.Keys)
                {
                    Debug.WriteLine("Aqui es: " + n);
                    if (counter == q)
                    {
                        break;
                    }
                    if (dic[n].Count != 0)
                    {
                        counter++;
                        List<Prediction> predictions = dic[n];
                        double AverageRelevance = 0;
                        double averageConfidence = 0;
                        StringBuilder text = new StringBuilder();
                        foreach (Prediction p in predictions)
                        {
                            averageConfidence += p.confidence;
                            AverageRelevance += p.relevance;
                            StringBuilder antecedent = new StringBuilder();
                            //antecedent.Append("Si el cliente compra estos items:");
                            StringBuilder consequent = new StringBuilder();
                            //consequent.Append("Probablemente comprara estos: ");
                            double minimaGananciaAdicional = 0;
                            double maximaGananciaAdicional = 0;
                            for (int i = 0; i < p.antecedent.Length; i++)
                            {
                                antecedent.Append(", ");
                                antecedent.Append(p.antecedent[i].itemName + " (" + p.antecedent[i].ItemCode + ")");
                            }
                            for (int i = 0; i < p.consequent.Length; i++)
                            {
                                consequent.Append(", ");
                                consequent.Append(p.consequent[i].itemName + " (" + p.consequent[i].ItemCode + ")");
                                minimaGananciaAdicional += p.consequent[i].price * p.minimumQuantity[i];
                                maximaGananciaAdicional += p.consequent[i].price * p.maximumQuantity[i];
                            }
                            text.Append(antecedent);
                            text.Append("\n");
                            text.Append(consequent);
                            text.Append("\n");
                            //text.Append("Relevancia de la prediccion: ");
                            text.Append((p.relevance * 100).ToString("0.##"));
                            text.Append("%\n");
                            //text.Append("Confiabilidad de la prediccion: ");
                            text.Append((p.confidence * 100).ToString("0.##"));
                            text.Append("%\n");
                            //text.Append("Ganancia minima adicional: ");
                            text.Append(minimaGananciaAdicional.ToString("C2"));
                            text.Append("\n");
                            //text.Append("Ganancia maxima adicional: ");
                            text.Append(maximaGananciaAdicional.ToString("C2"));
                            text.Append("\n");
                            text.Append("---------------------------------------\n");
                        }
                        averageConfidence /= predictions.Count;
                        AverageRelevance /= predictions.Count;
                        text.Insert(0, "%");
                        text.Insert(0, (AverageRelevance * 100).ToString("0.##"));
                        //text.Insert(0, "Relevancia promedio: ");
                        text.Insert(0, "\n");
                        text.Insert(0, "%");
                        text.Insert(0, (averageConfidence * 100).ToString("0.##"));
                        //text.Insert(0, "Confianza promedio: ");
                        text.Insert(0, "\n");
                        CrearExpandibleCallback d = new CrearExpandibleCallback(CrearExpandible);
                        this.Invoke(d, new object[] { text.ToString(), n });
                    }
                }
            }
        }
        delegate void CrearExpandibleCallback(string text, string n);
        private void CrearExpandible(string text, string n)
        {
            CustomerPredictionPane c1 = new CustomerPredictionPane();
            c1.setText(text);
            c1.Height -= 50;
            c1.Width -= 50;
            ExpandCollapsePanel ex = new ExpandCollapsePanel();
            ex.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            ex.ButtonSize = MakarovDev.ExpandCollapsePanel.ExpandCollapseButton.ExpandButtonSize.Normal;
            ex.ButtonStyle = MakarovDev.ExpandCollapsePanel.ExpandCollapseButton.ExpandButtonStyle.MagicArrow;
            ex.Controls.Add(c1);
            ex.ExpandedHeight = 405;
            ex.IsExpanded = false;
            ex.Location = new System.Drawing.Point(3, 3);
            ex.Name = n;
            ex.Size = new System.Drawing.Size(1000, 376);
            ex.TabIndex = 1;
            ex.Text = "Codigo: " + n;
            ex.UseAnimation = false;
            ex.ButtonStyle = ExpandCollapseButton.ExpandButtonStyle.MagicArrow;
            customerPane1.addControlToTheAdvanceControl(ex);
        }
        private void zedGraphControl1_MouseClick(object sender, MouseEventArgs e)
        {
            object nearestObject;
            int index;
            this.zedGraphControl1.GraphPane.FindNearestObject(new PointF(e.X, e.Y), this.CreateGraphics(), out nearestObject, out index);
            if (nearestObject != null && nearestObject.GetType() == typeof(LineItem))
            {
                PointPairList ppl = (PointPairList)(((LineItem)nearestObject).Points);
                PointPair p = null;
                double dis = double.MaxValue;
                for (int i = 0; i < ppl.Count; i++)
                {
                    double act = (ppl[i].X - e.X) * (ppl[i].X - e.X) +
                        (ppl[i].Y - e.Y) * (ppl[i].Y - e.Y);
                    if (act < dis) { p = ppl[i]; dis = act; }
                }
                Recommendation re = (Recommendation)p.Tag;
                CustomerInfoForm cif = new CustomerInfoForm(re);
                cif.ShowDialog();
                zedGraphControl1.Invalidate();
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

        private void zedGraphControl1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
