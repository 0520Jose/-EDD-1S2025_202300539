using System;
using Gtk;

namespace AutoGestPro.Views
{
    unsafe class CrearServicio 
    {
        public CrearServicio(Window ventana)
        {
            Window ventanaServicio = new Window("Crear Servicio");
            ventanaServicio.SetDefaultSize(800, 600);
            ventanaServicio.SetPosition(WindowPosition.Center);
            ventanaServicio.DeleteEvent += delegate { Application.Quit(); };

            VBox contenedor = new VBox(false, 5);
            ventanaServicio.Add(contenedor);

            Label titulo = new Label("<span size='xx-large'>Crear Servicio</span>");
            titulo.UseMarkup = true;
            contenedor.PackStart(titulo, false, false, 20);

            Table table = new Table(3, 2, true);
            contenedor.PackStart(table, true, true, 10);

            Label id = new Label("ID:");
            table.Attach(id, 0, 1, 0, 1);

            Label id_repuesto = new Label("ID Repuesto:");
            table.Attach(id_repuesto, 0, 1, 1, 2);

            Label id_vehiculo = new Label("ID Vehículo:");
            table.Attach(id_vehiculo, 0, 1, 2, 3);

            Label detalles = new Label("Detalles:");
            table.Attach(detalles, 1, 2, 0, 1);

            Label costo = new Label("Costo:");
            table.Attach(costo, 1, 2, 1, 2);

            Entry idEntry = new Entry();
            table.Attach(idEntry, 1, 2, 0, 1);

            Entry idRepuestoEntry = new Entry();
            table.Attach(idRepuestoEntry, 1, 2, 1, 2);

            Entry idVehiculoEntry = new Entry();
            table.Attach(idVehiculoEntry, 1, 2, 2, 3);

            Entry detallesEntry = new Entry();
            table.Attach(detallesEntry, 1, 2, 0, 1);

            Entry costoEntry = new Entry();
            table.Attach(costoEntry, 1, 2, 1, 2);

            Button crearButton = new Button("Guardar");
            crearButton.Clicked += (sender, e) => {
                ventanaServicio.Destroy();
            };
            contenedor.PackStart(crearButton, false, false, 10);

            Button regresar = new Button("Regresar");
            regresar.Clicked += (sender, e) => {
                gestionRepuestos.Show();
                ventana.Destroy();
            };
            buttonContainer.PackStart(regresar, true, true, 0);
            
            ventanaServicio.ShowAll();
        }
    } 
}