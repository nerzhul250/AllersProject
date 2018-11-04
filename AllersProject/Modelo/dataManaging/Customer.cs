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
        public string groupName { get; set; }
        public string cityName { get; set; }
        public string regionName { get; set; }
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
