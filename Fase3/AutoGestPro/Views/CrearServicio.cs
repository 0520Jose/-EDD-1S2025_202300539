using System;
using AutoGestPro.Models.Entidades;
using AutoGestPro.Models;
using AutoGestPro.Models.Estructuras;
using Gtk;

namespace AutoGestPro.Views
{
    class CrearServicio 
    {
        public CrearServicio(Window ventana)
        {
            Window ventanaServicio = new Window("Crear Servicio");
            ventanaServicio.SetDefaultSize(800, 600);
            ventanaServicio.SetPosition(WindowPosition.Center);
            ventanaServicio.DeleteEvent += delegate { Application.Quit(); };

            VBox contenedor = new VBox(false, 5);
            ventanaServicio.Add(contenedor);

            Label titulo = new Label("<span size='xx-large'>Crear Servicio</span>");
            titulo.UseMarkup = true;
            contenedor.PackStart(titulo, false, false, 20);

            Table table = new Table(3, 2, true);
            contenedor.PackStart(table, true, true, 10);

            Label id = new Label("ID:");
            table.Attach(id, 0, 1, 0, 1);

            Label id_repuesto = new Label("ID Repuesto:");
            table.Attach(id_repuesto, 0, 1, 1, 2);

            Label id_vehiculo = new Label("ID Vehículo:");
            table.Attach(id_vehiculo, 0, 1, 2, 3);

            Entry idEntry = new Entry();
            table.Attach(idEntry, 1, 2, 0, 1);

            Entry idRepuestoEntry = new Entry();
            table.Attach(idRepuestoEntry, 1, 2, 1, 2);

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

            Button crearButton = new Button("Guardar");
            crearButton.Clicked += (sender, e) => {
                try 
                {
                    string id = idEntry.Text;
                    string idRepuesto = idRepuestoEntry.Text;
                    string idVehiculo = idVehiculoEntry.Text;
                    string detalles = detallesEntry.Text;
                    string costo = costoEntry.Text;

                    if (id == "" || idRepuesto == "" || idVehiculo == "" || detalles == "" || costo == "")
                    {
                        MessageDialog dialog = new MessageDialog(ventanaServicio, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "Todos los campos son requeridos");
                        dialog.Run();
                        dialog.Destroy();
                    }
                    else
                    {
                        int id_ = int.Parse(id);
                        int idRepuesto_ = int.Parse(idRepuesto);
                        int idVehiculo_ = int.Parse(idVehiculo);
                        double costo_ = double.Parse(costo);
                        Vehiculo vehiculoComprobacion = EstructurasGlobales.listaVehiculos.buscarVehiculo(idVehiculo_);
                        NodoAVL repuestoComprobacion = EstructurasGlobales.arbolRepuestos.Buscar(EstructurasGlobales.arbolRepuestos.Raiz, idRepuesto_);
                        if (vehiculoComprobacion.Id == null)
                        {
                            MessageDialog vehiculoDialog = new MessageDialog(ventanaServicio, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "Vehículo no encontrado");
                            vehiculoDialog.Run();
                            vehiculoDialog.Destroy();
                            return;
                        }
                        if (repuestoComprobacion == null)
                        {
                            MessageDialog repuestoDialog = new MessageDialog(ventanaServicio, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "Repuesto no encontrado");
                            repuestoDialog.Run();
                            repuestoDialog.Destroy();
                            return;
                        }
                        NodoBinario nodoBinario = EstructurasGlobales.arbolServicios.BuscarNodo(id_);
                        if (nodoBinario != null)
                        {
                            MessageDialog servicioDialog = new MessageDialog(ventanaServicio, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "Servicio ya existe");
                            servicioDialog.Run();
                            servicioDialog.Destroy();
                            return;
                        }

                        Servicio Servicio = new Servicio(id_, idRepuesto_, idVehiculo_, detalles, costo_);
                        Repuesto repustoCosto = EstructurasGlobales.arbolRepuestos.Buscar(EstructurasGlobales.arbolRepuestos.Raiz, idRepuesto_).Repuesto;
                        double Total = costo_ + repustoCosto.Costo;
                        Factura Factura = new Factura(1+EstructurasGlobales.arbolFacturas.Facturas.tamanio, id_, Total, "Efectivo");
                        EstructurasGlobales.arbolFacturas.Insertar(Factura);
                        EstructurasGlobales.arbolServicios.Insertar(Servicio);
                        EstructurasGlobales.grafoVehiculos_Repuestos.insertar(idVehiculo_, idRepuesto_);
                        idEntry.Text = "";
                        idRepuestoEntry.Text = "";
                        idVehiculoEntry.Text = "";
                        detallesEntry.Text = "";
                        costoEntry.Text = "";
                        MessageDialog dialog = new MessageDialog(ventanaServicio, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "Servicio creado");
                        dialog.Run();
                        dialog.Destroy();
                    }
                }
                catch (Exception ex)
                {
                    MessageDialog dialog = new MessageDialog(ventanaServicio, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, ex.Message);
                    dialog.Run();
                    dialog.Destroy();
                }
            };
            contenedor.PackStart(crearButton, false, false, 10);

            Button regresar = new Button("Regresar");
            regresar.Clicked += (sender, e) => {
                ventana.Show();
                ventanaServicio.Destroy();
            };
            contenedor.PackStart(regresar, false, false, 10);
            
            ventanaServicio.ShowAll();
        }
    } 
}