using System;
using System.Text;
using AutoGestPro.Models.Entidades;

namespace AutoGestPro.Models.Listas.Arbol_B5
{
    public unsafe class ArbolB5
    {
        private int T;
        public int Tamaño { get; set; }
        public NodoB Raiz { get; set; }

        public ArbolB5(int Orden)
        {
            T = Orden;
            Raiz = new NodoB(T);
            Tamaño = 0;
        }

        public void Insertar(Factura nuevaFactura)
        {
            NodoB r = Raiz;
            if (r.n_facturas == T)
            {
                NodoB nuevaRaiz = new NodoB(T);
                nuevaRaiz.hijos[0] = r;
                DividirHijo(nuevaRaiz, 0, r);
                Raiz = nuevaRaiz;
                InsertarNoLleno(nuevaRaiz, nuevaFactura);
            }
            else
            {
                InsertarNoLleno(r, nuevaFactura);
            }
            Tamaño++;
        }

        private void InsertarNoLleno(NodoB nodo, Factura nuevaFactura)
        {
            int i = nodo.n_facturas - 1;
            if (nodo.hijos[0] == null)
            {
                while (i >= 0 && nuevaFactura.Id < nodo.Facturas[i].Id)
                {
                    nodo.Facturas[i + 1] = nodo.Facturas[i];
                    i--;
                }
                nodo.Facturas[i + 1] = nuevaFactura;
                nodo.n_facturas++;
            }
            else
            {
                while (i >= 0 && nuevaFactura.Id < nodo.Facturas[i].Id)
                {
                    i--;
                }
                i++;
                if (nodo.hijos[i].n_facturas == T)
                {
                    DividirHijo(nodo, i, nodo.hijos[i]);
                    if (nuevaFactura.Id > nodo.Facturas[i].Id)
                    {
                        i++;
                    }
                }
                InsertarNoLleno(nodo.hijos[i], nuevaFactura);
            }
        }

        private void DividirHijo(NodoB nodoPadre, int indice, NodoB nodoLleno)
        {
            NodoB nuevoNodo = new NodoB(T);
            nuevoNodo.n_facturas = (T - 1) / 2;

            for (int j = 0; j < (T - 1) / 2; j++)
            {
                nuevoNodo.Facturas[j] = nodoLleno.Facturas[j + (T + 1) / 2];
            }

            if (nodoLleno.hijos[0] != null)
            {
                for (int j = 0; j <= (T - 1) / 2; j++)
                {
                    nuevoNodo.hijos[j] = nodoLleno.hijos[j + (T + 1) / 2];
                }
            }

            nodoLleno.n_facturas = (T - 1) / 2;

            for (int j = nodoPadre.n_facturas; j >= indice + 1; j--)
            {
                nodoPadre.hijos[j + 1] = nodoPadre.hijos[j];
            }

            nodoPadre.hijos[indice + 1] = nuevoNodo;

            for (int j = nodoPadre.n_facturas - 1; j >= indice; j--)
            {
                nodoPadre.Facturas[j + 1] = nodoPadre.Facturas[j];
            }

            nodoPadre.Facturas[indice] = nodoLleno.Facturas[(T - 1) / 2];
            nodoPadre.n_facturas++;
        }

        public string GenerarDot()
        {
            StringBuilder dot = new StringBuilder();
            dot.Append("digraph G {\n");
            dot.Append("node [shape=box];\n");
            dot.Append("rankdir=TB;\n");
            dot.Append("node [height=0.5];\n");
            dot.Append("node [width=0.5];\n");
            dot.Append("node [style=filled];\n");
            dot.Append("node [fillcolor=\"#EEEEEE\"];\n");
            dot.Append("node [fontname=\"Arial\"];\n");
            dot.Append("edge [fontname=\"Arial\"];\n");
            dot.Append("edge [fontsize=8];\n");
            dot.Append("edge [fontcolor=\"#333333\"];\n");
            dot.Append("edge [style=\"solid\"];\n");
            dot.Append("edge [color=\"#333333\"];\n");
            dot.Append("edge [dir=\"forward\"];\n");
            dot.Append("edge [arrowhead=\"normal\"];\n");
            dot.Append("edge [arrowsize=\"0.5\"];\n");

            GenerarDotNodo(Raiz, dot);

            dot.Append("}");

            return dot.ToString();
        }

