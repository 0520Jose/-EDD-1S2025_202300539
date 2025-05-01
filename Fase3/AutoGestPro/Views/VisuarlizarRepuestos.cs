using System;
using Gtk;
using AutoGestPro.Models.Estructuras;
using AutoGestPro.Models;

namespace AutoGestPro.Views
{
    class VisualizarRepuesto
    {
        public VisualizarRepuesto(Window ventana)
        {
            Window ventanaRepuesto = new Window("Visualizar Repuesto");
            ventanaRepuesto.SetDefaultSize(800, 600);
            ventanaRepuesto.SetPosition(WindowPosition.Center);
            ventanaRepuesto.DeleteEvent += delegate { Application.Quit(); };

            VBox contenedor = new VBox(false, 5);
            ventanaRepuesto.Add(contenedor);

            Label titulo = new Label("<span size='xx-large'>Visualizar Repuesto</span>");
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
            TreeViewColumn repuesto = new TreeViewColumn { Title = "Repuesto" };
            TreeViewColumn detallesColumn = new TreeViewColumn { Title = "Detalles" };
            TreeViewColumn costoColumn = new TreeViewColumn { Title = "Costo" };

            treeView.AppendColumn(idColumn);
            treeView.AppendColumn(detallesColumn);
            treeView.AppendColumn(repuesto);
            treeView.AppendColumn(costoColumn);

            CellRendererText idCell = new CellRendererText();
            CellRendererText detallesCell = new CellRendererText();
            CellRendererText costoCell = new CellRendererText();
            CellRendererText repuestoCell = new CellRendererText();

            idColumn.PackStart(idCell, true);
            detallesColumn.PackStart(detallesCell, true);
            costoColumn.PackStart(costoCell, true);
            repuesto.PackStart(repuestoCell, true);

            idColumn.AddAttribute(idCell, "text", 0);
            repuesto.AddAttribute(repuestoCell, "text", 1);
            detallesColumn.AddAttribute(detallesCell, "text", 2);
            costoColumn.AddAttribute(costoCell, "text", 3);

            ListStore listStore = new ListStore(typeof(string), typeof(string), typeof(string), typeof(string));

            preOrden.Toggled += (sender, e) => {
                if (preOrden.Active)
                {
                    ArbolAVL arbol = EstructurasGlobales.arbolRepuestos;
                    listStore.Clear();

                    void llenarListaPreOrden(NodoAVL nodo)
                    {
                        if (nodo == null)
                            return;
                        listStore.AppendValues(nodo.Repuesto.Id.ToString(), nodo.Repuesto.REpuesto, nodo.Repuesto.Detalle, nodo.Repuesto.Costo.ToString());
                        llenarListaPreOrden(nodo.Izquierdo);
                        llenarListaPreOrden(nodo.Derecho);
                    }
                    llenarListaPreOrden(arbol.Raiz);
                }
            };

            inOrden.Toggled += (sender, e) => {
                if (inOrden.Active)
                {
                    ArbolAVL arbol = EstructurasGlobales.arbolRepuestos;
                    listStore.Clear();

                    void llenarListaInOrden(NodoAVL nodo)
                    {
                        if (nodo == null)
                            return;
                        llenarListaInOrden(nodo.Izquierdo);
                        listStore.AppendValues(nodo.Repuesto.Id.ToString(), nodo.Repuesto.REpuesto, nodo.Repuesto.Detalle, nodo.Repuesto.Costo.ToString());
                        llenarListaInOrden(nodo.Derecho);
                    }
                    llenarListaInOrden(arbol.Raiz);
                }
            };

            postOrden.Toggled += (sender, e) => {
                if (postOrden.Active)
                {
                    ArbolAVL arbol = EstructurasGlobales.arbolRepuestos;
                    listStore.Clear();

                    void llenarListaPostOrden(NodoAVL nodo)
                    {
                        if (nodo == null)
                            return;
                        llenarListaPostOrden(nodo.Izquierdo);
                        llenarListaPostOrden(nodo.Derecho);
                        listStore.AppendValues(nodo.Repuesto.Id.ToString(), nodo.Repuesto.REpuesto, nodo.Repuesto.Detalle, nodo.Repuesto.Costo.ToString());
                    }
                    llenarListaPostOrden(arbol.Raiz);
                }
            };

            treeView.Model = listStore;

            Button regresar = new Button("Regresar");
            regresar.Clicked += (sender, e) => {
                ventana.Show();
                ventanaRepuesto.Destroy();
            };
            buttonContainer.PackStart(regresar, true, true, 0);

            ventanaRepuesto.ShowAll();
        }
    }
}