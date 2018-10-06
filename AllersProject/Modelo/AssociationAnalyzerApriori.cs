using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class AssociationAnalyzerApriori
    {
        private double minSupport;
        private double minConfidence;
        private int itemsToEvaluate;
        private int maxItemSetSize;
        public int totalNumberOfTransactions { get; set; }

        public List<long> binaryTransactions { get; set; }
        public List<Tuple<long, long>> rules { get; set; }
        public Dictionary<long, int> itemSetToSupport { get; set; }
        public Dictionary<int, Item> mapFromBinaryPositionToItem { get; set; }

        public AssociationAnalyzerApriori(DataManager data,int itemsToEvaluate,double minSup,double minConfidence,int maxItemSetSize) {
            this.itemsToEvaluate = itemsToEvaluate;
            minSupport = minSup;
            this.minConfidence = minConfidence;
            this.maxItemSetSize = maxItemSetSize;
            rules = new List<Tuple<long, long>>();
            itemSetToSupport = new Dictionary<long, int>();

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
                if (num != 0)
                {
                    binaryTransactions.Add(num);
                }
            }
        }
        public AssociationAnalyzerApriori(int itemsToEvaluate, double minSup, double minConfidence, int maxItemSetSize)
        {
            this.itemsToEvaluate = itemsToEvaluate;
            minSupport = minSup;
            this.minConfidence = minConfidence;
            this.maxItemSetSize = maxItemSetSize;
            rules = new List<Tuple<long, long>>();
            itemSetToSupport = new Dictionary<long, int>();
        }
        /**Método auxiliar del Apriori R G
         * entra un conjunto de items y una regla que va de todos los elementos al conjunto vacío (en listas)
         * <pos>se añaden las reglas que cumplen con el mínimo de confianza (en setBinarios)</pos>
         * */
        public void ApGenRules(long kItemSet, LinkedList<long> itemSets) {
            if (itemSets.Count!=0) {
                int k = CountSetBits(kItemSet);
                int m = CountSetBits(itemSets.First.Value);
                if (k >= m + 1)
                {
                    LinkedListNode<long> h = itemSets.First;
                    while (h!=null) {
                        //bool contains = itemSetToSupport.ContainsKey(kItemSet) && itemSetToSupport.ContainsKey(kItemSet ^ h.Value);
                        double conf = 0;
                        bool contains = true;
                        if(contains)conf= (double)itemSetToSupport[kItemSet] / itemSetToSupport[kItemSet ^ h.Value];
                        if (contains && conf >= minConfidence)
                        {
                            rules.Add(new Tuple<long, long>(kItemSet ^ h.Value, h.Value));
                            h = h.Next;
                        }
                        else
                        {
                            LinkedListNode<long> toRemove = h;
                            h = h.Next;
                            itemSets.Remove(toRemove);
                        }
                    }
                    itemSets = AprioriGen(itemSets);
                    ApGenRules(kItemSet, itemSets);
                }
            }
        }
        public void AprioriRuleGeneration(List<List<long>> frequentItemSets) {
            rules = new List<Tuple<long, long>>();
            foreach (List<long> itemset in frequentItemSets)
            {
                foreach (long fItemSet in itemset)
                {
                    LinkedList<long> H = new LinkedList<long>();
                    String b = Convert.ToString(fItemSet,2);
                    for (int i = 0; i < b.Length; i++)
                    {
                        if (b[b.Length-1-i]=='1') {
                            H.AddLast((long)Math.Pow(2,i));
                        }
                    }
                    ApGenRules(fItemSet,H);
                }
            }
        }
        //STEVEN
        public LinkedList<long> RemoveNonFrequentItemSetsFromCandidateSet(LinkedList<long> candidateSet) {
            Stopwatch sw = Stopwatch.StartNew();
            LinkedListNode<long> cani;
            for (int i = 0; i < binaryTransactions.Count; i++)
            {
                long binaryRepresentation = binaryTransactions[i];
                cani = candidateSet.First;
                while (cani!=null) {
                    if ((cani.Value & binaryRepresentation) == cani.Value)
                    {
                        if (itemSetToSupport.ContainsKey(cani.Value))
                        {
                            itemSetToSupport[cani.Value] = 1 + itemSetToSupport[cani.Value];
                        }
                        else
                        {
                            itemSetToSupport.Add(cani.Value, 1);
                        }
                    }
                    cani=cani.Next;
                }
            }
            double minimunSupport =( totalNumberOfTransactions * minSupport);
            cani = candidateSet.First;
            while (cani!=null) {
                if (itemSetToSupport.ContainsKey(cani.Value))
                {
                    if (itemSetToSupport[cani.Value] < minimunSupport)
                    {
                        LinkedListNode<long> toRemove = cani;
                        cani = cani.Next;
                        candidateSet.Remove(toRemove);
                    }
                    else {
                        cani = cani.Next;
                    }
                }
                else
                {
                    LinkedListNode<long> toRemove = cani;
                    cani = cani.Next;
                    candidateSet.Remove(toRemove);
                }
            }
            Debug.WriteLine(sw.ElapsedMilliseconds+"Remove");
            sw.Stop();
            return candidateSet;
        }
        public LinkedList<long> AprioriGen(LinkedList<long> frequentItemSets) {
            LinkedList<long> candidates = new LinkedList<long>();
            LinkedListNode<long> fis = frequentItemSets.First;
            while (fis != null)
            {
                long[] f = divideUntilTheSecondOne(fis.Value);
                long f1 = f[0];
                long f2 = f[1];
                LinkedListNode<long> fis2 = fis.Next;
                while (fis2!=null) {
                    long[] s = divideUntilTheSecondOne(fis2.Value);
                    long s1 = s[0];
                    long s2 = s[1];
                    if ((f1 == s1 && f2 == s2) || CountSetBits(frequentItemSets.First.Value) == 1)
                    {
                        candidates.AddLast(fis.Value | fis2.Value);
                    }
                    fis2 = fis2.Next;
                }
                fis = fis.Next;
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
                    toReturn=toReturn >> 1;
                    numberOfDivisions++;
                }
                else
                {
                    ones++;
                    if (ones == 1)
                    {
                        toReturn=toReturn>>1;
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
                LinkedList<long> ck = AprioriGen(new LinkedList<long>(frequentKSubsets));
                RemoveNonFrequentItemSetsFromCandidateSet(ck);
                frequentKSubsets = ck.ToList();
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

                    if (itemSetAppears >= minSupport * totalNumberOfTransactions)
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
            totalNumberOfTransactions = data.listOfAllTransactions.Count;
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
                itemSetToSupport.Add(a.Number,dict[a]);
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
        public List<Item> BinaryItemSetToObjectItemSet(long itemSet) {
            List<Item> items = new List<Item>();
            string binaryString = Convert.ToString(itemSet, 2);
            for (int i = 0;i<binaryString.Length; i++)
            {
                if (binaryString[binaryString.Length-1-i]=='1') {
                    items.Add(mapFromBinaryPositionToItem[i]);
                }
            }
            return items;
        }
        public long ObjectItemSetToBinaryItemSet(Item[] itemSet) {
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
