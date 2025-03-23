using System;
using AutoGestPro.Views.Reportes;
using Gtk;

namespace AutoGestPro.Views.Reportes_
{
    class GenerarReportes   
    {
        public GenerarReportes(Window cerrarSesion)
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

            Button reportes4 = new Button("Reporte de usuarios");
            reportes4.Clicked += delegate
            {
                new ReporteUsuarios().Generar();
            };
            contenedor.PackStart(reportes4, false, false, 10);

            Button reportes5 = new Button("Reporte de facturas");
            reportes5.Clicked += delegate
            {
                new ReporteFacturas().Generar();
            };
            contenedor.PackStart(reportes5, false, false, 10);


            Button regresar = new Button("Regresar");
            regresar.Clicked += delegate
            {
                cerrarSesion.Show();
                ventana.Destroy();
            };
            contenedor.PackStart(regresar, false, false, 10);

            ventana.ShowAll();
            Application.Run();
        }
    }
}