        private void GenerarDotNodo(NodoB nodo, StringBuilder dot)
        {
            if (nodo == null) return;

            string nodeId = $"node{nodo.GetHashCode()}";

            dot.Append($"{nodeId} [label=\"<f0>");
            for (int i = 0; i < nodo.n_facturas; i++)
            {
            var factura = nodo.Facturas[i];
            dot.Append($"| Id: {factura.Id}\\nId Orden: {factura.Id_Orden}\\nTotal: {factura.Total} | <f{i + 1}>");
            }
            dot.Append("\", shape=record];\n");

            for (int i = 0; i <= nodo.n_facturas; i++)
            {
            if (nodo.hijos[i] != null)
            {
                string childNodeId = $"node{nodo.hijos[i].GetHashCode()}";
                dot.Append($"{nodeId}:f{i} -> {childNodeId};\n");
                GenerarDotNodo(nodo.hijos[i], dot);
            }
            }
        }

        public Factura Buscar(int id)
        {
            return BuscarEnNodo(Raiz, id);
        }

        private Factura BuscarEnNodo(NodoB nodo, int id)
        {
            int i = 0;
            while (i < nodo.n_facturas && id > nodo.Facturas[i].Id)
            {
                i++;
            }

            if (i < nodo.n_facturas && nodo.Facturas[i].Id == id)
            {
                return nodo.Facturas[i];
            }

            if (nodo.hijos[0] == null)
            {
                return null;
            }

            return BuscarEnNodo(nodo.hijos[i], id);
        }

        public bool PagarFactura(int idFactura)
        {
            NodoB r = Raiz;
            bool eliminado = EliminarFactura(r, idFactura);
            return eliminado;
        }

        private bool EliminarFactura(NodoB r, int idFactura)
        {
            int i = 0;
            while (i < r.n_facturas && idFactura > r.Facturas[i].Id)
            {
                i++;
            }

            if (i < r.n_facturas && r.Facturas[i].Id == idFactura)
            {
                if (r.hijos[0] == null)
                {
                    for (int j = i; j < r.n_facturas - 1; j++)
                    {
                        r.Facturas[j] = r.Facturas[j + 1];
                    }
                    r.n_facturas--;
                    return true;
                }
                else
                {
                    NodoB pred = r.hijos[i];
                    while (pred.hijos[0] != null)
                    {
                        pred = pred.hijos[pred.n_facturas];
                    }
                    r.Facturas[i] = pred.Facturas[pred.n_facturas - 1];
                    return EliminarFactura(r.hijos[i], pred.Facturas[pred.n_facturas - 1].Id);
                }
            }
            else if (r.hijos[0] != null)
            {
                bool eliminado = EliminarFactura(r.hijos[i], idFactura);
                if (r.hijos[i].n_facturas < T - 1)
                {
                    Rebalancear(r, i);
                }
                return eliminado;
            }

            return false;
        }

