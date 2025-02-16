using System;
using Gtk;

namespace AutoGestPro.Views
{
    class GestionDeUsuarios
    {
        public GestionDeUsuarios(Window menu)
        {
            Window ventana = new Window("Gestion de usuarios - Root");
            ventana.SetDefaultSize(800,600);
            ventana.SetPosition(WindowPosition.Center);
            ventana.DeleteEvent += delegate { Application.Quit(); };

            VBox contenedor = new VBox(false, 5);
            ventana.Add(contenedor);

            Label titulo = new Label("Gestion de usuarios");
            contenedor.PackStart(titulo, false, false, 5);

            Button EditarUsuario = new Button("Editar Usuario");
            EditarUsuario.Clicked += (sender, e) => {
                new EditarUsuario(ventana);
                ventana.Hide();
            };
            contenedor.PackStart(EditarUsuario, false, false, 5);

            Button VerUsuario = new Button("Ver Usuario");
            VerUsuario.Clicked += (sender, e) => {
                new verUsuario(ventana);
                ventana.Hide();
            };
            contenedor.PackStart(VerUsuario, false, false, 5);

            Button EliminarUsuario = new Button("Eliminar Usuario");
            EliminarUsuario.Clicked += (sender, e) => {
                new EliminarUsuario(ventana);
                ventana.Hide();
            };
            contenedor.PackStart(EliminarUsuario, false, false, 5);

            Button regresar = new Button("Regresar");
            regresar.Clicked += (sender, e) => {
                menu.Show();
                ventana.Destroy();
            };
            contenedor.PackStart(regresar, false, false, 5);

            ventana.ShowAll();
        }
    }
}