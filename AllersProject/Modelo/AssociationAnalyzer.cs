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

        public List<long> binaryTransactions { get; set; }
        public List<Tuple<long, long>> rules { get; set; }
        public Dictionary<long, int> itemSetToSupport { get; set; }

        public Dictionary<int, Item> mapFromBinaryPositionToItem { get; set; }

        public AssociationAnalyzer(DataManager data,int itemsToEvaluate,double minSup,double minConfidence,int maxItemSetSize) {
            this.itemsToEvaluate = itemsToEvaluate;
            minSupport = minSup;
            this.minConfidence = minConfidence;
            this.maxItemSetSize = maxItemSetSize;

            CommonItems(data);
            binaryTransactions = new List<long>();

            for (int i = 0; i < data.listOfAllTransactions.Count; i++)
            {
                Transaction act = data.listOfAllTransactions[i];
                long num = 0;
                foreach (Item item in act.MapFromItemToQuantity.Keys)
                {
                    num += item.Number;
                }
                if (num!=0) {
                    binaryTransactions.Add(num);
                }
            }
            itemSetToSupport = new Dictionary<long, int>();
        }
        /**Método auxiliar del Apriori R G
         * entra un conjunto de items y una regla que va de todos los elementos al conjunto vacío (en listas)
         * <pos>se añaden las reglas que cumplen con el mínimo de confianza (en setBinarios)</pos>
         * */
        public void ApGenRules(long kItemSet, List<long> itemSets) {
            if (itemSets.Count()!=0) {
                int k = CountSetBits(kItemSet);
                int m = CountSetBits(itemSets[0]);
                if (k >= m + 1)
                {
                    List<long> toRemove = new List<long>();
                    foreach (long h in itemSets)
                    {
                        double conf = itemSetToSupport[kItemSet] / itemSetToSupport[kItemSet ^ h];
                        if (conf >= minConfidence)
                        {
                            rules.Add(new Tuple<long, long>(kItemSet ^ h, h));
                        }
                        else
                        {
                            toRemove.Add(h);
                        }
                    }
                    foreach (long r in toRemove)
                    {
                        itemSets.Remove(r);
                    }
                    itemSets = AprioriGen(itemSets);
                    ApGenRules(kItemSet, itemSets);
                }
            }
        }
        private void AprioriRuleGeneration(List<List<long>> frequentItemSets) {
            rules = new List<Tuple<long, long>>();
            foreach (List<long> itemset in frequentItemSets)
            {
                foreach (long fItemSet in itemset)
                {
                    List<long> H = new List<long>();
                    String b = Convert.ToString(fItemSet,2);
                    for (int i = 0; i < b.Length; i++)
                    {
                        if (b[b.Length-1-i]=='1') {
                            H.Add((long)Math.Pow(2,i));
                        }
                    }
                    ApGenRules(fItemSet,H);
                }
            }
        }
        //STEVEN
        public List<long> RemoveNonFrequentItemSetsFromCandidateSet(List<long> candidateSet) {
            for(int i = 0; i < binaryTransactions.Count; i++)
            {
                long binaryRepresentation = binaryTransactions[i];
                for(int j = 0; j < candidateSet.Count; j++)
                {
                    if((candidateSet[j]&binaryRepresentation)== candidateSet[j])
                    {
                        if (itemSetToSupport.ContainsKey(candidateSet[j]))
                        {
                            itemSetToSupport[candidateSet[j]] = 1 + itemSetToSupport[candidateSet[j]];
                        }
                        else
                        {
                            itemSetToSupport.Add(candidateSet[j], 1);
                        }
                    }
                }
            }
            double minimunSupport =( binaryTransactions.Count * minSupport);
            for(int i = 0; i < candidateSet.Count; i++)
            {
                if (itemSetToSupport.ContainsKey(candidateSet[i]))
                {
                if (itemSetToSupport[candidateSet[i]] <minimunSupport)
                {
                    candidateSet.RemoveAt(i);
                    i--;
                }

                }
                else
                {
                    candidateSet.RemoveAt(i);
                    i--;
                }
            }
            return candidateSet;
        }
        public List<long> AprioriGen(List<long> frequentItemSets) {
            int no = 0;
            List<long> candidates = new List<long>();
            for(int i=0;i<frequentItemSets.Count(); i++)
            {
                long[] f = divideUntilTheSecondOne(frequentItemSets[i]);
                long f1 = f[0];
                long f2 = f[1];
                for(int j = i+1; j < frequentItemSets.Count(); j++)
                {
                    long[] s= divideUntilTheSecondOne(frequentItemSets[j]);
                    long s1 = s[0];
                    long s2 = s[1];
                    if((f1==s1&& f2==s2)|| CountSetBits(frequentItemSets[0])==1)
                    {
                        candidates.Add(frequentItemSets[i] | frequentItemSets[j]);
                    }
                }
            }
            return candidates;
        }
        private long[] divideUntilTheSecondOne(long numb)
        {
            int ones = 0;
            int numberOfDivisions = 0;
            long toReturn = numb;
            while (ones < 2&& toReturn!=0)
            {
                if (toReturn % 2 == 0)
                {
                    toReturn /= 2;
                    numberOfDivisions++;
                }
                else
                {
                    ones++;
                    if (ones == 1)
                    {
                        toReturn--;
                        toReturn /= 2;
                        numberOfDivisions++;
                    }
                }
            }
            return new long[] { toReturn,numberOfDivisions };
        }
        //STEVEN
        public List<List<long>> GenerateFrequentItemSetsApriori() {
            List<long> frequentKSubsets = new List<long>();
            List<List<long>> toreturn = new List<List<long>>();
            foreach(int pos in mapFromBinaryPositionToItem.Keys)
            {
                frequentKSubsets.Add((long)Math.Pow(2,pos));
            }
            while (frequentKSubsets.Count != 0 && CountSetBits(frequentKSubsets[0])<maxItemSetSize)
            {
                List<long> toAdd = new List<long>();
                for (int j = 0; j < frequentKSubsets.Count; j++)
                {
                    toAdd.Add(frequentKSubsets[j]);
                }
                toreturn.Add(toAdd);
                List<long> ck = AprioriGen(frequentKSubsets);
                RemoveNonFrequentItemSetsFromCandidateSet(ck);
                frequentKSubsets = ck;
            }
            return toreturn;
        }
        public List<Item[]> GenerateFrequentItemSets()
        {
            List<Item[]> FrequentItemSets = new List<Item[]>();
            int itemSet = 1;
            string x = "";
            for (int i = 0; i < maxItemSetSize; i++)
            {
                x += "1";
            }
            for (int i = maxItemSetSize; i < itemsToEvaluate; i++)
            {
                x += "0";
            }

            long maxNum = Convert.ToInt64(x, 2);
            for (long i = itemSet; i <= maxNum; i++)
            {
                int tot1 = CountSetBits(i);
                if (tot1 <= maxItemSetSize)
                {
                    int itemSetAppears = 0;
                    for (int j = 0; j < binaryTransactions.Count; j++)
                    {
                        long res = binaryTransactions[j] & i;
                        itemSetAppears += res == i ? 1 : 0;
                    }

                    if (itemSetAppears >= minSupport * binaryTransactions.Count)
                    {
                        Item[] ComItemSet = new Item[tot1];
                        char[] charArray = Convert.ToString(i, 2).ToCharArray();
                        Array.Reverse(charArray);
                        string bin = new string(charArray);
                        int pos = 0;
                        for (int j = 0; j < bin.Length; j++)
                        {
                            if (bin[j] == '1')
                            {
                                ComItemSet[pos++] = mapFromBinaryPositionToItem[j];
                            }
                        }
                        FrequentItemSets.Add(ComItemSet);
                    }

                }
            }
            return FrequentItemSets;
        }
        public void CommonItems(DataManager data)
        {
            List<Item> commons = new List<Item>();
            mapFromBinaryPositionToItem = new Dictionary<int, Item>();
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
                a.Number = (long)Math.Pow(2, cont);
                mapFromBinaryPositionToItem.Add(cont++, a);
            }
        }
        //CODE PROVIDED BY https://www.geeksforgeeks.org/count-set-bits-in-an-integer/
        public int CountSetBits(long n)
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
            List<Item> items = new List<Item>();
            char[] charArray = Convert.ToString(itemSet, 2).ToCharArray();
            Array.Reverse(charArray);
            string binaryString = new string(charArray);
            for (int i = 0;i<binaryString.Length; i++)
            {
                if (binaryString[i].Equals("1")) {
                    items.Add(mapFromBinaryPositionToItem[i]);
                }
            }
            return items.ToArray();
        }
        private long ObjectItemSetToBinaryItemSet(Item[] itemSet) {
            long num = 0;
            foreach(Item i in itemSet)
            {
                num += i.Number;
            }
            return num;
        }
        public List<long> getBinaryTransactions() {
            return binaryTransactions;
        }
    }
}
