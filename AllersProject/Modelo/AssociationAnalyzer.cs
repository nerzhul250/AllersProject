using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class AssociationAnalyzer
    {
        private double minSupport;
        private double minConfidence;
        private int itemsToEvaluate;
        private int maxItemSetSize;
        private Dictionary<long, int> itemSetToSupport;
        private Dictionary<long, Item> mapFromNumberToItem;

        public DataManager data;

        private List<Tuple<long, long>> rules;

        public AssociationAnalyzer(DataManager data,int itemsToEvaluate,double minSup,double minConfidence,int maxItemSetSize) {
            this.data = data;
            this.itemsToEvaluate = itemsToEvaluate;
            minSupport = minSup;
            this.minConfidence = minConfidence;
            this.maxItemSetSize = maxItemSetSize;

            mapFromNumberToItem = new Dictionary<long, Item>();
            itemSetToSupport = new Dictionary<long, int>();

            List<Item[]> testing=GenerateFrequentItemSets(CommonItems());
            foreach (Item[] it in testing) {
                foreach (Item i in it) {
                    Debug.WriteLine(i.ItemCode);
                }
            }
        }
        private void ApGenRules(long kItemSet,List<long> itemSets) {

        }
        private void AprioriRuleGeneration(List<List<long>> frequentItemSets) {

        }
        private void RemoveNonFrequentItemSetsFromCandidateSet(List<long> candidateSet) {

        }
        private List<long> AprioriGen(List<long> frequentItemSets) {
            List<long> candidates = new List<long>();
            for(int i=0;i<frequentItemSets.Count(); i++)
            {
                long first = divideUntilTheSecondOne(frequentItemSets[i]);
                for(int j = i+1; j < frequentItemSets.Count(); j++)
                {
                    long second = divideUntilTheSecondOne(frequentItemSets[j]);
                    if(first== second)
                    {
                        candidates.Add(frequentItemSets[i] | frequentItemSets[j]);
                    }

                }
            }
            return null;
        }
        private long divideUntilTheSecondOne(long numb)
        {
            int ones = 0;
            long toReturn = numb;
            while (ones < 2|| toReturn==0)
            {
                if (toReturn % 2 == 0)
                {
                    toReturn /= 2;
                }
                else
                {
                    ones++;
                    if (ones == 1)
                    {
                        toReturn--;
                        toReturn /= 2;
                    }
                }
            }
            return toReturn;
        }
        private List<List<long>> GenerateFrequentItemSetsApriori(Item[] frequentOneItemSets) {
            List<long> frequentSubsets = new List<long>();
            Item[] common = CommonItems();
            for(int i = 0; i < common.Length; i++)
            {
                
            }
            return null;
        }
        public List<Item[]> GenerateFrequentItemSets(Item[] frequentOneItemSets)
        {
            List<Item[]> FrequentItemSets = new List<Item[]>();
            int itemSet = 1;
            string x = "";
            for (int i = 0; i < maxItemSetSize; i++)
            {
                x += "1";
            }
            for (int i = maxItemSetSize; i < frequentOneItemSets.Length; i++)
            {
                x += "0";
            }

            long maxNum = Convert.ToInt64(x, 2);
            for (int i = itemSet; i <= maxNum; i++)
            {
                int tot1 = CountSetBits(i);
                if (tot1 <= maxItemSetSize)
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

                    if (itemSetAppears >= minSupport * data.getTransactionsCount())
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
            return FrequentItemSets;
        }
        public Item[] CommonItems()
        {
            List<Item> commons = new List<Item>();
            mapFromNumberToItem = new Dictionary<long, Item>();
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
            Item[] comonItems = commons.OrderByDescending(c => dict[c]).Take(itemsToEvaluate).ToArray();

            int cont = 0;
            foreach (Item a in comonItems)
            {
                a.Number = (int)Math.Pow(2, cont);
                mapFromNumberToItem.Add(cont++, a);
            }
            return comonItems;
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
        private Item[] BinaryItemSetToObjectItemSet(long itemSet) {
            return null;
        }
        private long ObjectItemSetToBinaryItemSet(Item[] itemSet) {
            return 0;
        }
    }
}
