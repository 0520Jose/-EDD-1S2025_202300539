using System;
using System.Runtime.InteropServices;
using AutoGestPro.Models.Entidades;

namespace AutoGestPro.Models.Listas.Arbol_B5
{
    unsafe class Lista
    {
        public Nodo* Inicio;
        public int Tamaño;
        int TamañoMaximo;

        public Lista(int tamañoMaximo)
        {
            Inicio = null;
            Tamaño = 0;
            TamañoMaximo = tamañoMaximo;
        }

        public void Insertar(int clave, Factura factura)
        {
            Nodo* nuevoNodo = (Nodo*)NativeMemory.Alloc((nuint)sizeof(Nodo));
            nuevoNodo->Clave = clave;
            nuevoNodo->Factura = factura;

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
                Nodo* nodoActual = Inicio;
                while (nodoActual->Siguiente != null)
                {
                    nodoActual = nodoActual->Siguiente;
                }
                nodoActual->Siguiente = nuevoNodo;
            }
            Tamaño++;
        }

        public Nodo Buscar(int clave)
        {
            Nodo* nodoActual = Inicio;
            while (nodoActual != null)
            {
                if (nodoActual->Clave == clave)
                {
                    return *nodoActual;
                }
                nodoActual = nodoActual->Siguiente;
            }
            return new Nodo();
        }

        public void EstablecerNodo(int clave, Nodo* Nodo)
        {
            Nodo* nodoActual = Inicio;
            while (nodoActual != null)
            {
                if (nodoActual->Clave == clave)
                {
                    nodoActual = Nodo;
                }
                nodoActual = nodoActual->Siguiente;
            }
        }

        public void EstablecerClave(int clave, int nuevaClave)
        {
            Nodo* nodoActual = Inicio;
            while (nodoActual != null)
            {
                if (nodoActual->Clave == clave)
                {
                    nodoActual->Clave = nuevaClave;
                }
                nodoActual = nodoActual->Siguiente;
            }
        }

        public void EstablecerFactura (int clave, Factura factura)
        {
            Nodo* nodoActual = Inicio;
            while (nodoActual != null)
            {
                if (nodoActual->Clave == clave)
                {
                    nodoActual->Factura = factura;
                }
                nodoActual = nodoActual->Siguiente;
            }
        }
    }
}