using System;
using AutoGestPro.Models.Entidades;

namespace AutoGestPro.Models.Listas.Arbol_Binario
{
    class NodoBinario 
    {
        public NodoBinario? Izquierdo { get; set; }
        public NodoBinario? Derecho { get; set; }
        public Servicio Servicio { get; set; }

        public NodoBinario(Servicio servicio)
        {
            Servicio = servicio;
            Izquierdo = null;
            Derecho = null;
        }
    }
}