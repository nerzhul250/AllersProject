using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    class SimilarityAnalysisKMeans
    {
        private int dimensionOfDataPoints;
        private int numberOfClusters;
        private List<Dictionary<int,double>> dataPoints;
        private int numberOfIterations;
        private int minimumNumberOfItemsPerCustomer;
        private Dictionary<Dictionary<int,double>, Customer> mapFromDataPointToCustomer;
        //mejor? o unico?
        private Dictionary<Customer, Dictionary<int,double>> mapFromCustomerToDataPoint;
        private Dictionary<int, Item> mapFromDimensionToItem;
        private List<Cluster> clusters;

        public SimilarityAnalysisKMeans (DataManager data, int dodp, int k, int noi, int mnoi)
        {
                int i = 0;
            foreach(Transaction c in data.listOfAllTransactions)
            {
                Customer cust = c.customer;
                Dictionary<Item, int> items = c.MapFromItemToQuantity;
                foreach ( KeyValuePair<Item,int> item in items)
                {
                    if (!mapFromDimensionToItem.ContainsValue(item.Key))
                    {
                        mapFromDimensionToItem.Add(i,item.Key);
                        i++;
                    }
                    
                }
            }
            dimensionOfDataPoints = dodp;
            numberOfClusters = k;
            numberOfIterations = noi;
            minimumNumberOfItemsPerCustomer = mnoi;
        }

        public double AngularDistance (Dictionary<int,double> x, Dictionary<int,double> y)
        {
            //los double son las cantidades de cada item?
            return 0.0;
        }

        public List<Tuple<Customer, Dictionary<int,double>, int>> GiveCustomers ()
        {
            //Give o get?
            return null;
        }
        public void Kmeans ()
        {
            //no debería entrar por parámetro la cantidad de centroides?
            
        }
    }
}
