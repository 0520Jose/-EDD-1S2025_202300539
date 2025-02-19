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
            ventana.SetDefaultSize(400, 200);
            ventana.SetPosition(WindowPosition.Center);
            ventana.DeleteEvent += delegate { Application.Quit(); };

            VBox contenedor = new VBox(false, 10);
            ventana.Add(contenedor);

            Label titulo = new Label("<b><big>Eliminar usuario</big></b>");
            titulo.UseMarkup = true;
            contenedor.PackStart(titulo, false, false, 10);

            HBox idContenedor = new HBox(false, 10);
            contenedor.PackStart(idContenedor, false, false, 10);

            Label idLabel = new Label("ID:");
            idContenedor.PackStart(idLabel, false, false, 10);

            Entry idEntry = new Entry();
            idContenedor.PackStart(idEntry, true, true, 10);

            HBox botonesContenedor = new HBox(true, 10);
            contenedor.PackStart(botonesContenedor, false, false, 10);

            Button eliminar = new Button("Eliminar");
            eliminar.Clicked += (sender, e) => {
                ListasGlobales.listaUsuarios.EliminarUsuario(int.Parse(idEntry.Text));
            };
            botonesContenedor.PackStart(eliminar, true, true, 10);

            Button regresar = new Button("Regresar");
            regresar.Clicked += (sender, e) => {
                gestionUsuarios.Show();
                ventana.Destroy();
            };
            botonesContenedor.PackStart(regresar, true, true, 10);

            ventana.ShowAll();
        }
    }
}