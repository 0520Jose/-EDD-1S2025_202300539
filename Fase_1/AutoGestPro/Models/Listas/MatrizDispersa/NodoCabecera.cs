using System;

namespace AutoGestPro.Models.Listas.MatrizDispersa
{
    public unsafe class NodoCabecera
    {
        public int id { get; set; }
        public NodoCabecera* siguiente;
        public NodoCabecera* anterior;
        public NodoCelda* acceso;

        public NodoCabecera(int Id)
        {
            id = Id;
            siguiente = null;
            anterior = null;
            acceso = null;
        }
    }
}