using System;
using Gtk;

namespace AutoGestPro.Views
{
    class CancelarFactura
    {
        public CancelarFactura()
        {
            Window ventana = new Window("Cancelar factura - Root");
            ventana.SetDefaultSize(400,300);
            ventana.DeleteEvent += delegate { Application.Quit(); };

            VBox contenedor = new VBox(false, 5);
            ventana.Add(contenedor);

            Label titulo = new Label("Facturacion");
            contenedor.PackStart(titulo, false, false, 5);

            HBox subContenedor = new HBox(false, 5);
            contenedor.PackStart(subContenedor, false, false, 5);

            Table table = new Table(5, 2, false);
            table.WidthRequest = 800;
            subContenedor.PackStart(table, true, true, 5);

            Label id = new Label("ID:");
            table.Attach(id, 0, 1, 0, 1);

            Label idActual = new Label("Null");
            table.Attach(idActual, 1, 2, 0, 1);

            Label idOrden = new Label("ID Orden:");
            table.Attach(idOrden, 0, 1, 1, 2);

            Label idOrdenActual = new Label("Null");
            table.Attach(idOrdenActual, 1, 2, 1, 2);

            Label total = new Label("Total:");
            table.Attach(total, 0, 1, 2, 3);

            Label totalActual = new Label("Null");
            table.Attach(totalActual, 1, 2, 2, 3);

            ventana.ShowAll();
        }
    }
}