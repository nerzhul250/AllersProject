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
            return 0.0;
        }

        public List<Tuple<Customer, double[], int>> GiveCustomers ()
        {
            return null;
        }
        public void Kmeans ()
        {

        }
    }
}
