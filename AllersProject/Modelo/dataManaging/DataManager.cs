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
        public const int MINIMUM_NUMBER_OF_TIMES_AN_ITEM_IS_PURCHASED = 10;
        public const int MINIMUM_NUMBER_OF_CUSTOMER_PURCHASES = 10;

        public String dataRoute { get; set; }


        public Dictionary<string, Item> mapFromItemCodeToItem { get; set; }
        public Dictionary<string, Customer> mapFromCustomerIdToCustomer { get; set; }
        public List<Transaction> listOfAllTransactions { get; set; }

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
            //There might be, customers in the map not appearing in transactions
            //There might be, items in the map not appearing in transactions
            //Probability, low.
            pruneData();
        }
        
        private void pruneData()
        {
            pruneItems();
            pruneCustomers();
        }

        private void pruneCustomers()
        {
            Dictionary<Customer, int> dict = new Dictionary<Customer, int>();
            List<Customer> commons = new List<Customer>();
            foreach (Transaction t in listOfAllTransactions)
            {
                if (dict.ContainsKey(t.customer))
                {
                    dict[t.customer]++;
                }
                else
                {
                    dict.Add(t.customer, 1);
                    commons.Add(t.customer);
                }
            }
            commons = commons.OrderBy(it => dict[it]).TakeWhile(it => dict[it] < MINIMUM_NUMBER_OF_CUSTOMER_PURCHASES).ToList();
            foreach (Customer cus in commons)
            {
                mapFromCustomerIdToCustomer.Remove(cus.id);
            }
            
            for (int i = 0; i < listOfAllTransactions.Count; i++)
            {
                Transaction t = listOfAllTransactions[i];
                foreach (Customer cus in commons) {
                    if (t.customer.Equals(cus)) {
                        listOfAllTransactions.Remove(t);
                        i--;
                        break;
                    }
                }
            }
        }

        private void pruneItems()
        {
            List<Item> commons = new List<Item>();
            Dictionary<Item, int> dict = new Dictionary<Item, int>();
            foreach (Transaction t in listOfAllTransactions)
            {
                foreach (Item i in t.MapFromItemToQuantity.Keys)
                {
                    if (dict.ContainsKey(i))
                    {
                        dict[i]++;
                    }
                    else
                    {
                        dict.Add(i, 1);
                        commons.Add(i);
                    }
                }
            }
            commons = commons.OrderBy(it => dict[it]).TakeWhile(it => dict[it] < MINIMUM_NUMBER_OF_TIMES_AN_ITEM_IS_PURCHASED).ToList();
            foreach (Item it in commons)
            {
                mapFromItemCodeToItem.Remove(it.ItemCode);
            }
            for (int i = 0; i < listOfAllTransactions.Count; i++)
            {
                Transaction t = listOfAllTransactions[i];
                foreach (Item it in commons)
                {
                    if (t.MapFromItemToQuantity.ContainsKey(it))
                    {
                        t.MapFromItemToQuantity.Remove(it);
                    }
                }
                if (t.MapFromItemToQuantity.Count == 0)
                {
                    listOfAllTransactions.Remove(t);
                    i--;
                }
            }
        }

        public DataManager(){
            mapFromCustomerIdToCustomer = new Dictionary<string, Customer>();
            mapFromItemCodeToItem = new Dictionary<string, Item>();
            listOfAllTransactions = new List<Transaction>();
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