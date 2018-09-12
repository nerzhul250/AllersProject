using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    class Customer
    {
        private string id;
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
    }
}
