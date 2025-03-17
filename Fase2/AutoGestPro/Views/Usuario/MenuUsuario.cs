using System;
using Gtk;
using AutoGestPro.Models;

namespace AutoGestPro.Views
{
    unsafe class MenuUsuario
    {
        public MenuUsuario(Window ventana, Usuario* usuario)
        {
            Window ventanaMenu = new Window("Menu Usuario");
            ventanaMenu.SetDefaultSize(800, 600);
            ventanaMenu.SetPosition(WindowPosition.Center);
            ventanaMenu.DeleteEvent += delegate { Application.Quit(); };

            VBox contenedor = new VBox(false, 5);
            ventanaMenu.Add(contenedor);

            Label titulo = new Label($"<span size='xx-large'>Menu {usuario->Correo}</span>");
            titulo.UseMarkup = true;
            contenedor.PackStart(titulo, false, false, 20);

            Button pagarFactura = new Button("Pagar Factura");
            pagarFactura.Clicked += (sender, e) => {
                PagarFactura pagarFactura = new PagarFactura(ventana);
                ventana.Hide();
            };
            contenedor.PackStart(pagarFactura, false, false, 10);

            Button verFacturas = new Button("Ver Facturas");
            verFacturas.Clicked += (sender, e) => {
                VisualizarFactura visualizarFactura = new VisualizarFactura(ventanaMenu);
                ventana.Hide();
            };
            contenedor.PackStart(verFacturas, false, false, 10);

            Button verServicios = new Button("Ver Servicios");
            verServicios.Clicked += (sender, e) => {
                VisualizarServicio visualizarServicio = new VisualizarServicio(ventanaMenu);
                ventana.Hide();
            };
            contenedor.PackStart(verServicios, false, false, 10);

            Button registrarVehiculos = new Button("Registrar Vehiculos");
            registrarVehiculos.Clicked += (sender, e) => {
                RegistrarVehiculo visualizarVehiculo = new RegistrarVehiculo(ventanaMenu);
                ventana.Hide();
            };
            contenedor.PackStart(registrarVehiculos, false, false, 10);

            Button cerrarSesion = new Button("Cerrar Sesion");
            cerrarSesion.Clicked += (sender, e) => {
                ventanaMenu.Destroy();
                ventana.Show();
            };
            contenedor.PackStart(cerrarSesion, false, false, 10);

            ventanaMenu.ShowAll();
        }
    }
}