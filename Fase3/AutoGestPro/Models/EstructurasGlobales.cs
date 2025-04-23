using System;
using AutoGestPro.Models.Estructuras.Grafos;

namespace AutoGestPro.Models.Estructuras
{
    class EstructurasGlobales
    {
        public static ListaDoble listaVehiculos = new ListaDoble();
        public static ArbolAVL arbolRepuestos = new ArbolAVL();
        public static ArbolBinario arbolServicios = new ArbolBinario();

        public static ArbolMerkle arbolFacturas = new ArbolMerkle();

        public static BlockChain blockChain = new BlockChain();

        public static Grafo grafoVehiculos_Repuestos = new Grafo();

    }
}