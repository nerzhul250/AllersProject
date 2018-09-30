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

        private List<long> binaryTransactions;
        private List<Tuple<long, long>> rules;
        private Dictionary<long, int> itemSetToSupport;

        private Dictionary<int, Item> mapFromBinaryPositionToItem;

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
        private void ApGenRules(List<long> kItemSet, Tuple<List<long>, List<long>> itemSets) {
            int k = kItemSet.Count;
            int m = itemSets.Item2.Count;
            if(k>m+1)
            {
                List<long>itemSetsMmas1 = AprioriGen(itemSets.Item1);
                foreach(long itemS in itemSetsMmas1)
                {
                    var confi = kItemSet.Count/(kItemSet.Count-(itemSetToSupport[itemS]));
                    if (confi >= minConfidence)
                    {
                        Item[] complement = BinaryItemSetToObjectItemSet(itemS);
                        for(int i = 0; i< complement.Length; i++)
                        {
                        kItemSet.Remove(complement[i].Number);
                        }
                        Item[] toItemSet = new Item[kItemSet.Count];
                        for (int i = 0; i < toItemSet.Length; i++)
                        {
                            toItemSet[i] = BinaryItemSetToObjectItemSet(kItemSet[i])[0];
                        }
                        long bin = ObjectItemSetToBinaryItemSet(toItemSet);
                        rules.Add(new Tuple<long,long>(bin,itemS));
                    }
                    else
                        itemSetsMmas1.Remove(itemS);
                }
                ApGenRules(kItemSet, new Tuple<List<long>,List<long>>(kItemSet,itemSetsMmas1));
            }
        }
        private void AprioriRuleGeneration(List<List<long>> frequentItemSets) {
            rules = new List<Tuple<long, long>>();
            
            foreach (List<long> itemset in frequentItemSets)
            {
                Tuple<List<long>, List<long>> iConseq = new Tuple<List<long>, List<long>>(itemset,new List<long>());
                ApGenRules(itemset, iConseq);
            }
        }
        //STEVEN
        private List<long> RemoveNonFrequentItemSetsFromCandidateSet(List<long> candidateSet) {
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
            double minimunSupport = binaryTransactions.Count * minSupport;
            for(int i = 0; i < candidateSet.Count; i++)
            {
                if (itemSetToSupport[candidateSet[i]] <minimunSupport)
                {
                    candidateSet.RemoveAt(i);
                    i--;
                }
            }
            return candidateSet;
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
            return candidates;
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
