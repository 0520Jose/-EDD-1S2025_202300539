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
            if (Tamaño >= TamañoMaximo) 
            {
                throw new InvalidOperationException("Lista ha alcanzado su tamaño máximo");
            }

            Nodo* nuevoNodo = (Nodo*)NativeMemory.Alloc((nuint)sizeof(Nodo));
            nuevoNodo->Clave = clave;
            nuevoNodo->Factura = factura;
            nuevoNodo->Siguiente = null;

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

        public Nodo Buscar(int indice)
        {
            if (indice < 0 || indice >= Tamaño)
            {
                return new Nodo(); // Devuelve un nodo inválido
            }

            Nodo* nodoActual = Inicio;
            for (int i = 0; i < indice; i++)
            {
                if (nodoActual == null) break;
                nodoActual = nodoActual->Siguiente;
            }

            return nodoActual != null ? *nodoActual : new Nodo();
        }

        public Nodo BuscarPorClave(int clave)
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

        public void EstablecerNodo(int clave, Nodo* nuevoNodo)
        {
            Nodo* nodoActual = Inicio;
            while (nodoActual != null)
            {
                if (nodoActual->Clave == clave)
                {
                    *nodoActual = *nuevoNodo;
                    return;
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
                    return;
                }
                nodoActual = nodoActual->Siguiente;
            }
        }

        public void EstablecerFactura(int clave, Factura factura)
        {
            Nodo* nodoActual = Inicio;
            while (nodoActual != null)
            {
                if (nodoActual->Clave == clave)
                {
                    nodoActual->Factura = factura;
                    return;
                }
                nodoActual = nodoActual->Siguiente;
            }
        }

        public void LiberarMemoria()
        {
            Nodo* nodoActual = Inicio;
            while (nodoActual != null)
            {
                Nodo* siguienteNodo = nodoActual->Siguiente;
                NativeMemory.Free(nodoActual);
                nodoActual = siguienteNodo;
            }
            Inicio = null;
            Tamaño = 0;
        }
    }
}