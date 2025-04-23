using System;
using AutoGestPro.Views.Reportes_;
using Gtk;

namespace AutoGestPro.Views
{
    class GenerarReportes   
    {
        public GenerarReportes(Window menu)
        {
            Application.Init();
            Window ventana = new Window("AutoGestPro");
            ventana.SetDefaultSize(800, 600);
            ventana.SetPosition(WindowPosition.Center);
            ventana.DeleteEvent += delegate { Application.Quit(); };

            VBox contenedor = new VBox(false, 10);
            ventana.Add(contenedor);

            Label titulo = new Label("<b>Reportes</b>");
            titulo.UseMarkup = true;
            titulo.Justify = Justification.Center;
            contenedor.PackStart(titulo, false, false, 10);


            Button reportes = new Button("Reporte de repuestos");
            reportes.Clicked += delegate
            {
                new ReporteRepuestos().Generar();
            };
            contenedor.PackStart(reportes, false, false, 10);

            Button reportes2 = new Button("Reporte de servicios");
            reportes2.Clicked += delegate
            {
                new ReporteServicios().Generar();
            };
            contenedor.PackStart(reportes2, false, false, 10);

            Button reportes3 = new Button("Reporte de vehículos");
            reportes3.Clicked += delegate
            {
                new ReporteVehiculos().Generar();
            };
            contenedor.PackStart(reportes3, false, false, 10);

            Entry idEntry = new Entry();
            idEntry.SetSizeRequest(200, 30);
            contenedor.PackStart(idEntry, false, false, 10);

            Button reportes4 = new Button("Reporte de usuarios");
            reportes4.Clicked += delegate
            {
                if (string.IsNullOrEmpty(idEntry.Text))
                {
                    MessageDialog md = new MessageDialog(ventana, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "Por favor ingrese un ID de usuario.");
                    md.Run();
                    md.Destroy();
                    return;
                }
                new ReporteUsuarios().Generar(int.Parse(idEntry.Text));
                idEntry.Text = "";
            };
            contenedor.PackStart(reportes4, false, false, 10);

            Button reportes5 = new Button("Reporte de facturas");
            reportes5.Clicked += delegate
            {
                new ReporteFacturas().Generar();
            };
            contenedor.PackStart(reportes5, false, false, 10);

            Button reporte6 = new Button ("Reporte de Grafo"); 
            reporte6.Clicked += delegate
            {
                new ReporteGrafo().Generar();
            };
            contenedor.PackStart(reporte6, false, false, 10);

            Button regresar = new Button("Regresar");
            regresar.Clicked += delegate
            {
                menu.Show();
                ventana.Destroy();
            };
            contenedor.PackStart(regresar, false, false, 10);

            ventana.ShowAll();
            Application.Run();
        }
    }
}