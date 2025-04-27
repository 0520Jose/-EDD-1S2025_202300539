using System;
using AutoGestPro.Models.Estructuras;
using AutoGestPro.Models.Entidades;

namespace AutoGestPro.Models
{
    class ArbolAVL
    {
        public NodoAVL Raiz { get; set; }

        public ArbolAVL()
        {
            Raiz = null;
        }
        private int getAltura(NodoAVL nodo) => nodo?.Altura ?? 0;
        private int getBalance(NodoAVL nodo) => nodo == null ? 0 : getAltura(nodo.Izquierdo) - getAltura(nodo.Derecho);
        
        private NodoAVL RotarDerecha(NodoAVL y)
        {
            NodoAVL x = y.Izquierdo;
            NodoAVL T2 = x.Derecho;
            x.Derecho = y;
            y.Izquierdo = T2;
            y.Altura = Math.Max(getAltura(y.Izquierdo), getAltura(y.Derecho)) + 1;
            x.Altura = Math.Max(getAltura(x.Izquierdo), getAltura(x.Derecho)) + 1;
            return x;
        }

        private NodoAVL RotarIzquierda(NodoAVL x)
        {
            NodoAVL y = x.Derecho;
            NodoAVL T2 = y.Izquierdo;
            y.Izquierdo = x;
            x.Derecho = T2;
            x.Altura = Math.Max(getAltura(x.Izquierdo), getAltura(x.Derecho)) + 1;
            y.Altura = Math.Max(getAltura(y.Izquierdo), getAltura(y.Derecho)) + 1;
            return y;
        }

        public NodoAVL Insertar(NodoAVL raiz, Repuesto repuesto)
        {
            if (raiz == null)
                return new NodoAVL(repuesto);
            if (repuesto.Id < raiz.Repuesto.Id)
                raiz.Izquierdo = Insertar(raiz.Izquierdo, repuesto);
            else if (repuesto.Id > raiz.Repuesto.Id)
                raiz.Derecho = Insertar(raiz.Derecho, repuesto);
            else
                return raiz;
            raiz.Altura = 1 + Math.Max(getAltura(raiz.Izquierdo), getAltura(raiz.Derecho));

            int balance = getBalance(raiz);

            if (balance > 1 && repuesto.Id < raiz.Izquierdo.Repuesto.Id)
                return RotarDerecha(raiz);
            if (balance < -1 && repuesto.Id > raiz.Derecho.Repuesto.Id)
                return RotarIzquierda(raiz);
            if (balance > 1 && repuesto.Id > raiz.Izquierdo.Repuesto.Id)
            {
                raiz.Izquierdo = RotarIzquierda(raiz.Izquierdo);
                return RotarDerecha(raiz);
            }
            if (balance < -1 && repuesto.Id < raiz.Derecho.Repuesto.Id)
            {
                raiz.Derecho = RotarDerecha(raiz.Derecho);
                return RotarIzquierda(raiz);
            }
            return raiz;

        }

        public void CargarDesdeTexto(string texto)
        {
            string[] lineas = texto.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (string linea in lineas)
            {
                string[] partes = linea.Split(',');
                if (partes.Length == 4)
                {
                    int id = int.Parse(partes[0]);
                    string repuesto = partes[1];
                    string detalle = partes[2];
                    float costo = float.Parse(partes[3]);
                    Repuesto nuevoRepuesto = new Repuesto(id, repuesto, detalle, costo);
                    Raiz = Insertar(Raiz, nuevoRepuesto);
                }
            }
        }

        public string ObtenerTexto()
        {
            return ObtenerTextoRecursivo(Raiz);
        }

        private string ObtenerTextoRecursivo(NodoAVL nodo)
        {
            if (nodo == null)
                return string.Empty;
            string texto = $"{nodo.Repuesto.Id},{nodo.Repuesto.REpuesto},{nodo.Repuesto.Detalle},{nodo.Repuesto.Costo}\n";
            texto += ObtenerTextoRecursivo(nodo.Izquierdo);
            texto += ObtenerTextoRecursivo(nodo.Derecho);
            return texto;
        }

        public NodoAVL Buscar(NodoAVL raiz, int id)
        {
            if (raiz == null)
                return null;
            if (raiz.Repuesto.Id == id)
                return raiz;
            if (raiz.Repuesto.Id < id)
                return Buscar(raiz.Derecho, id);
            return Buscar(raiz.Izquierdo, id);
        }

        public void ActualizarRepuesto(int id, string repuesto, string detalle, float costo)
        {
            NodoAVL nodo = Buscar(Raiz, id);
            if (nodo != null)
            {
                nodo.Repuesto.REpuesto = repuesto;
                nodo.Repuesto.Detalle = detalle;
                nodo.Repuesto.Costo = costo;
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
            
            void GenerarDotRecursivo(NodoAVL nodo)
            {
                if (nodo != null)
                {
                    dot += $"nodo{nodo.Repuesto.Id} [label=\"<f0> |<f1> ID: {nodo.Repuesto.Id}\\nRepuesto: {nodo.Repuesto.REpuesto}\\nDetalles: {nodo.Repuesto.Detalle}\\nCosto: {nodo.Repuesto.Costo}|<f2>\"];\n";

                    if (nodo.Izquierdo != null)
                    {
                        dot += $"nodo{nodo.Repuesto.Id}:f0 -> nodo{nodo.Izquierdo.Repuesto.Id}:f1;\n";
                        GenerarDotRecursivo(nodo.Izquierdo);
                    }

                    if (nodo.Derecho != null)
                    {
                        dot += $"nodo{nodo.Repuesto.Id}:f2 -> nodo{nodo.Derecho.Repuesto.Id}:f1;\n";
                        GenerarDotRecursivo(nodo.Derecho);
                    }
                }
            }

            GenerarDotRecursivo(Raiz);

            dot += "}";
            return dot;
        }
    }
}