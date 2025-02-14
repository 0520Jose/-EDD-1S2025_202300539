using System;
using Gtk;
using AutoGestPro.Views;

namespace AutoGestPro.Views
{
    class IniciarSesion
    {
        public IniciarSesion()
        {
            Window ventana = new Window("AutoGestPro");
            ventana.SetDefaultSize(400,300);
            ventana.DeleteEvent += delegate { Application.Quit(); };

            VBox contenedor = new VBox(false, 5);
            ventana.Add(contenedor);

            Label titulo = new Label("Iniciar Sesion");
            contenedor.PackStart(titulo, false, false, 5);

            Label tituloUsuario = new Label ("Usuario");
            Entry txtUsuario = new Entry();
            contenedor.PackStart(tituloUsuario, false, false, 5);
            contenedor.PackStart(txtUsuario, false, false, 5);

            Label tituloContrasena = new Label ("Contraseña");
            Entry txtContrasena = new Entry();
            contenedor.PackStart(tituloContrasena, false, false, 5);
            contenedor.PackStart(txtContrasena, false, false, 5);

            Button iniciarSesion = new Button("Iniciar Sesión");
            iniciarSesion.Clicked += (sender, e) => {
                if (txtUsuario.Text == "admin@usac.com" && txtContrasena.Text == "root"){
                    Menu menu = new Menu();
                } else {
                    Console.WriteLine("Usuario no encontrado");
                }
            };
            contenedor.PackStart(iniciarSesion, false, false, 5);

            ventana.ShowAll();
        }
    }
}