using System;
using AutoGestPro.Models;
using AutoGestPro.Models.Listas;
using Gtk;

namespace AutoGestPro.Views
{
    class VerUsuario
    {
        public VerUsuario(Window gestionUsuarios)
        {
            Window ventana = new Window("Gestión de usuarios - Root");
            ventana.SetDefaultSize(600, 400);
            ventana.SetPosition(WindowPosition.Center);
            ventana.DeleteEvent += delegate { Application.Quit(); };

            VBox contenedor = new VBox(false, 10);
            ventana.Add(contenedor);

            Label titulo = new Label("<b>Ver Usuario</b>");
            titulo.UseMarkup = true;
            titulo.Justify = Justification.Center;
            contenedor.PackStart(titulo, false, false, 10);

            HBox idBox = new HBox(false, 5);
            contenedor.PackStart(idBox, false, false, 5);

            Label idLabel = new Label("ID:");
            idBox.PackStart(idLabel, false, false, 5);

            Entry idEntry = new Entry();
            idBox.PackStart(idEntry, true, true, 5);

            Button buscarButton = new Button("Buscar");
            idBox.PackStart(buscarButton, false, false, 5);

            Table table = new Table(3, 2, false);
            table.RowSpacing = 10;
            table.ColumnSpacing = 10;
            contenedor.PackStart(table, true, true, 10);

            Label nombreLabel = new Label("Nombres:");
            table.Attach(nombreLabel, 0, 1, 0, 1);

            Label nombreActual = new Label("Null");
            table.Attach(nombreActual, 1, 2, 0, 1);

            Label apellidoLabel = new Label("Apellidos:");
            table.Attach(apellidoLabel, 0, 1, 1, 2);

            Label apellidoActual = new Label("Null");
            table.Attach(apellidoActual, 1, 2, 1, 2);

            Label correoLabel = new Label("Correo:");
            table.Attach(correoLabel, 0, 1, 2, 3);

            Label correoActual = new Label("Null");
            table.Attach(correoActual, 1, 2, 2, 3);

            buscarButton.Clicked += (sender, e) => {
                int id = int.Parse(idEntry.Text);
                Usuario usuario = ListasGlobales.listaUsuarios.buscarUsuario(id);
                if (usuario.Nombre != null)
                {
                    nombreActual.Text = usuario.Nombre;
                    apellidoActual.Text = usuario.Apellido;
                    correoActual.Text = usuario.Correo;
                }
                else
                {
                    nombreActual.Text = "Usuario no encontrado";
                    apellidoActual.Text = "Usuario no encontrado";
                    correoActual.Text = "Usuario no encontrado";
                }
            };

            HBox buttonBox = new HBox(true, 10);
            contenedor.PackStart(buttonBox, false, false, 10);

            Button regresarButton = new Button("Regresar");
            regresarButton.Clicked += (sender, e) => {
                gestionUsuarios.Show();
                ventana.Destroy();
            };
            buttonBox.PackStart(regresarButton, true, true, 10);

            ventana.ShowAll();
        }
    }
}