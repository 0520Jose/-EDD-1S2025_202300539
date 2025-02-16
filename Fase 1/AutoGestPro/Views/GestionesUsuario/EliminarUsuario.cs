using System;
using AutoGestPro.Models;
using AutoGestPro.Models.Listas;
using Gtk;

namespace AutoGestPro.Views
{
    class EliminarUsuario
    {
        public EliminarUsuario(Window gestionUsuarios)
        {
            Window ventana = new Window("Gestion de usuarios - Root");
            ventana.SetDefaultSize(800,600);
            ventana.SetPosition(WindowPosition.Center);
            ventana.DeleteEvent += delegate { Application.Quit(); };

            VBox contenedor = new VBox(false, 5);
            ventana.Add(contenedor);

            Label titulo = new Label("Eliminar usuario");
            contenedor.PackStart(titulo, false, false, 5);

            HBox subContenedor = new HBox(false, 5);
            contenedor.PackStart(subContenedor, false, false, 5);

            Table table = new Table(5, 3, false);
            table.WidthRequest = 800;
            subContenedor.PackStart(table, true, true, 5);

            Label id = new Label("ID:");
            table.Attach(id, 0, 1, 0, 1);

            Entry idEntry = new Entry();
            table.Attach(idEntry, 1, 2, 0, 1);

            Button eliminar = new Button("Eliminar");
            table.Attach(eliminar, 2, 3, 0, 1);
            eliminar.Clicked += (sender, e) => {
                ListasGlobales.listaUsuarios.EliminarUsuario(int.Parse(idEntry.Text));
            };

            Button regresar = new Button("Regresar");
            regresar.Clicked += (sender, e) => {
                gestionUsuarios.Show();
                ventana.Destroy();
            };
            contenedor.PackStart(regresar, false, false, 5);

            ventana.ShowAll();
        }
    }
}