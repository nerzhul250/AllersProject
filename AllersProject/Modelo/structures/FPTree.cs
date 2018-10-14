using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estructura
{
    public class FPTree
    {
        //La raiz del árbol, la cual tiene un valor de null.
        public FPNode Raiz { get; set; }

        //Diccionario que para cada string (Representa el identificador de un elemento), apunta al último elemento de la lista enlazada 
        //formada por el árbol de dicho item.
        public Dictionary<string, FPNode> ultimoListaEnlazada { get; set; }


        //Diccionario que para cada string (Representa el identificador de un elemento), apunta al primer elemento de la lista enlazada 
        //formada por el árbol de dicho item.
        public Dictionary<string, FPNode> primeroListaEnlazada { get; set; }

        //Lista con los items que están en el árbol.
        public List<string> items { get; set; }

        public double minSup { get; set; }

        public Dictionary<List<string>, int> frequentsSupport;

        public FPTree(List<List<String>> Transactions, double minSup)
        {
            Raiz = new FPNode(null, null);
            ultimoListaEnlazada = new Dictionary<string, FPNode>();
            primeroListaEnlazada = new Dictionary<string, FPNode>();
            items = new List<string>();
            this.minSup = minSup;
            ConstructFPTree(Transactions, minSup);
        }

        public FPTree()
        {
            Raiz = new FPNode(null, null);
            ultimoListaEnlazada = new Dictionary<string, FPNode>();
            primeroListaEnlazada = new Dictionary<string, FPNode>();
            items = new List<string>();
        }

        public void ConstructFPTree(List<List<String>> Transactions, double minSup)
        {
            //Guarda, para cada producto (El string es el identificador del producto) el numero de veces que aparece en las transacciones.
            Dictionary<string, int> numberOfOcurrances = new Dictionary<string, int>();

            foreach (List<String> list in Transactions)
            {
                foreach (String ident in list)
                {
                    if (numberOfOcurrances.ContainsKey(ident))
                    {
                        numberOfOcurrances[ident]++;
                    }
                    else
                    {
                        numberOfOcurrances.Add(ident, 1);
                        items.Add(ident);
                    }
                }


            }

            items = items.OrderByDescending(i => numberOfOcurrances[i]).ToList();

            foreach (List<string> trans in Transactions)
            {
                List <string> ordered = trans.OrderByDescending(e => numberOfOcurrances[e]).ToList();
                
                Raiz.InsertarTransaccion(ordered, ultimoListaEnlazada, primeroListaEnlazada, (int)Math.Ceiling(minSup * Transactions.Count), numberOfOcurrances);
            }
        }



        public void ConstructFPTree(Dictionary<List<string>, int> Transactions, double minSup)
        {
            //Guarda, para cada producto (El string es el identificador del producto) el numero de veces que aparece en las transacciones.
            Dictionary<string, int> numberOfOcurrances = new Dictionary<string, int>();

            foreach (List<string> list in Transactions.Keys)
            {
                int times = Transactions[list];
                foreach (string ident in list)
                {
                    if (numberOfOcurrances.ContainsKey(ident))
                    {
                        numberOfOcurrances[ident]+=times;
                    }
                    else
                    {
                        numberOfOcurrances.Add(ident, times);
                        items.Add(ident);
                    }
                }


            }

            items = items.OrderByDescending(i => numberOfOcurrances[i]).ToList();

            foreach (List<string> trans in Transactions.Keys)
            {
                List<string> ordered = trans.OrderByDescending(e => numberOfOcurrances[e]).ToList();

                Raiz.InsertarTransaccion(ordered, ultimoListaEnlazada, primeroListaEnlazada, (int)Math.Ceiling(minSup * Transactions.Count), numberOfOcurrances, Transactions[trans]);
            }
        }

        public List<List<string>> FindFrequentItemsets()
        {
            List < List<string> > frequents = new List<List<string>>();
            FrequentItemSets(new List<string>(), frequents, frequentsSupport);

            return frequents;

        }

        public void FrequentItemSets(List<string> frecuente, List<List<string>> frequents, Dictionary<List<string>, int> supports)
        {
            if (items.Count != 0)
            {
                int j = items.Count - 1;
                while (!primeroListaEnlazada.ContainsKey(items[j]))
                {
                    j--;
                }
                for (int i = j; i >= 0; i--)
                {
                    List<string> frecuenteItem = new List<string>();
                    foreach(string st in frecuente)
                    {
                        frecuenteItem.Add(st);
                    }
                    frecuenteItem.Add(items[i]);
                    int tam = frecuenteItem.Count();
                    if (tam > 1)
                    {
                        frequents.Add(frecuenteItem);
                    }
                    int sup = 0;
                    Dictionary<List <string>, int> transacc = new Dictionary<List<string>, int>();
                    FPNode prim = primeroListaEnlazada[items[i]];
                    while (prim != null)
                    {
                        sup += prim.Ocurrencia;
                        int cont = prim.Ocurrencia;
                        FPNode act = prim.Padre;
                        List<string> transaccion = new List<string>();
                        while (act.Identificador != null)
                        {
                            transaccion.Add(act.Identificador);
                            act = act.Padre;
                        }
                        transacc.Add(transaccion, cont);
                        prim = prim.Siguiente;
                    }
                    supports.Add(frecuenteItem, sup);
                    FPTree conditional = new FPTree();
                    conditional.ConstructFPTree(transacc, minSup);
                    conditional.FrequentItemSets(frecuenteItem, frequents, supports);
                }
            }
        }
    }
}
