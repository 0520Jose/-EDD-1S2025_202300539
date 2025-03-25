using System;
using Gtk;

namespace AutoGestPro.Views
{
    public class PagarFactura
    {
        public PagarFactura(Window ventana)
        {
            Window ventanaFactura = new Window("Pagar Factura");
            ventanaFactura.SetDefaultSize(800, 600);
            ventanaFactura.SetPosition(WindowPosition.Center);
            ventanaFactura.DeleteEvent += delegate { Application.Quit(); };

            VBox contenedor = new VBox(false, 5);
            ventanaFactura.Add(contenedor);

            Label titulo = new Label("<span size='xx-large'>Pagar Factura</span>");
            titulo.UseMarkup = true;
            contenedor.PackStart(titulo, false, false, 20);

            Table table = new Table(3, 2, true);
            contenedor.PackStart(table, true, true, 10);

            Label id = new Label("ID:");
            table.Attach(id, 0, 1, 0, 1);

            Entry idEntry = new Entry();
            table.Attach(idEntry, 1, 2, 0, 1);

            Button buscar = new Button("Buscar");
            buscar.Clicked += (sender, e) => {
                Console.WriteLine($"Buscando factura: ID={idEntry.Text}");
            };
            table.Attach(buscar, 2, 3, 0, 1);

            Label Orden = new Label("Orden:");
            table.Attach(Orden, 0, 1, 3, 4);

            Label OrdenDato = new Label();
            table.Attach(OrdenDato, 1, 2, 3, 4);

            Label Total = new Label("Total:");
            table.Attach(Total, 0, 1, 4, 5);

            Label TotalDato = new Label();
            table.Attach(TotalDato, 1, 2, 4, 5);

            Button pagarButton = new Button("Pagar");
            pagarButton.Clicked += (sender, e) => {
                ventanaFactura.Destroy();
            };
            contenedor.PackStart(pagarButton, false, false,
                10);

            Button regresar = new Button("Regresar");
            regresar.Clicked += (sender, e) => {
                ventanaFactura.Destroy();
            };
            contenedor.PackStart(regresar, false, false, 10);

            ventanaFactura.ShowAll();

        }
    }
}