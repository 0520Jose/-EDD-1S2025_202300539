using System;
using Gtk;
using AutoGestPro.Models;
using AutoGestPro.Models.Listas;

namespace AutoGestPro.Views
{
    unsafe class RegistrarVehiculo
    {
        public RegistrarVehiculo(Window cerrarSesion, Usuario* usuarioActual)
        {
            Window ventana = new Window("Registrar Vehículo");
            ventana.SetDefaultSize(800, 600);
            ventana.SetPosition(WindowPosition.Center);
            ventana.DeleteEvent += delegate { Application.Quit(); };

            VBox contenedor = new VBox(false, 5);
            ventana.Add(contenedor);

            Label titulo = new Label("<span size='xx-large'>Registrar Vehículo</span>");
            titulo.UseMarkup = true;
            contenedor.PackStart(titulo, false, false, 20);

            Label id = new Label("ID:");
            contenedor.PackStart(id, false, false, 5);
            Entry idEntry = new Entry();
            contenedor.PackStart(idEntry, false, false, 5);
            Label marca = new Label("Marca:");
            contenedor.PackStart(marca, false, false, 5);
            Entry marcaEntry = new Entry();
            contenedor.PackStart(marcaEntry, false, false, 5);
            Label modelo = new Label("Modelo:");
            contenedor.PackStart(modelo, false, false, 5);
            Entry modeloEntry = new Entry();
            contenedor.PackStart(modeloEntry, false, false, 5);
            Label placa = new Label("Placa:");
            contenedor.PackStart(placa, false, false, 5);
            Entry placaEntry = new Entry();
            contenedor.PackStart(placaEntry, false, false, 5);

            Button guardar = new Button("Guardar");
            guardar.Clicked += (sender, e) => {
                try 
                {
                    int id = int.Parse(idEntry.Text);
                    int id_usuario = usuarioActual->Id;
                    string marca = marcaEntry.Text;
                    string modelo = modeloEntry.Text;
                    string placa = placaEntry.Text;
                    if (id == 0 || id_usuario == 0 || marca == "" || modelo == "" || placa == "")
                    {
                        MessageDialog error = new MessageDialog(ventana, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "Debe llenar todos los campos");
                        error.Run();
                        error.Destroy();
                        return;
                    }
                    if (id.ToString() == "" || id_usuario.ToString() == "" || marca == "" || modelo == "" || placa == "")
                    {
                        MessageDialog error = new MessageDialog(ventana, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "Debe llenar todos los campos");
                        error.Run();
                        error.Destroy();
                        return;
                    }
                    Vehiculo vehiculoComprobacion = ListasGlobales.listaVehiculos.buscarVehiculo(id);
                    Usuario usuarioComprobacion = ListasGlobales.listaUsuarios.buscarUsuario(id_usuario);
                    if (usuarioComprobacion.Id == null)
                    {
                        MessageDialog error = new MessageDialog(ventana, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "El usuario no existe");
                        error.Run();
                        error.Destroy();
                        return;
                    }
                    if (vehiculoComprobacion.Id > 0)
                    {
                        MessageDialog error = new MessageDialog(ventana, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "Ya existe un vehículo con ese ID");
                        error.Run();
                        error.Destroy();
                        return;
                    }
                    ListasGlobales.listaVehiculos.Insertar(id, id_usuario, marca, modelo, placa);
                    MessageDialog mensaje = new MessageDialog(ventana, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "Vehículo registrado con éxito");
                    mensaje.Run();
                    mensaje.Destroy();
                    idEntry.Text = "";
                    marcaEntry.Text = "";
                    modeloEntry.Text = "";
                    placaEntry.Text = "";
                }
                catch (Exception ex)
                {
                    MessageDialog error = new MessageDialog(ventana, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "Error: " + ex.Message);
                    error.Run();
                    error.Destroy();
                }
            };
            contenedor.PackStart(guardar, false, false, 10);

            Button volver = new Button("Volver al menú");
            volver.Clicked += (sender, e) => {
                cerrarSesion.Show();
                ventana.Destroy();
            };
            contenedor.PackStart(volver, false, false, 10);

            ventana.ShowAll();
        }
    }
}