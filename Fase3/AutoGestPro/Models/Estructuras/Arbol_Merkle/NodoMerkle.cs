using System;
using AutoGestPro.Models.Entidades;
using System.Security.Cryptography;
using System.Text;

namespace AutoGestPro.Models.Estructuras
{
    public class Nodo
    {
        public Nodo Izquierdo;
        public Nodo Derecho;
        public Factura Factura;
        public string Hash;

        public Nodo(Factura factura)
        {
            Izquierdo = null;
            Derecho = null;
            Factura = factura;
            Hash = factura.GetHash();
        }

        public Nodo(Nodo izquierdo, Nodo derecho)
        {
            Factura = null;
            Izquierdo = izquierdo;
            Derecho = derecho;
            Hash = CalcularHash(izquierdo.Hash, derecho?.Hash);
        }

        private string CalcularHash(string leftHash, string rightHash)
        {

            string combined = leftHash + (rightHash ?? leftHash);
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(combined));
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