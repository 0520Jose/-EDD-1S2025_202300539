using System;
using AutoGestPro.Models.Listas.Arbol_AVL;
using AutoGestPro.Models.Listas.Arbol_Binario;

namespace AutoGestPro.Models.Listas
{
    class ListasGlobales
    {
        public static ListaSimple listaUsuarios = new ListaSimple();
        public static ListaDoble listaVehiculos = new ListaDoble();
        public static ArbolAVL arbolRepuestos = new ArbolAVL();
        public static ArbolBinario arbolServicios = new ArbolBinario();
    }
}