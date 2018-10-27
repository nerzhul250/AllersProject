namespace Modelo.services
{
    public class Prediction
    {
        public double relevance { get; set; }
        public double confidence { get; set; }
        public Item[] antecedent { get; set; }
        public Item[] consequent { get; set; }
        public int[] minimumQuantity { get; set; }
        public int[] maximumQuantity { get; set; }
        public Prediction()
        {

        }
    }
}