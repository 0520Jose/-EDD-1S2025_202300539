using System;
using AutoGestPro.Models.Listas;
using AutoGestPro.Models;
using Gtk;
using AutoGestPro.Models.Entidades;

namespace AutoGestPro.Views
{
    class GenerarServicio
    {
        public GenerarServicio(Window menu)
        {
            Window ventana = new Window("Crear Servicio - Root");
            ventana.SetDefaultSize(800,600);
            ventana.SetPosition(WindowPosition.Center);
            ventana.DeleteEvent += delegate { Application.Quit(); };

            VBox contenedor = new VBox(false, 5);
            ventana.Add(contenedor);

            Label titulo = new Label("Generar Servicio");
            contenedor.PackStart(titulo, false, false, 5);

            HBox subContenedor = new HBox(false, 5);
            contenedor.PackStart(subContenedor, false, false, 5);

            Table table = new Table(5, 2, false);
            table.WidthRequest = 800;
            subContenedor.PackStart(table, true, true, 5);

            Label id = new Label("ID:");
            table.Attach(id, 0, 1, 0, 1);

            Entry idEntry = new Entry();
            table.Attach(idEntry, 1, 2, 0, 1);

            Label idRepuesto = new Label("ID Repuesto:");
            table.Attach(idRepuesto, 0, 1, 1, 2);

            Entry idRepuestoEntry = new Entry();
            table.Attach(idRepuestoEntry, 1, 2, 1, 2);

            Label idVehiculo = new Label("ID Vehiculo:");
            table.Attach(idVehiculo, 0, 1, 2, 3);

            Entry idVehiculoEntry = new Entry();
            table.Attach(idVehiculoEntry, 1, 2, 2, 3);

            Label detalles = new Label("Detalles:");
            table.Attach(detalles, 0, 1, 3, 4);

            Entry detallesEntry = new Entry();
            table.Attach(detallesEntry, 1, 2, 3, 4);

            Label costo = new Label("Costo:");
            table.Attach(costo, 0, 1, 4, 5);

            Entry costoEntry = new Entry();
            table.Attach(costoEntry, 1, 2, 4, 5);

            Button guardar = new Button("Guardar");
            guardar.Clicked += (sender, e) => {
                int ID = Convert.ToInt32(idEntry.Text);
                int IDRepuesto = Convert.ToInt32(idRepuestoEntry.Text);
                int IDVehiculo = Convert.ToInt32(idVehiculoEntry.Text);
                string Detalles = detallesEntry.Text;
                double Costo = Convert.ToDouble(costoEntry.Text);
                ListasGlobales.colaServicios.Encolar(ID, IDRepuesto, IDVehiculo, Detalles, Costo);
                idEntry.Text = "";
                idRepuestoEntry.Text = "";
                idVehiculoEntry.Text = "";
                detallesEntry.Text = "";
                costoEntry.Text = "";
                Usuario usuario = ListasGlobales.listaUsuarios.buscarUsuario(ID);
                Repuesto cRepuesto = ListasGlobales.listaRepuestos.buscarRepuesto(IDRepuesto);
                Vehiculo vehiculo = ListasGlobales.listaVehiculos.buscarVehiculo(IDVehiculo);
                Boolean existe = true;
                if (usuario.Nombre == null)
                {
                        MessageDialog dialog = new MessageDialog(ventana, 
                        DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, 
                        "No existe el usuario ingresado.");
                    dialog.Run();
                    dialog.Destroy();
                    existe = false;
                }
                if (cRepuesto.detalle == null)
                {
                        MessageDialog dialog = new MessageDialog(ventana, 
                        DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, 
                        "No existe el repuesto ingresado.");
                    dialog.Run();
                    dialog.Destroy();
                    existe = false;
                }
                if (vehiculo.Marca == null)
                {
                        MessageDialog dialog = new MessageDialog(ventana, 
                        DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, 
                        "No existe el vehiculo ingresado.");
                    dialog.Run();
                    dialog.Destroy();
                    existe = false;
                }
                if (existe)
                {
                    float costoRepuesto = cRepuesto.costo;
                    float total = costoRepuesto + (float)Costo;
                    ListasGlobales.pilaFacturas.Apilar(ID, IDVehiculo, total);
                }
                existe = true;
            };
            HBox buttonContainer = new HBox(false, 5);
            buttonContainer.PackStart(guardar, false, false, 4);
            contenedor.PackStart(buttonContainer, false, false, 5);

            Button regresar = new Button("Regresar");
            regresar.Clicked += (sender, e) => {
                menu.Show();
                ventana.Destroy();
            };
            contenedor.PackStart(regresar, false, false, 5);

            ventana.ShowAll();
        }
    }
}