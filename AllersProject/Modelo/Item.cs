using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Item
    {
        private string itemCode;
        private string itemName;
        private int price;
        public int Number { get; set; }

        public Item(string ic, string ina) {
            itemCode = ic;
            itemName = ina;
            Number = 0;
        }

        public void setPrice(int price) {
            this.price = price;
        }
    }
}
