using System;

namespace AutoGestPro.Models.Listas.Arbol_B5
{
    class NodoB {
        private const int Grado = 5;
        public int[] Claves { get; set; }
        public NodoB[] Hijos { get; set; }
        public int NumeroClaves { get; set; }
        public bool EsHoja { get; set; }

        
        public NodoB()
        {
            Claves = new int[Grado - 1];
            Hijos = new NodoB[Grado];
            NumeroClaves = 0;
            EsHoja = true;
        }
    }
}