using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    class Transaction
    {
        private string tid;
        private DateTime transactionDate;
        private Customer customer;
        private Dictionary<Item,int> mapFromItemToQuantity;

        public Transaction(string tid,DateTime date,Customer cus,Object[,] itemCode_Quantity) {
            mapFromItemToQuantity = new Dictionary<Item, int>();
            this.tid = tid;
            transactionDate = date;
            customer = cus;
            //null added because of anomalies pointed out in dataManager data loading.
            for (int i = 0; i < itemCode_Quantity.GetLength(0); i++)
            {
                if (itemCode_Quantity[i, 0] != null) {
                    if (mapFromItemToQuantity.ContainsKey((Item)itemCode_Quantity[i, 0])) {
                        mapFromItemToQuantity[(Item)itemCode_Quantity[i, 0]] = mapFromItemToQuantity[(Item)itemCode_Quantity[i, 0]] + (int)itemCode_Quantity[i, 1];
                    }
                    else {
                        mapFromItemToQuantity.Add((Item)itemCode_Quantity[i, 0], (int)itemCode_Quantity[i, 1]);
                    }
                }
            }
        }
    }
}
