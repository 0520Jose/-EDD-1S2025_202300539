using System;
using AutoGestPro.Models.Listas;
using AutoGestPro.Models.Listas.MatrizDispersa;
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
            ventana.SetDefaultSize(600, 400);
            ventana.SetPosition(WindowPosition.Center);
            ventana.DeleteEvent += delegate { Application.Quit(); };

            VBox contenedor = new VBox(false, 10);
            ventana.Add(contenedor);

            Label titulo = new Label("<span size='large'>Generar Servicio</span>");
            titulo.UseMarkup = true;
            contenedor.PackStart(titulo, false, false, 10);

            Table table = new Table(5, 2, false);
            table.ColumnSpacing = 10;
            table.RowSpacing = 10;
            contenedor.PackStart(table, true, true, 10);

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

            HButtonBox buttonBox = new HButtonBox();
            buttonBox.Layout = ButtonBoxStyle.End;
            contenedor.PackStart(buttonBox, false, false, 10);

            Button guardar = new Button("Guardar");
            guardar.Clicked += (sender, e) => {
                int ID = 0;
                int IDRepuesto = 0;
                int IDVehiculo = 0;
                double Costo = 0;
                try
                {
                    ID = Convert.ToInt32(idEntry.Text);
                    IDRepuesto = Convert.ToInt32(idRepuestoEntry.Text);
                    IDVehiculo = Convert.ToInt32(idVehiculoEntry.Text);
                    Costo = Convert.ToDouble(costoEntry.Text);
                }
                catch (FormatException)
                {
                    MessageDialog dialog = new MessageDialog(ventana, 
                        DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, 
                        "Por favor, ingrese un número en los campos ID, ID Repuesto e ID Vehiculo.");
                    dialog.Run();
                    dialog.Destroy();
                    return;
                }
                string Detalles = detallesEntry.Text;
                if (ID == 0 || IDRepuesto == 0 || IDVehiculo == 0 || Detalles == "" || Costo == 0)
                {
                    MessageDialog dialog = new MessageDialog(ventana, 
                        DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, 
                        "Por favor, llene todos los campos.");
                    dialog.Run();
                    dialog.Destroy();
                    return;
                }
                if (ID == null || IDRepuesto == null || IDVehiculo == null || Detalles == null || Costo == null)
                {
                    MessageDialog dialog = new MessageDialog(ventana, 
                        DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, 
                        "Por favor, llene todos los campos.");
                    dialog.Run();
                    dialog.Destroy();
                    return;
                }
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
                    Bitacora bitacora = new Bitacora(Detalles, IDVehiculo, IDRepuesto);
                    ListasGlobales.matrizDispersa.insertar(ID, IDVehiculo, bitacora);
                    MessageDialog dialog = new MessageDialog(ventana, 
                        DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, 
                        "Servicio guardado con éxito.");
                    dialog.Run();
                    dialog.Destroy();
                }
                existe = true;
            };
            buttonBox.PackStart(guardar, false, false, 5);

            Button regresar = new Button("Regresar");
            regresar.Clicked += (sender, e) => {
                menu.Show();
                ventana.Destroy();
            };
            buttonBox.PackStart(regresar, false, false, 5);

            ventana.ShowAll();
        }
    }
}