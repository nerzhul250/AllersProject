using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Item
    {
        public string ItemCode { get; set; }
        public string itemName { get; set; }
        private int price;
        public long Number { get; set; }

        public Item(string ic, string ina) {
            ItemCode = ic;
            itemName = ina;
            Number = 0;
        }

        public void setPrice(int price) {
            this.price = price;
        }

        //public int CompareTo(object obj)
        //{
        //    Item alv = (Item)obj;
        //    if (alv.ItemCode == ItemCode)
        //        return 0;
        //    return -1;
        //}
    }
}