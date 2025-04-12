using System;
using AutoGestPro.Models.Entidades;

namespace AutoGestPro.Models.Estructuras
{
    class NodoAVL
    {
        public NodoAVL? Izquierdo;
        public NodoAVL? Derecho;
        public Repuesto Repuesto;
        public int Altura;

        public NodoAVL(Repuesto repuesto)
        {
            Repuesto = repuesto;
            Izquierdo = null;
            Derecho = null;
            Altura = 1;
        }
    }
}