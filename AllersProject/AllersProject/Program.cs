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
            AssociationAnalyzer aA = new AssociationAnalyzer(data,1,0.0005,0.005,1);
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
                sum += lis.Count();
            }
            Debug.WriteLine("Numero de conjuntos de items frecuentes: "+sum);
            Debug.WriteLine("Tiempo de ejecucion en milisegundos "+sw.ElapsedMilliseconds);
            sw.Stop();
            Debug.WriteLine("TERMINANDOApriori------------------------");
            //RARARARAR
            Debug.WriteLine("EMPEZANDOBRUTE------------------------");
            sw = Stopwatch.StartNew();
            List<Item[]> items = aA.GenerateFrequentItemSets();
            Debug.WriteLine("Numero de conjuntos de items frecuentes: " + items.Count);
            Debug.WriteLine("Tiempo de ejecucion en milisegundos " + sw.ElapsedMilliseconds);
            sw.Stop();
            Debug.WriteLine("TERMINANDOBRUTE------------------------");
        }
    }
}
