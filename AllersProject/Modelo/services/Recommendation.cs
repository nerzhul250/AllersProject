namespace Modelo.services
{
    public class Recommendation
    {
        private Customer customer;
        private Item[] recommendations;
        private int[] currentQuantityBought;
        private double[] averageQuantityBoughBySimilarCustomers;
        private double[] customer2dRepresentation;

        public Recommendation ()
        {

        }
    }
}