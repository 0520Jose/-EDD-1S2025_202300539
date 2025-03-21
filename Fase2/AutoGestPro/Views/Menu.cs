using System;
using AutoGestPro.Views.Reportes_;
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
            ventana.DeleteEvent += (o, args) => {
                Application.Quit();
                args.RetVal = true;
            };

            VBox contenedor = new VBox(false, 5);
            ventana.Add(contenedor);

            Label titulo = new Label("<span size='xx-large'>Menu</span>");
            titulo.UseMarkup = true;
            contenedor.PackStart(titulo, false, false, 20);

            Table table = new Table(5, 2, true);
            contenedor.PackStart(table, true, true, 10);

            Button cargaMasiva = new Button("Carga masiva");
            cargaMasiva.Clicked += (sender, e) => {
                CargaMasiva cargaMasiva = new CargaMasiva(ventana);
                ventana.Hide();
            };
            table.Attach(cargaMasiva, 0, 1, 0, 1);

            Button gestionDeUsuarios = new Button("Gestión de usuarios");
            gestionDeUsuarios.Clicked += (sender, e) => {
                EditarUsuario gestionDeUsuarios = new EditarUsuario(ventana);
                ventana.Hide();
            };
            table.Attach(gestionDeUsuarios, 1, 2, 0, 1);

            Button gestionDeVehiculos = new Button("Gestión de vehículos");
            gestionDeVehiculos.Clicked += (sender, e) => {
                EditarVehiculo gestionDeVehiculos = new EditarVehiculo(ventana);
                ventana.Hide();
            };
            table.Attach(gestionDeVehiculos, 0, 1, 1, 2);

            Button gestionDeRepuestos = new Button("Gestión de repuestos");
            gestionDeRepuestos.Clicked += (sender, e) => {
                ActualizarRepuesto gestionDeRepuestos = new ActualizarRepuesto(ventana);
                ventana.Hide();
            };
            table.Attach(gestionDeRepuestos, 1, 2, 1, 2);

            Button actualizarRepuesto = new Button("Actualizar repuesto");
            actualizarRepuesto.Clicked += (sender, e) => {
                ActualizarRepuesto actualizarRepuesto = new ActualizarRepuesto(ventana);
                ventana.Hide();
            };
            table.Attach(actualizarRepuesto, 0, 1, 2, 3);

            Button visualizarRepuesto = new Button("Visualizar repuesto");
            visualizarRepuesto.Clicked += (sender, e) => {
                VisualizarRepuesto visualizarRepuesto = new VisualizarRepuesto(ventana);
                ventana.Hide();
            };
            table.Attach(visualizarRepuesto, 1, 2, 2, 3);

            Button generarServicios = new Button("Generar servicios");
            generarServicios.Clicked += (sender, e) => {
                CrearServicio generarServicios = new CrearServicio(ventana);
                ventana.Hide();
            };
            table.Attach(generarServicios, 0, 1, 3, 4);

            Button controlLogueo = new Button("Control de logueo");
            controlLogueo.Clicked += (sender, e) => {
                ControlLogueo controlLogueo = new ControlLogueo(ventana);
                ventana.Hide();
            };
            table.Attach(controlLogueo, 1, 2, 3, 4);

            Button generarReportes = new Button ("Generar reportes");
            generarReportes.Clicked += (sender, e) => {
                GenerarReportes reportes = new GenerarReportes(ventana);
                ventana.Hide();
            };
            table.Attach(generarReportes, 0, 1, 4, 5);

            Button cerrarSesionButton = new Button("Cerrar sesión");
            cerrarSesionButton.Clicked += (sender, e) => {
                cerrarSesion.Show();
                ventana.Destroy();
            };
            table.Attach(cerrarSesionButton, 1, 2, 4, 5);

            ventana.ShowAll();
        }
    }
}
