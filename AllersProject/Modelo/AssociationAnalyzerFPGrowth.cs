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
        public List<List<String>> TransactionCodes { get; set; }
        
        public FPTree FPTree { get; set; }

        public AssociationAnalyzerFPGrowth (DataManager data, double minSupport)
        {

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

            FPTree = new FPTree(transactions, minSupport);
        }

        public List<List<string>> frequentItemSets()
        {
            return FPTree.FindFrequentItemsets();
        }
    }
}
