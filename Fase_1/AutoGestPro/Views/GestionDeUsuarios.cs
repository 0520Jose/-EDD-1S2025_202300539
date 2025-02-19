using System;
using Gtk;

namespace AutoGestPro.Views
{
    class GestionDeUsuarios
    {
        public GestionDeUsuarios(Window menu)
        {
            Window ventana = new Window("Gestion de usuarios - Root");
            ventana.SetDefaultSize(800, 600);
            ventana.SetPosition(WindowPosition.Center);
            ventana.DeleteEvent += delegate { Application.Quit(); };

            VBox contenedor = new VBox(false, 10);
            ventana.Add(contenedor);

            Label titulo = new Label("Gestion de usuarios");
            titulo.ModifyFont(Pango.FontDescription.FromString("Sans Bold 24"));
            contenedor.PackStart(titulo, false, false, 20);

            Table table = new Table(2, 2, true);
            contenedor.PackStart(table, true, true, 10);

            Button EditarUsuario = new Button("Editar Usuario");
            EditarUsuario.Clicked += (sender, e) => {
                new EditarUsuario(ventana);
                ventana.Hide();
            };
            table.Attach(EditarUsuario, 0, 1, 0, 1);

            Button VerUsuario = new Button("Ver Usuario");
            VerUsuario.Clicked += (sender, e) => {
                new VerUsuario(ventana);
                ventana.Hide();
            };
            table.Attach(VerUsuario, 1, 2, 0, 1);

            Button EliminarUsuario = new Button("Eliminar Usuario");
            EliminarUsuario.Clicked += (sender, e) => {
                new EliminarUsuario(ventana);
                ventana.Hide();
            };
            table.Attach(EliminarUsuario, 0, 1, 1, 2);

            Button regresar = new Button("Regresar");
            regresar.Clicked += (sender, e) => {
                menu.Show();
                ventana.Destroy();
            };
            table.Attach(regresar, 1, 2, 1, 2);

            ventana.ShowAll();
        }
    }
}