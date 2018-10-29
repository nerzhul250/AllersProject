
using System;
using System.Collections.Generic;

namespace Modelo.services
{
    public class Recommendation : IComparable<Recommendation>
    {
        public Customer customer { get; set; }
        public List<Tuple<Item,double>> recommendations { get; set; }
        public double[] customerRepresentation { get; set; }
        public double[] customer2dRepresentation { get; set; }
        public int groupColor { get; set; }
        public Double maxDiff { get; set; }
        public Recommendation (List<Tuple<Item, double>> re)
        {
            recommendations = re;
            double dif = 0;
            for (int i = 0; i < re.Count; i++)
            {
                dif += re[i].Item2 * re[i].Item2;
            }
            maxDiff = dif;
        }

        public int CompareTo(Recommendation other)
        {
            return maxDiff.CompareTo(other.maxDiff);
        }
    }
}