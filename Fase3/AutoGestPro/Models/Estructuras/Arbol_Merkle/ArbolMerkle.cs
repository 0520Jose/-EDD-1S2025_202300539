using System.Text;
using System.Security.Cryptography;
using AutoGestPro.Models.Estructuras;
using AutoGestPro.Models.Entidades;

namespace AutoGestPro.Models
{
    public class ArbolMerkle
    {
        public Nodo? Raiz { get; private set; }
        public Lista_Doble Facturas;

        public ArbolMerkle()
        {
            Raiz = new Nodo();
            Facturas = new Lista_Doble();
        }

        public void Insertar(Factura factura)
        {
            Facturas.Insertar(factura);
            OrdenarArbol();
        }

        public void OrdenarArbol()
        {
            Raiz = new Nodo();
            
            if (Facturas.inicio == null)
                return;
            
            List<Nodo> nodos = new List<Nodo>();
            Factura actualFactura = Facturas.inicio;
            
            while (actualFactura != null)
            {
                Nodo nodo = new Nodo();
                nodo.IzquierdoFinal = actualFactura;
                nodo.DerechoFinal = actualFactura;
                nodo.Hash = nodo.HashPadre();
                nodos.Add(nodo);
                actualFactura = actualFactura.siguiente;
            }
            
            while (nodos.Count > 1)
            {
                List<Nodo> nuevaLista = new List<Nodo>();
                bool impar = nodos.Count % 2 == 1;
                
                for (int i = 0; i < nodos.Count; i += 2)
                {
                    Nodo izquierdo = nodos[i];
                    
                    if (i == nodos.Count - 1 && impar)
                    {
                        Nodo padre = new Nodo();
                        padre.Izquierdo = izquierdo;
                        padre.Derecho = null;
                        padre.IzquierdoFinal = izquierdo.IzquierdoFinal;
                        padre.DerechoFinal = izquierdo.DerechoFinal;
                        padre.EsNodoImpar = true;
                        padre.Hash = padre.HashPadre();
                        nuevaLista.Add(padre);
                    }
                    else
                    {
                        Nodo derecho = nodos[i + 1];
                        
                        if (izquierdo.EsNodoImpar)
                        {
                            izquierdo.Derecho = derecho;
                            izquierdo.DerechoFinal = derecho.DerechoFinal;
                            izquierdo.EsNodoImpar = false;
                            izquierdo.Hash = izquierdo.HashPadre();
                            nuevaLista.Add(izquierdo);
                        }
                        else
                        {
                            Nodo padre = new Nodo();
                            padre.Izquierdo = izquierdo;
                            padre.Derecho = derecho;
                            padre.IzquierdoFinal = izquierdo.IzquierdoFinal;
                            padre.DerechoFinal = derecho.DerechoFinal;
                            padre.Hash = padre.HashPadre();
                            nuevaLista.Add(padre);
                        }
                    }
                }
                
                nodos = nuevaLista;
            }
            
            Raiz = nodos[0];
        }

        public bool VerificarIntegridad(Factura factura)
        {
            Nodo actual = EncontrarNodo(Raiz, factura);
            if (actual == null) return false;
            
            string hashCalculado = CalcularHash(actual.IzquierdoFinal.Id.ToString() + actual.IzquierdoFinal.Id_Servicio.ToString() + actual.IzquierdoFinal.Total.ToString() + actual.IzquierdoFinal.Fecha + actual.IzquierdoFinal.MetodoDePago);
            return hashCalculado == actual.Hash;
        }

        public Nodo EncontrarNodo(Nodo nodo, Factura factura)
        {
            if (nodo == null) return null;

            if (nodo.IzquierdoFinal == factura || nodo.DerechoFinal == factura)
                return nodo;

            Nodo encontrado = EncontrarNodo(nodo.Izquierdo, factura);
            if (encontrado != null) return encontrado;

            return EncontrarNodo(nodo.Derecho, factura);
        }

        public static string CalcularHash(string texto)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(texto);
                byte[] hash = sha256.ComputeHash(bytes);
                return BitConverter.ToString(hash).Replace("-", "").ToLower();
            }
        }


        public void mostrarFacturas()
        {
            Factura actual = Facturas.inicio;

            while (actual != null)
            {
                Console.Write(actual.Id);
                actual = actual.siguiente;
            }
            Console.WriteLine();

        } 

        public string GenerarDot()
        {
            var dot = new StringBuilder();
            dot.AppendLine("digraph G {");
            dot.AppendLine("node [shape=record];");
            dot.AppendLine("rankdir=BT;");
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
            if (Raiz != null)
            {
            GenerarDotRecursivo(Raiz, dot);
            }
            dot.AppendLine("}");
            return dot.ToString();
        }

        private void GenerarDotRecursivo(Nodo nodo, StringBuilder dot)
        {
            if (nodo == null) return;
            
            if (nodo.IzquierdoFinal != null && nodo.DerechoFinal != null && nodo.IzquierdoFinal == nodo.DerechoFinal)
            {
                dot.AppendLine($"\"{nodo.Hash}\" [label=\"Id: {nodo.IzquierdoFinal.Id}\\nId_Servicio: {nodo.IzquierdoFinal.Id_Servicio}\\nTotal: {nodo.IzquierdoFinal.Total}\\nFecha: {nodo.IzquierdoFinal.Fecha}\\nMetodoDePago: {nodo.IzquierdoFinal.MetodoDePago}\\nHash: {nodo.Hash.Substring(0, 8)}...\"];");
            }
            else
            {
                dot.AppendLine($"\"{nodo.Hash}\" [label=\"Hash: {nodo.Hash.Substring(0, 8)}...\"];");
            }
            
            if (nodo.EsNodoImpar && nodo.Derecho == null)
            {
                if (nodo.Izquierdo != null)
                {
                    dot.AppendLine($"\"{nodo.Izquierdo.Hash}\" -> \"{nodo.Hash}\";");
                    GenerarDotRecursivo(nodo.Izquierdo, dot);
                }
            }
            else
            {
                if (nodo.Izquierdo != null)
                {
                    dot.AppendLine($"\"{nodo.Izquierdo.Hash}\" -> \"{nodo.Hash}\";");
                    GenerarDotRecursivo(nodo.Izquierdo, dot);
                }
                
                if (nodo.Derecho != null)
                {
                    dot.AppendLine($"\"{nodo.Derecho.Hash}\" -> \"{nodo.Hash}\";");
                    GenerarDotRecursivo(nodo.Derecho, dot);
                }
            }
        }
    }
}
