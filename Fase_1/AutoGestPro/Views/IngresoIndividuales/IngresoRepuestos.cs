using System;
using AutoGestPro.Models;
using AutoGestPro.Models.Listas;
using Gtk;

namespace AutoGestPro.Views
{
    class IngresoRepuestos
    {
        public IngresoRepuestos(Window ingresoIndividual)
        {
            Window ventana = new Window("Ingreso individual - Root");
            ventana.SetDefaultSize(800,600);
            ventana.SetPosition(WindowPosition.Center);
            ventana.DeleteEvent += delegate { Application.Quit(); };

            VBox contenedor = new VBox(false, 5);
            ventana.Add(contenedor);

            Label titulo = new Label("Ingreso de repuesto");
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

            Label Repuesto = new Label("Repuesto:");
            table.Attach(Repuesto, 0, 1, 1, 2);

            Entry RepuestoEntry = new Entry();
            table.Attach(RepuestoEntry, 1, 2, 1, 2);

            Label Detalles = new Label("Detalles:");
            table.Attach(Detalles, 0, 1, 2, 3);

            Entry DetallesEntry = new Entry();
            table.Attach(DetallesEntry, 1, 2, 2, 3);

            Label Costo = new Label("Costo:");
            table.Attach(Costo, 0, 1, 3, 4);

            Entry CostoEntry = new Entry();
            table.Attach(CostoEntry, 1, 2, 3, 4);

            Button guardar = new Button("Guardar");
            guardar.Clicked += (sender, e) => {
                string Repuesto = RepuestoEntry.Text;
                string Detalles = DetallesEntry.Text;
                float Costo = float.Parse(CostoEntry.Text);
                int id = int.Parse(idEntry.Text);
                ListasGlobales.listaRepuestos.Insertar(id, Repuesto, Detalles, Costo);
                    MessageDialog dialog = new MessageDialog(ventana, 
                    DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, 
                    "Carga masiva exitosa de repuestos");
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