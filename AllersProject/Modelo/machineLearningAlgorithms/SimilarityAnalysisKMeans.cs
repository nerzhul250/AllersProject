using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PCA;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Generic;
using MathNet.Numerics.LinearAlgebra.Storage;

namespace Modelo
{
    public class SimilarityAnalysisKMeans
    {
        private int dimensionOfDataPoints;
        private int numberOfClusters;
        private List<DataPoint> dataPoints;
        private int numberOfIterations;
        private int minimumNumberOfItemsPerCustomer;
        private Dictionary<Customer, DataPoint> mapFromCustomerToDataPoint;
        private Dictionary<int, Item> mapFromDimensionToItem;
        public List<Cluster> clusters { get; set; }
        //cantidad máxima de un item o dimensión. La mayor cantidad de una casilla de los vectores
        private int maxQuantityItem;

        public SimilarityAnalysisKMeans(DataManager data, int dimOfDataP, int k, int numOfIter, int minNroItemsPerC)
        {
            int posicionDimension = 0;
            int idDataP = 0;
            mapFromCustomerToDataPoint = new Dictionary<Customer, DataPoint>();
            dataPoints = new List<DataPoint>();
            mapFromDimensionToItem = new Dictionary<int, Item>();
            Dictionary<Item, int> mapFromItemToDimension = new Dictionary<Item, int>();
            foreach (Transaction t in data.listOfAllTransactions)
            {
                Customer cust = t.customer;
                Dictionary<Item, int> items = t.MapFromItemToQuantity;
                foreach ( KeyValuePair<Item,int> item in items)
                {
                    //genera los números de las dimensiones
                    if (!mapFromItemToDimension.ContainsKey(item.Key))
                    {
                        mapFromItemToDimension.Add(item.Key,posicionDimension);
                        mapFromDimensionToItem.Add(posicionDimension, item.Key);
                        posicionDimension++;
                    }
                    //agrega la cantidad del item al vector del cliente
                    if (!mapFromCustomerToDataPoint.ContainsKey(cust))
                    {
                        mapFromCustomerToDataPoint.Add(cust, new DataPoint(idDataP, new double[dimOfDataP]));
                        idDataP++;
                    }
                    //posible nullpointer
                        mapFromCustomerToDataPoint[cust].vector[mapFromItemToDimension[item.Key]] += item.Value;
                    if (mapFromCustomerToDataPoint[cust].vector[mapFromItemToDimension[item.Key]] > maxQuantityItem)
                        maxQuantityItem = (int) mapFromCustomerToDataPoint[cust].vector[mapFromItemToDimension[item.Key]];
                }
            }
            dimensionOfDataPoints = dimOfDataP;
            numberOfClusters = k;
            numberOfIterations = numOfIter;
            minimumNumberOfItemsPerCustomer = minNroItemsPerC;
            pruningDataPoints();
            dataPoints = mapFromCustomerToDataPoint.Values.ToList();
        }
        
        public SimilarityAnalysisKMeans(int numOfClus, int numOfIter, int dimOfDP, int maxQuant, Dictionary<Customer,DataPoint> mapCusToDP, Dictionary<int, Item>mapDimToIt)
        {
            numberOfClusters = numOfClus;
            numberOfIterations = numOfIter;
            dimensionOfDataPoints = dimOfDP;
            maxQuantityItem = maxQuant;
            mapFromCustomerToDataPoint= mapCusToDP;
            mapFromDimensionToItem = mapDimToIt;
            dataPoints = mapFromCustomerToDataPoint.Values.ToList();
        }
        public double AngularDistance (double[] x, double[] y)
        {
            double dotProduct = 0;
            double magnitudeX = 0;
            double magnitudeY = 0;
            for(int i=0; i< x.Length; i++)
            {
                dotProduct += x[i]*y[i];
                magnitudeX += Math.Pow(x[i], 2);
                magnitudeY += Math.Pow(y[i], 2);
            }
            double similarity = dotProduct / (Math.Sqrt(magnitudeX) * Math.Sqrt(magnitudeY));
            return Math.Acos(similarity)/Math.PI;
        }

        public void pruningDataPoints ()
        {
            foreach (KeyValuePair<Customer, DataPoint> customer in mapFromCustomerToDataPoint)
            {
                int cantItems = 0;
                for (int i = 0; i < dimensionOfDataPoints; i++)
                {
                    if (customer.Value.vector[i] > 0)
                        cantItems++;
                }
                //si arroja error, guardarlos en una lista y después eliminarlos
                if (cantItems < minimumNumberOfItemsPerCustomer)
                    mapFromCustomerToDataPoint.Remove(customer.Key);
            }
        }
        public List<Tuple<Customer, DataPoint, int>> GetCustomers ()
        {
            return null;
        }
        public void Kmeans ()
        {
            clusters = new List<Cluster>();
                Random alv = new Random();
            for (int i = 0; i < numberOfClusters; i++)
            {
                double[] newCentroid = new double[dimensionOfDataPoints];
                for (int j = 0; j < dimensionOfDataPoints; j++)
                {
                    newCentroid[j] = alv.Next(maxQuantityItem);
                }
                Cluster clus = new Cluster(newCentroid);
                clusters.Add(clus);
            }
            for (int i = 0; i < numberOfIterations; i++)
            {
                foreach (KeyValuePair<Customer, DataPoint> customer in mapFromCustomerToDataPoint)
                {
                    double mDistance = Double.MaxValue;
                    Cluster toEnter = null;
                    foreach (Cluster clActual in clusters)
                    {
                        double dActual = AngularDistance(customer.Value.vector, clActual.centroid);
                        if (dActual < mDistance)
                        {
                            mDistance = dActual;
                            toEnter = clActual;
                        }
                    }
                    toEnter.AddDataPoint(customer.Value);
                }
                bool changes = false;
                List<Cluster> aEliminar = new List<Cluster>();
                foreach (Cluster cl in clusters)
                {
                    double[] centroidBefore = cl.centroid;
                    if (cl.ComputeNewCentroid())
                        aEliminar.Add(cl);
                    if (AngularDistance(centroidBefore, cl.centroid) != 0 && !changes)
                        changes = true;
                }
                foreach (Cluster ae in aEliminar)
                    clusters.Remove(ae);
                if (!changes)
                    break;
                else if(i < numberOfIterations-1)
                    clusters.ForEach(x => x.RemoveAll());
            }
        }

        public List<double[]> Uncompress()
        {
            PCADimReducer lol = new PCADimReducer();
            List<double[]> source = GetNormalizedData();
            Matrix <double> Z; // PCA output 
            double variance_retained = 8;
            List<double[]> retorno = lol.CompressData(source, 1, out Z, out variance_retained);
            return retorno;
        }
        private List<double[]> GetNormalizedData()
        {
            List<double[]> retorno = new List<double[]>();
            for (int i = 0; i < dataPoints.Count; i++)
                retorno.Add(dataPoints[i].vector);
            return retorno;
        }
    }
}
