using System;
using AutoGestPro.Models.Entidades;

namespace AutoGestPro.Models.Listas.Arbol_B5
{
    unsafe struct Nodo
    {
        public Nodo* Siguiente;
        public int Clave { get; set; }

        public Factura Factura { get; set; }

        public Nodo(int clave, Factura factura)
        {
            Clave = clave;
            Factura = factura;
            Siguiente = null;
        }
    }
}