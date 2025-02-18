using System;
using System.Runtime.InteropServices;
using AutoGestPro.Models;
using AutoGestPro.Models.Entidades;

namespace AutoGestPro.Models.Listas
{
    unsafe class Cola
    {
        public Servicio* inicio = null;

        public void Encolar(int id, int Id_Repuesto, int Id_Vehiculo, string detalles, double costo)
        {
            Servicio* nuevoServicio = (Servicio*)NativeMemory.Alloc((nuint)sizeof(Servicio));
            nuevoServicio->Id = id;
            nuevoServicio->Id_Repuesto = Id_Repuesto;
            nuevoServicio->Id_Vehiculo = Id_Vehiculo;
            nuevoServicio->Detalles = detalles;
            nuevoServicio->Costo = costo;
            
            if (inicio == null)
            {
                inicio = nuevoServicio;
            }
            else
            {
                Servicio* servicioActual = inicio;
                while (servicioActual->siguiente != null)
                {
                    servicioActual = servicioActual->siguiente;
                }
                servicioActual->siguiente = nuevoServicio;
            }
        }

        public Servicio Desencolar()
        {
            if (inicio != null)
            {
                Servicio* servicioDesencolado = inicio;
                inicio = inicio->siguiente;
                return *servicioDesencolado;
            }
            return new Servicio();
        }
    }
}