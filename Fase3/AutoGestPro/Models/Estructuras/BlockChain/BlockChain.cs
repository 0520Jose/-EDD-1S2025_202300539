using System;
using System.Text;
using AutoGestPro.Models.Entidades;
using System.Security.Cryptography;

namespace AutoGestPro.Models.Estructuras
{
    public class BlockChain
    {
        public UsuarioNodo Inicio { get; set; }
        public UsuarioNodo Fin { get; set; }
        public int Tamanio { get; set; }

        public BlockChain()
        {
            Inicio = null;
            Fin = null;
            Tamanio = 0;
        }

        public void Insertar(int id, string nombres, string apellidos, string correo, int edad, string contrasenia)
        {
            string contraseniaEncriptada = GetSHA256(contrasenia);
            var nuevoUsuario = new UsuarioNodo
            {
                Index = Tamanio,
                Id = id,
                Nombres = nombres,
                Apellidos = apellidos,
                Correo = correo,
                Edad = edad,
                Contrasenia = contraseniaEncriptada,
                Fecha = DateTime.Now.ToString("dd-MM-yy::HH:mm:ss"),
                Nonce = 0
            };

            if (Inicio == null)
            {
                nuevoUsuario.HashAnterior = "00000";
            }
            else
            {
                nuevoUsuario.HashAnterior = Fin.Hash;
            }

            nuevoUsuario.Hash = nuevoUsuario.GenerateHash();

            if (Inicio == null)
            {
                Inicio = nuevoUsuario;
                Fin = nuevoUsuario;
            }
            else
            {
                Fin.Siguiente = nuevoUsuario;
                nuevoUsuario.Anterior = Fin;
                Fin = nuevoUsuario;
            }
            Tamanio++;
        }

        public void Minar(int id)
        {
            var usuarioActual = Inicio;
            while (usuarioActual != null)
            {
                if (usuarioActual.Id == id)
                {
                    usuarioActual.MineBlock();
                    break;
                }
                usuarioActual = usuarioActual.Siguiente;
            }
        }

        public void MinarTodo()
        {
            if (Inicio == null) return;

            try
            {
                UsuarioNodo actual = Inicio;
                actual.HashAnterior = "00000";
                actual.Nonce = 0;
                actual.MineBlock();

                UsuarioNodo anterior = actual;
                actual = actual.Siguiente;

                while (actual != null)
                {
                    actual.HashAnterior = anterior.Hash;
                    actual.Nonce = 0;
                    actual.MineBlock();

                    anterior = actual;
                    actual = actual.Siguiente;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al minar la cadena de bloques: " + ex.Message);
            }
        }




        public UsuarioNodo BuscarPorId(int id)
        {
            var usuarioActual = Inicio;
            while (usuarioActual != null)
            {
                if (usuarioActual.Id == id)
                {
                    return usuarioActual;
                }
                usuarioActual = usuarioActual.Siguiente;
            }
            return null;
        }

        public bool EliminarBloque(int index)
        {
            if (index == 0) return false;

            var usuarioActual = Inicio;
            while (usuarioActual != null)
            {
                if (usuarioActual.Index == index)
                {
                    if (usuarioActual.Anterior != null)
                        usuarioActual.Anterior.Siguiente = usuarioActual.Siguiente;
                    if (usuarioActual.Siguiente != null)
                        usuarioActual.Siguiente.Anterior = usuarioActual.Anterior;
                    if (usuarioActual == Fin)
                        Fin = usuarioActual.Anterior;

                    recalcularChain(usuarioActual.Anterior, Fin);
                    Tamanio--;
                    return true;
                }
                usuarioActual = usuarioActual.Siguiente;
            }
            return false;
        }

        public void recalcularChain(UsuarioNodo usuario, UsuarioNodo fin)
        {
            var usuarioActual = usuario != null ? usuario.Siguiente : fin;
            var anterior = usuario;
            int index = usuario != null ? usuario.Index + 1 : 0;

            while (usuarioActual != null)
            {
                usuarioActual.Index = index;
                usuarioActual.HashAnterior = anterior != null ? anterior.Hash : "00000";
                usuarioActual.Hash = usuarioActual.GenerateHash();
                anterior = usuarioActual;
                usuarioActual = usuarioActual.Siguiente;
                index++;
            }
        }

        public void imprimir()
        {
            try
            {
                var usuarioActual = Inicio;
                while (usuarioActual != null)
                {
                    Console.WriteLine($"ID: {usuarioActual.Id}, Nombre: {usuarioActual.Nombres} {usuarioActual.Apellidos}, Correo: {usuarioActual.Correo}, Edad: {usuarioActual.Edad}, Hash: {usuarioActual.Hash}");
                    usuarioActual = usuarioActual.Siguiente;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al imprimir la cadena de bloques: " + e.Message);
            }
        }

        public static string GetSHA256(string str)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }

        public string GenerarDot(int id)
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

            var usuarioActual = Inicio;
            while (usuarioActual != null)
            {
                if (usuarioActual.Id == id)
                {   
                    string Id = id.ToString();
                    string id_ = Id.Replace("{", "\\{").Replace("}", "\\}").Replace("\"", "\\\"");
                    string nombres = usuarioActual.Nombres.Replace("{", "\\{").Replace("}", "\\}").Replace("\"", "\\\"");
                    string apellidos = usuarioActual.Apellidos.Replace("{", "\\{").Replace("}", "\\}").Replace("\"", "\\\"");
                    string correo = usuarioActual.Correo.Replace("{", "\\{").Replace("}", "\\}").Replace("\"", "\\\"");
                    string contrasenia = usuarioActual.Contrasenia.Replace("{", "\\{").Replace("}", "\\}").Replace("\"", "\\\"");
                    string hashAnterior = usuarioActual.HashAnterior.Replace("{", "\\{").Replace("}", "\\}").Replace("\"", "\\\"");
                    string hash = usuarioActual.Hash.Replace("{", "\\{").Replace("}", "\\}").Replace("\"", "\\\"");

                    dot.AppendLine($"\"{usuarioActual.Index}\" [label=\"{{");
                    dot.AppendLine($"INDEX: {usuarioActual.Index}\\l");
                    dot.AppendLine($"TIMESTAMP: {usuarioActual.Fecha}\\l");
                    dot.AppendLine($"DATA: \\{{ID: {id_}, NOMBRE: {nombres}, APELLIDO: {apellidos}, CORREO: {correo}, EDAD: {usuarioActual.Edad}, CONTRASEÑA: {contrasenia}\\}}\\l");
                    dot.AppendLine($"NONCE: {usuarioActual.Nonce}\\l");
                    dot.AppendLine($"PREVIOUS HASH: {hashAnterior}\\l");
                    dot.AppendLine($"HASH: {hash}\\l");
                    dot.AppendLine("}\"]");
                    break;
                }
                usuarioActual = usuarioActual.Siguiente;
            }

            dot.AppendLine("}");
            return dot.ToString();
        }

    }
}
