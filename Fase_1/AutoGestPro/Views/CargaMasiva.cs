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
        public CargaMasiva(Window menu)
        {
            Window ventana = new Window("Carga masiva - Root");
            ventana.SetDefaultSize(800, 600);
            ventana.SetPosition(WindowPosition.Center);
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
                                    //MessageDialog dialog = new MessageDialog(ventana, 
                                    //DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, 
                                    //"Carga masiva exitosa");
                                //dialog.Run();
                                //dialog.Destroy();
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
                                string marca = element.GetProperty("Marca").GetString();
                                string modelo = element.GetProperty("Modelo").GetString();
                                string placa = element.GetProperty("Placa").GetString();
                                ListasGlobales.listaVehiculos.Insertar(id, idUsuario, marca, modelo, placa);
                                    MessageDialog dialog = new MessageDialog(ventana, 
                                    DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, 
                                    "Carga masiva exitosa");
                                dialog.Run();
                                dialog.Destroy();
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
                                string repuesto = element.GetProperty("Repuesto").GetString();
                                string Detalles = element.GetProperty("Detalles").GetString();
                                float Costo = element.GetProperty("Costo").GetSingle();
                                ListasGlobales.listaRepuestos.Insertar(id, repuesto, Detalles, Costo);
                                    MessageDialog dialog = new MessageDialog(ventana, 
                                    DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, 
                                    "Carga masiva exitosa");
                                dialog.Run();
                                dialog.Destroy();
                            }
                        }
                    }
                    else
                    {
                            MessageDialog dialog = new MessageDialog(ventana, 
                            DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, 
                            "Error archivo no valido");
                        dialog.Run();
                        dialog.Destroy();
                    }
                    
                }
                fileChooser.Destroy();
            };
            contenedor.PackStart(cargar, false, false, 5);

            Button regresar = new Button("Regresar");
            regresar.Clicked += (sender, e) => {
                menu.Show();
                ventana.Destroy();
            };
            contenedor.PackStart(regresar, false, false, 5);

            ventana.ShowAll();
        }
    }
}