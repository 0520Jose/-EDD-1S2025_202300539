using System;
using AutoGestPro.Models;
using AutoGestPro.Models.Listas;
using Gtk;

namespace AutoGestPro.Views
{
    class IngresoVehiculos
    {
        public IngresoVehiculos(Window ingresoIndividual)
        {
            Window ventana = new Window("Ingreso individual - Root");
            ventana.SetDefaultSize(400, 300);
            ventana.SetPosition(WindowPosition.Center);
            ventana.DeleteEvent += delegate { Application.Quit(); };

            VBox contenedor = new VBox(false, 10);
            ventana.Add(contenedor);

            Label titulo = new Label("<b>Ingreso de Vehículo</b>");
            titulo.UseMarkup = true;
            titulo.Justify = Justification.Center;
            contenedor.PackStart(titulo, false, false, 10);

            Table table = new Table(5, 2, false);
            table.ColumnSpacing = 10;
            table.RowSpacing = 10;
            contenedor.PackStart(table, true, true, 10);

            Label id = new Label("ID:");
            table.Attach(id, 0, 1, 0, 1);

            Entry idEntry = new Entry();
            table.Attach(idEntry, 1, 2, 0, 1);

            Label IdUsuario = new Label("IdUsuario:");
            table.Attach(IdUsuario, 0, 1, 1, 2);

            Entry IdUsuarioEntry = new Entry();
            table.Attach(IdUsuarioEntry, 1, 2, 1, 2);

            Label marca = new Label("Marca:");
            table.Attach(marca, 0, 1, 2, 3);

            Entry marcaEntry = new Entry();
            table.Attach(marcaEntry, 1, 2, 2, 3);

            Label modelo = new Label("Modelo:");
            table.Attach(modelo, 0, 1, 3, 4);

            Entry modeloEntry = new Entry();
            table.Attach(modeloEntry, 1, 2, 3, 4);

            Label placa = new Label("Placa:");
            table.Attach(placa, 0, 1, 4, 5);

            Entry placaEntry = new Entry();
            table.Attach(placaEntry, 1, 2, 4, 5);

            HButtonBox buttonBox = new HButtonBox();
            buttonBox.Layout = ButtonBoxStyle.End;
            buttonBox.Spacing = 10;
            contenedor.PackStart(buttonBox, false, false, 10);

            Button guardar = new Button("Guardar");
            guardar.Clicked += (sender, e) => {
                string marca = marcaEntry.Text;
                string modelo = modeloEntry.Text;
                string placa = placaEntry.Text;               
                if (IdUsuarioEntry.Text == "" || marca == "" || modelo == "" || placa == "" || idEntry.Text == "")
                {
                    MessageDialog dialogError = new MessageDialog(ventana, 
                        DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, 
                        "Por favor, llene todos los campos");
                    dialogError.Run();
                    dialogError.Destroy();
                    return;
                }
                int id = 0;
                int IdUsuario = 0;
                try
                {
                    id = int.Parse(idEntry.Text);
                    IdUsuario = int.Parse(IdUsuarioEntry.Text);
                }
                catch (FormatException)
                {
                    MessageDialog dialogError = new MessageDialog(ventana, 
                        DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, 
                        "Por favor, ingrese un número en los campos ID y IdUsuario");
                    dialogError.Run();
                    dialogError.Destroy();
                    return;
                }
                if (id == null || IdUsuario == null || marca == null || modelo == null || placa == null)
                {
                    MessageDialog dialogError = new MessageDialog(ventana, 
                        DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, 
                        "Por favor, llene todos los campos");
                    dialogError.Run();
                    dialogError.Destroy();
                    return;
                }
                if (ListasGlobales.listaVehiculos.buscarVehiculo(id).Id == id)
                {
                    MessageDialog dialogError = new MessageDialog(ventana, 
                        DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, 
                        "El vehículo ya existe");
                    dialogError.Run();
                    dialogError.Destroy();
                    return;
                }
                idEntry.Text = "";
                IdUsuarioEntry.Text = "";
                marcaEntry.Text = "";
                modeloEntry.Text = "";
                placaEntry.Text = "";
                ListasGlobales.listaVehiculos.Insertar(id, IdUsuario, marca, modelo, placa);
                MessageDialog dialog = new MessageDialog(ventana, 
                    DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, 
                    "Vehículo guardado exitosamente");
                dialog.Run();
                dialog.Destroy();
            };
            buttonBox.PackStart(guardar, false, false, 0);

            Button regresar = new Button("Regresar");
            regresar.Clicked += (sender, e) => {
                ingresoIndividual.Show();
                ventana.Destroy();
            };
            buttonBox.PackStart(regresar, false, false, 0);

            ventana.ShowAll();
        }
    }
}