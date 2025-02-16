using System;
using System.IO;
using System.Text.Json;
using Gtk;
using AutoGestPro.Models;
using AutoGestPro.Models.Listas;
using System.Diagnostics;


namespace AutoGestPro.Views
{
    unsafe class Reportes 
    {
        public Reportes(Window menu)
        {
            Window ventana = new Window("Reportes - Root");
            ventana.SetDefaultSize(800, 600);
            ventana.SetPosition(WindowPosition.Center);
            ventana.DeleteEvent += delegate { Application.Quit(); };

            VBox contenedor = new VBox(false, 5);
            ventana.Add(contenedor);

            Label titulo = new Label("Generar reportes");
            contenedor.PackStart(titulo, false, false, 5);

            Button reporteUsuarios = new Button("Reporte de usuarios");
            reporteUsuarios.Clicked += (sender, e) => {
                GenerarReporteUsuarios();
            };
            contenedor.PackStart(reporteUsuarios, false, false, 5);

            Button regresar = new Button("Regresar");
            regresar.Clicked += (sender, e) => {
                menu.Show();
                ventana.Destroy();
            };
            contenedor.PackStart(regresar, false, false, 5);

            ventana.ShowAll();
        }

        void GenerarReporteUsuarios()
        {
            string dotPath = "/usr/bin/dot";
            string outputPath = "/home/emanuel/Escritorio/Proyectos/-EDD-Proyecto_202300539/Fase_1/AutoGestPro/Reportes/reporte_usuarios.png";
            string dotFilePath = "/home/emanuel/Escritorio/Proyectos/-EDD-Proyecto_202300539/Fase_1/AutoGestPro/Reportes/reporte_usuarios.dot";

            string dotContent = "digraph G {\n";
            dotContent += "node [shape=record];\n";
            ListaSimple listaUsuarios = ListasGlobales.listaUsuarios;
            Usuario* actual = listaUsuarios.inicio;
            dotContent += "rankdir=LR;\n";
            while (actual != null)
            {
                String nombre = actual->Nombre;
                String apellido = actual->Apellido;
                String correo = actual->Correo;
                string id = actual->Id.ToString();
                dotContent += "node" + id + "[label=\"{ID: " + id + " | \nNombre y Apellido: " + nombre + " " + apellido + " | \nCorreo: " + correo + "}\", shape=record];\n";
                if (actual->siguiente != null)
                {
                    dotContent += $"node{actual->Id} -> node{actual->siguiente->Id};\n";
                }
                actual = actual->siguiente;
            }
            dotContent += "}";

            File.WriteAllText(dotFilePath, dotContent);

            ProcessStartInfo startInfo = new ProcessStartInfo(dotPath)
            {
                Arguments = $"-Tpng {dotFilePath} -o {outputPath}",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = Process.Start(startInfo))
            {
                process.WaitForExit();
            }

            MessageDialog dialog = new MessageDialog(null, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "Reporte de usuarios generado exitosamente.");
            dialog.Run();
            dialog.Destroy();

            System.Diagnostics.Process.Start("xdg-open", outputPath);
        }
    }
}

