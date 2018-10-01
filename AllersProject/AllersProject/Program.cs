using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
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
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
            DataManager data = new DataManager("../../../Datos/");
            AssociationAnalyzer aA = new AssociationAnalyzer(data,5,0.0005,0.005,5);
            Debug.WriteLine("ItsBeenASuccesThereAre " + data.getTransactionsCount() + "Transactions!");
            Debug.WriteLine(data.getCustomersCount() + "Customers!");
            Debug.WriteLine(data.getItemsCount() + "Items!");
            Debug.WriteLine(aA.getBinaryTransactions().Count + "binaryTransactions!");
            //aA.GenerateFrequentItemSets();
            Debug.WriteLine("EMPEZANDOApriori------------------------");
            Stopwatch sw = Stopwatch.StartNew();
            List<List<long>>list = aA.GenerateFrequentItemSetsApriori();
            Debug.WriteLine("Numero de listas: "+list.Count);
            int sum = 0;
            foreach(List<long> lis in list)
            {
                Debug.WriteLine("Conjuntos de items frecuentes con "+aA.CountSetBits(lis[0])+" Items ");
                for (int i = 0; i < lis.Count(); i++)
                {
                    String a = "";
                    List<Item> items = aA.BinaryItemSetToObjectItemSet(lis[i]);
                    for (int j = 0; j < items.Count(); j++)
                    {
                        a = a + "//" + items[j].itemName;
                    }
                    Debug.WriteLine(a);
                }
                sum += lis.Count();
            }
            Debug.WriteLine("Numero de conjuntos de items frecuentes: "+sum);
            Debug.WriteLine("Tiempo de ejecucion en milisegundos "+sw.ElapsedMilliseconds);
            sw.Stop();
            Debug.WriteLine("TERMINANDOApriori------------------------");
            //RARARARAR
            //Debug.WriteLine("EMPEZANDOBRUTE------------------------");
            //sw = Stopwatch.StartNew();
            //List<Item[]> items = aA.GenerateFrequentItemSets();
            //Debug.WriteLine("Numero de conjuntos de items frecuentes: " + items.Count);
            //Debug.WriteLine("Tiempo de ejecucion en milisegundos " + sw.ElapsedMilliseconds);
            //sw.Stop();
            //Debug.WriteLine("TERMINANDOBRUTE------------------------");
        }
    }
}
