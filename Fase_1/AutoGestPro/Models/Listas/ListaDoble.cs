using System;
using System.Runtime.InteropServices;
using AutoGestPro.Models;
unsafe class ListaDoble
{
    public Vehiculo* inicio = null;
    public int tamanio = 0;

    public void Insertar(int id, int IdUsuario, string marca, string modelo, string placa)
    {
        Vehiculo* nuevoVehiculo = (Vehiculo*)NativeMemory.Alloc((nuint)sizeof(Vehiculo));
        nuevoVehiculo->Id = id;
        nuevoVehiculo->Id_Usuario = IdUsuario;
        nuevoVehiculo->Marca = marca;
        nuevoVehiculo->Modelo = modelo;
        nuevoVehiculo->Placa = placa;
        nuevoVehiculo->siguiente = inicio;
        if (inicio != null)
        {
            inicio->anterior = nuevoVehiculo;
        }
        inicio = nuevoVehiculo;
        nuevoVehiculo->anterior = null;
        tamanio++;
    }

    public Vehiculo buscarVehiculo(int id)
    {
        Vehiculo* vehiculoActual = inicio;
        while (vehiculoActual != null)
        {
            if (vehiculoActual->Id == id)
            {
                return *vehiculoActual;
            }
            vehiculoActual = vehiculoActual->siguiente;
        }
        return new Vehiculo();
    }
}