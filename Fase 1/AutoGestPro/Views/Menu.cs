using System;
using Gtk;

namespace AutoGestPro.Views
{
    class Menu
    {
        public Menu(Window cerrarSesion)
        {
            Window ventana = new Window("Menu - Root");
            ventana.SetDefaultSize(800,600);
            ventana.SetPosition(WindowPosition.Center);
            ventana.DeleteEvent += delegate { Application.Quit(); };

            VBox contenedor = new VBox(false, 5);
            ventana.Add(contenedor);

            Label titulo = new Label("Menu");
            contenedor.PackStart(titulo, false, false, 5);

            Button cargaMasiva = new Button("Carga masiva");
            cargaMasiva.Clicked += (sender, e) => {
                CargaMasiva cargaMasiva = new CargaMasiva(ventana);
                ventana.Hide();
            };
            contenedor.PackStart(cargaMasiva, false, false, 5);

            Button ingresoIndividual = new Button("Ingreso individual");
            ingresoIndividual.Clicked += (sender, e) => {
                IngresoIndividual ingresoIndividual = new IngresoIndividual(ventana);
                ventana.Hide();
            };
            contenedor.PackStart(ingresoIndividual, false, false, 5);

            Button gestionDeUsuarios = new Button("Gestión de usuarios");
            gestionDeUsuarios.Clicked += (sender, e) => {
                GestionDeUsuarios gestionDeUsuarios = new GestionDeUsuarios(ventana);
                ventana.Hide();
            };
            contenedor.PackStart(gestionDeUsuarios, false, false, 5);

            Button generarServicio = new Button("Generar servicio");
            generarServicio.Clicked += (sender, e) => {
                GenerarServicio generarServicio = new GenerarServicio(ventana);
                ventana.Hide();
            };
            contenedor.PackStart(generarServicio, false, false, 5);

            Button cancelarFactura = new Button("Cancelar factura");
            cancelarFactura.Clicked += (sender, e) => {
                CancelarFactura cancelarFactura = new CancelarFactura(ventana);
                ventana.Hide();
            };
            contenedor.PackStart(cancelarFactura, false, false, 5);

            Button CerrarSesion = new Button("Cerrar sesion");
            CerrarSesion.Clicked += (sender, e) => {
                cerrarSesion.Show();
                ventana.Destroy();
            };
            contenedor.PackStart(CerrarSesion, false, false, 5);

            ventana.ShowAll();
        }
    }
}