using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Numerics;
using Estructura;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Modelo;
namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {

        AssociationAnalyzerApriori asso;
        private void setupEscenario1()
        {
            asso = new AssociationAnalyzerApriori(3, 0, 0.35, 0);
            asso.binaryTransactions = new List<BigInteger> {1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
            asso.totalNumberOfTransactions = asso.binaryTransactions.Count;
            asso.itemSetToSupport.Add(5, 2);
            asso.itemSetToSupport.Add(4, 5);
            asso.itemSetToSupport.Add(1, 6);
        }

        private void setupEscenario2()
        { 
            asso = new AssociationAnalyzerApriori(6, 0, 0.35, 0);
        }

        private void setupEscenario3()
        {
            asso = new AssociationAnalyzerApriori(4, 0.5, 0, 3);
            asso.mapFromBinaryPositionToItem = new Dictionary<int, Item>();
            asso.mapFromBinaryPositionToItem.Add(0, new Item("120", "Alcohol"));
            asso.mapFromBinaryPositionToItem.Add(1, new Item("130", "Manzana"));
            asso.mapFromBinaryPositionToItem.Add(2, new Item("200", "Pera"));
            asso.mapFromBinaryPositionToItem.Add(3, new Item("300", "Papa"));

            asso.binaryTransactions = new List<BigInteger>
            {
                3, 12, 6, 1, 8, 15, 12, 14, 9, 14
            };
            asso.totalNumberOfTransactions = asso.binaryTransactions.Count;
        }

        private void setupEscenario4()
        {
            asso = new AssociationAnalyzerApriori(6, 0.3, 0, 3);

            asso.binaryTransactions = new List<BigInteger>
            {
                39, 60, 8, 56, 56, 56, 35, 42, 51, 27, 54, 63, 1, 32
            };
            asso.totalNumberOfTransactions = asso.binaryTransactions.Count;
        }

        private void setupEscenario5()
        {
            asso = new AssociationAnalyzerApriori(4, 0, 0.35, 3);
            asso.binaryTransactions = new List<BigInteger>();
            for (int i = 0; i < 100; i++)
            {
                asso.binaryTransactions.Add(i + 1);
            }
            asso.totalNumberOfTransactions = asso.binaryTransactions.Count;
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
            AssociationAnalyzerApriori alv = new AssociationAnalyzerApriori(data, 3, 0.4, 0.4, 2);
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

            alv = new AssociationAnalyzerApriori(new DataManager("../../../Datos/Escenario1/"), 4, 0.3, 0.3, 2);
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

            alv = new AssociationAnalyzerApriori(new DataManager("../../../Datos/Escenario2/"), 4, 0.3, 0.3, 2);
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
            asso.AprioriRuleGeneration(new List<List<BigInteger>>
            {
                new List<BigInteger>{
                    10, 3
                },
                new List<BigInteger>
                {
                    14, 11
                }

            });
            List<Tuple<BigInteger, BigInteger>> asos = asso.rules;

            Assert.AreEqual(asos.Count, 10);
            Tuple<BigInteger, BigInteger> act = asos[0];
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
            LinkedList<BigInteger> ha = new LinkedList<BigInteger>();
            ha.AddFirst(4);
            ha.AddFirst(1);
            asso.ApGenRules(5, ha);
            Assert.AreEqual(asso.rules.Count, 1);
            Assert.AreEqual(asso.rules[0].Item1, 4);
            Assert.AreEqual(asso.rules[0].Item2, 1);
        }

        [TestMethod]
        public void testApioriGen()
        {
            setupEscenario2();
            LinkedList<BigInteger> ha = new LinkedList<BigInteger>();
            ha.AddLast(44);
            ha.AddLast(28);
            ha.AddLast(49);
            ha.AddLast(41);
            ha.AddLast(26);
            ha.AddLast(50);
            LinkedList<BigInteger> res = asso.AprioriGen(ha);
            Assert.AreEqual(res.Count, 3);
            LinkedListNode<BigInteger> fis = res.First;
            Assert.AreEqual(fis.Value, 45);
            fis=fis.Next;
            Assert.AreEqual(fis.Value, 30);
            fis = fis.Next;
            Assert.AreEqual(fis.Value, 51);
        }

        [TestMethod]
        public void testGenerateFrequentItemSetsApiori()
        {
            setupEscenario3();
            List<List<BigInteger>> freq = asso.GenerateFrequentItemSetsApriori();
            Assert.AreEqual(freq.Count, 2);
            Assert.AreEqual(freq[0].Count, 4);
            Assert.AreEqual(freq[1].Count, 1);

            Assert.AreEqual(freq[1][0], 12);

        }

        [TestMethod]
        public void testRemoveNonFrequentItemSets()
        {
            setupEscenario4();
            LinkedList<BigInteger> ha = new LinkedList<BigInteger>();
            ha.AddFirst(36);
            ha.AddFirst(24);
            ha.AddFirst(40);
            LinkedList<BigInteger> res = asso.RemoveNonFrequentItemSetsFromCandidateSet(ha);
            LinkedListNode<BigInteger> fis = res.First;
            Assert.AreEqual(res.Count, 2);
            Assert.AreEqual(fis.Value, 40);
            fis = fis.Next;
            Assert.AreEqual(fis.Value, 24);
        }






        ArbolFP prueba;

        private void setEscenario1()
        {
            List<List<string>> transacc = new List<List<string>>();
            List<string> tranUniq = new List<string> { "b", "a", "c" };
            transacc.Add(tranUniq);
            List<string> tranUniq2 = new List<string> { "c", "a", "d" };
            transacc.Add(tranUniq2);
            List<string> tranUniq3 = new List<string> { "a", "d" };
            transacc.Add(tranUniq3);
            List<string> tranUniq4 = new List<string> { "c", "e" };
            transacc.Add(tranUniq4);
            List<string> tranUniq5 = new List<string> { "b", "f" };
            transacc.Add(tranUniq5);
            List<string> tranUniq6 = new List<string> { "d", "c" };
            transacc.Add(tranUniq6);
            prueba = new ArbolFP(transacc, 0.5);
        }




        [TestMethod]
        public void testNewFP()
        {
            setEscenario1();
            Nodo raiz = prueba.Raiz;
            Assert.AreEqual(raiz.hijos.Count, 2);
            Nodo prim = raiz.hijos["a"];
            Nodo sec = raiz.hijos["c"];

            Assert.AreEqual(sec.Ocurrencia, 4);
            Assert.AreEqual(prim.Ocurrencia, 1);

            Assert.AreEqual(prim.hijos.Count, 1);
            Nodo prim2 = prim.hijos["d"];
            Assert.AreEqual(prim2.Ocurrencia, 1);

            Assert.AreEqual(sec.hijos.Count, 2);
            Nodo sec1 = sec.hijos["d"];
            Nodo sec2 = sec.hijos["a"];
            Assert.AreEqual(sec1.Ocurrencia, 1);
            Assert.AreEqual(sec2.Ocurrencia, 2);

            Assert.AreEqual(prim2.hijos.Count, 0);
            Assert.AreEqual(sec1.hijos.Count, 0);

            Assert.AreEqual(sec2.hijos.Count, 1);
            Nodo sec21 = sec2.hijos["d"];
            Assert.AreEqual(sec21.Ocurrencia, 1);

            Assert.AreEqual(sec21.hijos.Count, 0);

            Dictionary<string, Nodo> listaEn = prueba.primeroListaEnlazada;
            Nodo A1 = listaEn["a"];
            Assert.AreEqual(A1, sec2);
            Nodo A2 = A1.Siguiente;
            Assert.AreEqual(A2, prim);
            Assert.IsNull(A2.Siguiente);

            Nodo C1 = listaEn["c"];
            Assert.AreEqual(C1, sec);
            Assert.IsNull(C1.Siguiente);

            Nodo D1 = listaEn["d"];
            Assert.AreEqual(D1, sec21);
            Nodo D2 = D1.Siguiente;
            Assert.AreEqual(D2, prim2);
            Nodo D3 = D2.Siguiente;
            Assert.AreEqual(D3, sec1);
            Assert.IsNull(D3.Siguiente);
        }

    }
}