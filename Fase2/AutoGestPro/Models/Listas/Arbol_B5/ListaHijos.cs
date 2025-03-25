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
            nuevoNodo->Claves = new Lista(5);
            nuevoNodo->Siguiente = null;

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
            return new NodoB();
        }

        public void EstablecerHijo(int clave, NodoB* nuevoNodo)
        {
            NodoB* nodoActual = Inicio;
            while (nodoActual != null)
            {
                if (nodoActual->Claves.Buscar(clave).Clave == clave)
                {
                    nodoActual->Claves = nuevoNodo->Claves;
                    nodoActual->Siguiente = nuevoNodo->Siguiente;
                    return;
                }
                nodoActual = nodoActual->Siguiente;
            }
        }

        public void LiberarMemoria()
        {
            NodoB* nodoActual = Inicio;
            while (nodoActual != null)
            {
                NodoB* siguiente = nodoActual->Siguiente;
                NativeMemory.Free(nodoActual);
                nodoActual = siguiente;
            }
            Inicio = null;
            Tamaño = 0;
        }
    }
}