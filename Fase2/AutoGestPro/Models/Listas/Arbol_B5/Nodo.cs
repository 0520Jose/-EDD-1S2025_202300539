using System;
using AutoGestPro.Models.Entidades;

namespace AutoGestPro.Models.Listas.Arbol_B5
{
    public unsafe struct Nodo
    {
        public int Clave;
        public Factura Factura;
        public Nodo* Siguiente;
    }
}