using System;
using AutoGestPro.Models;
using AutoGestPro.Models.Listas;
using Gtk;

namespace AutoGestPro.Views
{
    class EditarUsuario
    {
        public EditarUsuario(Window gestionUsuarios)
        {
            Window ventana = new Window("Gestion de usuarios - Root");
            ventana.SetDefaultSize(600, 400);
            ventana.SetPosition(WindowPosition.Center);
            ventana.DeleteEvent += delegate { Application.Quit(); };

            VBox contenedor = new VBox(false, 10);
            ventana.Add(contenedor);

            Label titulo = new Label("<b>Editor de usuario</b>");
            titulo.UseMarkup = true;
            titulo.Justify = Justification.Center;
            contenedor.PackStart(titulo, false, false, 10);

            Table table = new Table(4, 3, false);
            table.ColumnSpacing = 10;
            table.RowSpacing = 10;
            contenedor.PackStart(table, true, true, 10);

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

            Label apellido = new Label("Apellidos:");
            table.Attach(apellido, 0, 1, 2, 3);

            Label apellidoActual = new Label("Null");
            table.Attach(apellidoActual, 1, 2, 2, 3);

            Label correo = new Label("Correo:");
            table.Attach(correo, 0, 1, 3, 4);

            Label correoActual = new Label("Null");
            table.Attach(correoActual, 1, 2, 3, 4);

            buscar.Clicked += (sender, e) => {
                int id = int.Parse(idEntry.Text);
                Usuario usuario = ListasGlobales.listaUsuarios.buscarUsuario(id);
                if (usuario.Nombres != null)
                {
                    nombreActual.Text = usuario.Nombres;
                    apellidoActual.Text = usuario.Apellidos;
                    correoActual.Text = usuario.Correo;
                }
                else
                {
                    nombreActual.Text = "Usuario no encontrado";
                    apellidoActual.Text = "Usuario no encontrado";
                    correoActual.Text = "Usuario no encontrado";
                }
            };

            HBox buttonContainer = new HBox(true, 10);
            contenedor.PackStart(buttonContainer, false, false, 10);

            Button eliminar = new Button("Eliminar");
            eliminar.Clicked += (sender, e) => {
                if (idEntry.Text == "")
                {
                    MessageDialog dialog = new MessageDialog(ventana, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "Debe ingresar un ID");
                    dialog.Run();
                    dialog.Destroy();
                    return;
                }
                else 
                {
                    if (ListasGlobales.listaUsuarios.buscarUsuario(int.Parse(idEntry.Text)).Nombres == null)
                    {
                        MessageDialog dialog = new MessageDialog(ventana, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "El usuario no existe");
                        dialog.Run();
                        dialog.Destroy();
                        return;
                    }
                    else
                    {
                        ListasGlobales.listaUsuarios.EliminarUsuario(int.Parse(idEntry.Text));
                        MessageDialog dialog = new MessageDialog(ventana, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "Usuario eliminado");
                        dialog.Run();
                        dialog.Destroy();
                        idEntry.Text = "";

                    }
                }
                nombreActual.Text = "Usuario no encontrado";
                apellidoActual.Text = "Usuario no encontrado";
                correoActual.Text = "Usuario no encontrado";
            };
            
            buttonContainer.PackStart(eliminar, true, true, 0);

            Button regresar = new Button("Regresar");
            regresar.Clicked += (sender, e) => {
                gestionUsuarios.Show();
                ventana.Destroy();
            };
            contenedor.PackStart(regresar, false, false, 10);

            ventana.ShowAll();
        }
    }
}