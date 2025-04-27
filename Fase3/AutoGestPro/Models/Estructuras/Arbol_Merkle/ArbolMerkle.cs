using System.Text;
using System.Security.Cryptography;
using AutoGestPro.Models.Estructuras;
using AutoGestPro.Models.Entidades;

namespace AutoGestPro.Models
{
    public class ArbolMerkle
    {
        public Nodo? Raiz;
        public List<Nodo> Facturas;

        public ArbolMerkle()
        {
            Raiz = null;
            Facturas = new List<Nodo>();
        }

        public void Insertar(int id, int id_servicio, double total, string metodoDePago)
        {
            foreach(var factura in Facturas)
            {
                if(factura.Factura.Id == id)
                {
                    Console.WriteLine("Error: Ya existe una factura con el ID:", Convert.ToString(id));
                }
            }

            Factura factura_ = new Factura(id, id_servicio, total, metodoDePago);

            Nodo nuevoNodo = new Nodo(factura_);
            Facturas.Add(nuevoNodo);


            CrearArbol();
        }

        public void CrearArbol()
        {
            if(Facturas.Count == 0)
            {
                Raiz = null;
                return;
            }


            List<Nodo> nivelActual = new List<Nodo>(Facturas);

            while(nivelActual.Count > 1)
            {
                List<Nodo> siguienteNivel = new List<Nodo>();

                for(int i = 0; i < nivelActual.Count; i +=2)
                {

                    Nodo izquierdo = nivelActual[i];
                    Nodo derecho = (i + 1 < nivelActual.Count) ? nivelActual[i + 1] : null;
                    Nodo padre = new Nodo(izquierdo, derecho);

                    siguienteNivel.Add(padre);

                }

                nivelActual = siguienteNivel;
            }

            Raiz = nivelActual[0];

        }

        public bool VerificarIntegridad(Factura factura)
        {
            if (Raiz == null) return false;

            string hashFactura = factura.GetHash();
            Nodo nodoEncontrado = EncontrarNodo(Raiz, factura);

            if (nodoEncontrado == null) return false;

            if (nodoEncontrado.Hash != hashFactura) return false;

            return true;   
        }

        public Nodo EncontrarNodo(Nodo nodo, Factura factura)
        {
            if (nodo == null) return null;
            if (nodo.Factura.Id == factura.Id)
            {
                return nodo;
            }
            Nodo encontradoIzquierdo = EncontrarNodo(nodo.Izquierdo, factura);
            if (encontradoIzquierdo != null) return encontradoIzquierdo;
            Nodo encontradoDerecho = EncontrarNodo(nodo.Derecho, factura);
            if (encontradoDerecho != null) return encontradoDerecho;
            return null;
        }

        public string GenerarDot()
        {
            if (Raiz == null)
            {
                return "digraph G {\n  // Árbol vacío\n}";
            }

            var dot = new StringBuilder();
            dot.AppendLine("digraph G {");
            dot.AppendLine("nodo [shape=record];");
            dot.AppendLine("rankdir=BT;");
            dot.AppendLine("nodo [height=0.5];");
            dot.AppendLine("nodo [width=0.5];");
            dot.AppendLine("nodo [style=filled];");
            dot.AppendLine("nodo [fillcolor=\"#EEEEEE\"];");
            dot.AppendLine("nodo [fontname=\"Arial\"];");
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

            var nodeIds = new Dictionary<string, int>();
            int idCounter = 0;

            GenerarDotRecursivo(Raiz, dot, nodeIds, ref idCounter);

            dot.AppendLine("}");
            return dot.ToString();
        }

        private void GenerarDotRecursivo(Nodo nodo, StringBuilder dot, Dictionary<string, int> nodeIds, ref int idCounter)
        {
            if (nodo == null) return;

            if (!nodeIds.ContainsKey(nodo.Hash))
            {
                nodeIds[nodo.Hash] = idCounter++;
            }

            int nodeId = nodeIds[nodo.Hash];

            string label = nodo.Factura != null
                ? $"\"Factura {nodo.Factura.Id}\\nTotal: {nodo.Factura.Total}\\nHash: {nodo.Hash.Substring(0, 8)}...\""
                : $"\"Hash: {nodo.Hash.Substring(0, 8)}...\"";

            dot.AppendLine($"  nodo{nodeId} [label={label}];");

            if (nodo.Izquierdo != null)
            {
                if (!nodeIds.ContainsKey(nodo.Izquierdo.Hash))
                {
                    nodeIds[nodo.Izquierdo.Hash] = idCounter++;
                }
                int leftId = nodeIds[nodo.Izquierdo.Hash];
                dot.AppendLine($" nodo{leftId} -> nodo{nodeId};");
                GenerarDotRecursivo(nodo.Izquierdo, dot, nodeIds, ref idCounter);
            }

            if (nodo.Derecho != null)
            {
                if (!nodeIds.ContainsKey(nodo.Derecho.Hash))
                {
                    nodeIds[nodo.Derecho.Hash] = idCounter++;
                }
                int rightId = nodeIds[nodo.Derecho.Hash];
                dot.AppendLine($" nodo{rightId} -> nodo{nodeId};");
                GenerarDotRecursivo(nodo.Derecho, dot, nodeIds, ref idCounter);
            }
        }
    }
}
