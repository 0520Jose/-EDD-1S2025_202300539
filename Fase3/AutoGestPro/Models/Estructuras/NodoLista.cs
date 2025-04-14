using System;
using AutoGestPro.Models.Entidades;

public class NodoLista
{
    public Factura Dato { get; set; }
    public NodoLista? Siguiente { get; set; }
    public NodoLista? Anterior { get; set; }

    public NodoLista(Factura dato)
    {
        Dato = dato;
        Siguiente = null;
        Anterior = null;
    }
}
