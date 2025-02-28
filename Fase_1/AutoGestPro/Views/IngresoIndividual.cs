using System;
using AutoGestPro.Models;
using AutoGestPro.Models.Listas;
using Gtk;

namespace AutoGestPro.Views
{
    class IngresoIndividual
    {
        public IngresoIndividual(Window menu)
        {
            Window ventana = new Window("Ingreso individual - Root");
            ventana.SetDefaultSize(800, 600);
            ventana.SetPosition(WindowPosition.Center);
            ventana.DeleteEvent += delegate { Application.Quit(); };

            VBox contenedor = new VBox(false, 5);
            ventana.Add(contenedor);

            Label titulo = new Label("Ingreso individual");
            titulo.SetAlignment(0.5f, 0.5f);
            contenedor.PackStart(titulo, false, false, 10);

            Table table = new Table(4, 1, true);
            table.RowSpacing = 10;
            table.ColumnSpacing = 10;
            contenedor.PackStart(table, true, true, 10);

            Button ingresarUsuario = new Button("Ingresar usuario");
            ingresarUsuario.Clicked += (sender, e) => {
                new IngresoUsuarios(ventana);
                ventana.Hide();
            };
            table.Attach(ingresarUsuario, 0, 1, 0, 1);

            Button ingresarVehiculo = new Button("Ingresar vehiculo");
            ingresarVehiculo.Clicked += (sender, e) => {
                new IngresoVehiculos(ventana);
                ventana.Hide();
            };
            table.Attach(ingresarVehiculo, 0, 1, 1, 2);

            Button ingresarRepuesto = new Button("Ingresar repuesto");
            ingresarRepuesto.Clicked += (sender, e) => {
                new IngresoRepuestos(ventana);
                ventana.Hide();
            };
            table.Attach(ingresarRepuesto, 0, 1, 2, 3);

            Button regresar = new Button("Regresar");
            regresar.Clicked += (sender, e) => {
                menu.Show();
                ventana.Destroy();
            };
            table.Attach(regresar, 0, 1, 3, 4);

            ventana.ShowAll();
        }
    }
}