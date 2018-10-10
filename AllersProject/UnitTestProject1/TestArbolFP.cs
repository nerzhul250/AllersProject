using Estructura;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTestProject1
{
    [TestClass]
    class TestArbolFP
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

        [TestMethod]
        public void testNewFP()
        {
            Nodo raiz = prueba.Raiz;
            Nodo prim = raiz.hijos["a"];
            Nodo sec = raiz.hijos["c"];
        }
    }
}
