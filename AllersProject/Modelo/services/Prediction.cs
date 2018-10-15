namespace Modelo.services
{
    public class Prediction
    {
        private double relevance;
        private double confidence;
        private Item[] antecedent;
        private Item[] consequent;
        private int[] minimumQuantity;
        private int[] maximumQuantity;
        public Prediction()
        {
        }
    }
}