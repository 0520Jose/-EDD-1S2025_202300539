using System;
using AutoGestPro.Models.Listas;
using AutoGestPro.Models.Listas.Arbol_AVL;
using Gtk;

namespace AutoGestPro.Views
{
    class VisualizarServicio
    {
        public VisualizarServicio(Window cerrarSesion)
        {
            Window ventana = new Window("Visualizar Servicio");
            ventana.SetDefaultSize(800, 600);
            ventana.SetPosition(WindowPosition.Center);
            ventana.DeleteEvent += delegate { Application.Quit(); };

            VBox contenedor = new VBox(false, 5);
            ventana.Add(contenedor);

            Label titulo = new Label("<span size='xx-large'>Visualizar Servicio</span>");
            titulo.UseMarkup = true;
            contenedor.PackStart(titulo, false, false, 20);

            HBox ordenContainer = new HBox(false, 5);
            contenedor.PackStart(ordenContainer, false, false, 10);

            RadioButton preOrden = new RadioButton("PRE-ORDEN");
            RadioButton inOrden = new RadioButton(preOrden, "IN-ORDEN");
            RadioButton postOrden = new RadioButton(preOrden, "POST-ORDEN");

            ordenContainer.PackStart(preOrden, false, false, 5);
            ordenContainer.PackStart(inOrden, false, false, 5);
            ordenContainer.PackStart(postOrden, false, false, 5);

            ScrolledWindow scrolledWindow = new ScrolledWindow();
            contenedor.PackStart(scrolledWindow, true, true, 10);

            TreeView treeView = new TreeView();
            scrolledWindow.Add(treeView);

            TreeViewColumn idColumn = new TreeViewColumn { Title = "Id"};
            TreeViewColumn idRepuesto = new TreeViewColumn { Title = "Id Repuesto"};
            TreeViewColumn idVehiculo = new TreeViewColumn { Title = "Id Vehiculo"}; 
            TreeaviewColumn detallescolumn = new TreeaviewColumn { Title = "Detalles"};
            TreeViewColumn costoColumn = new TreeViewColumn { Title = "Costo"};

            treeView.AppendColumn(idColumn);
            treeView.AppendColumn(idRepuesto);
            treeView.AppendColumn(idVehiculo);
            treeView.AppendColumn(detallescolumn);
            treeView.AppendColumn(costoColumn);

            CellRendererText idCell = new CellRendererText();
            CellRendererText idRepuestoCell = new CellRendererText();
            CellRendererText idVehiculoCell = new CellRendererText();
            CellRendererText detallesCell = new CellRendererText();
            CellRendererText costoCell = new CellRendererText(); 

            idColumn.PackStart(idCell, true);
            idRepuesto.PackStart(idRepuestoCell, true);
            idVehiculo.PackStart(idVehiculoCell, true);
            detallescolumn.PackStart(detallesCell, true);
            costoColumn.PackStart(costoCell, true);

            idColumn.AddAttribute(idCell, "text", 0);
            idRepuesto.AddAttribute(idRepuestoCell, "text", 1);
            idVehiculo.AddAttribute(idVehiculoCell, "text", 2);
            detallescolumn.AddAttribute(detallesCell, "text", 3);
            costoColumn.AddAttribute(costoCell, "text", 4);

            ListStore listStore = new ListStore(typeof(string), typeof(string), typeof(string), typeof(string), typeof(string));

            preOrden.Toggled += (sender, e) => {
                if (preOrden.Active)
                {
                    ArbolBinario arbol = ListasGlobales.arbolServicios;
                    listStore.Clear();
                    void llenarListaPreOrden(NodoBinario nodo)
                    {
                        if (nodo != null)
                        {
                            listStore.AppendValues(nodo.Id.ToString(), nodo.IdRepuesto.ToString(), nodo.IdVehiculo.ToString(), nodo.Detalles, nodo.Costo.ToString());
                            llenarListaPreOrden(nodo.Izquierdo);
                            llenarListaPreOrden(nodo.Derecho);
                        }
                    }
                    llenarListaPreOrden(arbol.Raiz);
                }
            };

            inOrden.Toggled += (sender, e) => {
                if (inOrden.Active)
                {
                    ArbolBinario arbol = ListasGlobales.arbolServicios;
                    listStore.Clear();
                    void llenarListaInOrden(NodoBinario nodo)
                    {
                        if (nodo != null)
                        {
                            llenarListaInOrden(nodo.Izquierdo);
                            listStore.AppendValues(nodo.Id.ToString(), nodo.IdRepuesto.ToString(), nodo.IdVehiculo.ToString(), nodo.Detalles, nodo.Costo.ToString());
                            llenarListaInOrden(nodo.Derecho);
                        }
                    }
                    llenarListaInOrden(arbol.Raiz);
                }
            };

            postOrden.Toggled += (sender, e) => {
                if (postOrden.Active)
                {
                    ArbolBinario arbol = ListasGlobales.arbolServicios;
                    listStore.Clear();
                    void llenarListaPostOrden(NodoBinario nodo)
                    {
                        if (nodo != null)
                        {
                            llenarListaPostOrden(nodo.Izquierdo);
                            llenarListaPostOrden(nodo.Derecho);
                            listStore.AppendValues(nodo.Id.ToString(), nodo.IdRepuesto.ToString(), nodo.IdVehiculo.ToString(), nodo.Detalles, nodo.Costo.ToString());
                        }
                    }
                    llenarListaPostOrden(arbol.Raiz);
                }
            };

            treeView.Model = listStore;

            Button regresar = new Button("Regresar");
            regresar.Clicked += (sender, e) => {
                cerrarSesion.Show();
                ventana.Hide();
            };
            contenedor.PackStart(regresar, false, false, 10);

            ventana.ShowAll();
        }
    }
}