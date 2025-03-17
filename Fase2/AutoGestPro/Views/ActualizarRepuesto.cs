using System;
using Gtk;

namespace AutoGestPro.Views
{
    class ActualizarRepuesto
    {
        public ActualizarRepuesto(Window gestionRepuestos)
        {
            Window ventana = new Window("Gestion de repuestos - Root");
            ventana.SetDefaultSize(600, 400);
            ventana.SetPosition(WindowPosition.Center);
            ventana.DeleteEvent += delegate { Application.Quit(); };

            VBox contenedor = new VBox(false, 10);
            ventana.Add(contenedor);

            Label titulo = new Label("<b>Editor de repuesto</b>");
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

            Label nombre = new Label("Nombre:");
            table.Attach(nombre, 0, 1, 1, 2);

            Label nombreActual = new Label("Null");
            table.Attach(nombreActual, 1, 2, 1, 2);

            Entry nombreEntry = new Entry();
            table.Attach(nombreEntry, 2, 3, 1, 2);

            Label descripcion = new Label("Descripción:");
            table.Attach(descripcion, 0, 1, 2, 3);

            Label descripcionActual = new Label("Null");
            table.Attach(descripcionActual, 1, 2, 2, 3);

            Entry descripcionEntry = new Entry();
            table.Attach(descripcionEntry, 2, 3, 2, 3);

            Label precio = new Label("Precio:");
            table.Attach(precio, 0, 1, 3, 4);

            Label precioActual = new Label("Null");
            table.Attach(precioActual, 1, 2, 3, 4);

            Entry precioEntry = new Entry();
            table.Attach(precioEntry, 2, 3, 3, 4);

            buscar.Clicked += (sender, e) => {
                //int id = int.Parse(idEntry.Text);
                //Repuesto repuesto = ListasGlobales.listaRepuestos.buscarRepuesto(id);
                //if (repuesto.Nombre != null)
                //{
                  //  nombreActual.Text = repuesto.Nombre;
                  //  descripcionActual.Text = repuesto.Descripcion;
                  //  precioActual.Text = repuesto.Precio.ToString();
                //}
                //else
                //{
                //    nombreActual.Text = "Repuesto no encontrado";
                //    descripcionActual.Text = "Repuesto no encontrado";
                //    precioActual.Text = "Repuesto no encontrado";
                //}
            };

            HBox buttonContainer = new HBox(true, 10);
            contenedor.PackStart(buttonContainer, false, false, 10);

            Button actualizar = new Button("Actualizar");
            actualizar.Clicked += (sender, e) => {
                int id = int.Parse(idEntry.Text);
                string nombre_ = nombreEntry.Text;
                string descripcion_ = descripcionEntry.Text;
                string precio_ = precioEntry.Text;
                if (nombre_ != "")
                {
                    nombreActual.Text = nombre_;
                }
                if (descripcion_ != "")
                {
                    descripcionActual.Text = descripcion_;
                }
                if (precio_ != "")
                {
                    precioActual.Text = precio_;
                }
                nombreEntry.Text = "";
                descripcionEntry.Text = "";
                precioEntry.Text = "";
                //ListasGlobales.listaRepuestos.ActualizarRepuesto(id, nombre_, descripcion_, precio_);
            };
            buttonContainer.PackStart(actualizar, true, true, 0);

            Button regresar = new Button("Regresar");
            regresar.Clicked += (sender, e) => {
                gestionRepuestos.Show();
                ventana.Destroy();
            };
            buttonContainer.PackStart(regresar, true, true, 0);

            ventana.ShowAll();
        }
    }
}