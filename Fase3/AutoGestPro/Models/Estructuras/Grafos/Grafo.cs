using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;

namespace AutoGestPro.Models.Estructuras.Grafos
{
    public class Grafo
    {
        public Nodo2 Raiz { get; set; }

        public Grafo () {
            Raiz = null;
        }

        public void insertar(int id2, int id1)
        {
            Nodo2 nuevoNodo2 = new Nodo2(id2);
            Nodo1 nuevoNodo1 = new Nodo1(id1);

            if (Raiz == null) {
                Raiz = nuevoNodo2;
                nuevoNodo2.AgregarHijo(nuevoNodo1);
                return;
            }

            Nodo2 temp = Raiz;
            while (temp != null)
            {
                if (id2 == temp.Id) {
                    break;
                }
                temp = temp.siguiente;
            }

            if (temp != null)
            {
                bool existe = temp.buscarHijo(id1);

                if (existe)
                {
                    return;
                }
                else {
                    temp.AgregarHijo(nuevoNodo1);
                }
            }
            else
            {
                Nodo2 temp2 = Raiz;
                while (temp2.siguiente != null) {
                    temp2 = temp2.siguiente;
                }
                temp2.siguiente = nuevoNodo2;
                nuevoNodo2.AgregarHijo(nuevoNodo1);
            }
        }

        public void imprimir()
        {
            Nodo2 temp = Raiz;
            while (temp != null)
            {
                Console.WriteLine("Nodo: " + Convert.ToString(temp.Id) + " hijos: ");
                foreach (Nodo1 hijo in temp.hijos) {
                    Console.WriteLine(hijo.Id);
                }
                temp = temp.siguiente;
            }
        }

        public string Graficar()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("graph G {");
            sb.AppendLine("    rankdir=LR;");
            sb.AppendLine("    node [shape=circle];");

            Nodo2 temp = Raiz;
            while (temp != null)
            {
                string nodoPadreId = $"V{temp.Id}";
                sb.AppendLine($"    {nodoPadreId} [label=\"{nodoPadreId}\"];");

                foreach (Nodo1 hijo in temp.hijos)
                {
                    string nodoHijoId = $"R{hijo.Id}";
                    sb.AppendLine($"    {nodoHijoId} [label=\"{nodoHijoId}\"];");
                    sb.AppendLine($"    {nodoPadreId} -- {nodoHijoId};");
                }
                temp = temp.siguiente;
            }

            sb.AppendLine("}");
            return sb.ToString();
        }


    }
}