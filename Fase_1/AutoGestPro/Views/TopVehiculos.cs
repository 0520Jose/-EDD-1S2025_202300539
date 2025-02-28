using System;
using Gtk;
using AutoGestPro.Models;
using AutoGestPro.Models.Listas;
using AutoGestPro.Models.Entidades;
using System.Runtime.InteropServices;

namespace AutoGestPro.Views
{
    unsafe class TopVehiculos
    {
        public TopVehiculos(Window menu)
        {
            Window ventana = new Window("Top Vehiculos");
            ventana.SetDefaultSize(800, 600);
            ventana.SetPosition(WindowPosition.Center);
            ventana.DeleteEvent += delegate { Application.Quit(); };

            VBox container = new VBox(false, 10);
            ventana.Add(container);

            Label title = new Label("<span size='xx-large'>Top Vehiculos</span>");
            title.UseMarkup = true;
            container.PackStart(title, false, false, 10);

            Label masServicios = new Label("Vehiculos con más servicios:");
            container.PackStart(masServicios, false, false, 10);

            Label top1 = new Label("");
            container.PackStart(top1, false, false, 10);

            Label top2 = new Label("");
            container.PackStart(top2, false, false, 10);

            Label top3 = new Label("");
            container.PackStart(top3, false, false, 10);

            Label top4 = new Label("");
            container.PackStart(top4, false, false, 10);

            Label top5 = new Label("");
            container.PackStart(top5, false, false, 10);

            Label masAntiguo = new Label("Vehiculos más antiguos:");
            container.PackStart(masAntiguo, false, false, 10);

            Label top1Antiguo = new Label("");
            container.PackStart(top1Antiguo, false, false, 10);

            Label top2Antiguo = new Label("");
            container.PackStart(top2Antiguo, false, false, 10);

            Label top3Antiguo = new Label("");
            container.PackStart(top3Antiguo, false, false, 10);

            Label top4Antiguo = new Label("");
            container.PackStart(top4Antiguo, false, false, 10);

            Label top5Antiguo = new Label("");
            container.PackStart(top5Antiguo, false, false, 10);


            Dictionary<int, int> vehiculoServicios = new Dictionary<int, int>();

            Cola servicios = ListasGlobales.colaServicios;
            ListaDoble listaVehiculos = ListasGlobales.listaVehiculos;
            Vehiculo* actual = listaVehiculos.inicio;
            Servicio* aux = servicios.inicio;

            while (aux != null)
            {
                if (vehiculoServicios.ContainsKey(aux->Id_Vehiculo))
                {
                    vehiculoServicios[aux->Id_Vehiculo]++;
                }
                else
                {
                    vehiculoServicios[aux->Id_Vehiculo] = 1;
                }
                aux = aux->siguiente;
            }

            var topVehiculos = vehiculoServicios.OrderByDescending(v => v.Value).Take(5).ToList();

            if (topVehiculos.Count > 0) top1.Text = "1. " + topVehiculos[0].Key + " con " + topVehiculos[0].Value + " servicios";
            if (topVehiculos.Count > 1) top2.Text = "2. " + topVehiculos[1].Key + " con " + topVehiculos[1].Value + " servicios";
            if (topVehiculos.Count > 2) top3.Text = "3. " + topVehiculos[2].Key + " con " + topVehiculos[2].Value + " servicios";
            if (topVehiculos.Count > 3) top4.Text = "4. " + topVehiculos[3].Key + " con " + topVehiculos[3].Value + " servicios";
            if (topVehiculos.Count > 4) top5.Text = "5. " + topVehiculos[4].Key + " con " + topVehiculos[4].Value + " servicios";


            Vehiculo* actual2 = listaVehiculos.inicio;
            Dictionary<int, int> vehiculoAntiguedad = new Dictionary<int, int>();

            while (actual2 != null)
            {
                vehiculoAntiguedad[actual2->Id] = DateTime.Now.Year - int.Parse(actual2->Modelo);
                actual2 = actual2->siguiente;
            }

            var topVehiculosAntiguedad = vehiculoAntiguedad.OrderByDescending(v => v.Value).Take(5).ToList();

            if (topVehiculosAntiguedad.Count > 0) top1Antiguo.Text = "1. " + topVehiculosAntiguedad[0].Key + " con " + topVehiculosAntiguedad[0].Value + " años";
            if (topVehiculosAntiguedad.Count > 1) top2Antiguo.Text = "2. " + topVehiculosAntiguedad[1].Key + " con " + topVehiculosAntiguedad[1].Value + " años";
            if (topVehiculosAntiguedad.Count > 2) top3Antiguo.Text = "3. " + topVehiculosAntiguedad[2].Key + " con " + topVehiculosAntiguedad[2].Value + " años";
            if (topVehiculosAntiguedad.Count > 3) top4Antiguo.Text = "4. " + topVehiculosAntiguedad[3].Key + " con " + topVehiculosAntiguedad[3].Value + " años";
            if (topVehiculosAntiguedad.Count > 4) top5Antiguo.Text = "5. " + topVehiculosAntiguedad[4].Key + " con " + topVehiculosAntiguedad[4].Value + " años";

            Button regresar = new Button("Regresar");
            regresar.WidthRequest = 100;
            regresar.HeightRequest = 40;
            regresar.Clicked += (sender, e) => {
                menu.Show();
                ventana.Destroy();
            };
            container.PackStart(regresar, false, false, 10);
            
            ventana.ShowAll();

        }
    }
}