using System;
using System.Runtime.InteropServices;

namespace AutoGestPro.Models.Listas.Arbol_B5
{
    unsafe class ListaHijos
    {
        public NodoB* Inicio;
        public int Tamaño;
        int TamañoMaximo;

        public ListaHijos(int tamañoMaximo)
        {
            Inicio = null;
            Tamaño = 0;
            TamañoMaximo = tamañoMaximo;
        }

        public void Insertar(int clave)
        {
            NodoB* nuevoNodo = (NodoB*)NativeMemory.Alloc((nuint)sizeof(NodoB));

            if (TamañoMaximo == Tamaño)
            {
                NativeMemory.Free(nuevoNodo);
                return;
            }

            if (Inicio == null)
            {
                Inicio = nuevoNodo;
            }
            else 
            {
                NodoB* nodoActual = Inicio;
                while (nodoActual->Siguiente != null)
                {
                    nodoActual = nodoActual->Siguiente;
                }
                nodoActual->Siguiente = nuevoNodo;
            }
            Tamaño++;
        }

        public NodoB Buscar(int clave)
        {
            NodoB* nodoActual = Inicio;
            while (nodoActual != null)
            {
                if (nodoActual->Claves.Buscar(clave).Clave == clave)
                {
                    return *nodoActual;
                }
                nodoActual = nodoActual->Siguiente;
            }
            return null;
        }

        public void EstablecerHijo(int clave, NodoB* Nodo)
        {
            NodoB* nodoActual = Inicio;
            while (nodoActual != null)
            {
                if (nodoActual->Claves.Buscar(clave).Clave == clave)
                {
                    nodoActual = Nodo;
                    return;
                }
                nodoActual = nodoActual->Siguiente;
            }
        }
    }
}