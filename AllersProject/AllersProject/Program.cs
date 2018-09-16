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
            DataManager dm = new DataManager();
            Debug.WriteLine("ItsBeenASuccesThereAre " + dm.getTransactionsCount() + "Transactions!");
            Debug.WriteLine(dm.getCustomersCount() + "Customers!");
            Debug.WriteLine(dm.getItemsCount() + "Items!");
            Debug.WriteLine(dm.getItemSetsCount() + "Common Item Sets!");
        }
    }
}
