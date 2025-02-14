using System;
using System.IO;
using System.Text.Json;
using Gtk;
using AutoGestPro.Models;


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

            store.AppendValues("Usuarios");
            store.AppendValues("Vehiculos");
            store.AppendValues("Repuestos");

            opciones.Active = 0;
            string opcionSeleccionada = "Usuarios";
            opciones.Changed += (sender, e) => {
                TreeIter iter;
                if (((ComboBox)sender).GetActiveIter(out iter))
                {
                    opcionSeleccionada = (string)((ComboBox)sender).Model.GetValue(iter, 0);
                    Console.WriteLine(opcionSeleccionada);
                }
            };

            Label tituloOpciones = new Label("Seleccione una opcion:");
            contenedor.PackStart(tituloOpciones, false, false, 5);
            contenedor.PackStart(opciones, false, false, 5);

            Button cargar = new Button("Cargar");
            cargar.Clicked += (sender, e) => {
                FileChooserDialog fileChooser = new FileChooserDialog(
                    "Seleccione un archivo .json",
                    null,
                    FileChooserAction.Open,
                    "Cancelar", ResponseType.Cancel,
                    "Abrir", ResponseType.Accept
                );

                FileFilter filter = new FileFilter();
                filter.AddPattern("*.json");
                fileChooser.Filter = filter;

                if (fileChooser.Run() == (int)ResponseType.Accept)
                {
                    string filePath = fileChooser.Filename;
                    if (opcionSeleccionada == "Usuarios")
                    {
                        string json = File.ReadAllText(filePath);
                        var usuarios = JsonSerializer.Deserialize<Usuario[]>(json);

                        

                    }
                    else if (opcionSeleccionada == "Vehiculos")
                    {
                        Console.WriteLine("Cargando vehiculos");
                    }
                    else if (opcionSeleccionada == "Repuestos")
                    {
                        Console.WriteLine("Cargando repuestos");
                    }
                    else
                    {
                        Console.WriteLine("Opcion no valida");
                    }
                    Console.WriteLine("Archivo seleccionado: " + filePath);
                    
                }

                fileChooser.Destroy();
            };
            contenedor.PackStart(cargar, false, false, 5);

            ventana.ShowAll();
        }
    }
}