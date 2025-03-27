using System;
using AutoGestPro.Models.Entidades;

namespace AutoGestPro.Models.Listas.Arbol_B5
{
    public class NodoB
    {
        public int n_facturas { get; set; }
        public Factura[] Facturas { get; set; }
        public NodoB[] hijos { get; set; }

        public NodoB(int Orden)
        {
            n_facturas = 0;
            Facturas = new Factura[Orden];
            hijos = new NodoB[Orden + 1];
        }
    }
}