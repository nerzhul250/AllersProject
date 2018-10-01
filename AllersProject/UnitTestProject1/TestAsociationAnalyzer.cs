using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Modelo;
namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {

        AssociationAnalyzer asso;
        private void setupEscenario1()
        {
            DataManager data = new DataManager("../../../DatosTests/Escenario1/");
            asso = new AssociationAnalyzer(data, 3, 0, 0.35, 0);
            asso.binaryTransactions = new List<long> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            asso.itemSetToSupport.Add(5, 2);
            asso.itemSetToSupport.Add(4, 5);
            asso.itemSetToSupport.Add(1, 6);
        }

        private void setupEscenario2()
        {
            DataManager data = new DataManager("../../../DatosTests/Escenario1/");
            asso = new AssociationAnalyzer(data, 6, 0, 0.35, 0);
        }

        private void setupEscenario3()
        {
            DataManager data = new DataManager("../../../DatosTests/Escenario1/");
            asso = new AssociationAnalyzer(data, 4, 0.5, 0, 3);
            asso.mapFromBinaryPositionToItem = new Dictionary<int, Item>();
            asso.mapFromBinaryPositionToItem.Add(0, new Item("120", "Alcohol"));
            asso.mapFromBinaryPositionToItem.Add(1, new Item("130", "Manzana"));
            asso.mapFromBinaryPositionToItem.Add(2, new Item("200", "Pera"));
            asso.mapFromBinaryPositionToItem.Add(3, new Item("300", "Papa"));

            asso.binaryTransactions = new List<long>
            {
                3, 12, 6, 1, 8, 15, 12, 14, 9, 14
            };
        }

        private void setupEscenario4()
        {
            DataManager data = new DataManager("../../../DatosTests/Escenario1/");
            asso = new AssociationAnalyzer(data, 6, 0.3, 0, 3);

            asso.binaryTransactions = new List<long>
            {
                39, 60, 8, 56, 56, 56, 35, 42, 51, 27, 54, 63, 1, 32
            };
        }

        private void setupEscenario5()
        {
            DataManager data = new DataManager("../../../DatosTests/Escenario1/");
            asso = new AssociationAnalyzer(data, 4, 0, 0.35, 3);
            asso.binaryTransactions = new List<long>();
            for (int i = 0; i < 100; i++)
            {
                asso.binaryTransactions.Add(i + 1);
            }
            asso.itemSetToSupport.Add(8, 70);
            asso.itemSetToSupport.Add(12, 50);
            asso.itemSetToSupport.Add(4, 80);
            asso.itemSetToSupport.Add(9, 55);
            asso.itemSetToSupport.Add(2, 83);
            asso.itemSetToSupport.Add(14, 30);
            asso.itemSetToSupport.Add(1, 91);
            asso.itemSetToSupport.Add(11, 10);
            asso.itemSetToSupport.Add(10, 58);
            asso.itemSetToSupport.Add(6, 66);
            asso.itemSetToSupport.Add(3, 60);



        }

        
        //[TestMethod]
        public void TestGenerateFIS()
        {
            DataManager data = new DataManager("../../../DatosTests/Escenario1/");
            AssociationAnalyzer alv = new AssociationAnalyzer(data, 3, 0.4, 0.4, 2);
            Item[] items = new Item[3];
            items[0] = data.mapFromItemCodeToItem["1"];
            items[1] = data.mapFromItemCodeToItem["2"];
            items[2] = data.mapFromItemCodeToItem["3"];
            List<Item[]> resultado = alv.GenerateFrequentItemSets();
            //for (int i = 0; i <resultado.Count; i++) {
            //    for(int j = 0; j<resultado[i].Length; j++)
            //    {
            //        Debug.Write(resultado[i][j].ItemCode + ",");
            //    }
            //    Debug.WriteLine("");
            //}
            Item[] freq = new Item[1];
            freq[0] = data.mapFromItemCodeToItem["1"];
            //Debug.WriteLine(freq[0].ItemCode + "," + freq[1].ItemCode);
            Assert.AreEqual(freq[0], resultado[0][0]);
            freq = new Item[1];
            freq[0] = data.mapFromItemCodeToItem["2"];
            Assert.AreEqual(freq[0], resultado[1][0]);
            freq = new Item[2];
            freq[0] = data.mapFromItemCodeToItem["1"];
            freq[1] = data.mapFromItemCodeToItem["2"];
            Assert.AreEqual(freq[0], resultado[2][0]);
            Assert.AreEqual(freq[1], resultado[2][1]);

            alv = new AssociationAnalyzer(new DataManager("../../../Datos/Escenario1/"), 4, 0.3, 0.3, 2);
            items = new Item[3];
            items[0] = data.mapFromItemCodeToItem["1"];
            items[1] = data.mapFromItemCodeToItem["2"];
            items[2] = data.mapFromItemCodeToItem["3"];
            resultado = alv.GenerateFrequentItemSets();
            //for (int i = 0; i < resultado.Count; i++)
            //{
            //    for (int j = 0; j < resultado[i].Length; j++)
            //    {
            //        Debug.Write(resultado[i][j].ItemCode + ",");
            //    }
            //    Debug.WriteLine("");
            //}
            freq = new Item[1];
            freq[0] = data.mapFromItemCodeToItem["2"];
            Assert.AreEqual(freq[0], resultado[0][0]);
            freq = new Item[1];
            freq[0] = data.mapFromItemCodeToItem["1"];
            Assert.AreEqual(freq[0], resultado[1][0]);
            freq = new Item[2];
            freq[0] = data.mapFromItemCodeToItem["2"];
            freq[1] = data.mapFromItemCodeToItem["1"];
            Assert.AreEqual(freq[0], resultado[2][0]);
            Assert.AreEqual(freq[1], resultado[2][1]);
            freq = new Item[1];
            freq[0] = data.mapFromItemCodeToItem["3"];
            Assert.AreEqual(freq[0], resultado[3][0]);
            freq = new Item[2];
            freq[0] = data.mapFromItemCodeToItem["3"];
            freq[1] = data.mapFromItemCodeToItem["2"];
            Assert.AreEqual(freq[0], resultado[4][0]);
            Assert.AreEqual(freq[1], resultado[4][1]);

            alv = new AssociationAnalyzer(new DataManager("../../../Datos/Escenario2/"), 4, 0.3, 0.3, 2);
            items = new Item[4];
            items[0] = data.mapFromItemCodeToItem["1"];
            items[1] = data.mapFromItemCodeToItem["2"];
            items[2] = data.mapFromItemCodeToItem["3"];
            items[3] = data.mapFromItemCodeToItem["4"];
            resultado = alv.GenerateFrequentItemSets();
            //for (int i = 0; i < resultado.Count; i++)
            //{
            //    for (int j = 0; j < resultado[i].Length; j++)
            //    {
            //        Debug.Write(resultado[i][j].ItemCode + ",");
            //    }
            //    Debug.WriteLine("");
            //}
            freq = new Item[1];
            freq[0] = data.mapFromItemCodeToItem["2"];
            Assert.AreEqual(freq[0], resultado[0][0]);
            freq = new Item[1];
            freq[0] = data.mapFromItemCodeToItem["4"];
            Assert.AreEqual(freq[0], resultado[1][0]);
            freq = new Item[2];
            freq[0] = data.mapFromItemCodeToItem["4"];
            freq[1] = data.mapFromItemCodeToItem["2"];
            Assert.AreEqual(freq[0], resultado[2][0]);
            Assert.AreEqual(freq[1], resultado[2][1]);
            freq = new Item[1];
            freq[0] = data.mapFromItemCodeToItem["3"];
            Assert.AreEqual(freq[0], resultado[3][0]);
            freq = new Item[2];
            freq[0] = data.mapFromItemCodeToItem["3"];
            freq[1] = data.mapFromItemCodeToItem["2"];
            Assert.AreEqual(freq[0], resultado[4][0]);
            Assert.AreEqual(freq[1], resultado[4][1]);
            freq = new Item[2];
            freq[0] = data.mapFromItemCodeToItem["3"];
            freq[1] = data.mapFromItemCodeToItem["4"];
            Assert.AreEqual(freq[0], resultado[5][0]);
            Assert.AreEqual(freq[1], resultado[5][1]);
            freq = new Item[1];
            freq[0] = data.mapFromItemCodeToItem["1"];
            Assert.AreEqual(freq[0], resultado[6][0]);
            freq = new Item[2];
            freq[0] = data.mapFromItemCodeToItem["1"];
            freq[1] = data.mapFromItemCodeToItem["2"];
            Assert.AreEqual(freq[0], resultado[7][0]);
            Assert.AreEqual(freq[1], resultado[7][1]);
            freq = new Item[2];
            freq[0] = data.mapFromItemCodeToItem["1"];
            freq[1] = data.mapFromItemCodeToItem["3"];
            Assert.AreEqual(freq[0], resultado[8][0]);
            Assert.AreEqual(freq[1], resultado[8][1]);
            
        }

        [TestMethod]
        public void testAprioriRuleGeneration()
        {
            setupEscenario5();
            asso.AprioriRuleGeneration(new List<List<long>>
            {
                new List<long>{
                    10, 3
                },
                new List<long>
                {
                    14, 11
                }

            });
            List<Tuple<long, long>> asos = asso.rules;

            Assert.AreEqual(asos.Count, 10);
            Tuple<long, long> act = asos[0];
            Assert.AreEqual(act.Item1, 8);
            Assert.AreEqual(act.Item2, 2);

            act = asos[1];
            Assert.AreEqual(act.Item1, 2);
            Assert.AreEqual(act.Item2, 8);

            act = asos[2];
            Assert.AreEqual(act.Item1, 2);
            Assert.AreEqual(act.Item2, 1);

            act = asos[3];
            Assert.AreEqual(act.Item1, 1);
            Assert.AreEqual(act.Item2, 2);

            act = asos[4];
            Assert.AreEqual(act.Item1, 12);
            Assert.AreEqual(act.Item2, 2);

            act = asos[5];
            Assert.AreEqual(act.Item1, 10);
            Assert.AreEqual(act.Item2, 4);

            act = asos[6];
            Assert.AreEqual(act.Item1, 6);
            Assert.AreEqual(act.Item2, 8);

            act = asos[7];
            Assert.AreEqual(act.Item1, 8);
            Assert.AreEqual(act.Item2, 6);

            act = asos[8];
            Assert.AreEqual(act.Item1, 4);
            Assert.AreEqual(act.Item2, 10);

            act = asos[9];
            Assert.AreEqual(act.Item1, 2);
            Assert.AreEqual(act.Item2, 12);



        }

        [TestMethod]
        public void testApGenRules()
        {
            setupEscenario1();
            asso.ApGenRules(5, new List<long> {4, 1});
            Assert.AreEqual(asso.rules.Count, 1);
            Assert.AreEqual(asso.rules[0].Item1, 4);
            Assert.AreEqual(asso.rules[0].Item2, 1);
        }

        [TestMethod]
        public void testApioriGen()
        {
            setupEscenario2();
            List<long> res = asso.AprioriGen(new List<long> { 44, 28, 49, 41, 26, 50 });
            Assert.AreEqual(res.Count, 3);
            Assert.AreEqual(res[0], 45);
            Assert.AreEqual(res[1], 30);
            Assert.AreEqual(res[2], 51);
        }

        [TestMethod]
        public void testGenerateFrequentItemSetsApiori()
        {
            setupEscenario3();
            List<List<long>> freq = asso.GenerateFrequentItemSetsApriori();
            Assert.AreEqual(freq.Count, 2);
            Assert.AreEqual(freq[0].Count, 4);
            Assert.AreEqual(freq[1].Count, 1);

            Assert.AreEqual(freq[1][0], 12);

        }

        [TestMethod]
        public void testRemoveNonFrequentItemSets()
        {
            setupEscenario4();
            List<long> res = asso.RemoveNonFrequentItemSetsFromCandidateSet(new List<long>
            {
                36, 24, 40
            });

            Assert.AreEqual(res.Count, 2);
            Assert.AreEqual(res[0], 24);
            Assert.AreEqual(res[1], 40);
        }




    }
}