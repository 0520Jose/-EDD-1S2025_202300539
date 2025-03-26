using System;
using System.Text;
using AutoGestPro.Models.Entidades;

namespace AutoGestPro.Models.Listas.Arbol_B5
{
    class ArbolB5
    {
        private int Grado;
        private NodoB Raiz;

        public ArbolB5(int grado)
        {
            Grado = grado;
            Raiz = new NodoB();
        }

        public void Insertar(int clave, Factura factura)
        {
            if (Raiz.Claves.Count == Grado - 1)
            {
                NodoB nuevaRaiz = new NodoB();
                nuevaRaiz.EsHoja = false;
                
                nuevaRaiz.Hijos.Add(Raiz);
                nuevaRaiz.SepararHijo(0, Raiz);
                Raiz = nuevaRaiz;
            }

            Raiz.InsertarEnNodo(clave, factura);
        }

        public void Mostrar()
        {
            MostrarNodo(Raiz, 0);
        }

        public void MostrarNodo(NodoB nodo, int nivel)
        {
            Console.Write($"Nivel {nivel}: ");
            foreach (var clave in nodo.Claves)
            {
                Console.Write($"{clave} ");
            }
            Console.WriteLine();

            if (!nodo.EsHoja)
            {
                foreach (var hijo in nodo.Hijos)
                {
                    MostrarNodo(hijo, nivel + 1);
                }
            }
        }

        public string GenerarDot()
        {
            StringBuilder dot = new StringBuilder();
            dot.AppendLine("digraph G {");
            dot.AppendLine("node [shape=record];");
            dot.AppendLine("rankdir=TB;");
            dot.AppendLine("node [height=0.5];");
            dot.AppendLine("node [width=0.5];");
            dot.AppendLine("node [shape=record];");
            dot.AppendLine("node [style=filled];");
            dot.AppendLine("node [fillcolor=\"#EEEEEE\"];");
            dot.AppendLine("node [fontname=\"Arial\"];");
            dot.AppendLine("edge [fontname=\"Arial\"];");
            dot.AppendLine("edge [fontsize=8];");
            dot.AppendLine("edge [fontcolor=\"#333333\"];");
            dot.AppendLine("edge [labelfloat=false];");
            dot.AppendLine("edge [decorate=true];");
            dot.AppendLine("edge [style=\"solid\"];");
            dot.AppendLine("edge [color=\"#333333\"];");
            dot.AppendLine("edge [dir=\"forward\"];");
            dot.AppendLine("edge [arrowhead=\"normal\"];");
            dot.AppendLine("edge [arrowsize=\"0.5\"];");
            dot.AppendLine("edge [arrowtail=\"normal\"];");
            dot.AppendLine("edge [taillabel=\"\"];");
            dot.AppendLine("edge [headlabel=\"\"];");
            dot.AppendLine("edge [label=\"\"];");
            dot.AppendLine("edge [weight=\"1\"];");

            GenerarDotNodo(Raiz, dot);

            dot.AppendLine("}");
            return dot.ToString();
        }

        private void GenerarDotNodo(NodoB nodo, StringBuilder dot)
        {
            string nodoId = $"node_{nodo.GetHashCode()}";

            string label = $"\"{{";
            for (int i = 0; i < nodo.Claves.Count; i++)
            {
            label += $"{nodo.Claves[i]}\\nFactura: {nodo.Facturas[i].Id}, Orden: {nodo.Facturas[i].Id_Orden}, Total: {nodo.Facturas[i].Total}";
            if (i < nodo.Claves.Count - 1)
                label += "|";
            }
            label += "}\"";

            dot.AppendLine($"{nodoId} [label={label}];");

            if (!nodo.EsHoja)
            {
            for (int i = 0; i < nodo.Hijos.Count; i++)
            {
                string hijoId = $"node_{nodo.Hijos[i].GetHashCode()}";
                dot.AppendLine($"{nodoId} -> {hijoId};");
                GenerarDotNodo(nodo.Hijos[i], dot);
            }
            }
        }
    }
}