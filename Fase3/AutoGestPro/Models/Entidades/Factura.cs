using System;
using System.Net.Http.Json;
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

        public Factura(int id, int idServicio, double total, string metodoDePago)
        {
            Id = id;
            Id_Servicio = idServicio;
            Total = total;
            Fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            MetodoDePago = metodoDePago;
        }

        public string GetHash()
        {
            string data = System.Text.Json.JsonSerializer.Serialize(this);
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(data));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        
    }
}
