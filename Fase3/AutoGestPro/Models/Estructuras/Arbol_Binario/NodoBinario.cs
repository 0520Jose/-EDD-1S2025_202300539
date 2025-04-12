using System;
using AutoGestPro.Models.Entidades;

namespace AutoGestPro.Models
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