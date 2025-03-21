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
        nuevoVehiculo->siguiente = null;
        nuevoVehiculo->anterior = null;

        if (inicio == null)
        {
            inicio = nuevoVehiculo;
        }
        else
        {
            Vehiculo* actual = inicio;
            Vehiculo* anterior = null;

            while (actual != null && actual->Id < id)
            {
                anterior = actual;
                actual = actual->siguiente;
            }

            if (anterior == null)
            {
                nuevoVehiculo->siguiente = inicio;
                inicio->anterior = nuevoVehiculo;
                inicio = nuevoVehiculo;
            }
            else
            {
                nuevoVehiculo->siguiente = actual;
                nuevoVehiculo->anterior = anterior;
                anterior->siguiente = nuevoVehiculo;
                if (actual != null)
                {
                    actual->anterior = nuevoVehiculo;
                }
            }
        }
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

    public void eliminarVehiculo(int id)
    {
        Vehiculo* vehiculoActual = inicio;
        while (vehiculoActual != null)
        {
            if (vehiculoActual->Id == id)
            {
                if (vehiculoActual->anterior != null)
                {
                    vehiculoActual->anterior->siguiente = vehiculoActual->siguiente;
                }
                if (vehiculoActual->siguiente != null)
                {
                    vehiculoActual->siguiente->anterior = vehiculoActual->anterior;
                }
                tamanio--;
                break;
            }
            vehiculoActual = vehiculoActual->siguiente;
        }
    }

    public String GenerarDot()
    {
        String dot = "digraph G {\n";
        dot += "node [shape=record];\n";
        dot += "rankdir=LR;\n";
        dot += "node [height=0.5];\n";
        dot += "node [width=0.5];\n";
        dot += "node [shape=record];\n";
        dot += "node [style=filled];\n";
        dot += "node [fillcolor=\"#EEEEEE\"];\n";
        dot += "node [fontname=\"Arial\"];\n";
        dot += "edge [fontname=\"Arial\"];\n";
        dot += "edge [fontsize=8];\n";
        dot += "edge [fontcolor=\"#333333\"];\n";
        dot += "edge [labelfloat=false];\n";
        dot += "edge [decorate=true];\n";
        dot += "edge [style=\"solid\"];\n";
        dot += "edge [color=\"#333333\"];\n";
        dot += "edge [dir=\"forward\"];\n";
        dot += "edge [arrowhead=\"normal\"];\n";
        dot += "edge [arrowsize=\"0.5\"];\n";
        dot += "edge [arrowtail=\"normal\"];\n";
        dot += "edge [taillabel=\"\"];\n";
        dot += "edge [headlabel=\"\"];\n";
        dot += "edge [label=\"\"];\n";
        dot += "edge [weight=\"1\"];\n";

        Vehiculo* vehiculoActual = inicio;
        while (vehiculoActual != null)
        {
            dot += $"\"{vehiculoActual->Id}\" [label=\"{{Id: {vehiculoActual->Id} | Id Usuario: {vehiculoActual->Id_Usuario} | Marca: {vehiculoActual->Marca} | Modelo: {vehiculoActual->Modelo} | Placa: {vehiculoActual->Placa}}}\"];\n";
            if (vehiculoActual->siguiente != null)
            {
                dot += $"\"{vehiculoActual->Id}\" -> \"{vehiculoActual->siguiente->Id}\";\n";
                dot += $"\"{vehiculoActual->siguiente->Id}\" -> \"{vehiculoActual->Id}\";\n";
            }
            vehiculoActual = vehiculoActual->siguiente;
        }
        dot += "}";
        return dot;
    }
}