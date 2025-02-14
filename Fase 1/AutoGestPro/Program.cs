using System;
using System.Windows.Forms;
using Gtk;
class Program
{
    static void Main()
    {
        Application.Init();
        Window ventana = new Window("AutoGestPro");
        ventana.SetDefaultSize(800, 600);
        ventana.DeleteEvent += (o, args) => Application.Quit();

        Label titulo = new Label("AutoGestPro");
        ventana.Add(titulo);

        Label iniciarSesion = new Label("Iniciar sesión");
        ventana.Add(iniciarSesion);

        TextBox inputUsuario = new TextBox();
        inputUsuario.PlaceholderText = "Usuario";
        ventana.Controls.Add(inputUsuario);

        TextBox inputContrasena = new TextBox();
        inputContrasena.UseSystemPasswordChar = true;
        ventana.Controls.Add(inputContrasena);

        ventana.ShowAll();
        Application.Run();
    }
}
