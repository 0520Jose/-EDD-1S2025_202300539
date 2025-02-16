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
            ventana.SetDefaultSize(800,600);
            ventana.SetPosition(WindowPosition.Center);
            ventana.DeleteEvent += delegate { Application.Quit(); };

            VBox contenedor = new VBox(false, 5);
            ventana.Add(contenedor);

            Label titulo = new Label("Ingreso de vehiculo");
            contenedor.PackStart(titulo, false, false, 5);

            HBox subContenedor = new HBox(false, 5);
            contenedor.PackStart(subContenedor, false, false, 5);
    
            Table table = new Table(5, 2, false);
            table.WidthRequest = 800;
            subContenedor.PackStart(table, true, true, 5);

            Label id = new Label("ID:");
            table.Attach(id, 0, 1, 0, 1);

            Entry idEntry = new Entry();
            table.Attach(idEntry, 1, 2, 0, 1);

            Label IdUsuario = new Label("IdUsuario:");
            table.Attach(IdUsuario, 0, 1, 1, 2);

            Entry IdUsuarioEntry = new Entry();
            table.Attach(IdUsuarioEntry, 1, 2, 1, 2);

            Label marca = new Label("marca:");
            table.Attach(marca, 0, 1, 2, 3);

            Entry marcaEntry = new Entry();
            table.Attach(marcaEntry, 1, 2, 2, 3);

            Label modelo = new Label("modelo:");
            table.Attach(modelo, 0, 1, 3, 4);

            Entry modeloEntry = new Entry();
            table.Attach(modeloEntry, 1, 2, 3, 4);

            Label placa = new Label("Contraseña:");
            table.Attach(placa, 0, 1, 4, 5);

            Entry placaEntry = new Entry();
            table.Attach(placaEntry, 1, 2, 4, 5);

            Button guardar = new Button("Guardar");
            guardar.Clicked += (sender, e) => {
                int IdUsuario = int.Parse(IdUsuarioEntry.Text);
                string marca = marcaEntry.Text;
                string modelo = modeloEntry.Text;
                string placa = placaEntry.Text;
                int id = int.Parse(idEntry.Text);
                ListasGlobales.listaVehiculos.Insertar(id, IdUsuario, marca, modelo, placa);
                    MessageDialog dialog = new MessageDialog(ventana, 
                    DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, 
                    "Carga masiva exitosa de vehiculos");
                dialog.Run();
                dialog.Destroy();
            };
            HBox buttonContainer = new HBox(false, 5);
            buttonContainer.PackStart(guardar, false, false, 4);
            contenedor.PackStart(buttonContainer, false, false, 5);

            Button regresar = new Button("Regresar");
            regresar.Clicked += (sender, e) => {
                ingresoIndividual.Show();
                ventana.Destroy();
            };
            contenedor.PackStart(regresar, false, false, 5);

            ventana.ShowAll();
        }
    }
}