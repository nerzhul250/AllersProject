using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    class Cluster
    {
        private HashSet<DataPoint> cluster;
        public double[] centroid { get; set; }

        public Cluster(double[]cent)
        {
            centroid = cent;
        }
        
        public void AddDataPoint(DataPoint dp)
        {
            if(dp != null)
            cluster.Add(dp);
        }
        public void RemoveAll ()
        {
            cluster = new HashSet<DataPoint>();
        }
        public void ComputeNewCentroid ()
        {
            double[] average = new double[centroid.Length];
            foreach(DataPoint dp in cluster)
            {
                for (int i = 0; i < dp.vector.Length; i++)
                    average[i] += dp.vector[i];
            }
            for (int i = 0; i < average.Length; i++)
                average[i] /= cluster.Count;
            centroid = average;
        }
        public Boolean ContainsDataPoint(DataPoint dp)
        {
            if (cluster.Contains(dp))
            return true;
            return false;
        }
    }
}
