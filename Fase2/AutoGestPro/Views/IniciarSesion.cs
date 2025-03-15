using System;
using Gtk;
using AutoGestPro.Views;
using AutoGestPro.Models.Listas;
using AutoGestPro.Models;

namespace AutoGestPro.Views
{
    unsafe class IniciarSesion
    {
        public IniciarSesion()
        {
            Application.Init();
            Window ventana = new Window("AutoGestPro");
            ventana.Opacity = 0.75;
            ventana.SetDefaultSize(400, 300);
            ventana.DeleteEvent += delegate { Application.Quit(); };

            VBox contenedor = new VBox(false, 10);
            contenedor.BorderWidth = 20;
            ventana.Add(contenedor);

            Label titulo = new Label("<span size='xx-large' weight='bold'>Iniciar Sesión</span>");
            titulo.UseMarkup = true;
            contenedor.PackStart(titulo, false, false, 10);

            HBox usuarioBox = new HBox(false, 5);
            Label tituloUsuario = new Label("Usuario:");
            Entry txtUsuario = new Entry();
            usuarioBox.PackStart(tituloUsuario, false, false, 5);
            usuarioBox.PackStart(txtUsuario, true, true, 5);
            contenedor.PackStart(usuarioBox, false, false, 5);

            HBox contrasenaBox = new HBox(false, 5);
            Label tituloContrasena = new Label("Contraseña:");
            Entry txtContrasena = new Entry();
            txtContrasena.Visibility = false;
            contrasenaBox.PackStart(tituloContrasena, false, false, 5);
            contrasenaBox.PackStart(txtContrasena, true, true, 5);
            contenedor.PackStart(contrasenaBox, false, false, 5);

            Button iniciarSesion = new Button("Iniciar Sesión");
            iniciarSesion.Clicked += (sender, e) => {
                ListaSimple listaUsuarios = ListasGlobales.listaUsuarios;
                bool usuarioEncontrado = false;
                Usuario* usuario = listaUsuarios.inicio;
                while (usuario != null)
                {
                    if (usuario->Correo == txtUsuario.Text && usuario->Contrasenia == txtContrasena.Text)
                    {
                        usuarioEncontrado = true;
                        break;
                    }
                    usuario = usuario->siguiente;
                }
                if (usuarioEncontrado)
                {
                    txtUsuario.Text = "";
                    txtContrasena.Text = "";
                    MessageDialog mensaje = new MessageDialog(ventana, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "Bienvenido");
                    mensaje.Run();
                    //Menu menu = new Menu(ventana);
                    ventana.Hide();
                    mensaje.Destroy();
                }
                else if (txtUsuario.Text == "admin@usac.com" && txtContrasena.Text == "admint123")
                {
                    txtUsuario.Text = "";
                    txtContrasena.Text = "";
                    MessageDialog mensaje = new MessageDialog(ventana, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "Bienvenido");
                    mensaje.Run();
                    Menu menu = new Menu(ventana);
                    ventana.Hide();
                    mensaje.Destroy();
                }
                else
                {
                    MessageDialog mensaje = new MessageDialog(ventana, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "Usuario o contraseña incorrectos");
                    mensaje.Run();
                    mensaje.Destroy();
                    txtUsuario.Text = "";
                    txtContrasena.Text = "";
                }
            };
            contenedor.PackStart(iniciarSesion, false, false, 20);

            ventana.ShowAll();
            Application.Run();
        }
    }
}