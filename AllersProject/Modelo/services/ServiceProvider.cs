using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Diagnostics;

namespace Modelo.services
{
    public class ServiceProvider
    {
        private DataManager data;

        public ServiceProvider (String dataRoute)
        {
            data = new DataManager(dataRoute);
        }

        public DataManager GetDataBy(String customerId)
        {
            DataManager cusData = new DataManager();
            cusData.dataRoute = data.dataRoute;
            cusData.mapFromCustomerIdToCustomer[customerId] = data.mapFromCustomerIdToCustomer[customerId];
            for (int i = 0; i < data.listOfAllTransactions.Count; i++)
            {
                Transaction t = data.listOfAllTransactions[i];
                if (t.customer.id.Equals(customerId)) {
                    cusData.listOfAllTransactions.Add(t);
                    foreach (Item it in t.MapFromItemToQuantity.Keys) {
                        if (!cusData.mapFromItemCodeToItem.ContainsKey(it.ItemCode))
                        {
                            it.maxQuantity= t.MapFromItemToQuantity[it];
                            it.minQuantity = t.MapFromItemToQuantity[it];
                            cusData.mapFromItemCodeToItem[it.ItemCode] = it;
                        }
                        else {
                            if (it.minQuantity > t.MapFromItemToQuantity[it])
                            {
                                it.minQuantity = t.MapFromItemToQuantity[it];
                            }
                            if (it.maxQuantity< t.MapFromItemToQuantity[it]) {
                                it.maxQuantity = t.MapFromItemToQuantity[it];
                            }
                        }
                    }
                }
            }
            return cusData;
        }
        public double GetCustomerAveragePurchasesByMonth (String customerId)
        {
            DataManager cusData = GetDataBy(customerId);
            return cusData.listOfAllTransactions
                .GroupBy(t =>new { t.transactionDate.Month,t.transactionDate.Year})
                .Average(g=>g.Sum(t=>t.getTotalPurchased()));
        }
        public Dictionary<String, List<Prediction>> getRelevantCustomersByHisAveragePurchases(double minSup, double minConfidence)
        {
            
            Dictionary<String, List<Prediction>> toReturn = new Dictionary<string, List<Prediction>>();
            
            var consult = data.mapFromCustomerIdToCustomer.Keys.Take(1);
            //.Select(x => new { Id = x, average = GetCustomerAveragePurchasesByMonth(x) }).OrderBy(x => x.average).Take(10)
            //consult.ToList().ForEach(x => toReturn.Add(x.Id, GetPredictionsOfCustomer(x.Id, minSup, minConfidence)));
            foreach (var a in consult)
            {
                toReturn.Add(a, GetPredictionsOfCustomer(a, minSup, minConfidence));
            Debug.WriteLine("aaaaaaa");
            }
            return toReturn;
        }
   
        public List<Prediction> GetPredictionsOfCustomer(String customerId,double minSup,double minConfidence)
        {
            DataManager cusData = GetDataBy(customerId);
            AssociationAnalyzerApriori aaa = new AssociationAnalyzerApriori(cusData,
                cusData.mapFromItemCodeToItem.Keys.Count,
                minSup,
                minConfidence,
                15);
            aaa.AprioriRuleGeneration(aaa.GenerateFrequentItemSetsApriori());
            List<Prediction> predictions = new List<Prediction>();
            foreach (Tuple<BigInteger,BigInteger> rule in aaa.rules) {
                Prediction pre=new Prediction();
                pre.relevance = (aaa.itemSetToSupport[rule.Item1]+ aaa.itemSetToSupport[rule.Item2] + 0.0) / aaa.totalNumberOfTransactions;
                pre.confidence= (aaa.itemSetToSupport[rule.Item1] + aaa.itemSetToSupport[rule.Item2] + 0.0) / aaa.itemSetToSupport[rule.Item1];
                pre.antecedent = aaa.BinaryItemSetToObjectItemSet(rule.Item1).ToArray();
                pre.consequent = aaa.BinaryItemSetToObjectItemSet(rule.Item2).ToArray();
                pre.minimumQuantity = new int[pre.consequent.Length];
                pre.maximumQuantity = new int[pre.consequent.Length];
                for (int i = 0; i < pre.consequent.Length; i++)
                {
                    pre.minimumQuantity[i] = pre.consequent[i].minQuantity;
                    pre.maximumQuantity[i] = pre.consequent[i].maxQuantity;
                }
                predictions.Add(pre);
            }
            return predictions;
        }

        public List<Prediction> GetGeneralPredictions(double minSup, double minConfidence)
        {
            AssociationAnalyzerFPGrowth aafpg = new AssociationAnalyzerFPGrowth(data,minSup,minConfidence);
            aafpg.RuleGeneration(aafpg.frequentItemSets());
            List<Prediction> predictions = new List<Prediction>();
            foreach (Tuple<List<string>,List<string>> rule in aafpg.rules)
            {
                Prediction pre = new Prediction();
                pre.relevance = (aafpg.fptree.frequentsSupport[rule.Item1] +
                    aafpg.fptree.frequentsSupport[rule.Item2] + 0.0) / aafpg.TransactionCodes.Count;
                pre.confidence = (aafpg.fptree.frequentsSupport[rule.Item1] +
                    aafpg.fptree.frequentsSupport[rule.Item2] + 0.0) / aafpg.fptree.frequentsSupport[rule.Item1];
                List<Item> items = new List<Item>();
                foreach (string itemCode in rule.Item1)
                {
                    items.Add(data.mapFromItemCodeToItem[itemCode]);
                }
                pre.antecedent = items.ToArray();
                items = new List<Item>();
                foreach (string itemCode in rule.Item2)
                {
                    items.Add(data.mapFromItemCodeToItem[itemCode]);
                }
                pre.consequent = items.ToArray();
                pre.minimumQuantity = new int[pre.consequent.Length];
                pre.maximumQuantity = new int[pre.consequent.Length];
                for (int i = 0; i < pre.consequent.Length; i++)
                {
                    pre.minimumQuantity[i] = pre.consequent[i].minQuantity;
                    pre.maximumQuantity[i] = pre.consequent[i].maxQuantity;
                }
                predictions.Add(pre);
            }
            return predictions;
        }

        public List<Recommendation> GetItemsCustomersMightBuyMoreButBuyFew(int numberOfGroups)
        {
            return null;
        }
    }
}
