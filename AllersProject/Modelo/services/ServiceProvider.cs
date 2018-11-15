using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Diagnostics;
using Accord.Statistics;
using Accord.Statistics.Analysis;

namespace Modelo.services
{
    public class TupleComparer : IComparer<Tuple<Item, double>>
    {
        public int Compare(Tuple<Item, double> x, Tuple<Item, double> y)
        {
            return x.Item2.CompareTo(y.Item2);
        }
    }
    public class ServiceProvider
    {
        private DataManager data;

        public List<Prediction> Predictions { get; set; }

        public ServiceProvider (String dataRoute)
        {
            data = new DataManager(dataRoute);
        }

        public DataManager GetDataBy(String customerId)
        {
            DataManager cusData = new DataManager();
            cusData.dataRoute = data.dataRoute;
            if (!data.mapFromCustomerIdToCustomer.ContainsKey(customerId))
            {
                throw new Exception("No existe ningún cliente con el código especificado");
            }
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
            if (cusData.listOfAllTransactions.Count == 0)
            {
                return 0;
            }
            else {
                return cusData.listOfAllTransactions
                .GroupBy(t => new { t.transactionDate.Month, t.transactionDate.Year })
                .Average(g => g.Sum(t => t.getTotalPurchased()));
            }
            
        }
        public Dictionary<String, List<Prediction>> getRelevantCustomersByHisAveragePurchases(double minSup, double minConfidence, int quantity)
        {
            
            Dictionary<String, List<Prediction>> toReturn = new Dictionary<string, List<Prediction>>();

            var consult = data.mapFromCustomerIdToCustomer.Keys.Select(x => new { Id = x, average = GetCustomerAveragePurchasesByMonth(x) }).OrderByDescending(x => x.average).Take(quantity);
            consult.ToList().ForEach(x => toReturn.Add(x.Id+"-Compras promedio: "+string.Format("{0:C}", x.average), GetPredictionsOfCustomer(x.Id, minSup, minConfidence)));
            return toReturn;
        }
   
