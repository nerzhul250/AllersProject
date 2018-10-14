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

            prueba = new ArbolFP();
            prueba.ConstructFPTree(transacc, 0.5);

        }




        [TestMethod]
        public void testNewFP()
        {
            setEscenario1();
            Nodo raiz = prueba.Raiz;
            Assert.AreEqual(raiz.hijos.Count, 2);
            Nodo prim = raiz.hijos["a"];
            Nodo sec = raiz.hijos["c"];

            Assert.AreEqual(prim.Padre, raiz);
            Assert.AreEqual(sec.Padre, raiz);
            Assert.AreEqual(sec.Ocurrencia, 4);
            Assert.AreEqual(prim.Ocurrencia, 1);

            Assert.AreEqual(prim.hijos.Count, 1);
            Nodo prim2 = prim.hijos["d"];
            Assert.AreEqual(prim2.Ocurrencia, 1);
            Assert.AreEqual(prim2.Padre, prim);

            Assert.AreEqual(sec.hijos.Count, 2);
            Nodo sec1 = sec.hijos["d"];
            Nodo sec2 = sec.hijos["a"];
            Assert.AreEqual(sec1.Ocurrencia, 1);
            Assert.AreEqual(sec2.Ocurrencia, 2);
            Assert.AreEqual(sec1.Padre, sec);
            Assert.AreEqual(sec2.Padre, sec);

            Assert.AreEqual(prim2.hijos.Count, 0);
            Assert.AreEqual(sec1.hijos.Count, 0);

            Assert.AreEqual(sec2.hijos.Count, 1);
            Nodo sec21 = sec2.hijos["d"];
            Assert.AreEqual(sec21.Ocurrencia, 1);
            Assert.AreEqual(sec21.Padre, sec2);

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

            List<List<string>> list = prueba.FindFrequentItemsets();
            foreach (List<string> list2 in list)
            {
                Debug.WriteLine("AQUI");
                foreach(string x in list2)
                {
                    Debug.Write(x + " ");
                }
            }
        }


        [TestMethod]
        public void testNewFPDict()
        {
            setEscenario2();
            Nodo raiz = prueba.Raiz;
            Assert.AreEqual(raiz.hijos.Count, 2);
            Nodo prim = raiz.hijos["a"];
            Nodo sec = raiz.hijos["c"];

            Assert.AreEqual(prim.Padre, raiz);
            Assert.AreEqual(sec.Padre, raiz);
            Assert.AreEqual(sec.Ocurrencia, 4);
            Assert.AreEqual(prim.Ocurrencia, 1);

            Assert.AreEqual(prim.hijos.Count, 1);
            Nodo prim2 = prim.hijos["d"];
            Assert.AreEqual(prim2.Ocurrencia, 1);
            Assert.AreEqual(prim2.Padre, prim);

            Assert.AreEqual(sec.hijos.Count, 2);
            Nodo sec1 = sec.hijos["d"];
            Nodo sec2 = sec.hijos["a"];
            Assert.AreEqual(sec1.Ocurrencia, 1);
            Assert.AreEqual(sec2.Ocurrencia, 2);
            Assert.AreEqual(sec1.Padre, sec);
            Assert.AreEqual(sec2.Padre, sec);

            Assert.AreEqual(prim2.hijos.Count, 0);
            Assert.AreEqual(sec1.hijos.Count, 0);

            Assert.AreEqual(sec2.hijos.Count, 1);
            Nodo sec21 = sec2.hijos["d"];
            Assert.AreEqual(sec21.Ocurrencia, 1);
            Assert.AreEqual(sec21.Padre, sec2);

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
