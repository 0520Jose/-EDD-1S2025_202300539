using System;
using System.Runtime.InteropServices;
using AutoGestPro.Models;
unsafe class ListaCircular
{
    private Repuesto* inicio = null;

    public void Insertar(int id, string repuesto, string detalles, float costo)
    {
        Repuesto* nuevoRepuesto = (Repuesto*)NativeMemory.Alloc((nuint)sizeof(Repuesto));
        nuevoRepuesto->Id = id;
        nuevoRepuesto->repuesto = repuesto;
        nuevoRepuesto->detalle = detalles;
        nuevoRepuesto->costo = costo;
        if (inicio == null)
        {
            inicio = nuevoRepuesto;
            inicio->siguiente = inicio;
        }
        else
        {
            Repuesto* ultimo = inicio;
            while (ultimo->siguiente != inicio)
            {
                ultimo = ultimo->siguiente;
            }
            nuevoRepuesto->siguiente = inicio;
            ultimo->siguiente = nuevoRepuesto;
        }
    }

    public Repuesto buscarRepuesto(int id)
    {
        Repuesto* repuestoActual = inicio;
        while (repuestoActual != null)
        {
            if (repuestoActual->Id == id)
            {
                return *repuestoActual;
            }
            repuestoActual = repuestoActual->siguiente;
            if (repuestoActual == inicio)
            {
                return new Repuesto();
            }
        }
        return new Repuesto();
    }
}