using System;

namespace AutoGestPro.Models.Entidades
{
    public class Factura
    {
        public int Id { get; set; }
        public int Id_Orden { get; set; }
        public double Total { get; set; }

        public Factura(int id, int idOrden, double total)
        {
            Id = id;
            Id_Orden = idOrden;
            Total = total;
        }
    }
}