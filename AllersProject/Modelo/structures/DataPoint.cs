using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    class DataPoint : IEquatable<DataPoint>
    {
        public int id { get; set; }
        public double[] vector { get; set; }

        public DataPoint(int id, double[] vector) {
            this.id = id;
            this.vector = vector;
                }

        public bool Equals(DataPoint other)
        {
            if (id == other.id)
                return true;
            return false;
        }
    }
}
