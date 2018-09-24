using System;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Modelo;
namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestGenerateFIS()
        {
            AssociationAnalyzer alv = new AssociationAnalyzer(new DataManager("../../../Datos/Escenario1/"), 3, 0.4, 0.4, 2);
            Item[] items = new Item[3];
            items[0] = alv.data.mapFromItemCodeToItem["1"];
            items[1] = alv.data.mapFromItemCodeToItem["2"];
            items[2] = alv.data.mapFromItemCodeToItem["3"];
            System.Collections.Generic.List<Item[]> resultado = alv.GenerateFrequentItemSets(items);
            //for (int i = 0; i <resultado.Count; i++) {
            //    for(int j = 0; j<resultado[i].Length; j++)
            //    {
            //        Debug.Write(resultado[i][j].ItemCode + ",");
            //    }
            //    Debug.WriteLine("");
            //}
            Item[] freq = new Item[1];
            freq[0] = alv.data.mapFromItemCodeToItem["1"];
            //Debug.WriteLine(freq[0].ItemCode + "," + freq[1].ItemCode);
            Assert.AreEqual(freq[0], resultado[0][0]);
            freq = new Item[1];
            freq[0] = alv.data.mapFromItemCodeToItem["2"];
            Assert.AreEqual(freq[0], resultado[1][0]);
            freq = new Item[2];
            freq[0] = alv.data.mapFromItemCodeToItem["2"];
            freq[1] = alv.data.mapFromItemCodeToItem["1"];
            Assert.AreEqual(freq[0], resultado[2][0]);
            Assert.AreEqual(freq[1], resultado[2][1]);

            alv = new AssociationAnalyzer(new DataManager("../../../Datos/Escenario1/"), 4, 0.3, 0.3, 2);
            items = new Item[3];
            items[0] = alv.data.mapFromItemCodeToItem["1"];
            items[1] = alv.data.mapFromItemCodeToItem["2"];
            items[2] = alv.data.mapFromItemCodeToItem["3"];
            resultado = alv.GenerateFrequentItemSets(items);
            //for (int i = 0; i < resultado.Count; i++)
            //{
            //    for (int j = 0; j < resultado[i].Length; j++)
            //    {
            //        Debug.Write(resultado[i][j].ItemCode + ",");
            //    }
            //    Debug.WriteLine("");
            //}
            freq = new Item[1];
            freq[0] = alv.data.mapFromItemCodeToItem["1"];
            Assert.AreEqual(freq[0], resultado[0][0]);
            freq = new Item[1];
            freq[0] = alv.data.mapFromItemCodeToItem["2"];
            Assert.AreEqual(freq[0], resultado[1][0]);
            freq = new Item[2];
            freq[0] = alv.data.mapFromItemCodeToItem["2"];
            freq[1] = alv.data.mapFromItemCodeToItem["1"];
            Assert.AreEqual(freq[0], resultado[2][0]);
            Assert.AreEqual(freq[1], resultado[2][1]);
            freq = new Item[1];
            freq[0] = alv.data.mapFromItemCodeToItem["3"];
            Assert.AreEqual(freq[0], resultado[3][0]);
            freq = new Item[2];
            freq[0] = alv.data.mapFromItemCodeToItem["3"];
            freq[1] = alv.data.mapFromItemCodeToItem["2"];
            Assert.AreEqual(freq[0], resultado[4][0]);
            Assert.AreEqual(freq[1], resultado[4][1]);

            alv = new AssociationAnalyzer(new DataManager("../../../Datos/Escenario2/"), 4, 0.3, 0.3, 2);
            items = new Item[4];
            items[0] = alv.data.mapFromItemCodeToItem["1"];
            items[1] = alv.data.mapFromItemCodeToItem["2"];
            items[2] = alv.data.mapFromItemCodeToItem["3"];
            items[3] = alv.data.mapFromItemCodeToItem["4"];
            resultado = alv.GenerateFrequentItemSets(items);
            //for (int i = 0; i < resultado.Count; i++)
            //{
            //    for (int j = 0; j < resultado[i].Length; j++)
            //    {
            //        Debug.Write(resultado[i][j].ItemCode + ",");
            //    }
            //    Debug.WriteLine("");
            //}
            freq = new Item[1];
            freq[0] = alv.data.mapFromItemCodeToItem["2"];
            Assert.AreEqual(freq[0], resultado[0][0]);
            freq = new Item[1];
            freq[0] = alv.data.mapFromItemCodeToItem["4"];
            Assert.AreEqual(freq[0], resultado[1][0]);
            freq = new Item[2];
            freq[0] = alv.data.mapFromItemCodeToItem["4"];
            freq[1] = alv.data.mapFromItemCodeToItem["2"];
            Assert.AreEqual(freq[0], resultado[2][0]);
            Assert.AreEqual(freq[1], resultado[2][1]);
            freq = new Item[1];
            freq[0] = alv.data.mapFromItemCodeToItem["3"];
            Assert.AreEqual(freq[0], resultado[3][0]);
            freq = new Item[2];
            freq[0] = alv.data.mapFromItemCodeToItem["3"];
            freq[1] = alv.data.mapFromItemCodeToItem["2"];
            Assert.AreEqual(freq[0], resultado[4][0]);
            Assert.AreEqual(freq[1], resultado[4][1]);
            freq = new Item[2];
            freq[0] = alv.data.mapFromItemCodeToItem["3"];
            freq[1] = alv.data.mapFromItemCodeToItem["4"];
            Assert.AreEqual(freq[0], resultado[5][0]);
            Assert.AreEqual(freq[1], resultado[5][1]);
            freq = new Item[1];
            freq[0] = alv.data.mapFromItemCodeToItem["1"];
            Assert.AreEqual(freq[0], resultado[6][0]);
            freq = new Item[2];
            freq[0] = alv.data.mapFromItemCodeToItem["1"];
            freq[1] = alv.data.mapFromItemCodeToItem["2"];
            Assert.AreEqual(freq[0], resultado[7][0]);
            Assert.AreEqual(freq[1], resultado[7][1]);
            freq = new Item[2];
            freq[0] = alv.data.mapFromItemCodeToItem["1"];
            freq[1] = alv.data.mapFromItemCodeToItem["3"];
            Assert.AreEqual(freq[0], resultado[8][0]);
            Assert.AreEqual(freq[1], resultado[8][1]);

        }

        [TestMethod]
        public void TestCommonItems()
        {
            AssociationAnalyzer alv = new AssociationAnalyzer(new DataManager("../../../Datos/Escenario1/"), 2, 0.2, 0.2, 3);
            Item[] comunes = alv.CommonItems();
            //for ( int i=0; i<comunes.Length; i++)
            //{
            //Debug.Write(comunes[i].ItemCode +",");
            //}
            Assert.AreEqual("1", comunes[0].ItemCode);
            Assert.AreEqual("2", comunes[1].ItemCode);

            alv = new AssociationAnalyzer(new DataManager("../../../Datos/Escenario2/"), 3, 0.2, 0.2, 3);
            comunes = alv.CommonItems();
            for (int i = 0; i < comunes.Length; i++)
            {
                Debug.Write(comunes[i].ItemCode + ",");
            }
            Assert.AreEqual("2", comunes[0].ItemCode);
            Assert.AreEqual("4", comunes[1].ItemCode);
            Assert.AreEqual("3", comunes[2].ItemCode);
        }
    }
}
