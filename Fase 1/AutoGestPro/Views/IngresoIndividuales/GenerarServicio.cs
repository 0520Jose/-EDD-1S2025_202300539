using System;
using Gtk;

namespace AutoGestPro.Views
{
    class GenerarServicio
    {
        public GenerarServicio()
        {
            Window ventana = new Window("Crear Servicio - Root");
            ventana.SetDefaultSize(800,600);
            ventana.DeleteEvent += delegate { Application.Quit(); };

            VBox contenedor = new VBox(false, 5);
            ventana.Add(contenedor);

            Label titulo = new Label("Generar Servicio");
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

            Label idRepuesto = new Label("ID Repuesto:");
            table.Attach(idRepuesto, 0, 1, 1, 2);

            Entry idRepuestoEntry = new Entry();
            table.Attach(idRepuestoEntry, 1, 2, 1, 2);

            Label idVehiculo = new Label("ID Vehiculo:");
            table.Attach(idVehiculo, 0, 1, 2, 3);

            Entry idVehiculoEntry = new Entry();
            table.Attach(idVehiculoEntry, 1, 2, 2, 3);

            Label detalles = new Label("Detalles:");
            table.Attach(detalles, 0, 1, 3, 4);

            Entry detallesEntry = new Entry();
            table.Attach(detallesEntry, 1, 2, 3, 4);

            Label costo = new Label("Costo:");
            table.Attach(costo, 0, 1, 4, 5);

            Entry costoEntry = new Entry();
            table.Attach(costoEntry, 1, 2, 4, 5);

            Button guardar = new Button("Guardar");
            guardar.Clicked += (sender, e) => {
                Console.WriteLine("Guardado");
            };
            HBox buttonContainer = new HBox(false, 5);
            buttonContainer.PackStart(guardar, false, false, 4);
            contenedor.PackStart(buttonContainer, false, false, 5);

            ventana.ShowAll();
        }
    }
}