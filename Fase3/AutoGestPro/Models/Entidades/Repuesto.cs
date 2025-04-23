using System;

namespace AutoGestPro.Models.Entidades
{
    public class Repuesto
    {
        public int Id { get; set; }
        public string REpuesto { get; set; }
        public string Detalle { get; set; }   
        public float Costo { get; set; }
        public Repuesto(int id, string repuesto, string detalle, float costo)
        {
            Id = id;
            REpuesto = repuesto;
            Detalle = detalle;
            Costo = costo;
        }
    }
}