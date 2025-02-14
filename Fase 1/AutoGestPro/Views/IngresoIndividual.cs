using System;
using AutoGestPro.Models;
using AutoGestPro.Models.Listas;
using Gtk;

namespace AutoGestPro.Views
{
    class IngresoIndividual
    {
        public IngresoIndividual()
        {
            Window ventana = new Window("Ingreso individual - Root");
            ventana.SetDefaultSize(800,600);
            ventana.DeleteEvent += delegate { Application.Quit(); };

            VBox contenedor = new VBox(false, 5);
            ventana.Add(contenedor);

            Label titulo = new Label("Ingreso individual");
            contenedor.PackStart(titulo, false, false, 5);

            Button ingresarUsuario = new Button("Ingresar usuario");
            ingresarUsuario.Clicked += (sender, e) => {
                new IngresoUsuarios();
            };
            contenedor.PackStart(ingresarUsuario, false, false, 5);

            Button ingresarVehiculo = new Button("Ingresar vehiculo");
            ingresarVehiculo.Clicked += (sender, e) => {
                new IngresoVehiculos();
            };
            contenedor.PackStart(ingresarVehiculo, false, false, 5);

            Button ingresarRepuesto = new Button("Ingresar repuesto");
            ingresarRepuesto.Clicked += (sender, e) => {
                new IngresoRepuestos();
            };
            contenedor.PackStart(ingresarRepuesto, false, false, 5);

            ventana.ShowAll();
        }
    }
}