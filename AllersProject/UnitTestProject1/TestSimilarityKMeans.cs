using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Modelo;
using System.Diagnostics;

namespace UnitTestProject1
{
    [TestClass]
    public class TestSimilarityKMeans
    {
        SimilarityAnalysisKMeans kMeans;
        public void setupEscenario1()
        {
            Dictionary<Customer, DataPoint> mapCusToDP = new Dictionary<Customer,DataPoint>();
            Dictionary<int, Item> mapDimToIt = new Dictionary<int, Item>();
            mapCusToDP.Add(new Customer("1", "alv", "alv", "alv", "alv"), new DataPoint(1, new double[] { 2, 8, 4, 0, 6, 7, 0 }));
            mapCusToDP.Add(new Customer("2", "alv", "alv", "alv", "alv"), new DataPoint(2, new double[] { 0, 2, 5, 6, 1, 8, 0 }));
            mapCusToDP.Add(new Customer("3", "alv", "alv", "alv", "alv"), new DataPoint(3, new double[] { 2, 2, 3, 9, 0, 5, 3 }));
            mapCusToDP.Add(new Customer("4", "alv", "alv", "alv", "alv"), new DataPoint(4, new double[] { 2, 8, 4, 0, 6, 7, 0 }));
            mapCusToDP.Add(new Customer("5", "alv", "alv", "alv", "alv"), new DataPoint(5, new double[] { 2, 0, 4, 0, 6, 3, 0 }));
            mapCusToDP.Add(new Customer("6", "alv", "alv", "alv", "alv"), new DataPoint(6, new double[] { 5, 0, 2, 0, 6, 0, 9 }));
            mapCusToDP.Add(new Customer("7", "alv", "alv", "alv", "alv"), new DataPoint(7, new double[] { 2, 3, 4, 5, 0, 7, 0 }));
            mapCusToDP.Add(new Customer("8", "alv", "alv", "alv", "alv"), new DataPoint(8, new double[] { 0, 9, 0, 0, 6, 7, 1 }));
            mapDimToIt.Add(0, new Item("a", "lv"));
            mapDimToIt.Add(1, new Item("b", "lv"));
            mapDimToIt.Add(2, new Item("c", "lv"));
            mapDimToIt.Add(3, new Item("d", "lv"));
            mapDimToIt.Add(4, new Item("e", "lv"));
            mapDimToIt.Add(5, new Item("f", "lv"));
            mapDimToIt.Add(6, new Item("g", "lv"));
            kMeans = new SimilarityAnalysisKMeans(4, 3, 7, 9, mapCusToDP, mapDimToIt);
        }

        private void setupEscenario2 ()
        {
            kMeans = new SimilarityAnalysisKMeans(4, 3, 7, 9, new Dictionary<Customer, DataPoint>(), new Dictionary<int, Item>());
        }
        [TestMethod]
            public void TestAngularDistance ()
        {
            setupEscenario2();
            double[] x = { 3, 4, 0, 0, 7 };
            double[] y = { 0, 3, 7, 5, 0 };
            double[] z = { 0, 1, 2, 0, 0 };
            //la distancia entre y y z debería ser menor que x y y o x y z
            Assert.IsTrue(kMeans.AngularDistance(x, y) > kMeans.AngularDistance(z, y));
            Assert.IsTrue(kMeans.AngularDistance(x, y) > kMeans.AngularDistance(z, x));

            double[] a = { 155, 985, 0, 0, 5132 };
            double[] b = { 0, 1000, 75516, 55, 0 };
            double[] c = { 0, 12, 2222, 0, 0 };
            Assert.IsTrue(kMeans.AngularDistance(a, b) > kMeans.AngularDistance(b, c));
            Assert.IsTrue(kMeans.AngularDistance(a, c) > kMeans.AngularDistance(b, a));

            double[] d = { 155, 985, 0, 0, 5132, 5132, 0, 3816 };
            double[] e = { 0, 356, 20516, 55, 0, 13632, 954, 0 };
            double[] f = { 7865, 0, 2222, 0, 0, 4853, 7845, 4865 };
            Assert.IsTrue(kMeans.AngularDistance(e, f) > kMeans.AngularDistance(d, f));
            Assert.IsTrue(kMeans.AngularDistance(d, e) > kMeans.AngularDistance(f, d));
        }
        
        [TestMethod]
        public void TestKMeans()
        {
            setupEscenario1();
            kMeans.Kmeans();
            List<Cluster> clus = kMeans.clusters;
            Debug.WriteLine("-------------------------");
            for(int i = 0; i<clus.Count; i++)
            {
                Debug.WriteLine(clus.Count);
                for (int j = 0; j < clus.Count; j++)
                {
                    if (i == j && j < clus.Count-1)
                        j++;
                    else if (i == j)
                        break;
                    foreach (DataPoint dp in clus[i].cluster)
                    {
                        Debug.WriteLine(kMeans.AngularDistance(clus[i].centroid, dp.vector) + " <= " + kMeans.AngularDistance(clus[j].centroid, dp.vector));
                        Assert.IsTrue(kMeans.AngularDistance(clus[i].centroid, dp.vector) <= kMeans.AngularDistance(clus[j].centroid, dp.vector));
                    }
                }
            }
        }
    }
}
