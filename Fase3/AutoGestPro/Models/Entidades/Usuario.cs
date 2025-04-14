using System;
using System.Text;
using System.Security.Cryptography;

namespace AutoGestPro.Models.Entidades
{
    public class UsuarioNodo
    {
        public int Index;
        public string Fecha;
        public int Id;
        public string Nombres;
        public string Apellidos;
        public string Correo;
        public int Edad;
        public string Contrasenia;
        public string HashAnterior;
        public string Hash;
        public int Nonce;
        public UsuarioNodo Siguiente;
        public UsuarioNodo Anterior;

        public string GenerateHash()
        {
            string data = $"{Index}{Fecha}{Id}{Nombres}{Apellidos}{Correo}{Edad}{Contrasenia}{Nonce}{HashAnterior}";
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            byte[] hashBytes = SHA256.HashData(bytes);
            return BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }

        public void MineBlock()
        {
            int dificultad = 3;
            string target = new string('0', dificultad);
            while (!Hash.StartsWith(target))
            {
                Nonce++;
                Hash = GenerateHash();
            }
        }
    }
}
