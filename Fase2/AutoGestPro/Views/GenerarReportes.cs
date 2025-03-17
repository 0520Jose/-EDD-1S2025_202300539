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