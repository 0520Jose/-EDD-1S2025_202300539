using System;


namespace AutoGestPro.Models
{
    unsafe struct Usuario
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public int Edad { get; set; }
        public string Contrasenia { get; set;}
        public Usuario* siguiente;
        public Usuario(int id, string nombre, string apellido, string correo, int edad, string contrasenia)
        {
            Id = id;
            Nombres = nombre;
            Apellidos = apellido;
            Correo = correo;
            Edad = edad;
            Contrasenia = contrasenia;
        }
    }
}