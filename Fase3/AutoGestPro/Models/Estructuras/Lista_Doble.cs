using System;
using AutoGestPro.Models.Entidades;

public class Lista_Doble
{
    public Factura inicio;
    public int tamanio;

    public Lista_Doble()
    {
        inicio = null;
        tamanio = 0;
    }
        public void Insertar(Factura factura)
    {

        if (inicio == null)
        {
            inicio = factura;
        }
        else
        {
            Factura actual = inicio;
            Factura anterior = null;

            while (actual != null && actual.Id < factura.Id)
            {
                anterior = actual;
                actual = actual.siguiente;
            }

            if (anterior == null)
            {
                factura.siguiente = inicio;
                inicio.anterior = factura;
                inicio = factura;
            }
            else
            {
                factura.siguiente = actual;
                factura.anterior = anterior;
                anterior.siguiente = factura;
                if (actual != null)
                {
                    actual.anterior = factura;
                }
            }
        }
        tamanio++;
    }

    public Factura buscarFactura(int id)
    {
        Factura FacturaActual = inicio;
        while (FacturaActual != null)
        {
            if (FacturaActual.Id == id)
            {
                return FacturaActual;
            }
            FacturaActual = FacturaActual.siguiente;
        }
        return null;
    }

    public int Contar()
    {
        return tamanio;
    }
}