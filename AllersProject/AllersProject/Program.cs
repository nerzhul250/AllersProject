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

            AssociationAnalyzer aA = new AssociationAnalyzer(new DataManager("../../../Datos/"),28,0.05,0.05,3);
            Debug.WriteLine("ItsBeenASuccesThereAre " + aA.data.getTransactionsCount() + "Transactions!");
            Debug.WriteLine(aA.data.getCustomersCount() + "Customers!");
            Debug.WriteLine(aA.data.getItemsCount() + "Items!");
        }
    }
}
