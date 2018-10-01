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
            AssociationAnalyzer aA = new AssociationAnalyzer(data,63,0.0005,0.005,63);
            Debug.WriteLine("ItsBeenASuccesThereAre " + data.getTransactionsCount() + "Transactions!");
            Debug.WriteLine(data.getCustomersCount() + "Customers!");
            Debug.WriteLine(data.getItemsCount() + "Items!");
            Debug.WriteLine(aA.getBinaryTransactions().Count + "binaryTransactions!");
            //aA.GenerateFrequentItemSets();
            Debug.WriteLine("EMPEZANDO------------------------");
            int starTime = DateTime.Now.Second;
            List<List<long>>list = aA.GenerateFrequentItemSetsApriori();
            Debug.WriteLine(list.Count);
            foreach(List<long> lis in list)
            {
                Debug.WriteLine(lis.Count());
            }
            Debug.WriteLine(DateTime.Now.Second - starTime + "");

            Debug.WriteLine("TERMINANDO------------------------");
        }
    }
}
