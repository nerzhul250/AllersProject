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
            AssociationAnalyzer aA = new AssociationAnalyzer(data,28,0.05,0.05,3);
            Debug.WriteLine("ItsBeenASuccesThereAre " + data.getTransactionsCount() + "Transactions!");
            Debug.WriteLine(data.getCustomersCount() + "Customers!");
            Debug.WriteLine(data.getItemsCount() + "Items!");
            Debug.WriteLine(aA.getBinaryTransactions().Count + "binaryTransactions!");
            aA.GenerateFrequentItemSets();
            //Debug.WriteLine("EMPEZANDO------------------------");
            //Debug.WriteLine(aA.GenerateFrequentItemSetsApriori(aA.CommonItems()).Count);
            //Debug.WriteLine("TERMINANDO------------------------");
        }
    }
}
