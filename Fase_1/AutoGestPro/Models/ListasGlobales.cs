using System;
using AutoGestPro.Models.Listas.MatrizDispersa;

namespace AutoGestPro.Models.Listas
{
    class ListasGlobales
    {
        public static ListaSimple listaUsuarios = new ListaSimple();
        public static ListaDoble listaVehiculos = new ListaDoble();
        public static ListaCircular listaRepuestos = new ListaCircular();
        public static Cola colaServicios = new Cola();
        public static Pila pilaFacturas = new Pila();
        public static Matriz_Dispersa matrizDispersa = new Matriz_Dispersa();
    }
}