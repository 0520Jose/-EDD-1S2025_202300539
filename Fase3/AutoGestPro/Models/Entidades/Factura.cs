using System;
using System.Security.Cryptography;
using System.Text;

namespace AutoGestPro.Models.Entidades
{
    public class Factura
    {
        public int Id { get; set; }
        public int Id_Servicio { get; set; }
        public double Total { get; set; }

        public string Fecha { get; set; }

        public string MetodoDePago { get; set; }

        public Factura? Izquierdo { get; set; }
        public Factura? Derecho { get; set; }

        public string Hash { get; set; }

        public Factura(int id, int idServicio, double total, string metodoDePago)
        {
            Id = id;
            Id_Servicio = idServicio;
            Total = total;
            Fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            MetodoDePago = metodoDePago;
            Hash = CalcularHash();
        }

        public Factura(Factura izquierdo, Factura derecho)
        {
            Izquierdo = izquierdo;
            Derecho = derecho;
            Hash = CalcularHash(Izquierdo.Hash + Derecho.Hash);
        }

        public string CalcularHash(string? data = null)
        {
            using (var sha256 = SHA256.Create())
            {
                if (data == null)
                    data = $"{Id}-{Id_Servicio}-{Total}-{Fecha}-{MetodoDePago}";

                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(data));
                return BitConverter.ToString(bytes).Replace("-", "").ToLower();
            }
        }
    }
}