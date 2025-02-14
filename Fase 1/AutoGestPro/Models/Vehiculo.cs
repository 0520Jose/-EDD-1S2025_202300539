using System;

class Vehiculo
{
    public string Id { get; set; }
    public string Id_Usuario { get; set; }
    public string Marca { get; set; }   
    public string Modelo { get; set; }
    public string Placa { get; set; }

    public Vehiculo(string id, string id_usuario, string marca, string modelo, string placa)
    {
        Id = id;
        Id_Usuario = id_usuario;
        Marca = marca;
        Modelo = modelo;
        Placa = placa;
    }
}