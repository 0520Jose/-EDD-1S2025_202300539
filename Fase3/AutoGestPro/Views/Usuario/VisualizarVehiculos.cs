using System;
using Gtk;
using AutoGestPro.Models.Entidades;
using AutoGestPro.Models.Estructuras;

namespace AutoGestPro.Views.Usuario
{
    public unsafe class VisualizarVehiculo
    {
        public VisualizarVehiculo(Window ventana, int id_usuario)
        {
            Window ventanaVehiculo = new Window("Visualizar Vehiculos");
            ventanaVehiculo.SetDefaultSize(800, 600);
            ventanaVehiculo.SetPosition(WindowPosition.Center);
            ventanaVehiculo.DeleteEvent += delegate { Application.Quit(); };

            VBox contenedor = new VBox(false, 5);
            ventanaVehiculo.Add(contenedor);

            Label titulo = new Label("<span size='xx-large'>Visualizar Vehiculos</span>");
            titulo.UseMarkup = true;
            contenedor.PackStart(titulo, false, false, 20);

            HBox ordenContainer = new HBox(false, 5);
            contenedor.PackStart(ordenContainer, false, false, 10);


            ScrolledWindow scrolledWindow = new ScrolledWindow();
            contenedor.PackStart(scrolledWindow, true, true, 10);

            HBox buttonContainer = new HBox(false, 5);
            contenedor.PackStart(buttonContainer, false, false, 10);

            TreeView treeView = new TreeView();
            scrolledWindow.Add(treeView);

            TreeViewColumn idColumn = new TreeViewColumn { Title = "Id" };
            TreeViewColumn Vehiculo = new TreeViewColumn { Title = "Vehiculo" };
            TreeViewColumn detallesColumn = new TreeViewColumn { Title = "Detalles" };
            TreeViewColumn costoColumn = new TreeViewColumn { Title = "Costo" };

            treeView.AppendColumn(idColumn);
            treeView.AppendColumn(detallesColumn);
            treeView.AppendColumn(Vehiculo);
            treeView.AppendColumn(costoColumn);

            CellRendererText idCell = new CellRendererText();
            CellRendererText detallesCell = new CellRendererText();
            CellRendererText costoCell = new CellRendererText();
            CellRendererText VehiculoCell = new CellRendererText();

            idColumn.PackStart(idCell, true);
            detallesColumn.PackStart(detallesCell, true);
            costoColumn.PackStart(costoCell, true);
            Vehiculo.PackStart(VehiculoCell, true);

            idColumn.AddAttribute(idCell, "text", 0);
            Vehiculo.AddAttribute(VehiculoCell, "text", 1);
            detallesColumn.AddAttribute(detallesCell, "text", 2);
            costoColumn.AddAttribute(costoCell, "text", 3);

            ListStore listStore = new ListStore(typeof(string), typeof(string), typeof(string), typeof(string));

            ListaDoble listaVehiculos = EstructurasGlobales.listaVehiculos;
            listStore.Clear();

            void llenarListaInOrden(Vehiculo* vehiculo)
            {
                if (vehiculo == null)
                    return;
                
                if (vehiculo->Id_Usuario == id_usuario)
                {
                    listStore.AppendValues(vehiculo->Id.ToString(), vehiculo->Marca, vehiculo->Modelo, vehiculo->Placa.ToString());
                }
                llenarListaInOrden(vehiculo->siguiente);
            }
            llenarListaInOrden(listaVehiculos.inicio);

            treeView.Model = listStore;

            Button regresar = new Button("Regresar");
            regresar.Clicked += (sender, e) => {
                ventana.Show();
                ventanaVehiculo.Destroy();
            };
            buttonContainer.PackStart(regresar, true, true, 0);

            ventanaVehiculo.ShowAll();
        }
    }
}