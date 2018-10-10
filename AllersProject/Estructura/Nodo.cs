using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Estructura
{
    public class Nodo
    {
        //Numero de veces que se pasa por el nodo.
        public int Ocurrencia { get; set; }

        //Identificador del producto representado por este nodo. Por ejemplo, el código del item.
        public string Identificador { get; set; }

        //El siguiente nodo que contiene el mismo identificador dentro de la lista enlazada.
        public Nodo Siguiente { get; set; }

        //Indica el nodo padre del nodo actual en el Árbol FP.
        public Nodo Padre { get; set; }

        //Dictionary con los hijos del nodo actual;
        public Dictionary<string, Nodo> hijos;

        //Constructor de la instancia Nodo. Recibe por parametro el identificador del producto representado en el nado y de su padre.
        public Nodo(string Ident, Nodo Padre)
        {
            Ocurrencia = 1;
            Identificador = Ident;
            Siguiente = null;
            this.Padre = Padre;
            hijos = new Dictionary<string, Nodo>();
        }

        /**Método para insertar una nueva transacción desde este nodo. t es una Lista de strings con los identificadores de cada producto
         * de la transacción. listaEn hace referencia al diccionario que señala al útimo elemento de todas las listas enlazadas de los 
         * productos que se han agregado en algún momento al árbol. Se detiene cuando la transacción ya no tiene más items o el siguiente
         * item tiene un soporte inferior al minSup. Se asume que t ya está ordenado. minSup es el minimo de VECES que debe aparecer en TODAS
         * las transacciones.
         **/
        public void InsertarTransaccion(List<String> t, Dictionary<string, Nodo> listaEn, int minSup, Dictionary<string, int> supports)
        {
            if (t.Count != 0 && supports[t[0]] >= minSup)
            {
                string act = t[0];
                t.RemoveAt(0);
                if (hijos.ContainsKey(act))
                {
                    Nodo hijo = hijos[act];
                    hijo.Ocurrencia++;
                    hijo.InsertarTransaccion(t, listaEn, minSup, supports);
                }
                else
                {
                    Nodo nuev = new Nodo(act, this);
                    hijos.Add(act, nuev);
                    if (listaEn.ContainsKey(act))
                    {
                        listaEn[act].Siguiente = nuev;
                        listaEn[act] = nuev;
                    } else
                    {
                        listaEn.Add(act, nuev);
                    }
                    
                    hijos[act].InsertarTransaccion(t, listaEn, minSup, supports);
                }

            }
        }

    }
}
