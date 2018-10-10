using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    class Cluster
    {
        private int id;
        private HashSet<double[]> cluster;
        private double[] centroid;

        public Cluster()
        {

        }
        
        public void AddDataPoint(double[] dp)
        {
            if(dp != null)
            cluster.Add(dp);
        }
        public void RemoveAll ()
        {
            cluster = new HashSet<double[]>();
            centroid = null;
            id = -1;
        }
        public void ComputeNewCentroid ()
        {
            
        }
        public Boolean ContainsDataPoint(double[] dp)
        {
            // out dp es correcto?
            //if (cluster.TryGetValue(dp, out dp))
            return true;
            return false;
        }
    }
}
