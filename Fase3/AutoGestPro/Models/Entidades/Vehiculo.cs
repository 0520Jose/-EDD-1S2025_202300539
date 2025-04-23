using System;

namespace AutoGestPro.Models.Entidades
{
    public unsafe struct Vehiculo
    {
        public int Id { get; set; }
        public int Id_Usuario { get; set; }
        public string Marca { get; set; }   
        public string Modelo { get; set; }
        public string Placa { get; set; }
        public Vehiculo* siguiente;
        public Vehiculo* anterior;
        
        public Vehiculo(int id, int id_usuario, string marca, string modelo, string placa)
        {
            Id = id;
            Id_Usuario = id_usuario;
            Marca = marca;
            Modelo = modelo;
            Placa = placa;
        }
    }
}