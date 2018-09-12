using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    class Item
    {
        private string itemCode;
        private string itemName;
        private int price;

        public Item(string ic, string ina) {
            itemCode = ic;
            itemName = ina;
        }

        public void setPrice(int price) {
            this.price = price;
        }
    }
}
