using System;
using AutoGestPro.Models.Entidades;
using System.Text;

namespace AutoGestPro.Models.Estructuras
{
    public class ArbolMerkle
    {
        public Factura Raiz { get; set; }
        private NodoLista? inicio;
        private NodoLista? fin;

        public int Tamanio { get; set; }

        public ArbolMerkle()
        {
            inicio = null;
            fin = null;
        }

        public void Insertar(Factura factura)
        {
            var nuevo = new NodoLista(factura);
            if (inicio == null)
            {
                inicio = nuevo;
                fin = nuevo;
            }
            else
            {
                fin.Siguiente = nuevo;
                nuevo.Anterior = fin;
                fin = nuevo;
            }

            RecalcularArbol();
        }

        private void RecalcularArbol()
        {
            var nodos = new List<Factura>();
            var actual = inicio;

            while (actual != null)
            {
                nodos.Add(actual.Dato);
                actual = actual.Siguiente;
            }

            while (nodos.Count > 1)
            {
                var nivelSuperior = new List<Factura>();
                for (int i = 0; i < nodos.Count; i += 2)
                {
                    if (i + 1 < nodos.Count)
                    {
                        var padre = new Factura(nodos[i], nodos[i + 1]);
                        nivelSuperior.Add(padre);
                    }
                    else
                    {
                        var padre = new Factura(nodos[i], null);
                        nivelSuperior.Add(padre);
                    }
                }
                nodos = nivelSuperior;
            }
            Raiz = nodos.FirstOrDefault();
        }


        public Factura? Buscar(int id)
        {
            return BuscarPorRecorrido(Raiz, id);
        }

        private Factura? BuscarPorRecorrido(Factura? nodo, int id)
        {
            if (nodo == null) return null;
            if (nodo.Id == id) return nodo;

            Factura? encontrado = BuscarPorRecorrido(nodo.Izquierdo, id);
            if (encontrado != null) return encontrado;

            return BuscarPorRecorrido(nodo.Derecho, id);
        }

        public string GenerarDot()
        {
            var dot = new StringBuilder();
            dot.AppendLine("digraph G {");
            dot.AppendLine("node [shape=record];");
            dot.AppendLine("rankdir=TB;");
            dot.AppendLine("node [height=0.5];");
            dot.AppendLine("node [width=0.5];");
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

            GenerarDotRecursivo(Raiz, dot);

            dot.AppendLine("}");
            return dot.ToString();
        }

        public void GenerarDotRecursivo(Factura? factura, StringBuilder dot)
        {
            if (factura == null) return;

            dot.AppendLine($"n{factura.Id} [label=\"<f0> |<f1> ID: {factura.Id}\\nId Servicio: {factura.Id_Servicio}\\nTotal: {factura.Total}\\nFecha: {factura.Fecha}\\nMetodo de pago: {factura.MetodoDePago}\\nHash: {factura.Hash}|<f2>\"];");

            if (factura.Izquierdo != null)
            {
                dot.AppendLine($"n{factura.Id} -> n{factura.Izquierdo.Id};");
            }

            if (factura.Derecho != null)
            {
                dot.AppendLine($"n{factura.Id} -> n{factura.Derecho.Id};");
            }

            GenerarDotRecursivo(factura.Izquierdo, dot);
            GenerarDotRecursivo(factura.Derecho, dot);
        }


    }
}