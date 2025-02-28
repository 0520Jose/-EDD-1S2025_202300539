using System;
using System.Runtime.InteropServices;
using AutoGestPro.Models.Entidades;

namespace AutoGestPro.Models.Listas
{
    unsafe class Pila
    {
        public Factura* sima = null;

        public void Apilar(int id, int idOrden, float total)
        {
            Factura* nuevaFactura = (Factura*)NativeMemory.Alloc((nuint)sizeof(Factura));
            nuevaFactura->Id = id;
            nuevaFactura->Id_Orden = idOrden;
            nuevaFactura->Total = total;
            
            if (sima == null)
            {
                sima = nuevaFactura;
                sima->abajo = null;
            }
            else
            {
                nuevaFactura->abajo = sima;
                sima = nuevaFactura;
            }
        }

        public Factura Desapilar()
        {
            if (sima != null)
            {
            Factura* factura = sima;
            sima = sima->abajo;
            Factura result = *factura;
            NativeMemory.Free(factura);
            return result;
            }
            return new Factura();
        }

        public Factura* DesapilarFactura()
        {
            if (sima != null)
            {
                Factura* factura = sima;
                sima = sima->abajo;
                return factura;
            }
            return null;
        }
    }
}