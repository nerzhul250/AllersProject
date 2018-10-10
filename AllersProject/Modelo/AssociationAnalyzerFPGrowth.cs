using Estructura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo
{
    public class AssociationAnalyzerFPGrowth
    {
        public double MinSuport {get; set;}

        public List<List<String>> TransactionCodes { get; set; }
        
        public ArbolFP FPTree { get; set; }

        public AssociationAnalyzerFPGrowth (DataManager data, double minSupport)
        {
            MinSuport = minSupport;

            List <List<String>> transactions = new List<List<string>>();

            for (int i = 0; i < data.listOfAllTransactions.Count; i++)
            {
                List<string> items = new List<string>();
                foreach (Item it in data.listOfAllTransactions[i].MapFromItemToQuantity.Keys)
                {
                    items.Add(it.ItemCode);
                }
                transactions.Add(items);
            }

            FPTree = new ArbolFP(transactions, minSupport);
        }
    }
}
