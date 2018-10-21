using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Cluster
    {
        public HashSet<DataPoint> cluster { get; set; }
        public double[] centroid { get; set; }

        public Cluster(double[]cent)
        {
            centroid = cent;
            cluster = new HashSet<DataPoint>();
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
        /**
         * Retorna true si se va a eliminar por falta de DP's
         */
        public bool ComputeNewCentroid ()
        {
            if (cluster.Count ==0)
                return true;
            double[] average = new double[centroid.Length];
            foreach(DataPoint dp in cluster)
            {
                for (int i = 0; i < dp.vector.Length; i++)
                    average[i] += dp.vector[i];
            }
            for (int i = 0; i < average.Length; i++)
                average[i] /= cluster.Count;
            centroid = average;
            return false;
        }
        public Boolean ContainsDataPoint(DataPoint dp)
        {
            if (cluster.Contains(dp))
            return true;
            return false;
        }
    }
}
