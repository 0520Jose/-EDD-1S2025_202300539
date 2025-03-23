using System;
using System.Runtime.InteropServices;
using AutoGestPro.Models.Entidades;

namespace AutoGestPro.Models.Listas.Arbol_B5
{
    unsafe class NodoB {
        private const int Grado = 5;
        public Lista Claves;
        public ListaHijos Hijos { get; set; }
        public bool EsHoja { get; set; }
        public NodoB* Siguiente;

        
        public NodoB()
        {
            Claves = new Lista(Grado - 1);
            Hijos = new ListaHijos(Grado);
            EsHoja = true;
        }

        public void InsertarEnNodo(int clave, Factura factura)
        {
            int i = Claves.Tamaño - 1;

            if (EsHoja)
            {
            Claves.Insertar(0, null);
            while (i >= 0 && Claves.Buscar(i).Clave > clave)
            {
                Claves.EstablecerFactura(i+1, factura);
                Claves.EstablecerClave(i + 1, Claves.Buscar(i).Clave);
                i--;
            }
            Claves.EstablecerFactura(i + 1, factura);
            Claves.EstablecerClave(i + 1, clave);
            }
            else
            {
            while (i >= 0 && Claves.Buscar(i).Clave > clave)
            {
                i--;
            }
            i++;

            if (Hijos.Buscar(i).Claves.Tamaño == Grado - 1)
            {
                SepararHijo(i, Hijos.Buscar(i));
                if (Claves.Buscar(i).Clave < clave)
                {
                i++;
                }
            }
            Hijos.Buscar(i).InsertarEnNodo(clave, factura);
            }
        }

        public void SepararHijo(int i, NodoB hijo)
        {
            NodoB* nuevoNodo = (NodoB*)NativeMemory.Alloc((nuint)sizeof(NodoB));
            nuevoNodo->EsHoja = hijo.EsHoja;
            nuevoNodo->Claves = new Lista(Grado - 1);

            for (int j = 0; j < Grado - 1; j++)
            {
            nuevoNodo->Claves.Insertar(0, null);
            }

            for (int j = 0; j < Grado - 1; j++)
            {
            nuevoNodo->Claves.EstablecerFactura(j, hijo.Claves.Buscar(j + Grado).Factura);
            nuevoNodo->Claves.EstablecerClave(j, hijo.Claves.Buscar(j + Grado).Clave);
            }

            if (!hijo.EsHoja)
            {
            nuevoNodo->Hijos = new ListaHijos(Grado);
            for (int j = 0; j < Grado; j++)
            {
                nuevoNodo->Hijos.Insertar(0);
            }

            for (int j = 0; j < Grado; j++)
            {   
                NodoB nodo = hijo.Hijos.Buscar(j + Grado);
                NodoB* Nodo = &nodo;
                nuevoNodo->Hijos.EstablecerHijo(j, Nodo);
            }
            }

            for (int j = Claves.Tamaño; j > i; j--)
            {
                NodoB nodo = Hijos.Buscar(j);
                NodoB* Nodo = &nodo;
            Hijos.EstablecerHijo(j + 1, Nodo);
            }

            Hijos.EstablecerHijo(i + 1, nuevoNodo);

            for (int j = Claves.Tamaño - 1; j >= i; j--)
            {
            Claves.EstablecerFactura(j + 1, Claves.Buscar(j).Factura);
            Claves.EstablecerClave(j + 1, Claves.Buscar(j).Clave);
            }
            Claves.EstablecerFactura(i, hijo.Claves.Buscar(Grado - 1).Factura);
            Claves.EstablecerClave(i, hijo.Claves.Buscar(Grado - 1).Clave);
            Claves.Tamaño++;
        }
    }
}