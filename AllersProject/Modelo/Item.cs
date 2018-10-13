using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Item : IEquatable<Item>
    {
        public string ItemCode { get; set; }
        public string itemName { get; set; }
        private int price;
        public BigInteger Number { get; set; }

        public Item(string ic, string ina) {
            ItemCode = ic;
            itemName = ina;
            Number = 0;
        }

        public void setPrice(int price) {
            this.price = price;
        }

        public bool Equals(Item other)
        {
            return ItemCode.Equals(other.ItemCode);
        }
        public override int GetHashCode()
        {
            return ItemCode.GetHashCode();
        }
    }
}