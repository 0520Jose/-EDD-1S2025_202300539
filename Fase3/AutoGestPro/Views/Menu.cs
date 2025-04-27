using System;
using AutoGestPro.Models.Entidades;
using AutoGestPro.Models.Estructuras;
using Gtk;

namespace AutoGestPro.Views
{
    class Menu
    {
        public Menu(Window cerrarSesion)
        {
            

            Window ventana = new Window("Menu - Root");
            ventana.SetDefaultSize(800, 600);
            ventana.SetPosition(WindowPosition.Center);
            ventana.DeleteEvent += (o, args) => {
                Application.Quit();
                args.RetVal = true;
            };

            VBox contenedor = new VBox(false, 5);
            ventana.Add(contenedor);

            Label titulo = new Label("<span size='xx-large'>Menu</span>");
            titulo.UseMarkup = true;
            contenedor.PackStart(titulo, false, false, 20);

            Table table = new Table(4, 3, true);
            contenedor.PackStart(table, true, true, 10);

            Button cargaMasiva = new Button("Carga masiva");
            cargaMasiva.Clicked += (sender, e) => {
                CargaMasiva cargaMasiva = new CargaMasiva(ventana);
                ventana.Hide();
            };
            table.Attach(cargaMasiva, 0, 1, 0, 1);

            Button cargaUsuarios = new Button("Carga de usuarios");
            cargaUsuarios.Clicked += (sender, e) => {
                CargaUsuarios cargaUsuarios = new CargaUsuarios(ventana);
                ventana.Hide();
            };
            table.Attach(cargaUsuarios, 1, 2, 0, 1);

            Button verUsuarios = new Button("Ver usuarios");
            verUsuarios.Clicked += (sender, e) => {
                VerUsuario verUsuario = new VerUsuario(ventana);
                ventana.Hide();
            };
            table.Attach(verUsuarios, 2, 3, 0, 1);

            Button verRepuestos = new Button("Ver repuestos");
            verRepuestos.Clicked += (sender, e) => {
                VisualizarRepuesto visualizarRepuesto = new VisualizarRepuesto(ventana);
                ventana.Hide();
            };
            table.Attach(verRepuestos, 0, 1, 1, 2);

            Button crearServicio = new Button("Crear servicio");
            crearServicio.Clicked += (sender, e) => {
                CrearServicio crearServicio = new CrearServicio(ventana);
                ventana.Hide();
            };
            table.Attach(crearServicio, 1, 2, 1, 2);

            Button reportes = new Button("Generar reportes");
            reportes.Clicked += (sender, e) => {
                GenerarReportes generarReportes = new GenerarReportes(ventana);
                ventana.Hide();
            };
            table.Attach(reportes, 2, 3, 1, 2);
    
            Button logueos = new Button("Ver logueos");
            logueos.Clicked += (sender, e) => {
                string filePath = "/home/emanuel/Escritorio/Proyectos/-EDD-Proyecto_202300539/-EDD-Proyecto_202300539/Fase3/AutoGestPro/Logueos.json";
                if (System.IO.File.Exists(filePath))
                {
                    string logueosContent = System.IO.File.ReadAllText(filePath);
                    MessageDialog dialog = new MessageDialog(
                        null,
                        DialogFlags.Modal,
                        MessageType.Info,
                        ButtonsType.Ok,
                        logueosContent
                    );
                    dialog.Run();
                    dialog.Destroy();
                }
                else
                {
                    MessageDialog dialog = new MessageDialog(
                        null,
                        DialogFlags.Modal,
                        MessageType.Error,
                        ButtonsType.Ok,
                        "El archivo Logueos.json no existe."
                    );
                    dialog.Run();
                    dialog.Destroy();
                }
            };
            table.Attach(logueos, 0, 1, 2, 3);

            Button backup = new Button("Backup");
            backup.Clicked += (sender, e) => {
                Backup backup = new Backup();
                backup.GenerarBackup();
            };
            table.Attach(backup, 0, 1, 2, 3);

            Button cerrarSesionButton = new Button("Cerrar sesión");
            cerrarSesionButton.Clicked += (sender, e) => {
                cerrarSesion.Show();
                ventana.Destroy();
            };
            table.Attach(cerrarSesionButton, 1, 2, 2, 3);

            ventana.ShowAll();
        }
    }
}