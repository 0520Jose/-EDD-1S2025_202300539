using System;
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