using System;
using AutoGestPro.Views;
using Gtk;

class Program
{
    static void Main()
    {
        Application.Init();
        IniciarSesion login = new IniciarSesion();
        Application.Run();
    }
}
