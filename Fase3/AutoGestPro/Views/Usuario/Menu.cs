using System;
using AutoGestPro.Models.Entidades;
using System.Text.Json;
using Gtk;

namespace AutoGestPro.Views.Usuario
{
    public unsafe class MenuUsuario
    {
        public MenuUsuario(Window cerrarSesion ,UsuarioNodo usuario)
        {
            Window ventana = new Window("Menu - " + usuario.Correo);
            ventana.SetDefaultSize(800, 600);
            ventana.SetPosition(WindowPosition.Center);
            ventana.DeleteEvent += (o, args) => {
                Application.Quit();
                args.RetVal = true;
            };

            VBox contenedor = new VBox(false, 5);
            ventana.Add(contenedor);

            Label titulo = new Label($"<span size='xx-large'>Bienvenido, {usuario.Correo}</span>");
            titulo.UseMarkup = true;
            contenedor.PackStart(titulo, false, false, 20);

            Table table = new Table(4, 3, true);
            contenedor.PackStart(table, true, true, 10);

            Button VizualizarVehiculo = new Button("Visualizar vehiculos");
            VizualizarVehiculo.Clicked += (sender, e) => {
                VisualizarVehiculo visualizarVehiculo = new VisualizarVehiculo(ventana, usuario.Id);
                ventana.Hide();
            };
            table.Attach(VizualizarVehiculo, 0, 1, 0, 1);

            Button VizualizarServicio = new Button("Visualizar servicios");
            VizualizarServicio.Clicked += (sender, e) => {
                VisualizarServicio visualizarServicio = new VisualizarServicio(ventana);
                ventana.Hide();
            };
            table.Attach(VizualizarServicio, 1, 2, 0, 1);

            Button VizualizarFactura = new Button("Visualizar facturas");
            VizualizarFactura.Clicked += (sender, e) => {
                VisualizarFactura visualizarFactura = new VisualizarFactura(ventana, usuario.Id);
                ventana.Hide();
            };
            table.Attach(VizualizarFactura, 2, 3, 0, 1);
            

            Button boton = new Button("Cerrar Sesion");
            boton.Clicked += (sender, e) => {
                string filePath = "/home/emanuel/Escritorio/Proyectos/-EDD-Proyecto_202300539/-EDD-Proyecto_202300539/Fase3/AutoGestPro/Logueos.json";

                if (System.IO.File.Exists(filePath))
                {
                    string existingData = System.IO.File.ReadAllText(filePath);
                    var existingUsers = System.Text.Json.JsonSerializer.Deserialize<List<JsonElement>>(existingData) ?? new List<JsonElement>();

                    var currentUser = existingUsers.Find(user =>
                    {
                        string userCorreo = user.GetProperty("usuario").GetString();
                        return userCorreo == usuario.Correo && !user.TryGetProperty("salida", out _);
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

                ventana.Destroy();
                cerrarSesion.Show();
            };
            table.Attach(boton, 0, 1, 2, 3);

            ventana.ShowAll();
        }
    }
}