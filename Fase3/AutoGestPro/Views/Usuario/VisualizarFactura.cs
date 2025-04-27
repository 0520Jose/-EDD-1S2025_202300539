using System;
using AutoGestPro.Models.Estructuras;
using AutoGestPro.Models;
using Gtk;
using AutoGestPro.Models.Entidades;

namespace AutoGestPro.Views
{
    class VisualizarFactura
    {
        public VisualizarFactura(Window ventana, int id)
        {
            Window ventanaFactura = new Window("Visualizar Factura");
            ventanaFactura.SetDefaultSize(800, 600);
            ventanaFactura.SetPosition(WindowPosition.Center);
            ventanaFactura.DeleteEvent += delegate { Application.Quit(); };

            VBox contenedor = new VBox(false, 5);
            ventanaFactura.Add(contenedor);

            Label titulo = new Label("<span size='xx-large'>Visualizar Factura</span>");
            titulo.UseMarkup = true;
            contenedor.PackStart(titulo, false, false, 20);

            ScrolledWindow scrolledWindow = new ScrolledWindow();
            contenedor.PackStart(scrolledWindow, true, true, 10);

            HBox buttonContainer = new HBox(false, 5);
            contenedor.PackStart(buttonContainer, false, false, 10);

            TreeView treeView = new TreeView();
            scrolledWindow.Add(treeView);

            TreeViewColumn idColumn = new TreeViewColumn { Title = "Id" };
            TreeViewColumn id_servicio = new TreeViewColumn { Title = "Id Servicio" };
            TreeViewColumn total = new TreeViewColumn { Title = "Total" };
            TreeViewColumn fecha = new TreeViewColumn { Title = "Fecha" };
            TreeViewColumn metodoDePago = new TreeViewColumn { Title = "Metodo de Pago" };

            treeView.AppendColumn(idColumn);   
            treeView.AppendColumn(id_servicio);
            treeView.AppendColumn(total);
            treeView.AppendColumn(fecha);
            treeView.AppendColumn(metodoDePago);

            CellRendererText idCell = new CellRendererText();
            CellRendererText id_servicioCell = new CellRendererText();
            CellRendererText totalCell = new CellRendererText();
            CellRendererText fechaCell = new CellRendererText();
            CellRendererText metodoDePagoCell = new CellRendererText();
            
            idColumn.PackStart(idCell, true);
            id_servicio.PackStart(id_servicioCell, true);
            total.PackStart(totalCell, true);
            fecha.PackStart(fechaCell, true);
            metodoDePago.PackStart(metodoDePagoCell, true);

            idColumn.AddAttribute(idCell, "text", 0);
            id_servicio.AddAttribute(id_servicioCell, "text", 1);
            total.AddAttribute(totalCell, "text", 2);
            fecha.AddAttribute(fechaCell, "text", 3);
            metodoDePago.AddAttribute(metodoDePagoCell, "text", 4);

            ListStore listStore = new ListStore(typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));
            
            ArbolMerkle arbol = EstructurasGlobales.arbolFacturas;
            ArbolBinario arbolServicio = EstructurasGlobales.arbolServicios;
            ListaDoble lista = EstructurasGlobales.listaVehiculos;
            listStore.Clear();
            void llenarListaInOrden(Nodo nodo)
            {
                if (nodo == null)
                    return;

                llenarListaInOrden(nodo.Izquierdo);

                if (nodo.Izquierdo != null && nodo.Derecho != null)
                {
                    var nodoServicio = arbolServicio.Buscar(nodo.Izquierdo.Factura.Id_Servicio);
                    if (nodoServicio)
                    {   
                        NodoBinario NodoServicio = arbolServicio.BuscarNodo(nodo.Izquierdo.Factura.Id_Servicio);
                        int Id_Vehiculo = NodoServicio.Servicio.Id_Vehiculo;
                        var vehiculo = lista.buscarVehiculo(Id_Vehiculo);
                        if (vehiculo.Id != 0)
                        {
                            int idUsuario = vehiculo.Id_Usuario;
                            if (idUsuario == id)
                            {
                                listStore.AppendValues(
                                    nodo.Izquierdo.Factura.Id.ToString(),
                                    nodo.Izquierdo.Factura.Id_Servicio.ToString(),
                                    nodo.Izquierdo.Factura.Total.ToString("F2"),
                                    $"{nodo.Izquierdo.Factura.Fecha:yyyy-MM-dd}",
                                    nodo.Izquierdo.Factura.MetodoDePago
                                );
                            }
                        }
                    }
                }

                llenarListaInOrden(nodo.Derecho);
            }
            llenarListaInOrden(arbol.Raiz);

            treeView.Model = listStore;

            Button regresar = new Button("Regresar");
            regresar.Clicked += (sender, e) => {
                ventanaFactura.Destroy();
                ventana.Show();
            };
            buttonContainer.PackStart(regresar, false, false, 10);

            ventanaFactura.ShowAll();

            

        }
    }

}