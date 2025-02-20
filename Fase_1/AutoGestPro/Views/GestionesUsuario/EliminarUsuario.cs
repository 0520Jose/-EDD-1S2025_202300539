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
                if (idEntry.Text == "")
                {
                    MessageDialog dialog = new MessageDialog(ventana, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "Debe ingresar un ID");
                    dialog.Run();
                    dialog.Destroy();
                    return;
                }
                else 
                {
                    if (ListasGlobales.listaUsuarios.buscarUsuario(int.Parse(idEntry.Text)).Nombre == null)
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