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
        private List<double[]> dataPoints;
        private int numberOfIterations;
        private int minimumNumberOfItemsPerCustomer;
        private Dictionary<double[], Customer> mapFromDataPointToCustomer;
        private List<Cluster> clusters;

        public SimilarityAnalysisKMeans ()
        {

        }

        public double AngularDistance (double[] x, double[] y)
        {
            //los double son las cantidades de cada item?
            return 0.0;
        }

        public List<Tuple<Customer, double[], int>> GiveCustomers ()
        {
            //Give o get?
            return null;
        }
        public void Kmeans ()
        {
            //no debería entrar por parámetro la cantidad de centroides?
            int k = 4;
        }
    }
}
