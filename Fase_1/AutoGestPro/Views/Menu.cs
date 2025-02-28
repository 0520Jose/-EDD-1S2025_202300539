using System;
using Gtk;

namespace AutoGestPro.Views
{
    class Menu
    {
        public Menu(Window cerrarSesion)
        {
            Window ventana = new Window("Menu - Root");
            ventana.SetDefaultSize(800, 600);
            ventana.SetPosition(WindowPosition.Center);
            ventana.DeleteEvent += delegate { Application.Quit(); };

            VBox contenedor = new VBox(false, 5);
            ventana.Add(contenedor);

            Label titulo = new Label("<span size='xx-large'>Menu</span>");
            titulo.UseMarkup = true;
            contenedor.PackStart(titulo, false, false, 20);

            Table table = new Table(3, 2, true);
            contenedor.PackStart(table, true, true, 10);

            Button cargaMasiva = new Button("Carga masiva");
            cargaMasiva.Clicked += (sender, e) => {
                CargaMasiva cargaMasiva = new CargaMasiva(ventana);
                ventana.Hide();
            };
            table.Attach(cargaMasiva, 0, 1, 0, 1);

            Button ingresoIndividual = new Button("Ingreso individual");
            ingresoIndividual.Clicked += (sender, e) => {
                IngresoIndividual ingresoIndividual = new IngresoIndividual(ventana);
                ventana.Hide();
            };
            table.Attach(ingresoIndividual, 1, 2, 0, 1);

            Button gestionDeUsuarios = new Button("Gestión de usuarios");
            gestionDeUsuarios.Clicked += (sender, e) => {
                GestionDeUsuarios gestionDeUsuarios = new GestionDeUsuarios(ventana);
                ventana.Hide();
            };
            table.Attach(gestionDeUsuarios, 0, 1, 1, 2);

            Button generarServicio = new Button("Generar servicio");
            generarServicio.Clicked += (sender, e) => {
                GenerarServicio generarServicio = new GenerarServicio(ventana);
                ventana.Hide();
            };
            table.Attach(generarServicio, 1, 2, 1, 2);

            Button cancelarFactura = new Button("Cancelar factura");
            cancelarFactura.Clicked += (sender, e) => {
                CancelarFactura cancelarFactura = new CancelarFactura(ventana);
                ventana.Hide();
            };
            table.Attach(cancelarFactura, 0, 1, 2, 3);

            Button reportes = new Button("Reportes");
            reportes.Clicked += (sender, e) => {
                Reportes reportes = new Reportes(ventana);
                ventana.Hide();
            };
            table.Attach(reportes, 1, 2, 2, 3);

            Button tops = new Button("Tops");
            tops.Clicked += (sender, e) => {
                TopVehiculos tops = new TopVehiculos(ventana);
                ventana.Hide();
            };
            table.Attach(tops, 0, 1, 3, 4);

            Button CerrarSesion = new Button("Cerrar sesión");
            CerrarSesion.Clicked += (sender, e) => {
                cerrarSesion.Show();
                ventana.Destroy();
            };
            contenedor.PackStart(CerrarSesion, false, false, 20);

            ventana.ShowAll();
        }
    }
}