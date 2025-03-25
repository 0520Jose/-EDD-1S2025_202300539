using System;
using AutoGestPro.Models.Entidades;
using System.Runtime.InteropServices;

namespace AutoGestPro.Models.Listas.Arbol_B5
{
    unsafe class ArbolB5
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
            NodoB raiz = Raiz;
            NodoB* raizPtr = &raiz;

            if (raiz.Claves.Tamaño == Grado - 1)
            {
                NodoB nuevaRaiz = new NodoB();
                nuevaRaiz.EsHoja = false;
                nuevaRaiz.Hijos.Insertar(0);
                nuevaRaiz.Hijos.EstablecerHijo(0, raizPtr);

                Raiz = nuevaRaiz;
                Raiz.InsertarEnNodo(clave, factura);
            }
            else 
            {
                raiz.InsertarEnNodo(clave, factura);
            }
        }

        public void Mostrar()
        {
            MostrarNodo(Raiz, 0);
        }

        public void MostrarNodo(NodoB nodo, int nivel)
        {
            Console.Write("Nivel " + nivel + ": ");
            for (int i = 0; i < nodo.Claves.Tamaño; i++)
            {
                Console.Write(nodo.Claves.Buscar(i).Clave + " ");
            }
            Console.WriteLine();

            if (!nodo.EsHoja)
            {
                for (int i = 0; i < nodo.Hijos.Tamaño; i++)
                {
                    MostrarNodo(nodo.Hijos.Buscar(i), nivel + 1);
                }
            }
        }

        public String GenerarDot()
        {
            String dot = "digraph G {\n";
            dot += "node [shape=record];\n";
            dot += "rankdir=TB;\n";
            dot += "node [height=0.5];\n";
            dot += "node [width=0.5];\n";
            dot += "node [shape=record];\n";
            dot += "node [style=filled];\n";
            dot += "node [fillcolor=\"#EEEEEE\"];\n";
            dot += "node [fontname=\"Arial\"];\n";
            dot += "edge [fontname=\"Arial\"];\n";
            dot += "edge [fontsize=8];\n";
            dot += "edge [fontcolor=\"#333333\"];\n";
            dot += "edge [labelfloat=false];\n";
            dot += "edge [decorate=true];\n";
            dot += "edge [style=\"solid\"];\n";
            dot += "edge [color=\"#333333\"];\n";
            dot += "edge [dir=\"forward\"];\n";
            dot += "edge [arrowhead=\"normal\"];\n";
            dot += "edge [arrowsize=\"0.5\"];\n";
            dot += "edge [arrowtail=\"normal\"];\n";
            dot += "edge [taillabel=\"\"];\n";
            dot += "edge [headlabel=\"\"];\n";
            dot += "edge [label=\"\"];\n";
            dot += "edge [weight=\"1\"];\n";

            void GenerarDotNodo(NodoB nodo, ref String dot)
            {
                if (nodo == null) return;

                dot += $"\"{nodo.GetHashCode()}\" [label=\"<P0>";
                for (int i = 0; i < nodo.Claves.Tamaño; i++)
                {
                    var clave = nodo.Claves.Buscar(i).Clave;
                    var factura = nodo.Claves.Buscar(i).Factura;
                    dot += $"|{clave}\\nId: {factura.Id}\\nOrden: {factura.Id_Orden}\\nTotal: {factura.Total}|<P{i + 1}>";
                }
                dot += "\"];\n";

                if (!nodo.EsHoja)
                {
                    for (int i = 0; i <= nodo.Claves.Tamaño; i++)
                    {
                        NodoB hijo = nodo.Hijos.Buscar(i);
                        if (hijo != null)
                        {
                            dot += $"\"{nodo.GetHashCode()}\":P{i} -> \"{hijo.GetHashCode()}\";\n";
                            GenerarDotNodo(hijo, ref dot);
                        }
                    }
                }
            }

            GenerarDotNodo(Raiz, ref dot);

            dot += "}";
            return dot;
        }
    }
}