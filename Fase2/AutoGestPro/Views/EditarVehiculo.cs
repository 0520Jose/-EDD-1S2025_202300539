using System;
using AutoGestPro.Models;
using AutoGestPro.Models.Listas;
using Gtk;

namespace AutoGestPro.Views
{
    class EditarVehiculo
    {
        public EditarVehiculo(Window gestionVehiculos)
        {
            Window ventana = new Window("Gestion de vehículos - Root");
            ventana.SetDefaultSize(600, 400);
            ventana.SetPosition(WindowPosition.Center);
            ventana.DeleteEvent += delegate { Application.Quit(); };

            VBox contenedor = new VBox(false, 10);
            ventana.Add(contenedor);

            Label titulo = new Label("<b>Editor de vehículo</b>");
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

            Label id_Usuario = new Label("ID Usuario:");
            table.Attach(id_Usuario, 0, 1, 1, 2);

            Label id_UsuarioActual = new Label("Null");
            table.Attach(id_UsuarioActual, 1, 2, 1, 2);

            Button buscar = new Button("Buscar");
            table.Attach(buscar, 2, 3, 0, 1);

            Label marca = new Label("Marca:");
            table.Attach(marca, 0, 1, 2, 3);

            Label marcaActual = new Label("Null");
            table.Attach(marcaActual, 1, 2, 2, 3);

            Label modelo = new Label("Modelo:");
            table.Attach(modelo, 0, 1, 3, 4);

            Label modeloActual = new Label("Null");
            table.Attach(modeloActual, 1, 2, 3, 4);

            Label placa = new Label("Placa:");
            table.Attach(placa, 0, 1, 4, 5);

            Label placaActual = new Label("Null");
            table.Attach(placaActual, 1, 2, 4, 5);

            buscar.Clicked += (sender, e) => {
                try {
                    int id = int.Parse(idEntry.Text);
                    Vehiculo vehiculo = ListasGlobales.listaVehiculos.buscarVehiculo(id);
                    if (vehiculo.Marca != null)
                    {
                        marcaActual.Text = vehiculo.Marca;
                        modeloActual.Text = vehiculo.Modelo;
                        placaActual.Text = vehiculo.Placa.ToString();
                        id_UsuarioActual.Text = vehiculo.Id_Usuario.ToString();
                    }
                    else
                    {
                        marcaActual.Text = "Vehículo no encontrado";
                        modeloActual.Text = "Vehículo no encontrado";
                        placaActual.Text = "Vehículo no encontrado";
                        id_UsuarioActual.Text = "Vehículo no encontrado";
                    }
                } catch (FormatException)
                {
                    MessageDialog dialog = new MessageDialog(ventana, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "El ID debe ser un número");
                    dialog.Run();
                    dialog.Destroy();
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
                    if (ListasGlobales.listaVehiculos.buscarVehiculo(int.Parse(idEntry.Text)).Marca == null)
                    {
                        MessageDialog dialog = new MessageDialog(ventana, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "El vehículo no existe");
                        dialog.Run();
                        dialog.Destroy();
                        return;
                    }
                    else
                    {
                        ListasGlobales.listaVehiculos.eliminarVehiculo(int.Parse(idEntry.Text));
                        MessageDialog dialog = new MessageDialog(ventana, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "Vehículo eliminado");
                        dialog.Run();
                        dialog.Destroy();
                        idEntry.Text = "";

                    }
                }
                marcaActual.Text = "Vehículo no encontrado";
                modeloActual.Text = "Vehículo no encontrado";
                placaActual.Text = "Vehículo no encontrado";
                id_UsuarioActual.Text = "Vehículo no encontrado";
            };
            
            buttonContainer.PackStart(eliminar, true, true, 0);

            Button regresar = new Button("Regresar");
            regresar.Clicked += (sender, e) => {
                gestionVehiculos.Show();
                ventana.Destroy();
            };
            contenedor.PackStart(regresar, false, false, 10);

            ventana.ShowAll();
        }
    }
}
