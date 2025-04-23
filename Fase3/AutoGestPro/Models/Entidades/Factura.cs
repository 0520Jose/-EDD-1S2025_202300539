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
        public Factura? siguiente { get; set; }
        public Factura? anterior { get; set; }
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

        public string CalcularHash()
        {
            string data = $"{Id}{Id_Servicio}{Total}{Fecha}{MetodoDePago}";
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            byte[] hashBytes = SHA256.HashData(bytes);
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }

        
    }
}