        public void Rebalancear(NodoB r, int i)
        {
            if (i > 0 && r.hijos[i - 1].n_facturas >= T)
            {
                NodoB hermanoIzquierdo = r.hijos[i - 1];

                for (int j = r.hijos[i].n_facturas; j > 0; j--)
                {
                    r.hijos[i].Facturas[j] = r.hijos[i].Facturas[j - 1];
                }

                if (r.hijos[i].hijos[0] != null)
                {
                    for (int j = r.hijos[i].n_facturas + 1; j > 0; j--)
                    {
                        r.hijos[i].hijos[j] = r.hijos[i].hijos[j - 1];
                    }
                }

                r.hijos[i].Facturas[0] = r.Facturas[i - 1];
                if (r.hijos[i].hijos[0] != null)
                {
                    r.hijos[i].hijos[0] = hermanoIzquierdo.hijos[hermanoIzquierdo.n_facturas];
                }

                r.Facturas[i - 1] = hermanoIzquierdo.Facturas[hermanoIzquierdo.n_facturas - 1];
                r.hijos[i].n_facturas++;
                hermanoIzquierdo.n_facturas--;
            }
            else if (i < r.n_facturas && r.hijos[i + 1].n_facturas >= T)
            {
                NodoB hermanoDerecho = r.hijos[i + 1];

                r.hijos[i].Facturas[r.hijos[i].n_facturas] = r.Facturas[i];
                if (r.hijos[i].hijos[0] != null)
                {
                    r.hijos[i].hijos[r.hijos[i].n_facturas + 1] = hermanoDerecho.hijos[0];
                }

                r.Facturas[i] = hermanoDerecho.Facturas[0];

                for (int j = 0; j < hermanoDerecho.n_facturas - 1; j++)
                {
                    hermanoDerecho.Facturas[j] = hermanoDerecho.Facturas[j + 1];
                }

                if (hermanoDerecho.hijos[0] != null)
                {
                    for (int j = 0; j < hermanoDerecho.n_facturas; j++)
                    {
                        hermanoDerecho.hijos[j] = hermanoDerecho.hijos[j + 1];
                    }
                }

                r.hijos[i].n_facturas++;
                hermanoDerecho.n_facturas--;
            }
            else
            {
                if (i < r.n_facturas)
                {
                    NodoB hijo = r.hijos[i];
                    NodoB hermano = r.hijos[i + 1];

                    hijo.Facturas[hijo.n_facturas] = r.Facturas[i];

                    for (int j = 0; j < hermano.n_facturas; j++)
                    {
                        hijo.Facturas[hijo.n_facturas + 1 + j] = hermano.Facturas[j];
                    }

                    if (hijo.hijos[0] != null)
                    {
                        for (int j = 0; j <= hermano.n_facturas; j++)
                        {
                            hijo.hijos[hijo.n_facturas + 1 + j] = hermano.hijos[j];
                        }
                    }

                    for (int j = i; j < r.n_facturas - 1; j++)
                    {
                        r.Facturas[j] = r.Facturas[j + 1];
                    }

                    for (int j = i + 1; j <= r.n_facturas; j++)
                    {
                        r.hijos[j] = r.hijos[j + 1];
                    }

                    hijo.n_facturas += 1 + hermano.n_facturas;
                    r.n_facturas--;
                }
                else
                {
                    NodoB hijo = r.hijos[i];
                    NodoB hermano = r.hijos[i - 1];

                    for (int j = hijo.n_facturas - 1; j >= 0; j--)
                    {
                        hijo.Facturas[j + T] = hijo.Facturas[j];
                    }

                    hijo.Facturas[T - 1] = r.Facturas[i - 1];

                    for (int j = hermano.n_facturas - 1; j >= 0; j--)
                    {
                        hijo.Facturas[j] = hermano.Facturas[j];
                    }

                    if (hijo.hijos[0] != null)
                    {
                        for (int j = hijo.n_facturas; j >= 0; j--)
                        {
                            hijo.hijos[j + T] = hijo.hijos[j];
                        }

                        for (int j = hermano.n_facturas; j >= 0; j--)
                        {
                            hijo.hijos[j] = hermano.hijos[j];
                        }
                    }

                    hijo.n_facturas += hermano.n_facturas + 1;

                    for (int j = i - 1; j < r.n_facturas - 1; j++)
                    {
                        r.Facturas[j] = r.Facturas[j + 1];
                    }

                    for (int j = i; j < r.n_facturas; j++)
                    {
                        r.hijos[j] = r.hijos[j + 1];
                    }

                    r.n_facturas--;
                }
            }
        }
    }
}
        