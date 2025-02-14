using System;


namespace AutoGestPro.Models
{
    unsafe struct Usuario
    {
        public string Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public string Contrasenia { get; set;}
        public Usuario* siguiente;

        public Usuario(string id, string nombre, string apellido, string correo, string contrasenia)
        {
            Id = id;
            Nombre = nombre;
            Apellido = apellido;
            Correo = correo;
            Contrasenia = contrasenia;
        }
    }
}