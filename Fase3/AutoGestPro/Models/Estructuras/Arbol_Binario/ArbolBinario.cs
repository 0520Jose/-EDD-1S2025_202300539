using System;
using AutoGestPro.Models.Entidades;
using System.Text;

namespace AutoGestPro.Models
{
    class ArbolBinario
    {
        public NodoBinario? Raiz { get; set; }

        public ArbolBinario()
        {
            Raiz = null;
        }

        public void Insertar(Servicio servicio)
        {
            NodoBinario nuevoNodo = new NodoBinario(servicio);
            if (Raiz == null)
            {
                Raiz = nuevoNodo;
            }
            else
            {
                NodoBinario anterior = null, reco;
                reco = Raiz;
                while (reco != null)
                {
                    anterior = reco;
                    if (servicio.Id < reco.Servicio.Id)
                    {
                        reco = reco.Izquierdo;
                    }
                    else
                    {
                        reco = reco.Derecho;
                    }
                }
                if (servicio.Id < anterior.Servicio.Id)
                {
                    anterior.Izquierdo = nuevoNodo;
                }
                else
                {
                    anterior.Derecho = nuevoNodo;
                }
            }
        }


        public NodoBinario? Buscar(int id)
        {
            return BuscarRecursivo(Raiz, id);
        }

        private NodoBinario? BuscarRecursivo(NodoBinario? nodo, int id)
        {
            if (nodo == null || nodo.Servicio.Id == id)
            {
                return nodo;
            }

            return id < nodo.Servicio.Id
                ? BuscarRecursivo(nodo.Izquierdo, id)
                : BuscarRecursivo(nodo.Derecho, id);
        }

        public void ActualizarServicio(Servicio servicio)
        {
            ActualizarServicioRecursivo(Raiz, servicio);
        }

        private void ActualizarServicioRecursivo(NodoBinario? nodo, Servicio servicio)
        {
            if (nodo == null) return;

            if (nodo.Servicio.Id == servicio.Id)
            {
                nodo.Servicio = servicio;
                return;
            }

            if (servicio.Id < nodo.Servicio.Id)
            {
                ActualizarServicioRecursivo(nodo.Izquierdo, servicio);
            }
            else
            {
                ActualizarServicioRecursivo(nodo.Derecho, servicio);
            }
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

        private void GenerarDotRecursivo(NodoBinario? nodo, StringBuilder dot)
        {
            if (nodo == null) return;

            dot.AppendLine($"n{nodo.Servicio.Id} [label=\"<f0> |<f1> ID: {nodo.Servicio.Id}\\nId repuesto: {nodo.Servicio.Id_Repuesto}\\nId vehiculo: {nodo.Servicio.Id_Vehiculo}\\nDetalles: {nodo.Servicio.Detalles}\\nCosto: {nodo.Servicio.Costo}|<f2>\"];");

            if (nodo.Izquierdo != null)
            {
            dot.AppendLine($"n{nodo.Servicio.Id} -> n{nodo.Izquierdo.Servicio.Id};");
            }

            if (nodo.Derecho != null)
            {
            dot.AppendLine($"n{nodo.Servicio.Id} -> n{nodo.Derecho.Servicio.Id};");
            }

            GenerarDotRecursivo(nodo.Izquierdo, dot);
            GenerarDotRecursivo(nodo.Derecho, dot);
        }
    }
}
