using System;
using Gtk;

namespace AutoGestPro.Views
{
    class RegistrarVehiculo
    {
        public RegistrarVehiculo(Window cerrarSesion)
        {
            Window ventana = new Window("Registrar Vehículo");
            ventana.SetDefaultSize(800, 600);
            ventana.SetPosition(WindowPosition.Center);
            ventana.DeleteEvent += delegate { Application.Quit(); };

            VBox contenedor = new VBox(false, 5);
            ventana.Add(contenedor);

            Label titulo = new Label("<span size='xx-large'>Registrar Vehículo</span>");
            titulo.UseMarkup = true;
            contenedor.PackStart(titulo, false, false, 20);

            Label id = new Label("ID:");
            contenedor.PackStart(id, false, false, 5);
            Entry idEntry = new Entry();
            contenedor.PackStart(idEntry, false, false, 5);
            Label marca = new Label("Marca:");
            contenedor.PackStart(marca, false, false, 5);
            Entry marcaEntry = new Entry();
            contenedor.PackStart(marcaEntry, false, false, 5);
            Label modelo = new Label("Modelo:");
            contenedor.PackStart(modelo, false, false, 5);
            Entry modeloEntry = new Entry();
            contenedor.PackStart(modeloEntry, false, false, 5);
            Label placa = new Label("Placa:");
            contenedor.PackStart(placa, false, false, 5);
            Entry placaEntry = new Entry();
            contenedor.PackStart(placaEntry, false, false, 5);

            Button guardar = new Button("Guardar");
            guardar.Clicked += (sender, e) => {
                Console.WriteLine($"Guardando vehículo: ID={idEntry.Text}, Marca={marcaEntry.Text}, Modelo={modeloEntry.Text}, Placa={placaEntry.Text}");
            };
            contenedor.PackStart(guardar, false, false, 10);

            Button volver = new Button("Volver al menú");
            volver.Clicked += (sender, e) => {
                Menu menu = new Menu(cerrarSesion);
                ventana.Hide();
            };
            contenedor.PackStart(volver, false, false, 10);

            ventana.ShowAll();
        }
    }
}