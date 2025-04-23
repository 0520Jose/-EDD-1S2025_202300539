using System;
using AutoGestPro.Models.Entidades;
using System.Security.Cryptography;
using System.Text;

namespace AutoGestPro.Models.Estructuras
{
    public class Nodo
    {
        public Factura? IzquierdoFinal { get; set; }
        public Factura? DerechoFinal { get; set; }
        public Nodo? Izquierdo { get; set; }
        public Nodo? Derecho { get; set; }
        public bool EsNodoImpar { get; set; }

        public string Hash;

        public Nodo()
        {
            Izquierdo = null;
            Derecho = null;
            IzquierdoFinal = null;
            DerechoFinal = null;
            Hash = null;
            EsNodoImpar = false;
        }


        public string HashPadre()
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                string combined;
                
                if (EsNodoImpar && Derecho == null)
                {
                    combined = Izquierdo?.Hash + Izquierdo?.Hash;
                }
                else
                {
                    combined = Izquierdo?.Hash + Derecho?.Hash;
                }
                
                if (IzquierdoFinal != null && DerechoFinal != null)
                {
                    combined = IzquierdoFinal.Hash + DerechoFinal.Hash;
                }
                
                byte[] bytes = Encoding.UTF8.GetBytes(combined);
                byte[] hashBytes = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hashBytes);
            }
        }

    }
}