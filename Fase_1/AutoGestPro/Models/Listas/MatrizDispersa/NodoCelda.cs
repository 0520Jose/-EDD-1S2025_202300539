using System;
using System.Collections.Generic;

namespace AutoGestPro.Models.Listas.MatrizDispersa
{
    public unsafe class NodoCelda
    {
        public int x { get; set; }
        public int y { get; set; }
        public Bitacora bitacora { get; set; }
        public NodoCelda* arriba;
        public NodoCelda* abajo;
        public NodoCelda* derecha;
        public NodoCelda* izquierda;

        public NodoCelda(int X, int Y, Bitacora Bitacora)
        {
            x = X;
            y = Y;
            bitacora = Bitacora;
            arriba = null;
            abajo = null;
            derecha = null;
            izquierda = null;
        }
    }
}
