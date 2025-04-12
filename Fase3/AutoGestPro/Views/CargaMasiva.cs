using System;
using System.IO;
using System.Text.Json;
using AutoGestPro.Models;
using AutoGestPro.Models.Estructuras;
using AutoGestPro.Models.Entidades;
using Gtk;


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

            VBox contenedor = new VBox(false, 10);
            ventana.Add(contenedor);

            Label titulo = new Label("<span size='xx-large'>Carga masiva</span>");
            titulo.UseMarkup = true;
            contenedor.PackStart(titulo, false, false, 10);

            Table table = new Table(3, 2, false);
            contenedor.PackStart(table, false, false, 10);

            Label tituloOpciones = new Label("Seleccione una opción:");
            table.Attach(tituloOpciones, 0, 1, 0, 1, AttachOptions.Fill, AttachOptions.Fill, 5, 5);

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

            opciones.WidthRequest = 200;
            opciones.HeightRequest = 40;
            table.Attach(opciones, 1, 2, 0, 1, AttachOptions.Fill, AttachOptions.Fill, 5, 5);

            Button cargar = new Button("Cargar");
            cargar.WidthRequest = 100;
            cargar.HeightRequest = 40;
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
                        bool error = false;
                        /*
                        ListaSimple listarespaldo = EstructurasGlobales.listaUsuarios;
                        using (JsonDocument doc = JsonDocument.Parse(json))
                        {
                            JsonElement root = doc.RootElement;
                            foreach (JsonElement element in root.EnumerateArray())
                            {
                                try
                                {
                                    int id = element.GetProperty("ID").GetInt32();
                                    string nombre = element.GetProperty("Nombres").GetString();
                                    string apellido = element.GetProperty("Apellidos").GetString();
                                    string correo = element.GetProperty("Correo").GetString();
                                    int edad = element.GetProperty("Edad").GetInt32();
                                    string contrasenia = element.GetProperty("Contrasenia").GetString();
                                    Usuario usuarioComprobacion = EstructurasGlobales.listaUsuarios.buscarUsuario(id);
                                    if (usuarioComprobacion.Id != 0)                                    
                                    {
                                        MostrarMensaje(ventana, $"Usuario con ID {usuarioComprobacion.Id} ya existe. Se omitirá este registro.");
                                        continue;
                                    }
                                    EstructurasGlobales.listaUsuarios.Insertar(id, nombre, apellido, correo, edad, contrasenia);
                                }
                                catch (Exception ex)
                                {
                                    EstructurasGlobales.listaUsuarios = listarespaldo;
                                    MostrarMensaje(ventana, $"Error al procesar el archivo: {ex.Message}");
                                    error = true;
                                    break;
                                }
                            }
                        }
                        
                        listarespaldo = null;
                        */
                        if (!error)
                        {                            
                            MostrarMensaje(ventana, "Carga masiva exitosa");
                        }
                    }
                    else if (opcionSeleccionada == "Vehiculos")
                    {
                        string json = File.ReadAllText(filePath);
                        bool error = false;
                        ListaDoble listarespaldo = EstructurasGlobales.listaVehiculos;
                        using (JsonDocument doc = JsonDocument.Parse(json))
                        {
                            JsonElement root = doc.RootElement;
                            foreach (JsonElement element in root.EnumerateArray())
                            {
                                try
                                {
                                    int id = element.GetProperty("ID").GetInt32();
                                    int idUsuario = element.GetProperty("ID_Usuario").GetInt32();
                                    string marca = element.GetProperty("Marca").GetString();
                                    string modelo = element.GetProperty("Modelo").GetInt32().ToString();
                                    string placa = element.GetProperty("Placa").GetString();
                                    /*if (EstructurasGlobales.listaUsuarios.buscarUsuario(idUsuario).Id == 0)
                                    {
                                        MostrarMensaje(ventana, $"Error al procesar el archivo: Usuario con ID {idUsuario} no existe");
                                        error = true;
                                        continue;
                                    }*/
                                    Vehiculo vehiculoComprobacion = EstructurasGlobales.listaVehiculos.buscarVehiculo(id);
                                    if (vehiculoComprobacion.Id != 0)
                                    {
                                        MostrarMensaje(ventana, $"Vehículo con ID {id} ya existe. Se omitirá este registro.");
                                        continue;
                                    }
                                    EstructurasGlobales.listaVehiculos.Insertar(id, idUsuario, marca, modelo, placa);
                                }
                                catch (Exception ex)
                                {
                                    EstructurasGlobales.listaVehiculos = listarespaldo;
                                    MostrarMensaje(ventana, $"Error al procesar el archivo: {ex.Message}");
                                    error = true;
                                    break;
                                }
                            }
                        }
                        listarespaldo = null;
                        if (!error)
                        {
                            MostrarMensaje(ventana, "Carga masiva exitosa");
                        }
                    }
                    else if (opcionSeleccionada == "Repuestos")
                    {
                        string json = File.ReadAllText(filePath);
                        bool error = false;
                        ArbolAVL arbolRespaldo = EstructurasGlobales.arbolRepuestos;
                        NodoAVL raiz = EstructurasGlobales.arbolRepuestos.Raiz;
                        using (JsonDocument doc = JsonDocument.Parse(json))
                        {
                            JsonElement root = doc.RootElement;
                            foreach (JsonElement element in root.EnumerateArray())
                            {
                                try
                                {
                                    int id = element.GetProperty("ID").GetInt32();
                                    string repuesto = element.GetProperty("Repuesto").GetString();
                                    string detalles = element.GetProperty("Detalles").GetString();
                                    float costo = element.GetProperty("Costo").GetSingle();
                                    NodoAVL repuestoComprobacion = EstructurasGlobales.arbolRepuestos.Buscar(raiz, id);
                                    Repuesto Repuesto = new Repuesto(id, repuesto, detalles, costo);
                                    raiz = EstructurasGlobales.arbolRepuestos.Insertar(raiz, Repuesto);

                                }
                                catch (Exception ex)
                                {
                                    EstructurasGlobales.arbolRepuestos = arbolRespaldo;
                                    MostrarMensaje(ventana, $"Error al procesar el archivo: {ex.Message}");
                                    error = true;
                                    break;
                                }
                            }
                            EstructurasGlobales.arbolRepuestos.Raiz = raiz;
                        }
                    arbolRespaldo = null;
                        if (!error)
                        {
                            MostrarMensaje(ventana, "Carga masiva exitosa");
                        }
                    }
                    else
                    {
                        MostrarMensaje(ventana, "Error archivo no válido");
                    }
                }
                fileChooser.Destroy();
            };
            table.Attach(cargar, 0, 1, 1, 2, AttachOptions.Fill, AttachOptions.Fill, 5, 5);

            Button regresar = new Button("Regresar");
            regresar.WidthRequest = 100;
            regresar.HeightRequest = 40;
            regresar.Clicked += (sender, e) => {
                menu.Show();
                ventana.Destroy();
            };
            table.Attach(regresar, 1, 2, 1, 2, AttachOptions.Fill, AttachOptions.Fill, 5, 5);

            ventana.ShowAll();
        }

        private void MostrarMensaje(Window ventana, string mensaje)
        {
            MessageDialog dialog = new MessageDialog(ventana, 
                DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, mensaje);
            dialog.Run();
            dialog.Destroy();
        }
    }
}