using System;
using System.Collections.Generic;
using AutoGestPro.Models.Entidades;

namespace AutoGestPro.Models.Listas.Arbol_B5
{
    class NodoB 
    {
        private const int Grado = 5;
        public List<int> Claves { get; private set; }
        public List<Factura> Facturas { get; private set; }
        public List<NodoB> Hijos { get; private set; }
        public bool EsHoja { get; set; }

        public NodoB()
        {
            Claves = new List<int>(Grado - 1);
            Facturas = new List<Factura>(Grado - 1);
            Hijos = new List<NodoB>(Grado);
            EsHoja = true;
        }

        public void InsertarEnNodo(int clave, Factura factura)
        {
            if (Claves.Count >= Grado - 1)
            {
                throw new InvalidOperationException("Nodo está lleno");
            }

            int i = Claves.Count - 1;
            
            if (EsHoja)
            {
                while (i >= 0 && Claves[i] > clave)
                {
                    i--;
                }

                if (i == Claves.Count - 1)
                {
                    Claves.Add(clave);
                    Facturas.Add(factura);
                }
                else
                {
                    Claves.Insert(i + 1, clave);
                    Facturas.Insert(i + 1, factura);
                }
            }
            else
            {
                while (i >= 0 && Claves[i] > clave)
                {
                    i--;
                }
                i++;

                if (Hijos[i].Claves.Count == Grado - 1)
                {
                    SepararHijo(i, Hijos[i]);
                    
                    if (Claves[i] < clave)
                    {
                        i++;
                    }
                }
                
                Hijos[i].InsertarEnNodo(clave, factura);
            }
        }

        public void SepararHijo(int indice, NodoB hijo)
        {
            NodoB nuevoNodo = new NodoB
            {
                EsHoja = hijo.EsHoja
            };

            int puntoMedio = (Grado - 1) / 2;

            for (int j = puntoMedio + 1; j < hijo.Claves.Count; j++)
            {
                nuevoNodo.Claves.Add(hijo.Claves[j]);
                nuevoNodo.Facturas.Add(hijo.Facturas[j]);
            }

            hijo.Claves.RemoveRange(puntoMedio + 1, hijo.Claves.Count - (puntoMedio + 1));
            hijo.Facturas.RemoveRange(puntoMedio + 1, hijo.Facturas.Count - (puntoMedio + 1));

            if (!hijo.EsHoja)
            {
                for (int j = puntoMedio + 1; j < hijo.Hijos.Count; j++)
                {
                    nuevoNodo.Hijos.Add(hijo.Hijos[j]);
                }
                
                hijo.Hijos.RemoveRange(puntoMedio + 1, hijo.Hijos.Count - (puntoMedio + 1));
            }

            Claves.Insert(indice, hijo.Claves[puntoMedio]);
            Facturas.Insert(indice, hijo.Facturas[puntoMedio]);

            hijo.Claves.RemoveAt(puntoMedio);
            hijo.Facturas.RemoveAt(puntoMedio);

            Hijos.Insert(indice + 1, nuevoNodo);
        }
    }
}