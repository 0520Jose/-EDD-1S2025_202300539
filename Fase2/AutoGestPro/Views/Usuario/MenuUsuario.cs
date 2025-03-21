using System;
using Gtk;
using System.Text.Json;
using AutoGestPro.Models;

namespace AutoGestPro.Views
{
    unsafe class MenuUsuario
    {
        public MenuUsuario(Window ventana, Usuario* usuario_)
        {
            Window ventanaMenu = new Window("Menu Usuario");
            ventanaMenu.SetDefaultSize(800, 600);
            ventanaMenu.SetPosition(WindowPosition.Center);
            ventanaMenu.DeleteEvent += delegate { Application.Quit(); };

            VBox contenedor = new VBox(false, 5);
            ventanaMenu.Add(contenedor);

            Label titulo = new Label($"<span size='xx-large'>Menu {usuario_->Correo}</span>");
            titulo.UseMarkup = true;
            contenedor.PackStart(titulo, false, false, 20);

            Button pagarFactura = new Button("Pagar Factura");
            pagarFactura.Clicked += (sender, e) => {
                PagarFactura pagarFactura = new PagarFactura(ventana);
                ventana.Hide();
            };
            contenedor.PackStart(pagarFactura, false, false, 10);

            Button verFacturas = new Button("Ver Facturas");
            verFacturas.Clicked += (sender, e) => {
                VisualizarFactura visualizarFactura = new VisualizarFactura(ventanaMenu);
                ventana.Hide();
            };
            contenedor.PackStart(verFacturas, false, false, 10);

            Button verServicios = new Button("Ver Servicios");
            verServicios.Clicked += (sender, e) => {
                VisualizarServicio visualizarServicio = new VisualizarServicio(ventanaMenu);
                ventana.Hide();
            };
            contenedor.PackStart(verServicios, false, false, 10);

            Button registrarVehiculos = new Button("Registrar Vehiculos");
            registrarVehiculos.Clicked += (sender, e) => {
                RegistrarVehiculo visualizarVehiculo = new RegistrarVehiculo(ventanaMenu, usuario_);
                ventana.Hide();
            };
            contenedor.PackStart(registrarVehiculos, false, false, 10);

            Button cerrarSesion = new Button("Cerrar Sesion");
            cerrarSesion.Clicked += (sender, e) => {
                string filePath = "/home/emanuel/Escritorio/Proyectos/-EDD-Proyecto_202300539/Fase2/AutoGestPro/Logueos.json";

                if (System.IO.File.Exists(filePath))
                {
                    string existingData = System.IO.File.ReadAllText(filePath);
                    var existingUsers = System.Text.Json.JsonSerializer.Deserialize<List<JsonElement>>(existingData) ?? new List<JsonElement>();

                    var currentUser = existingUsers.Find(user =>
                    {
                        string userCorreo = user.GetProperty("usuario").GetString();
                        return userCorreo == usuario_->Correo && !user.TryGetProperty("salida", out _);
                    });

                    if (currentUser.ValueKind != JsonValueKind.Undefined)
                    {
                        var userToUpdate = currentUser;
                        var updatedUser = JsonSerializer.Deserialize<Dictionary<string, object>>(currentUser.GetRawText());
                        updatedUser["salida"] = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");

                        existingUsers.Remove(currentUser);
                        var updatedUserJson = JsonSerializer.Serialize(updatedUser);
                        var updatedUserElement = JsonSerializer.Deserialize<JsonElement>(updatedUserJson);
                        existingUsers.Add(updatedUserElement);

                        string updatedData = System.Text.Json.JsonSerializer.Serialize(existingUsers);
                        System.IO.File.WriteAllText(filePath, updatedData);
                    }
                }

                ventanaMenu.Destroy();
                ventana.Show();
            };
            contenedor.PackStart(cerrarSesion, false, false, 10);

            ventanaMenu.ShowAll();
        }
    }
}