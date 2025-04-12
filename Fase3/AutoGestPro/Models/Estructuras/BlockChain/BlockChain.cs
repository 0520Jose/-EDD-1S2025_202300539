using System;
using AutoGestPro.Models.Entidades;
using System.Runtime.InteropServices;
using System.Text.Json;
using System.Text;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace AutoGestPro.Models.Estructuras.BlockChain
{
    unsafe class BlockChain
    {
        public Usuario* Inicio { get; set; }
        public Usuario* Fin { get; set; }
        public int Tamanio { get; set; }


        public BlockChain()
        {
            Inicio = null;
            Fin = null;
            Tamanio = 0;
        }

        public void Insertar(int id, string nombres, string apellidos, string correo, int edad, string contrasenia)
        {
            Usuario* nuevoUsuario = (Usuario*)Marshal.AllocHGlobal(sizeof(Usuario));
            nuevoUsuario->Index = Tamanio;
            nuevoUsuario->Id = id;
            nuevoUsuario->Nombres = nombres;
            nuevoUsuario->Apellidos = apellidos;
            nuevoUsuario->Correo = correo;
            nuevoUsuario->Edad = edad;
            nuevoUsuario->Contrasenia = contrasenia;

            if (Inicio == null)
            {
                nuevoUsuario->HashAnterior = "00000";
            }
            else
            {
                nuevoUsuario->HashAnterior = Fin->Hash;
            }

            nuevoUsuario->Hash = calcularHash(nuevoUsuario);

            if (Inicio == null)
            {
                Inicio = nuevoUsuario;
                Fin = nuevoUsuario;
            }
            else
            {
                Fin->Siguiente = nuevoUsuario;
                nuevoUsuario->Anterior = Fin;
                Fin = nuevoUsuario;
            }
            Tamanio++;
        }

        public static string calcularHash(Usuario* usuario)
        {
            var hashData = new
            {
                index = usuario->Index,
                fecha = usuario->Fecha,
                id = usuario->Id,
                nombres = usuario->Nombres,
                apellidos = usuario->Apellidos,
                correo = usuario->Correo,
                edad = usuario->Edad,
                contrasenia = usuario->Contrasenia,
                hashAnterior = usuario->HashAnterior
            };

            string json = JsonSerializer.Serialize(hashData);
            byte[] dataBytes = Encoding.UTF8.GetBytes(json);

            byte[] hashBytes = SHA256.HashData(dataBytes);
            var hashString = new StringBuilder();
            foreach (byte b in hashBytes)
            {
                hashString.Append(b.ToString("x2"));
            }
            return hashString.ToString();
        }

        public Boolean ChainValido()
        {
            if (Inicio == null)
            {
                return true;
            }

            Usuario* usuarioActual = Inicio;
            string hashAnterior = "00000";

            while (usuarioActual != null)
            {
                if (usuarioActual->HashAnterior != hashAnterior)
                {
                    return false;
                }

                string hashActual = calcularHash(usuarioActual);
                if (usuarioActual->Hash != hashActual)
                {
                    return false;
                }

                if (usuarioActual->Siguiente != null && usuarioActual->Siguiente->Anterior != usuarioActual)
                {
                    return false;
                }   
                hashAnterior = usuarioActual->Hash;
                usuarioActual = usuarioActual->Siguiente;
            }
            return true;
        }

        public Usuario BuscarPorId(int id)
        {
            Usuario* usuarioActual = Inicio;
            while (usuarioActual != null)
            {
                if (usuarioActual->Id == id)
                {
                    return *usuarioActual;
                }
                usuarioActual = usuarioActual->Siguiente;
            }
            return new Usuario();
        }

        public Boolean EliminarBloque(int index)
        {
            if (index == 0) {
                return false;
            }

            Usuario* usuarioActual = Inicio;

            while (usuarioActual != null)
            {
                if (usuarioActual->Index == index)
                {
                    if (usuarioActual->Anterior != null)
                    {
                        usuarioActual->Anterior->Siguiente = usuarioActual->Siguiente;
                    }

                    if (usuarioActual->Siguiente != null)
                    {
                        usuarioActual->Siguiente->Anterior = usuarioActual->Anterior;
                    }

                    if (usuarioActual == Fin)
                    {
                        Fin = usuarioActual->Anterior;
                    }

                    recalcularChain(usuarioActual->Anterior, Fin);

                    Marshal.FreeHGlobal((IntPtr)usuarioActual);
                    Tamanio--;
                    return true;
                }
                usuarioActual = usuarioActual->Siguiente;
            }
            return false;
        }

        public void recalcularChain(Usuario* usuario, Usuario* fin)
        {
            Usuario* usuarioActual = usuario != null ? usuario->Siguiente : fin;
            Usuario* anterior = usuario;
            int index = usuario != null ? usuario->Index + 1 : 0;

            while (usuarioActual != null)
            {
                usuarioActual->Index = index;
                usuarioActual->HashAnterior = anterior != null ? anterior->Hash : "00000";
                usuarioActual->Hash = calcularHash(usuarioActual);
                anterior = usuarioActual;
                usuarioActual = usuarioActual->Siguiente;
                index++;
            }

        }

        public string Graficar()
        {   
            StringBuilder dotCode = new StringBuilder();
            dotCode.AppendLine("digraph Blockchain {");
            dotCode.AppendLine("    label=\"Blockchain - Cadena de Suministro (Lista Doblemente Enlazada)\";");
            dotCode.AppendLine("    labelloc=t;");
            dotCode.AppendLine("    fontsize=20;");
            dotCode.AppendLine("    rankdir=LR;");
            dotCode.AppendLine("    bgcolor=\"#f8f9fa\";");
            dotCode.AppendLine();
            dotCode.AppendLine("    node [");
            dotCode.AppendLine("        shape=box3d,");
            dotCode.AppendLine("        style=\"filled,rounded\",");
            dotCode.AppendLine("        fillcolor=\"#e3f2fd\",");
            dotCode.AppendLine("        color=\"#1565c0\",");
            dotCode.AppendLine("        fontname=\"Arial\",");
            dotCode.AppendLine("        fontsize=12,");
            dotCode.AppendLine("        width=2.5,");
            dotCode.AppendLine("        height=1.2,");
            dotCode.AppendLine("        margin=0.3");
            dotCode.AppendLine("    ];");
            dotCode.AppendLine();
            dotCode.AppendLine("    edge [");
            dotCode.AppendLine("        color=\"#7e57c2\",");
            dotCode.AppendLine("        arrowhead=normal,");
            dotCode.AppendLine("        arrowtail=dot,");
            dotCode.AppendLine("        penwidth=2");
            dotCode.AppendLine("    ];");
            dotCode.AppendLine();

            string[] colors = new string[]
            {
            "#e3f2fd", "#bbdefb", "#90caf9", "#64b5f6",
            "#42a5f5", "#2196f3", "#1e88e5", "#1976d2"
            };

            StringBuilder nodes = new StringBuilder();
            StringBuilder connections = new StringBuilder();

            Usuario* usuarioActual = Inicio;
            while (usuarioActual != null)
            {
            int colorIndex = usuarioActual->Index % colors.Length;

            nodes.AppendLine($"    block{usuarioActual->Index} [");
            nodes.AppendLine("        label=<");
            nodes.AppendLine("            <table border=\"0\" cellborder=\"0\" cellspacing=\"5\">");
            nodes.AppendLine($"                <tr><td colspan=\"2\" bgcolor=\"#1565c0\" align=\"center\"><font color=\"white\">Bloque #{usuarioActual->Index}</font></td></tr>");
            nodes.AppendLine($"                <tr><td align=\"left\"><b>ID:</b></td><td align=\"left\">{usuarioActual->Id}</td></tr>");
            nodes.AppendLine($"                <tr><td align=\"left\"><b>Nombre:</b></td><td align=\"left\">{usuarioActual->Nombres} {usuarioActual->Apellidos}</td></tr>");
            nodes.AppendLine($"                <tr><td align=\"left\"><b>Correo:</b></td><td align=\"left\">{usuarioActual->Correo}</td></tr>");
            nodes.AppendLine($"                <tr><td align=\"left\"><b>Edad:</b></td><td align=\"left\">{usuarioActual->Edad}</td></tr>");
            nodes.AppendLine($"                <tr><td align=\"left\"><b>Hash:</b></td><td align=\"left\">{usuarioActual->Hash.Substring(0, Math.Min(12, usuarioActual->Hash.Length))}...</td></tr>");
            nodes.AppendLine($"                <tr><td align=\"left\"><b>PrevHash:</b></td><td align=\"left\">{usuarioActual->HashAnterior.Substring(0, Math.Min(12, usuarioActual->HashAnterior.Length))}...</td></tr>");
            nodes.AppendLine("            </table>");
            nodes.AppendLine("        >,");
            nodes.AppendLine($"        fillcolor=\"{colors[colorIndex]}\",");
            nodes.AppendLine("        gradientangle=\"90\"");
            nodes.AppendLine("    ];");
            nodes.AppendLine();

            if (usuarioActual->Anterior != null)
            {
                connections.AppendLine($"    block{usuarioActual->Anterior->Index} -> block{usuarioActual->Index} [");
                connections.AppendLine("        tailport=e,");
                connections.AppendLine("        headport=w,");
                connections.AppendLine("        color=\"#5e35b1\"");
                connections.AppendLine("    ];");
                connections.AppendLine();
                connections.AppendLine($"    block{usuarioActual->Index} -> block{usuarioActual->Anterior->Index} [");
                connections.AppendLine("        tailport=w,");
                connections.AppendLine("        headport=e,");
                connections.AppendLine("        color=\"#7e57c2\",");
                connections.AppendLine("        style=dashed");
                connections.AppendLine("    ];");
                connections.AppendLine();
            }

            usuarioActual = usuarioActual->Siguiente;
            }

            dotCode.Append(nodes.ToString());
            dotCode.AppendLine();
            dotCode.Append(connections.ToString());
            dotCode.AppendLine("}");

            return dotCode.ToString();
        }
    }
    
}   