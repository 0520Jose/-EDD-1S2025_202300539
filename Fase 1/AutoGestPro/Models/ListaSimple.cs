using System;
using System.Runtime.InteropServices;
using AutoGestPro.Models;
unsafe class ListaSimple
{
    private Usuario* inicio = null;

    public void Insertar(string id, string nombre, string apellido, string correo, string contrasenia)
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

    public Usuario buscarUsuario(string id)
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

    public void ActualizarUsuario(string id, string nombre, string apellido, string correo)
    {
        Usuario* usuarioActual = inicio;
        while (usuarioActual != null)
        {
            if (usuarioActual->Id == id)
            {
                usuarioActual->Nombre = nombre;
                usuarioActual->Apellido = apellido;
                usuarioActual->Correo = correo;
                return;
            }
            usuarioActual = usuarioActual->siguiente;
        }
    }
}