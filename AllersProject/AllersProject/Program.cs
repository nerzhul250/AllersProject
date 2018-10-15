using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using System.Windows.Forms;
using Estructura;
using Modelo;

namespace AllersProject
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());
            DataManager data = new DataManager("../../../Datos/");
            //<<<<<<< HEAD
            //            AssociationAnalyzer aA = new AssociationAnalyzer(data,20,0.0005,0.005,20);
            //            Debug.WriteLine("ItsBeenASuccesThereAre " + data.getTransactionsCount() + "Transactions!");
            //            Debug.WriteLine(data.getCustomersCount() + "Customers!");
            //            Debug.WriteLine(data.getItemsCount() + "Items!");
            //            Debug.WriteLine(aA.getBinaryTransactions().Count + "binaryTransactions!");
            //            //aA.GenerateFrequentItemSets();
            //            Debug.WriteLine("EMPEZANDOApriori------------------------");
            //            Stopwatch sw = Stopwatch.StartNew();
            //            List<List<long>>list = aA.GenerateFrequentItemSetsApriori();
            //            Debug.WriteLine("Numero de listas: "+list.Count);
            //            int sum = 0;
            //            foreach(List<long> lis in list)
            //            {
            //                Debug.WriteLine("Conjuntos de items frecuentes con "+aA.CountSetBits(lis[0])+" Items ");
            //                for (int i = 0; i < lis.Count(); i++)
            //                {
            //                    String a = "";
            //                    List<Item> items = aA.BinaryItemSetToObjectItemSet(lis[i]);
            //                    for (int j = 0; j < items.Count(); j++)
            //                    {
            //                        a = a + "//" + items[j].itemName;
            //                    }
            //                    Debug.WriteLine(a);
            //                }
            //                sum += lis.Count();
            //            }
            //            Debug.WriteLine("Numero de conjuntos de items frecuentes: "+sum);
            //            Debug.WriteLine("Tiempo de ejecucion en milisegundos "+sw.ElapsedMilliseconds);
            //            sw.Stop();
            //            Debug.WriteLine("TERMINANDOApriori------------------------");

            //AssociationAnalyzerApriori aA = new AssociationAnalyzerApriori(data, 100, 0.01, 0.05, 63);
            //Debug.WriteLine("ItsBeenASuccesThereAre " + data.getTransactionsCount() + "Transactions!");
            //Debug.WriteLine(data.getCustomersCount() + "Customers!");
            //Debug.WriteLine(data.getItemsCount() + "Items!");
            //Debug.WriteLine(aA.getBinaryTransactions().Count + "binaryTransactions!");
            //Debug.WriteLine("EMPEZANDOApriori------------------------");
            //Stopwatch sw = Stopwatch.StartNew();
            //List<List<BigInteger>> list = aA.GenerateFrequentItemSetsApriori();
            //Debug.WriteLine("Numero de listas: " + list.Count);
            //int sum = 0;
            //foreach (List<BigInteger> lis in list)
            //{
            //    Debug.WriteLine("Conjuntos de items frecuentes con " + aA.CountSetBits(lis[0]) + " Items ");
            //    for (int i = 0; i < lis.Count(); i++)
            //    {
            //        String a = "";
            //        List<Item> items = aA.BinaryItemSetToObjectItemSet(lis[i]);
            //        for (int j = 0; j < items.Count(); j++)
            //        {
            //            a = a + "//" + items[j].itemName;
            //        }
            //        Debug.WriteLine(a);
            //    }
            //    sum += lis.Count();
            //}
            //Debug.WriteLine("Numero de conjuntos de items frecuentes: " + sum);
            //Debug.WriteLine("Tiempo de ejecucion en milisegundos " + sw.ElapsedMilliseconds);
            //            //Debug.WriteLine("REGLAS------------------>");
            //            //aA.AprioriRuleGeneration(list);
            //            //foreach (Tuple<BigInteger,BigInteger> rule in aA.rules)
            //            //{
            //            //    String a = "";
            //            //    List<Item> items = aA.BinaryItemSetToObjectItemSet(rule.Item1);
            //            //    for (int j = 0; j < items.Count(); j++)
            //            //    {
            //            //        a = a + "//" + items[j].itemName;
            //            //    }
            //            //    Debug.WriteLine(a+"---->");
            //            //    a = "";
            //            //    List<Item> items2 = aA.BinaryItemSetToObjectItemSet(rule.Item2);
            //            //    for (int j = 0; j < items2.Count(); j++)
            //            //    {
            //            //        a = a + "//" + items2[j].itemName;
            //            //    }
            //            //    Debug.WriteLine(a + "//");
            //            //}
            //            //sw.Stop();
            //            //Debug.WriteLine("TERMINANDOApriori------------------------");
            //>>>>>>> a924833252ab5600b68569554a0bd0ead1dcc40c
            //            //RARARARAR
            //            //Debug.WriteLine("EMPEZANDOBRUTE------------------------");
            //            //sw = Stopwatch.StartNew();
            //            //List<Item[]> items = aA.GenerateFrequentItemSets();
            //            //Debug.WriteLine("Numero de conjuntos de items frecuentes: " + items.Count);
            //            //Debug.WriteLine("Tiempo de ejecucion en milisegundos " + sw.ElapsedMilliseconds);
            //            //sw.Stop();
            //            //Debug.WriteLine("TERMINANDOBRUTE------------------------");

            //Debug.WriteLine("EMPEZANDO FP--------------");
            //Stopwatch sw = Stopwatch.StartNew();
            //AssociationAnalyzerFPGrowth asso = new AssociationAnalyzerFPGrowth(data, 0.05);
            //List<List<string>> frequents = asso.frequentItemSets();
            //Debug.WriteLine("Ellapsed miliseconds = " + sw.ElapsedMilliseconds);
            //foreach (List<string> l in frequents)
            //{
            //    Debug.WriteLine("FREQUENT");
            //    foreach (string x in l)
            //    {
            //        Debug.WriteLine(x);
            //    }
            //}
            //Debug.WriteLine("FIN FP-------------");

            FPTree prueba;

            List<List<string>> transacc = new List<List<string>>();
            List<string> tranUniq = new List<string> { "b", "a", "c" };
            transacc.Add(tranUniq);
            List<string> tranUniq2 = new List<string> { "c", "a", "d" };
            transacc.Add(tranUniq2);
            List<string> tranUniq3 = new List<string> { "a", "d" };
            transacc.Add(tranUniq3);
            List<string> tranUniq4 = new List<string> { "c", "e" };
            transacc.Add(tranUniq4);
            List<string> tranUniq5 = new List<string> { "b", "f" };
            transacc.Add(tranUniq5);
            List<string> tranUniq6 = new List<string> { "d", "c" };
            transacc.Add(tranUniq6);
            prueba = new FPTree(transacc, 0.1);

            List<List<string>> list = prueba.FindFrequentItemsets();
            foreach (List<string> list2 in list)
            {
                Debug.WriteLine("AQUI");
                foreach (string x in list2)
                {
                    Debug.Write(x + " ");
                }
            }
        }




    }
}