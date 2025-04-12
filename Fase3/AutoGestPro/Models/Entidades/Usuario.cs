using System;


namespace AutoGestPro.Models.Entidades
{
    unsafe struct Usuario
    {
        public int Index { get; set; }
        public string Fecha { get; set; }
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public int Edad { get; set; }
        public string Contrasenia { get; set;}
        public string HashAnterior { get; set; }
        public string Hash { get; set; } 

        public Usuario* Siguiente { get; set; }
        public Usuario* Anterior { get; set; }

        public Usuario (int index, int id, string nombres, string apellidos, string correo, int edad, string contrasenia)
        {
            Index = index;
            Fecha = DateTime.Now.ToString("yyyy-MM-dd");
            Id = id;
            Nombres = nombres;
            Apellidos = apellidos;
            Correo = correo;
            Edad = edad;
            Contrasenia = contrasenia;
            HashAnterior = null;
            Hash = null;
            Siguiente = null;
            Anterior = null;
        }
    }
}