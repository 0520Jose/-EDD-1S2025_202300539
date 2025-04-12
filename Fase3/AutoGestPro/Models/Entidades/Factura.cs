using System;

namespace AutoGestPro.Models.Entidades
{
    public class Factura
    {
        public int Id { get; set; }
        public int Id_Servicio { get; set; }
        public double Total { get; set; }

        public string Fecha { get; set; }

        public string MetodoDePago { get; set; }

        public Factura(int id, int idServicio, double total, string fecha, string metodoDePago)
        {
            Id = id;
            Id_Servicio = idServicio;
            Total = total;
            Fecha = fecha;
            MetodoDePago = metodoDePago;
        }
    }
}