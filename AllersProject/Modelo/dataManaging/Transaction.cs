using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class Transaction
    {
        private string tid;
        public DateTime transactionDate { get; set; }
        public Dictionary<Item, int> MapFromItemToQuantity { get; set; }
        public Customer customer{ get; set; }
        public Transaction(string tid,DateTime date,Customer cus,Object[,] itemCode_Quantity) {
            MapFromItemToQuantity = new Dictionary<Item, int>();
            this.tid = tid;
            transactionDate = date;
            customer = cus;
            //null added because of anomalies pointed out in dataManager data loading.
            for (int i = 0; i < itemCode_Quantity.GetLength(0); i++)
            {
                if (itemCode_Quantity[i, 0] != null) {
                    if (MapFromItemToQuantity.ContainsKey((Item)itemCode_Quantity[i, 0])) {
                        MapFromItemToQuantity[(Item)itemCode_Quantity[i, 0]] = MapFromItemToQuantity[(Item)itemCode_Quantity[i, 0]] + (int)itemCode_Quantity[i, 1];
                    }
                    else {
                        MapFromItemToQuantity.Add((Item)itemCode_Quantity[i, 0], (int)itemCode_Quantity[i, 1]);
                    }
                }
            }
        }

        internal double getTotalPurchased()
        {
            double sum =0;
            foreach (Item it in MapFromItemToQuantity.Keys) {
                sum += it.price * MapFromItemToQuantity[it];
            }
            return sum;
        }
    }
}
