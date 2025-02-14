using System;
using System.Net.Http.Headers;
using Gtk;


namespace AutoGestPro.Views
{
    class CargaMasiva 
    {
        public CargaMasiva()
        {
            Window ventana = new Window("Carga masiva - Root");
            ventana.SetDefaultSize(800,600);
            ventana.DeleteEvent += delegate { Application.Quit(); };

            VBox contenedor = new VBox(false, 5);
            ventana.Add(contenedor);

            Label titulo = new Label("Carga masiva");
            contenedor.PackStart(titulo, false, false, 5);

            ComboBox opciones = new ComboBox();
            ListStore store = new ListStore(typeof(string));
            opciones.Model = store;

            CellRendererText celda = new CellRendererText();
            opciones.PackStart(celda, false);
            opciones.AddAttribute(celda, "text", 0);

            store.AppendValues("usuarios");
            store.AppendValues("Vehiculos");
            store.AppendValues("Repuestos");

            opciones.Active = 0;
            opciones.Changed += (sender, e) => {
                TreeIter iter;
                if (((ComboBox)sender).GetActiveIter(out iter))
                {
                    string opcionSeleccionada = (string)((ComboBox)sender).Model.GetValue(iter, 0);
                    Console.WriteLine(opcionSeleccionada);
                }
            };

            Label tituloOpciones = new Label("Seleccione una opcion:");
            contenedor.PackStart(tituloOpciones, false, false, 5);
            contenedor.PackStart(opciones, false, false, 5);

            ventana.ShowAll();
        }
    }
}