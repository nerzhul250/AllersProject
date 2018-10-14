using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelo.services
{
    class ServiceProvider
    {
        private DataManager data;

        public ServiceProvider (String dataRoute)
        {
            data = new DataManager(dataRoute);
        }

        public DataManager GetDataBy(String customerId, int month)
        {
            return null;
        }
        public double GetCustomerAveragePurchasesInMonth (String customerId, int month)
        {
            return 0.0;
        }

        public AssociationAnalyzerFPGrowth GetRules (DataManager data)
        {
            return null;
        }

        public List<Prediction> GetPredictionsOfCustomerInMonth (String customerId, int month)
        {
            return null;
        }

        public List<Prediction> GetGeneralPredictions ()
        {
            return null;
        }

        public List<Recommendation> GetItemsCustomersMightBuyMoreButBuyFew(int numberOfGroups)
        {
            return null;
        }
    }
}
