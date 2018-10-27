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
        public double price { get; set; }
        public BigInteger Number { get; set; }
        public int minQuantity { get; set; }
        public int maxQuantity { get; set; }

        public Item(string ic, string ina) {
            ItemCode = ic;
            itemName = ina;
            Number = 0;
            minQuantity = int.MaxValue;
            maxQuantity = int.MinValue;
        }

        public void setPrice(double price) {
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