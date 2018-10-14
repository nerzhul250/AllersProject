using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Customer : IEquatable<Customer>
    {
        public string id { get; set; }
        private string groupName;
        private string cityName;
        private string regionName;
        private string pymntGroup;//What is this?

        public Customer(string co,string gp,string cn,string rn,string pg) {
            id = co;
            groupName = gp;
            cityName = cn;
            regionName = rn;
            pymntGroup = pg;
        }
        public bool Equals(Customer other)
        {
            return id.Equals(other.id);
        }
        public override int GetHashCode()
        {
            return id.GetHashCode();
        }
    }
}
