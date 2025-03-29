using System;
using System.Runtime.InteropServices;
using AutoGestPro.Models;
unsafe class ListaSimple
{
    public Usuario* inicio = null;

    public void Insertar(int id, string nombre, string apellido, string correo, int edad, string contrasenia)
    {
        Usuario* nuevoUsuario = (Usuario*)NativeMemory.Alloc((nuint)sizeof(Usuario));
        nuevoUsuario->Id = id;
        nuevoUsuario->Nombres = nombre;
        nuevoUsuario->Apellidos = apellido;
        nuevoUsuario->Correo = correo;
        nuevoUsuario->Contrasenia = contrasenia;
        nuevoUsuario->Edad = edad;
        nuevoUsuario->siguiente = null;

        

        if (inicio == null)
        {
            inicio = nuevoUsuario;
        }
        else
        {
            Usuario* usuarioActual = inicio;
            while (usuarioActual->siguiente != null)
            {
                usuarioActual = usuarioActual->siguiente;
            }
            usuarioActual->siguiente = nuevoUsuario;
        }
    }

    public Usuario buscarUsuario(int id)
    {
        Usuario* usuarioActual = inicio;
        while (usuarioActual != null)
        {
            if (usuarioActual->Id == id)
            {
                return *usuarioActual;
            }
            usuarioActual = usuarioActual->siguiente;
        }
        return new Usuario();
    }

    public void ActualizarUsuario(int id, string nombre, string apellido, string correo)
    {
        Usuario* usuarioActual = inicio;
        while (usuarioActual != null)
        {
            if (usuarioActual->Id == id)
            {
                if (nombre != "") 
                {
                    usuarioActual->Nombres = nombre;
                }
                if (apellido != "") 
                {
                    usuarioActual->Apellidos = apellido;
                }
                if (correo != "") 
                {
                    usuarioActual->Correo = correo;
                }
                return;
            }
            usuarioActual = usuarioActual->siguiente;
        }
    }

    public void EliminarUsuario(int id)
    {
        Usuario* usuarioActual = inicio;
        Usuario* usuarioAnterior = null;
        while (usuarioActual != null)
        {
            if (usuarioActual->Id == id)
            {
                if (usuarioAnterior == null)
                {
                    inicio = usuarioActual->siguiente;
                    NativeMemory.Free(usuarioActual);
                }
                else
                {
                    usuarioAnterior->siguiente = usuarioActual->siguiente;
                    NativeMemory.Free(usuarioActual);
                }
                return;
            }
            usuarioAnterior = usuarioActual;
            usuarioActual = usuarioActual->siguiente;
        }
    }

    public String GenerarDot()
    {
        String dot = "digraph G {\n";
        dot += "node [shape=record];\n";
        dot += "rankdir=LR;\n";
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

        Usuario* usuarioActual = inicio;
        while (usuarioActual != null)
        {
            string id = usuarioActual->Id.ToString();
            string nombres = usuarioActual->Nombres ?? "N/A";
            string apellidos = usuarioActual->Apellidos ?? "N/A";
            string correo = usuarioActual->Correo ?? "N/A";
            string edad = usuarioActual->Edad.ToString();

            dot += $"\"{id}\" [label=\"{{<f0> Id: {id} | <f1> Nombres: {nombres} | <f2> Apellidos: {apellidos} | <f3> Correo: {correo} | <f4> Edad: {edad} }}\"];\n";

            if (usuarioActual->siguiente != null)
            {
                dot += $"\"{id}\" -> \"{usuarioActual->siguiente->Id}\";\n";
            }

            usuarioActual = usuarioActual->siguiente;
        }
        dot += "}";
        return dot;
    }
}