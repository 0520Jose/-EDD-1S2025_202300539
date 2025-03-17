using System;
using AutoGestPro.Models.Listas.Arbol_AVL;

namespace AutoGestPro.Models.Listas
{
    class ListasGlobales
    {
        public static ListaSimple listaUsuarios = new ListaSimple();
        public static ListaDoble listaVehiculos = new ListaDoble();
        public static ArbolAVL arbolRepuestos = new ArbolAVL();
    }
}