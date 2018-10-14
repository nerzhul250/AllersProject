using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class DataManager
    {
        private String dataRoute;

        public Dictionary<string, Item> mapFromItemCodeToItem;
        private Dictionary<string, Customer> mapFromCustomerIdToCustomer;
        internal List<Transaction> listOfAllTransactions;

        static void Main(string[] args)
        {
        }

        public DataManager(String dr)
        {
            dataRoute = dr;
            mapFromCustomerIdToCustomer = new Dictionary<string, Customer>();
            mapFromItemCodeToItem = new Dictionary<string, Item>();
            listOfAllTransactions = new List<Transaction>();
            LoadData();
        }

        public int getTransactionsCount()
        {
            return listOfAllTransactions.Count;
        }
        public int getItemsCount()
        {
            return mapFromItemCodeToItem.Count;
        }
        public int getCustomersCount()
        {
            return mapFromCustomerIdToCustomer.Count;
        }
        public void LoadData()
        {
            LoadItems();
            LoadCustomers();
            LoadSales();
        }
        public void LoadSales()
        {
            string salesRoute = dataRoute + "Ventas.csv";
            StreamReader sr = new StreamReader(salesRoute);
            string auxiliarLine;
            List<string[]> auxBillContainer = new List<string[]>();
            auxiliarLine = sr.ReadLine();
            auxBillContainer.Add(sr.ReadLine().Split(';'));
            while ((auxiliarLine = sr.ReadLine()) != null)
            {
                string[] auxiliarLineInArray = auxiliarLine.Split(';');
                if (!auxiliarLineInArray[1].Equals(auxBillContainer[0][1]))
                {
                    string[] tranArray = auxBillContainer[0];
                    //There are anomalies with a customer
                    if (mapFromCustomerIdToCustomer.ContainsKey(tranArray[0]))
                    {
                        Customer cus = mapFromCustomerIdToCustomer[tranArray[0]];
                        string tid = tranArray[1];
                        string[] dateSegmented = tranArray[2].Split('-');
                        int year = Int32.Parse(dateSegmented[0]);
                        int month = Int32.Parse(dateSegmented[1]);
                        int day = Int32.Parse(dateSegmented[2]);
                        DateTime date = new DateTime(year, month, day);

                        Object[,] items = new Object[auxBillContainer.Count, 2];
                        for (int i = 0; i < auxBillContainer.Count; i++)
                        {
                            tranArray = auxBillContainer[i];
                            //This conditional is made because of 
                            //some anomalies in itemCodes, where a single cell might have two simultaneous values
                            if (mapFromItemCodeToItem.ContainsKey(tranArray[4]))
                            {
                                items[i, 0] = mapFromItemCodeToItem[tranArray[4]];
                                items[i, 1] = Int32.Parse(tranArray[5]);
                                mapFromItemCodeToItem[tranArray[4]].setPrice(Int32.Parse(tranArray[6]));
                            }
                        }
                        listOfAllTransactions.Add(new Transaction(tid, date, cus, items));
                    }
                    auxBillContainer = new List<string[]>();
                }
                auxBillContainer.Add(auxiliarLineInArray);
            }
        }
        public void LoadCustomers()
        {
            string customersRoute = dataRoute + "Clientes.csv";
            StreamReader sr = new StreamReader(customersRoute);
            string auxiliarLine;
            auxiliarLine = sr.ReadLine();
            while ((auxiliarLine = sr.ReadLine()) != null)
            {
                string[] auxiliarLineInArray = auxiliarLine.Split(';');
                //There are repeated customer codes
                if (!mapFromCustomerIdToCustomer.ContainsKey(auxiliarLineInArray[0]))
                {
                    mapFromCustomerIdToCustomer.Add(auxiliarLineInArray[0],
                        new Customer(auxiliarLineInArray[0], auxiliarLineInArray[1], auxiliarLineInArray[2], auxiliarLineInArray[3], auxiliarLineInArray[4]));
                }
            }
        }
        public void LoadItems()
        {
            string itemsRoute = dataRoute + "Articulos.csv";
            StreamReader sr = new StreamReader(itemsRoute);
            string auxiliarLine;
            auxiliarLine = sr.ReadLine();
            while ((auxiliarLine = sr.ReadLine()) != null)
            {
                string[] auxiliarLineInArray = auxiliarLine.Split(';');
                if (!mapFromItemCodeToItem.ContainsKey(auxiliarLineInArray[0])) {
                    Item a = new Item(auxiliarLineInArray[0], auxiliarLineInArray[1]);
                    mapFromItemCodeToItem.Add(auxiliarLineInArray[0], a);
                }
            }
        }
    }
}