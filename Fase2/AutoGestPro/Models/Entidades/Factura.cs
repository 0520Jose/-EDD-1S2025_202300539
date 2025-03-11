using System;

namespace AutoGestPro.Models.Entidades
{
    unsafe struct Factura
    {
        public int Id { get; set; }
        public int Id_Orden { get; set; }
        public float Total { get; set; }

        public Factura(int id, int idOrden, float total)
        {
            Id = id;
            Id_Orden = idOrden;
            Total = total;
        }
    }
}