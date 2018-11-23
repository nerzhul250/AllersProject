using Estructura;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Modelo
{
    public class AssociationAnalyzerFPGrowth
    {
        public List<List<String>> TransactionCodes { get; set; }
        public List<Tuple<List<string>, List<string>>> rules { get; set; }
        public FPTree fptree { get; set; }

        public double minSupport { get; set; }
        public double minConfidence { get; set; }

        public AssociationAnalyzerFPGrowth(DataManager data, double minSupport,double minConfidence)
        {

            List<List<String>> transactions = new List<List<string>>();
            this.minSupport = minSupport;
            this.minConfidence = minConfidence;
            for (int i = 0; i < data.listOfAllTransactions.Count; i++)
            {
                if (data.listOfAllTransactions[i].MapFromItemToQuantity.Count > 0)
                {
                    List<string> items = new List<string>();
                    foreach (Item it in data.listOfAllTransactions[i].MapFromItemToQuantity.Keys)
                    {
                        if (it.minQuantity > data.listOfAllTransactions[i].MapFromItemToQuantity[it])
                        {
                            it.minQuantity = data.listOfAllTransactions[i].MapFromItemToQuantity[it];
                        }
                        if (it.maxQuantity < data.listOfAllTransactions[i].MapFromItemToQuantity[it])
                        {
                            it.maxQuantity = data.listOfAllTransactions[i].MapFromItemToQuantity[it];
                        }
                        items.Add(it.ItemCode);
                    }
                    transactions.Add(items);

                }
            }
            TransactionCodes = transactions;
            fptree = new FPTree(transactions, minSupport);

        }

        public List<List<string>> frequentItemSets()
        {
            return fptree.FindFrequentItemsets();
        }
        public void GenRules(List<string> kItemSet, List<List<string>> itemSets)
        {
            if (itemSets.Count != 0)
            {
                int k = kItemSet.Count;
                int m = itemSets[0].Count;
                if (k >= m + 1)
                {
                    for (int i = 0; i < itemSets.Count; i++)
                    {
                        List<string> h = itemSets[i];
                        List<string> theRemovedOne = kItemSet.Except(h).ToList();
                        bool contains = fptree.frequentsSupport.ContainsKey(kItemSet) && fptree.frequentsSupport.ContainsKey(theRemovedOne);
                        double conf = 0;
                        if (contains) conf = (double)fptree.frequentsSupport[kItemSet] / fptree.frequentsSupport[theRemovedOne];
                        if (contains && conf >= minConfidence)
                        {
                            rules.Add(new Tuple<List<string>, List<string>>(theRemovedOne, h));
                        }
                        else
                        {
                            itemSets.RemoveAt(i);
                            i--;
                        }
                    }
                    itemSets = AprioriGen(itemSets);
                    GenRules(kItemSet, itemSets);
                }
            }
        }
        public void RuleGeneration(List<List<string>> frequentItemSets)
        {
            rules = new List<Tuple<List<string>, List<string>>>();
            var group = frequentItemSets.GroupBy(k => k.Count).OrderBy(g => g.Key);
            group.ToList().ForEach(g =>
            {
                g.ToList().ForEach(listsfs =>
                {
                    List<List<string>> H = new List<List<string>>();
                    for (int i = 0; i < listsfs.Count; i++)
                    {
                        List<string> newList = new List<string>();
                        newList.Add(listsfs[i]);
                        H.Add(newList);
                    }
                    GenRules(listsfs, H);
                });
            });
        }

        private List<List<string>> AprioriGen(List<List<string>> frequentItemSets)
        {
            List<List<string>> candidates = new List<List<string>>();
            for (int i = 0; i < frequentItemSets.Count; i++)
            {
                List<string> fis1 = frequentItemSets[i];
                fis1.Sort();
                for (int j = i + 1; j < frequentItemSets.Count; j++)
                {
                    List<string> fis2 = frequentItemSets[j];
                    fis2.Sort();
                    if (canBeJoined(fis1, fis2))
                    {
                        List<string> atarashi = new List<string>();
                        for (int k = 0; k < fis1.Count; k++)
                        {
                            atarashi.Add(fis1[k]);
                        }
                        atarashi.Add(fis2[fis2.Count - 1]);
                        atarashi.Sort();
                        candidates.Add(atarashi);
                    }
                }
            }
            return candidates;
        }
    
        private bool canBeJoined(List<string> fis1, List<string> fis2)
        {
            if (fis1.Count == 1) return true;
            bool e = true;
            for (int i = 0; i < fis1.Count - 1 && e; i++)
            {
                e = fis1[i].Equals(fis2[i]);
            }
            return e;
        }
    }
}
