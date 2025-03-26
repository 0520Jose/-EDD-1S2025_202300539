using System;
using System.Collections.Generic;
using AutoGestPro.Models.Entidades;

namespace AutoGestPro.Models.Listas.Arbol_B5
{
    class ListaHijos
    {
        public List<NodoB> Hijos { get; private set; }
        public int Tamaño => Hijos.Count;
        private int TamañoMaximo;

        public ListaHijos(int tamañoMaximo)
        {
            Hijos = new List<NodoB>(tamañoMaximo);
            TamañoMaximo = tamañoMaximo;
        }

        public void Insertar(NodoB nodo)
        {
            if (Tamaño >= TamañoMaximo)
            {
                throw new InvalidOperationException("ListaHijos ha alcanzado su tamaño máximo");
            }
            Hijos.Add(nodo);
        }

        public NodoB Buscar(int indice)
        {
            if (indice < 0 || indice >= Tamaño)
            {
                return new NodoB();
            }
            return Hijos[indice];
        }

        public void EstablecerHijo(int indice, NodoB nodo)
        {
            if (indice < 0 || indice >= Tamaño)
            {
                throw new IndexOutOfRangeException("Índice fuera de rango");
            }
            Hijos[indice] = nodo;
        }

        public void LiberarMemoria()
        {
            Hijos.Clear();
        }
    }
}