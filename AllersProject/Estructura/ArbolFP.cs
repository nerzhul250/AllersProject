using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estructura
{
    class ArbolFP
    {
        //La raiz del árbol, la cual tiene un valor de null.
        public Nodo Raiz { get; set; }

        //Diccionario que para cada string (Representa el identificador de un elemento), apunta al último elemento de la lista enlazada 
        //formada por el árbol de dicho item.
        public Dictionary<string, Nodo> ultimoListaEnlazada;

        public ArbolFP(List<List<String>> Transactions, int minSup)
        {
            Raiz = new Nodo(null, null);
            ultimoListaEnlazada = new Dictionary<string, Nodo>();
            ConstructFPTree(Transactions, minSup);
        }

        public void ConstructFPTree (List<List<String>> Transactions, double minSup)
        {
            //Guarda, para cada producto (El string es el identificador del producto) el numero de veces que aparece en las transacciones.
            Dictionary<string, int> numberOfOcurrances = new Dictionary<string, int>();

            foreach (List<String> list in Transactions)
            {
                foreach(String ident in list)
                {
                    if (numberOfOcurrances.ContainsKey(ident))
                    {
                        numberOfOcurrances[ident]++;
                    } else
                    {
                        numberOfOcurrances.Add(ident, 1);
                    }
                }

                foreach(List<String> trans in Transactions)
                {
                    trans.OrderBy(e => numberOfOcurrances[e]);
                    Raiz.InsertarTransaccion(trans, ultimoListaEnlazada, (int) minSup * Transactions.Count + 1, numberOfOcurrances);
                }

            }
        }
    }
}
