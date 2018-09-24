using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Item:IComparable
    {
        public string ItemCode { get; set; }
        private string itemName;
        private int price;
        public int Number { get; set; }

        public Item(string ic, string ina) {
            ItemCode = ic;
            itemName = ina;
            Number = 0;
        }

        public void setPrice(int price) {
            this.price = price;
        }

        public int CompareTo(object obj)
        {
            Item alv = (Item)obj;
            if (alv.ItemCode == ItemCode)
                return 0;
            return -1;
        }
    }
}