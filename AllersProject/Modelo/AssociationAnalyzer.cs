using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class AssociationAnalyzer
    {
        public const double MINSUPPORT = 0.05;
        public const int MAXITEMSETSIZE = 3;
        public const int COMMONITEMSTOTRACK = 28;
        
        private Dictionary<int, Item> mapFromNumberToItem;
        public List<Item[]> FrequentItemSets { get; set; }
        public DataManager data;

        public AssociationAnalyzer() {
            data = new DataManager();
            mapFromNumberToItem = new Dictionary<int, Item>();
            FrequentItemSets = new List<Item[]>();
            GenerateFrequentItemSets(MAXITEMSETSIZE,MINSUPPORT, CommonItems(COMMONITEMSTOTRACK));
        }

        public int getItemSetsCount()
        {
            return FrequentItemSets.Count;
        }
        public void GenerateFrequentItemSets(int maxItemsetSize, double minSup, Item[] itemsToEvaluate)
        {
            int itemSet = 1;
            string x = "";
            //Item[] commonItems = CommonItems(28);
            for (int i = 0; i < maxItemsetSize; i++)
            {
                x += "1";
            }
            for (int i = maxItemsetSize; i < itemsToEvaluate.Length; i++)
            {
                x += "0";
            }

            long maxNum = Convert.ToInt64(x, 2);
            for (int i = itemSet; i <= maxNum; i++)
            {
                int tot1 = CountSetBits(i);
                if (tot1 <= maxItemsetSize)
                {
                    int itemSetAppears = 0;
                    for (int j = 0; j < data.listOfAllTransactions.Count; j++)
                    {
                        Transaction act = data.listOfAllTransactions[j];
                        int num = 0;
                        foreach (Item item in act.MapFromItemToQuantity.Keys)
                        {
                            num += item.Number;
                        }
                        int res = num & i;
                        itemSetAppears += res == i ? 1 : 0;
                    }

                    if (itemSetAppears >= minSup * data.getTransactionsCount())
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
                        FrequentItemSets.Add(ComItemSet);
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
            foreach (Transaction t in data.listOfAllTransactions)
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
            Item[] comonItems = commons.OrderByDescending(c => dict[c]).Take(top).ToArray();

            int cont = 0;
            foreach (Item a in comonItems)
            {
                a.Number = (int)Math.Pow(2, cont);
                mapFromNumberToItem.Add(cont++, a);
            }
            return comonItems;
        }
    }
}
