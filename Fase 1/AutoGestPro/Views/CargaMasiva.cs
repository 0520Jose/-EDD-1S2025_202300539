using System;
using System.IO;
using System.Text.Json;
using Gtk;
using AutoGestPro.Models;
using AutoGestPro.Models.Listas;


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
                        using (JsonDocument doc = JsonDocument.Parse(json))
                        {
                            JsonElement root = doc.RootElement;
                            foreach (JsonElement element in root.EnumerateArray())
                            {
                                int id = element.GetProperty("ID").GetInt32();
                                string nombre = element.GetProperty("Nombres").GetString();
                                string apellido = element.GetProperty("Apellidos").GetString();
                                string correo = element.GetProperty("Correo").GetString();
                                string contrasenia = element.GetProperty("Contrasenia").GetString();
                                ListasGlobales.listaUsuarios.Insertar(id, nombre, apellido, correo, contrasenia);
                            }
                        }
                    }
                    else if (opcionSeleccionada == "Vehiculos")
                    {
                        string json = File.ReadAllText(filePath);
                        using (JsonDocument doc = JsonDocument.Parse(json))
                        {
                            JsonElement root = doc.RootElement;
                            foreach (JsonElement element in root.EnumerateArray())
                            {
                                int id = element.GetProperty("ID").GetInt32();
                                int idUsuario = element.GetProperty("ID_Usuario").GetInt32();
                                string Repuesto = element.GetProperty("Repuesto").GetString();
                                string Detalles = element.GetProperty("Detalles").GetString();
                                string Costo = element.GetProperty("Costo").GetString();
                                ListasGlobales.listaVehiculos.Insertar(id, idUsuario, Repuesto, Detalles, Costo);
                            }
                        }
                    }
                    else if (opcionSeleccionada == "Repuestos")
                    {
                        string json = File.ReadAllText(filePath);
                        using (JsonDocument doc = JsonDocument.Parse(json))
                        {
                            JsonElement root = doc.RootElement;
                            foreach (JsonElement element in root.EnumerateArray())
                            {
                                int id = element.GetProperty("ID").GetInt32();
                                string Repuesto = element.GetProperty("Repuesto").GetString();
                                string Detalles = element.GetProperty("Detalles").GetString();
                                float Costo = element.GetProperty("Costo").GetSingle();
                                ListasGlobales.listaRepuestos.Insertar(id, Repuesto, Detalles, Costo);
                            }
                        }
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