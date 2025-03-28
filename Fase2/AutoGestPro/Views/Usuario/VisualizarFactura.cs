using System;
using AutoGestPro.Models.Listas;
using AutoGestPro.Models.Listas.Arbol_B5;
using Gtk;

namespace AutoGestPro.Views
{
    class VisualizarFactura
    {
        public VisualizarFactura(Window ventana)
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

            HBox buttonContainer = new HBox(false, 5);
            contenedor.PackStart(buttonContainer, false, false, 10);

            TreeView treeView = new TreeView();
            scrolledWindow.Add(treeView);

            TreeViewColumn idColumn = new TreeViewColumn { Title = "Id" };
            TreeViewColumn orden = new TreeViewColumn { Title = "Orden" };
            TreeViewColumn total = new TreeViewColumn { Title = "Total" };

            treeView.AppendColumn(idColumn);   
            treeView.AppendColumn(orden);
            treeView.AppendColumn(total);

            CellRendererText idCell = new CellRendererText();
            CellRendererText ordenCell = new CellRendererText();
            CellRendererText totalCell = new CellRendererText();
            
            idColumn.PackStart(idCell, true);
            orden.PackStart(ordenCell, true);
            total.PackStart(totalCell, true);

            idColumn.AddAttribute(idCell, "text", 0);
            orden.AddAttribute(ordenCell, "text", 1);
            total.AddAttribute(totalCell, "text", 2);

            ListStore listStore = new ListStore(typeof(string), typeof(string), typeof(string));
            
            inOrden.Toggled += (sender, e) => {
                if (inOrden.Active)
                {
                    ArbolB5 arbol = ListasGlobales.arbolFacturas;
                    listStore.Clear();
                    void llenarListaInOrden(NodoB nodo)
                    {
                        if (nodo == null)
                        {
                            return;
                        }
                        llenarListaInOrden(nodo.hijos[0]);
                        foreach (var factura in nodo.Facturas)
                        {
                            listStore.AppendValues(factura.Id.ToString(), factura.Id_Orden.ToString(), factura.Total.ToString());
                        }
                        llenarListaInOrden(nodo.hijos[1]);
                    }
                    llenarListaInOrden(arbol.Raiz);
                    
                }
            };

            preOrden.Toggled += (sender, e) => {
                if (preOrden.Active)
                {
                    ArbolB5 arbol = ListasGlobales.arbolFacturas;
                    listStore.Clear();
                    void llenarListaPreOrden(NodoB nodo)
                    {
                        if (nodo == null)
                        {
                            return;
                        }
                        foreach (var factura in nodo.Facturas)
                        {
                            listStore.AppendValues(factura.Id.ToString(), factura.Id_Orden.ToString(), factura.Total.ToString());
                        }
                        llenarListaPreOrden(nodo.hijos[0]);
                        llenarListaPreOrden(nodo.hijos[1]);
                    }
                    llenarListaPreOrden(arbol.Raiz);
                }
            };

            postOrden.Toggled += (sender, e) => {
                if (postOrden.Active)
                {
                    ArbolB5 arbol = ListasGlobales.arbolFacturas;
                    listStore.Clear();
                    void llenarListaPostOrden(NodoB nodo)
                    {
                        if (nodo == null)
                        {
                            return;
                        }
                        llenarListaPostOrden(nodo.hijos[0]);
                        llenarListaPostOrden(nodo.hijos[1]);
                        foreach (var factura in nodo.Facturas)
                        {
                            listStore.AppendValues(factura.Id.ToString(), factura.Id_Orden.ToString(), factura.Total.ToString());
                        }
                    }
                    llenarListaPostOrden(arbol.Raiz);
                }
            };

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