using System;

namespace AutoGestPro.Models
{
    unsafe struct Repuesto
    {
        public int Id { get; set; }
        public string repuesto { get; set; }
        public string detalle { get; set; }   
        public float costo { get; set; }


        public Repuesto* siguiente;
        public Repuesto* anterior;

        public Repuesto(int id, string repuesto, string detalle, float costo)
        {
            Id = id;
            repuesto = repuesto;
            detalle = detalle;
            costo = costo;
        }
    }
}