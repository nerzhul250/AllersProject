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
        public const String dataRoute = "../../../Datos/";

        private Dictionary<string,Item> mapFromItemCodeToItem;
        private Dictionary<string,Customer> mapFromCustomerIdToCustomer;
        private Dictionary<int, Item> mapFromNumberToItem;
        private List<Transaction> listOfAllTransactions;
        private List<Item[]> frequentItemSets;

        static void Main(string[] args)
        {
        }
        public DataManager() {
            mapFromCustomerIdToCustomer = new Dictionary<string, Customer>();
            mapFromItemCodeToItem = new Dictionary<string, Item>();
            mapFromNumberToItem = new Dictionary<int, Item>();
            listOfAllTransactions = new List<Transaction>();
            frequentItemSets = new List<Item[]>();
            LoadData();
        }

        public int getTransactionsCount() {
            return listOfAllTransactions.Count;
        }
        public int getItemsCount() {
            return mapFromItemCodeToItem.Count;
        }
        public int getCustomersCount() {
            return mapFromCustomerIdToCustomer.Count;
        }
        public int getItemSetsCount()
        {
            return frequentItemSets.Count;
        }
        public void LoadData() {
            LoadItems();
            LoadCustomers();
            LoadSales();
            GenerateFrequentItemSets(3, 0.2);
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
                if (!auxiliarLineInArray[1].Equals(auxBillContainer[0][1])) { 
                    string[] tranArray = auxBillContainer[0];
                    //There are anomalies with a customer
                    if (mapFromCustomerIdToCustomer.ContainsKey(tranArray[0])) {
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

        public void LoadItems() {
            string itemsRoute=dataRoute+"Articulos.csv";
            StreamReader sr = new StreamReader(itemsRoute);
            string auxiliarLine;
            auxiliarLine = sr.ReadLine();
            while ((auxiliarLine = sr.ReadLine()) != null)
            {
                string[] auxiliarLineInArray = auxiliarLine.Split(';');
                Item a = new Item(auxiliarLineInArray[0], auxiliarLineInArray[1]);
                mapFromItemCodeToItem.Add(auxiliarLineInArray[0], a);
                
            }
        }

        public void GenerateFrequentItemSets(int maxItemsetSize, double minSup)
        {
            int itemSet = 1;
            string x = "";
            Item[] commonItems = CommonItems(29);
            for (int i = 0; i < maxItemsetSize; i++)
            {
                x += "1";
            }
            for (int i = maxItemsetSize; i < commonItems.Length; i++)
            {
                x += "0";
            }

            long maxNum = Convert.ToInt64(x, 2);
            for (int i = itemSet; i <= maxNum; i++)
            {
                Debug.WriteLine(i + "------------------------------------------");
                int tot1 = CountSetBits(i);
                if (tot1 <= maxItemsetSize)
                {
                    int itemSetAppears = 0;
                    for (int j = 0; j < listOfAllTransactions.Count; j++)
                    {
                        Transaction act = listOfAllTransactions[j];
                        int num = 0;
                        foreach (Item item in act.MapFromItemToQuantity.Keys)
                        {
                            num += item.Number;
                        }
                        int res = num & i;
                        itemSetAppears += res == i ? 1 : 0;
                    }

                    if (itemSetAppears >= minSup* mapFromItemCodeToItem.Count)
                    {
                        Item[] ComItemSet = new Item[tot1];
                        string bin = Convert.ToString(i, 2);
                        int pos = 0;
                        for (int j = 0; j < bin.Length; j++)
                        {
                            if (bin[j] == '1')
                            {
                                ComItemSet[pos++] = mapFromNumberToItem[bin.Length - 1 - j];
                            }
                        }
                        frequentItemSets.Add(ComItemSet);
                    }

                }
            }
        }

        //CODE PROVIDED BY https://www.geeksforgeeks.org/count-set-bits-in-an-integer/
        public int CountSetBits(int n)
        {
            int count = 0;
            while (n > 0)
            {
                n &= (n - 1);
                count++;
            }
            return count;
        }
        public Item[] CommonItems(int top)
        {
            List<Item> commons = new List<Item>();

          Dictionary<Item, int> dict = new Dictionary<Item, int>();
            foreach(Transaction t in listOfAllTransactions)
            {
                foreach(Item i in t.MapFromItemToQuantity.Keys)
                {
                    if (dict.ContainsKey(i))
                    {
                        dict[i]++;
                    } else
                    {
                        dict.Add(i, 1);
                        commons.Add(i);
                    }
                }
            }
           Item[] comonItems = commons.OrderBy(c => dict[c]).Take(top).ToArray();

            int cont = 0;
            foreach (Item a in comonItems)
            {
                a.Number = (int) Math.Pow(2, cont);
                mapFromNumberToItem.Add(cont++, a);
            }


            return comonItems;
        }
    }


}