        public List<Prediction> GetPredictionsOfCustomer(String customerId,double minSup,double minConfidence)
        {
            DataManager cusData = GetDataBy(customerId);
            AssociationAnalyzerApriori apriori = new AssociationAnalyzerApriori(cusData,10,minSup,minConfidence,10);
            List<List<BigInteger>>sets = apriori.GenerateFrequentItemSetsApriori();
            if (sets.Count == 0)
            {
                throw new Exception("Insuficientes items frecuentes, disminuya el soporte");
            }
            apriori.AprioriRuleGeneration(sets);
            if (apriori.rules.Count==0)
            {
                throw new Exception("Insuficientes reglas, disminuya la confianza");
            }
            List<Prediction> predictions = new List<Prediction>();
            foreach (Tuple<BigInteger, BigInteger> rule in apriori.rules)
            {
                Prediction pre = new Prediction();
                pre.relevance = (apriori.itemSetToSupport[rule.Item1|rule.Item2] + 0.0) / apriori.totalNumberOfTransactions;
                pre.confidence = (apriori.itemSetToSupport[rule.Item2|rule.Item1]+ 0.0) / apriori.itemSetToSupport[rule.Item1];
                List<Item> items = apriori.BinaryItemSetToObjectItemSet(rule.Item1);
                pre.antecedent = items.ToArray();
                items = apriori.BinaryItemSetToObjectItemSet(rule.Item2);
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

        public List<Prediction> GetGeneralPredictions(double minSup, double minConfidence)
        {
            AssociationAnalyzerFPGrowth aafpg = new AssociationAnalyzerFPGrowth(data,minSup,minConfidence);
            List<List<string>> frequents = aafpg.frequentItemSets();
            if (frequents.Count == 0)
            {
                throw new Exception("No hay itemsets frecuentes: Intenta con un soporte más bajo");
            }
            aafpg.RuleGeneration(frequents);
            if (aafpg.rules.Count == 0)
            {
                throw new Exception("No hay predicciones: Intenta con una confianza más baja");
            }
            List<Prediction> predictions = new List<Prediction>();
            foreach (Tuple<List<string>,List<string>> rule in aafpg.rules)
            {
                Prediction pre = new Prediction();
                pre.relevance = (aafpg.fptree.frequentsSupport[rule.Item1.Concat(rule.Item2).ToList()] + 0.0) / aafpg.TransactionCodes.Count;
                pre.confidence = (aafpg.fptree.frequentsSupport[rule.Item1.Concat(rule.Item2).ToList()] + 0.0) / aafpg.fptree.frequentsSupport[rule.Item1];
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
            this.Predictions = predictions;
            return predictions;
        }

        public List<Recommendation> GetItemsCustomersMightBuyMoreButBuyFew(int numberOfGroups,int numberOfItemsInAnalysis,int minimumNumberOfItemPerCustomer,int numberOfItemsToRecommend)
        {
            int numberOfIterations = 100;
            SimilarityAnalysisKMeans sakm = new SimilarityAnalysisKMeans(data, numberOfItemsInAnalysis, numberOfGroups, numberOfIterations, minimumNumberOfItemPerCustomer);
            sakm.Kmeans();
            List<List<Tuple<Customer, double[]>>> groups = sakm.GetCustomers();
            List<Recommendation> recommendations = new List<Recommendation>();
            int groupNum = 0;
            foreach(List<Tuple<Customer,double[]>> group in groups)
            {
                double[] average = new double[sakm.dimensionOfDataPoints];
                foreach (Tuple<Customer, double[]> items in group) {
                    double[] vec = items.Item2;
                    for (int i = 0; i < vec.Length; i++)
                    {
                        average[i] += vec[i];
                    }
                }
                for (int i = 0; i < average.Length; i++)
                {
                    average[i] = average[i] / group.Count;
                }
                foreach (Tuple<Customer, double[]> customer in group)
                {
                    List<Tuple<Item,double>>diffs = new List<Tuple<Item, double>>();
                    for (int i = 0; i < customer.Item2.GetLength(0); i++)
                    {
                        diffs.Add(new Tuple<Item, double>(sakm.mapFromDimensionToItem[i],average[i]- customer.Item2[i]));
                    }
                    diffs = diffs.OrderByDescending(t=>t.Item2).Take(numberOfItemsToRecommend).ToList();
                    Recommendation re = new Recommendation(diffs);
                    re.customerRepresentation = customer.Item2;
                    re.customer = customer.Item1;
                    re.groupColor = groupNum;
                    recommendations.Add(re);
                }
                groupNum++;
            }
            recommendations.Sort();
            double[][] matrix = new double[recommendations.Count][];
            for (int i = 0; i < recommendations.Count; i++)
            {
                double[] dat = recommendations[i].customerRepresentation;
                matrix[i] = dat;
            }
            PrincipalComponentAnalysis pca = new PrincipalComponentAnalysis(matrix);
            pca.NumberOfOutputs = 2;
            
            // Compute the Principal Component Analysis
            pca.Compute();

            // Creates a projection
            double[][] components = pca.Transform(matrix);
            for (int i = 0; i < components.GetLength(0); i++)
            {
                recommendations[i].customer2dRepresentation = components[i];
            }
            recommendations = recommendations.OrderBy(r => r.customer.id).ToList();
            return recommendations;
        }

        private List<Prediction> GetPredictionsFromItemsets(Item [] itemsToPredict)
        {
            List<Prediction> SpecificPredictions = new List<Prediction>();
            foreach(Prediction pred in Predictions)
            {
                Item[] intersect = pred.antecedent.Intersect(itemsToPredict).ToArray();
                if (intersect.Length == pred.antecedent.Length)
                {
                    SpecificPredictions.Add(pred);
                }
            }
            if (SpecificPredictions.Count == 0)
            {
                throw new Exception("No se encontraron predicciones con los items especificados");
            }
            return SpecificPredictions;
        }

        public List<Prediction> GetPredictionsFromCodeItems(string[] ItemCodes)
        {
            List<Item> codes = new List<Item>();
            List<string> invalidCodes = new List<string>();
            foreach (string code in ItemCodes)
            {
                if (data.mapFromItemCodeToItem.ContainsKey(code))
                {
                    codes.Add(data.mapFromItemCodeToItem[code]);
                } else
                {
                    invalidCodes.Add(code);
                }
            }
            Item[] items = new Item[codes.Count];
            items = codes.ToArray();
            return (GetPredictionsFromItemsets(items));
        }

        private List<Prediction> GetPredictionsFromItemsetsForCostumer(Item[] itemsToPredict, string costumerId, double minSup, double minconf)
        {
            List<Prediction> SpecificPredictions = new List<Prediction>();
            List<Prediction> predCostumers = GetPredictionsOfCustomer(costumerId, minSup, minconf);
            foreach (Prediction pred in predCostumers)
            {
                Item[] intersect = pred.antecedent.Intersect(itemsToPredict).ToArray();
                if (intersect.Length == pred.antecedent.Length)
                {
                    SpecificPredictions.Add(pred);
                }
            }
            return SpecificPredictions;
        }

        public List<Prediction> GetPredictionsFromCodeItemsSpecificClient(string[] ItemCodes, string costumerId, double minSup, double minconf)
        {
            List<Item> codes = new List<Item>();
            List<string> invalidCodes = new List<string>();
            foreach (string code in ItemCodes)
            {
                if (data.mapFromItemCodeToItem.ContainsKey(code))
                {
                    codes.Add(data.mapFromItemCodeToItem[code]);
                }
                else
                {
                    invalidCodes.Add(code);
                }
            }
            Item[] items = new Item[codes.Count];
            items = codes.ToArray();
            return (GetPredictionsFromItemsetsForCostumer(items, costumerId, minSup, minconf));
        }

    }
}
