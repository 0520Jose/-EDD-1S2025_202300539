using System;
using System.Runtime.InteropServices;
using AutoGestPro.Models;
unsafe class ListaSimple
{
    private Usuario* inicio = null;

    public void Insertar(int id, string nombre, string apellido, string correo, string contrasenia)
    {
        Usuario* nuevoUsuario = (Usuario*)NativeMemory.Alloc((nuint)sizeof(Usuario));
        nuevoUsuario->Id = id;
        nuevoUsuario->Nombre = nombre;
        nuevoUsuario->Apellido = apellido;
        nuevoUsuario->Correo = correo;
        nuevoUsuario->Contrasenia = contrasenia;
        nuevoUsuario->siguiente = inicio;
        inicio = nuevoUsuario;
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
                    usuarioActual->Nombre = nombre;
                }
                if (apellido != "") 
                {
                    usuarioActual->Apellido = apellido;
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
                }
                else
                {
                    usuarioAnterior->siguiente = usuarioActual->siguiente;
                }
                return;
            }
            usuarioAnterior = usuarioActual;
            usuarioActual = usuarioActual->siguiente;
        }
    }
}