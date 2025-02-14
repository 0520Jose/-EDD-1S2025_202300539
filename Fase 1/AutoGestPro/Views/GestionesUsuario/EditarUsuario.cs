using System;
using AutoGestPro.Models;
using AutoGestPro.Models.Listas;
using Gtk;

namespace AutoGestPro.Views
{
    class EditarUsuario
    {
        public EditarUsuario()
        {
            Window ventana = new Window("Gestion de usuarios - Root");
            ventana.SetDefaultSize(800,600);
            ventana.DeleteEvent += delegate { Application.Quit(); };

            VBox contenedor = new VBox(false, 5);
            ventana.Add(contenedor);

            Label titulo = new Label("Editor de usuario");
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

            Button buscar = new Button("Buscar");
            
            table.Attach(buscar, 2, 3, 0, 1);

            Label nombre = new Label("Nombres:");
            table.Attach(nombre, 0, 1, 1, 2);

            Label nombreActual = new Label("Null");
            table.Attach(nombreActual, 1, 2, 1, 2);

            Entry nombreEntry = new Entry();
            table.Attach(nombreEntry, 2, 3, 1, 2);

            Label apellido = new Label("Apellidos:");
            table.Attach(apellido, 0, 1, 2, 3);

            Label apellidoActual = new Label("Null");
            table.Attach(apellidoActual, 1, 2, 2, 3);

            Entry apellidoEntry = new Entry();
            table.Attach(apellidoEntry, 2, 3, 2, 3);

            Label correo = new Label("Correo:");
            table.Attach(correo, 0, 1, 3, 4);

            Label correoActual = new Label("Null");
            table.Attach(correoActual, 1, 2, 3, 4);

            Entry correoEntry = new Entry();
            table.Attach(correoEntry, 2, 3, 3, 4);

            buscar.Clicked += (sender, e) => {
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

            Button actualizar = new Button("Actualizar");
            actualizar.Clicked += (sender, e) => {
                int id = int.Parse(idEntry.Text);
                string nombre_ = nombreEntry.Text;
                string apellido_ = apellidoEntry.Text;
                string correo_ = correoEntry.Text;
                if (nombre_ != "")
                {
                    nombreActual.Text = nombre_;
                }
                if (apellido_ != "")
                {
                    apellidoActual.Text = apellido_;
                }
                if (correo_ != "")
                {
                    correoActual.Text = correo_;
                }
                nombreEntry.Text = "";
                apellidoEntry.Text = "";
                correoEntry.Text = "";
                ListasGlobales.listaUsuarios.ActualizarUsuario(id, nombre_, apellido_, correo_);
            };
            HBox buttonContainer = new HBox(false, 5);
            buttonContainer.PackStart(actualizar, false, false, 4);
            contenedor.PackStart(buttonContainer, false, false, 5);

            ventana.ShowAll();
        }
    }
}