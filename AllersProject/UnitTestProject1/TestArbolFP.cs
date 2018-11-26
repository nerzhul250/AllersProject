using Estructura;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1
{
    [TestClass]
    public class TestArbolFP
    {
        FPTree prueba;

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
            prueba = new FPTree(transacc, 0.5);
        }

        private void setEscenario2()
        {
            Dictionary<List<string>, int> transacc = new Dictionary<List<string>, int>();
            List<string> tranUniq = new List<string> { "b", "a", "c" };
            transacc.Add(tranUniq,1);
            List<string> tranUniq2 = new List<string> { "c", "a", "d" };
            transacc.Add(tranUniq2,1);
            List<string> tranUniq3 = new List<string> { "a", "d" };
            transacc.Add(tranUniq3,1);
            List<string> tranUniq4 = new List<string> { "c", "e" };
            transacc.Add(tranUniq4,1);
            List<string> tranUniq5 = new List<string> { "b", "f" };
            transacc.Add(tranUniq5,1);
            List<string> tranUniq6 = new List<string> { "d", "c" };
            transacc.Add(tranUniq6,1);

            prueba = new FPTree();
            prueba.ConstructFPTree(transacc, 3);

        }

        private void setEscenario3()
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
            prueba = new FPTree(transacc, 0.1);
        }

        private void setEscenario4()
        {
            Dictionary<List<string>, int> transacc = new Dictionary<List<string>, int>();
            List<string> tranUniq = new List<string> { "a", "c", "b" };
            transacc.Add(tranUniq, 2);
            List<string> tranUniq2 = new List<string> { "d", "e" };
            transacc.Add(tranUniq2, 3);
            List<string> tranUniq3 = new List<string> { "b", "c" };
            transacc.Add(tranUniq3, 2);
            List<string> tranUniq4 = new List<string> { "b", "d" };
            transacc.Add(tranUniq4, 3);
            prueba = new FPTree();
            prueba.ConstructFPTree(transacc, 4);

        }

        private void setEscenario5()
        {
            List<List<string>> transacc = new List<List<string>>();
            List<string> tranUniq = new List<string> { "b", "c", "a", "e" };
            transacc.Add(tranUniq);
            List<string> tranUniq2 = new List<string> { "c", "d" };
            transacc.Add(tranUniq2);
            List<string> tranUniq3 = new List<string> { "a", "c", "f", "b" };
            transacc.Add(tranUniq3);
            List<string> tranUniq4 = new List<string> { "c", "a", "b" };
            transacc.Add(tranUniq4);
            List<string> tranUniq5 = new List<string> { "f", "e", "d" };
            transacc.Add(tranUniq5);
            List<string> tranUniq6 = new List<string> { "a", "d", "c" };
            transacc.Add(tranUniq6);
            prueba = new FPTree(transacc, 0.5);
        }



        [TestMethod]
        public void testNewFP()
        {
            setEscenario1();
            FPNode raiz = prueba.Raiz;
            Assert.AreEqual(raiz.hijos.Count, 2);
            FPNode prim = raiz.hijos["a"];
            FPNode sec = raiz.hijos["c"];

            Assert.AreEqual(prim.Padre, raiz);
            Assert.AreEqual(sec.Padre, raiz);
            Assert.AreEqual(sec.Ocurrencia, 4);
            Assert.AreEqual(prim.Ocurrencia, 1);

            Assert.AreEqual(prim.hijos.Count, 1);
            FPNode prim2 = prim.hijos["d"];
            Assert.AreEqual(prim2.Ocurrencia, 1);
            Assert.AreEqual(prim2.Padre, prim);

            Assert.AreEqual(sec.hijos.Count, 2);
            FPNode sec1 = sec.hijos["d"];
            FPNode sec2 = sec.hijos["a"];
            Assert.AreEqual(sec1.Ocurrencia, 1);
            Assert.AreEqual(sec2.Ocurrencia, 2);
            Assert.AreEqual(sec1.Padre, sec);
            Assert.AreEqual(sec2.Padre, sec);

            Assert.AreEqual(prim2.hijos.Count, 0);
            Assert.AreEqual(sec1.hijos.Count, 0);

            Assert.AreEqual(sec2.hijos.Count, 1);
            FPNode sec21 = sec2.hijos["d"];
            Assert.AreEqual(sec21.Ocurrencia, 1);
            Assert.AreEqual(sec21.Padre, sec2);

            Assert.AreEqual(sec21.hijos.Count, 0);

            Dictionary<string, FPNode> listaEn = prueba.primeroListaEnlazada;
            FPNode A1 = listaEn["a"];
            Assert.AreEqual(A1, sec2);
            FPNode A2 = A1.Siguiente;
            Assert.AreEqual(A2, prim);
            Assert.IsNull(A2.Siguiente);

            FPNode C1 = listaEn["c"];
            Assert.AreEqual(C1, sec);
            Assert.IsNull(C1.Siguiente);

            FPNode D1 = listaEn["d"];
            Assert.AreEqual(D1, sec21);
            FPNode D2 = D1.Siguiente;
            Assert.AreEqual(D2, prim2);
            FPNode D3 = D2.Siguiente;
            Assert.AreEqual(D3, sec1);
            Assert.IsNull(D3.Siguiente);
        }


        [TestMethod]
        public void testNewFPDict()
        {
            setEscenario2();
            FPNode raiz = prueba.Raiz;
            Assert.AreEqual(raiz.hijos.Count, 2);
            FPNode prim = raiz.hijos["a"];
            FPNode sec = raiz.hijos["c"];

            Assert.AreEqual(prim.Padre, raiz);
            Assert.AreEqual(sec.Padre, raiz);
            Assert.AreEqual(sec.Ocurrencia, 4);
            Assert.AreEqual(prim.Ocurrencia, 1);

            Assert.AreEqual(prim.hijos.Count, 1);
            FPNode prim2 = prim.hijos["d"];
            Assert.AreEqual(prim2.Ocurrencia, 1);
            Assert.AreEqual(prim2.Padre, prim);

            Assert.AreEqual(sec.hijos.Count, 2);
            FPNode sec1 = sec.hijos["d"];
            FPNode sec2 = sec.hijos["a"];
            Assert.AreEqual(sec1.Ocurrencia, 1);
            Assert.AreEqual(sec2.Ocurrencia, 2);
            Assert.AreEqual(sec1.Padre, sec);
            Assert.AreEqual(sec2.Padre, sec);

            Assert.AreEqual(prim2.hijos.Count, 0);
            Assert.AreEqual(sec1.hijos.Count, 0);

            Assert.AreEqual(sec2.hijos.Count, 1);
            FPNode sec21 = sec2.hijos["d"];
            Assert.AreEqual(sec21.Ocurrencia, 1);
            Assert.AreEqual(sec21.Padre, sec2);

            Assert.AreEqual(sec21.hijos.Count, 0);

            Dictionary<string, FPNode> listaEn = prueba.primeroListaEnlazada;
            FPNode A1 = listaEn["a"];
            Assert.AreEqual(A1, sec2);
            FPNode A2 = A1.Siguiente;
            Assert.AreEqual(A2, prim);
            Assert.IsNull(A2.Siguiente);

            FPNode C1 = listaEn["c"];
            Assert.AreEqual(C1, sec);
            Assert.IsNull(C1.Siguiente);

            FPNode D1 = listaEn["d"];
            Assert.AreEqual(D1, sec21);
            FPNode D2 = D1.Siguiente;
            Assert.AreEqual(D2, prim2);
            FPNode D3 = D2.Siguiente;
            Assert.AreEqual(D3, sec1);
            Assert.IsNull(D3.Siguiente);
        }

        [TestMethod]
        public void testNewFPAllFrequent()
        {
            setEscenario3();
            FPNode raiz = prueba.Raiz;
            Assert.AreEqual(raiz.hijos.Count, 3);
            FPNode prim = raiz.hijos["a"];
            FPNode sec = raiz.hijos["c"];
            FPNode ter = raiz.hijos["b"];

            Assert.AreEqual(prim.Padre, raiz);
            Assert.AreEqual(sec.Padre, raiz);
            Assert.AreEqual(ter.Padre, raiz);
            Assert.AreEqual(sec.Ocurrencia, 4);
            Assert.AreEqual(prim.Ocurrencia, 1);
            Assert.AreEqual(ter.Ocurrencia, 1);

            Assert.AreEqual(prim.hijos.Count, 1);
            FPNode prim2 = prim.hijos["d"];
            Assert.AreEqual(prim2.Ocurrencia, 1);
            Assert.AreEqual(prim2.Padre, prim);

            Assert.AreEqual(sec.hijos.Count, 3);
            FPNode sec1 = sec.hijos["d"];
            FPNode sec2 = sec.hijos["a"];
            FPNode sec3 = sec.hijos["e"];
            Assert.AreEqual(sec1.Ocurrencia, 1);
            Assert.AreEqual(sec2.Ocurrencia, 2);
            Assert.AreEqual(sec3.Ocurrencia, 1);
            Assert.AreEqual(sec1.Padre, sec);
            Assert.AreEqual(sec2.Padre, sec);
            Assert.AreEqual(sec3.Padre, sec);

            Assert.AreEqual(ter.hijos.Count, 1);
            FPNode ter1 = ter.hijos["f"];
            Assert.AreEqual(ter1.Ocurrencia, 1);
            Assert.AreEqual(ter1.Padre, ter);

            Assert.AreEqual(prim2.hijos.Count, 0);
            Assert.AreEqual(sec1.hijos.Count, 0);
            Assert.AreEqual(sec3.hijos.Count, 0);
            Assert.AreEqual(ter1.hijos.Count, 0);

            Assert.AreEqual(sec2.hijos.Count, 2);
            FPNode sec21 = sec2.hijos["d"];
            FPNode sec22 = sec2.hijos["b"];
            Assert.AreEqual(sec21.Ocurrencia, 1);
            Assert.AreEqual(sec21.Padre, sec2);
            Assert.AreEqual(sec22.Ocurrencia, 1);
            Assert.AreEqual(sec22.Padre, sec2);

            Assert.AreEqual(sec21.hijos.Count, 0);
            Assert.AreEqual(sec22.hijos.Count, 0);

            Dictionary<string, FPNode> listaEn = prueba.primeroListaEnlazada;
            FPNode A1 = listaEn["a"];
            Assert.AreEqual(A1, sec2);
            FPNode A2 = A1.Siguiente;
            Assert.AreEqual(A2, prim);
            Assert.IsNull(A2.Siguiente);

            FPNode B1 = listaEn["b"];
            Assert.AreEqual(B1, sec22);
            FPNode B2 = B1.Siguiente;
            Assert.AreEqual(B2, ter);
            FPNode B3 = B2.Siguiente;
            Assert.IsNull(B3);

            FPNode C1 = listaEn["c"];
            Assert.AreEqual(C1, sec);
            Assert.IsNull(C1.Siguiente);

            FPNode D1 = listaEn["d"];
            Assert.AreEqual(D1, sec21);
            FPNode D2 = D1.Siguiente;
            Assert.AreEqual(D2, prim2);
            FPNode D3 = D2.Siguiente;
            Assert.AreEqual(D3, sec1);
            Assert.IsNull(D3.Siguiente);

            FPNode E1 = listaEn["e"];
            Assert.AreEqual(E1, sec3);
            FPNode E2 = E1.Siguiente;
            Assert.IsNull(E2);

            FPNode F1 = listaEn["f"];
            Assert.AreEqual(F1, ter1);
            FPNode F2 = F1.Siguiente;
            Assert.IsNull(E2);
        }

        [TestMethod]
        public void testNewFPDictNumber()
        {
            setEscenario4();
            FPNode raiz = prueba.Raiz;
            Assert.AreEqual(raiz.hijos.Count, 2);
            FPNode prim = raiz.hijos["b"];
            FPNode sec = raiz.hijos["d"];

            Assert.AreEqual(prim.Identificador, "b");
            Assert.AreEqual(prim.Ocurrencia, 7);
            Assert.AreEqual(prim.hijos.Count, 2);

            Assert.AreEqual(sec.Identificador, "d");
            Assert.AreEqual(sec.Ocurrencia, 3);
            Assert.AreEqual(sec.hijos.Count, 0);

            FPNode prim1 = prim.hijos["c"];
            Assert.AreEqual(prim1.Identificador, "c");
            Assert.AreEqual(prim1.Ocurrencia, 4);
            Assert.AreEqual(prim1.hijos.Count, 0);

            FPNode prim2 = prim.hijos["d"];
            Assert.AreEqual(prim2.Identificador, "d");
            Assert.AreEqual(prim2.Ocurrencia, 3);
            Assert.AreEqual(prim2.hijos.Count, 0);

            Dictionary<string, FPNode> listas = prueba.primeroListaEnlazada;

            Assert.AreEqual(listas.Count, 3);
            FPNode B1 = listas["b"];
            Assert.AreEqual(B1, prim);
            Assert.IsNull(B1.Siguiente);

            FPNode C1 = listas["c"];
            Assert.AreEqual(C1, prim1);
            Assert.IsNull(C1.Siguiente);

            FPNode D1 = listas["d"];
            Assert.AreEqual(D1, sec);
            FPNode D2 = D1.Siguiente;
            Assert.AreEqual(D2, prim2);
            Assert.IsNull(D2.Siguiente);

        }

        [TestMethod]
        public void testFindFrequentItemsetsEmpty()
        {
            setEscenario1();
            Assert.AreEqual(prueba.FindFrequentItemsets().Count, 0);
        }

        [TestMethod]
        public void testAllTransactionsFrequents()
        {
            setEscenario3();
            List<List<string>> result = new List<List<string>> { new List<string> {"b","f" },
            new List<string> { "c", "e" },  new List<string> {"b", "c" },
             new List<string> {"a","b","c" },  new List<string>{"a","b" },
             new List<string> {"c","d" },  new List<string> {"a", "c", "d" },
             new List<string> {"a","d" },  new List<string> {"a", "c" } };

            List<List<string>> test = prueba.FindFrequentItemsets();
            Assert.AreEqual(test.Count, result.Count);
            for (int i = 0; i < test.Count; i++)
            {
                Assert.AreEqual(test[i].Count, result[i].Count);
                for (int j = 0; j < test[i].Count; j++)
                {
                    Assert.AreEqual(test[i][j], result[i][j]);
                }
            }
        }

        [TestMethod]
        public void testFrequentItemsets()
        {
            setEscenario5();

            List<List<string>> result = new List<List<string>> { new List<string> {"b","c" },
            new List<string> { "a", "b", "c" },  new List<string> {"a", "b"},
             new List<string> {"a", "c"} };

            List<List<string>> test = prueba.FindFrequentItemsets();
            Assert.AreEqual(test.Count, result.Count);
            for (int i = 0; i < test.Count; i++)
            {
                Assert.AreEqual(test[i].Count, result[i].Count);
                for (int j = 0; j < test[i].Count; j++)
                {
                    Assert.AreEqual(test[i][j], result[i][j]);
                }
            }
        }
    }
}
