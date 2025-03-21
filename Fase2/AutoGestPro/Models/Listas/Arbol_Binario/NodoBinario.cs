using System;
using AutoGestPro.Models.Entidades;

namespace AutoGestPro.Models.Listas.Arbol_Binario
{
    class NodoBinario 
    {
        public NodoBinario? Izquierdo;
        public NodoBinario? Derecho;
        public Servicio Servicio;

        public NodoBinario(Servicio servicio)
        {
            Servicio = servicio;
            Izquierdo = null;
            Derecho = null;
        }
    }
}