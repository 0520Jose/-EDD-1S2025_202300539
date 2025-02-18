using System;
using System.IO;
using System.Text.Json;
using Gtk;
using AutoGestPro.Models;
using AutoGestPro.Models.Listas;
using System.Diagnostics;
using AutoGestPro.Models.Entidades;


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

        void GenerarReporteVehiculos()
        {
            string dotPath = "/usr/bin/dot";
            string outputPath = "/home/emanuel/Escritorio/Proyectos/-EDD-Proyecto_202300539/Fase_1/AutoGestPro/Reportes/reporte_usuarios.png";
            string dotFilePath = "/home/emanuel/Escritorio/Proyectos/-EDD-Proyecto_202300539/Fase_1/AutoGestPro/Reportes/reporte_usuarios.dot";

            string dotContent = "digraph G {\n";
            dotContent += "node [shape=record];\n";

            ListaDoble listaVehiculos = ListasGlobales.listaVehiculos;
            Vehiculo* actual = listaVehiculos.inicio;
            dotContent += "rankdir=LR;\n";

            while (actual != null)
            {
                String marca = actual->Marca;
                String modelo = actual->Modelo;
                String placa = actual->Placa;
                string id = actual->Id.ToString();
                dotContent += "node" + id + "[label=\"{ID: " + id + " | \nMarca: " + marca + " | \nModelo: " + modelo + " | \nPlaca: " + placa + "}\", shape=record];\n";
                if (actual->siguiente != null)
                {
                    dotContent += $"node{actual->Id} -> node{actual->siguiente->Id};\n";
                    dotContent += $"node{actual->siguiente->Id} -> node{actual->Id};\n";
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

            MessageDialog dialog = new MessageDialog(null, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "Reporte de vehiculos generado exitosamente.");
            dialog.Run();
            dialog.Destroy();   

            System.Diagnostics.Process.Start("xdg-open", outputPath);
        }

        void GenerarReporteRepuestos()
        {
            ListaCircular listaRepuestos = ListasGlobales.listaRepuestos;
            string dotPath = "/usr/bin/dot";
            string outputPath = "/home/emanuel/Escritorio/Proyectos/-EDD-Proyecto_202300539/Fase_1/AutoGestPro/Reportes/reporte_repuestos.png";
            string dotFilePath = "/home/emanuel/Escritorio/Proyectos/-EDD-Proyecto_202300539/Fase_1/AutoGestPro/Reportes/reporte_repuestos.dot";

            string dotContent = "digraph G {\n";
            dotContent += "node [shape=record];\n";

            Repuesto* nodoActual = listaRepuestos.inicio;

            dotContent += "rankdir=LR;\n";
            while (nodoActual != null)
            {
                String repuesto = nodoActual->repuesto;
                String detalles = nodoActual->detalle;
                String costo = nodoActual->costo.ToString();
                string id = nodoActual->Id.ToString();
                dotContent += "node" + id + "[label=\"{ID: " + id + " | \nRepuesto: " + repuesto + " | \nDetalles: " + detalles + " | \nCosto: " + costo + "}\", shape=record];\n";
                if (nodoActual->siguiente != listaRepuestos.inicio)
                {
                    dotContent += $"node{id} -> node{nodoActual->siguiente->Id};\n";
                }
                if (nodoActual->siguiente == listaRepuestos.inicio)
                {
                    dotContent += $"node{id} -> node{listaRepuestos.inicio->Id};\n";
                }
                nodoActual = nodoActual->siguiente;
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

            MessageDialog dialog = new MessageDialog(null, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "Reporte de repuestos generado exitosamente.");

            dialog.Run();
            dialog.Destroy();

            System.Diagnostics.Process.Start("xdg-open", outputPath);
        }

        void GenerarReporteServicios()
        {
            Cola colaServicios = ListasGlobales.colaServicios;
            string dotPath = "/usr/bin/dot";

            string outputPath = "/home/emanuel/Escritorio/Proyectos/-EDD-Proyecto_202300539/Fase_1/AutoGestPro/Reportes/reporte_servicios.png";
            string dotFilePath = "/home/emanuel/Escritorio/Proyectos/-EDD-Proyecto_202300539/Fase_1/AutoGestPro/Reportes/reporte_servicios.dot";

            Servicio* actual = colaServicios.inicio;
            string dotContent = "digraph G {\n";
            dotContent += "node [shape=record];\n";
            dotContent += "rankdir=LR;\n";

            while (actual != null)
            {
                String id = actual->Id.ToString();
                String idRepuesto = actual->Id_Repuesto.ToString();
                String idVehiculo = actual->Id_Vehiculo.ToString();
                String detalles = actual->Detalles;
                String costo = actual->Costo.ToString();
                dotContent += "node" + id + "[label=\"{ID: " + id + " | \nID Repuesto: " + idRepuesto + " | \nID Vehiculo: " + idVehiculo + " | \nDetalles: " + detalles + " | \nCosto: " + costo + "}\", shape=record];\n";
                if (actual->siguiente != null)
                {
                    dotContent += $"node{id} -> node{actual->siguiente->Id};\n";
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

            MessageDialog dialog = new MessageDialog(null, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "Reporte de servicios generado exitosamente.");
            dialog.Run();
            dialog.Destroy();

            System.Diagnostics.Process.Start("xdg-open", outputPath);
        }

        void GenerarReporteFacturas()
        {
            Pila pilaFacturas = ListasGlobales.pilaFacturas;
            string dotPath = "/usr/bin/dot";
            string outputPath = "/home/emanuel/Escritorio/Proyectos/-EDD-Proyecto_202300539/Fase_1/AutoGestPro/Reportes/reporte_facturas.png";
            string dotFilePath = "/home/emanuel/Escritorio/Proyectos/-EDD-Proyecto_202300539/Fase_1/AutoGestPro/Reportes/reporte_facturas.dot";

            string dotContent = "digraph G {\n";
            dotContent += "node [shape=record];\n";
            dotContent += "rankdir=TB;\n";

            Factura* actual = pilaFacturas.sima;

            while (actual != null)
            {
                String id = actual->Id.ToString();
                String idOrden = actual->Id_Orden.ToString();
                String total = actual->Total.ToString();
                dotContent += "node" + id + "[label=\"{ID: " + id + " | \nID Orden: " + idOrden + " | \nTotal: " + total + "}\", shape=record];\n";
                if (actual->abajo != null)
                {
                    dotContent += $"node{id} -> node{actual->abajo->Id};\n";
                }
                actual = actual->abajo;
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

            MessageDialog dialog = new MessageDialog(null, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "Reporte de facturas generado exitosamente.");
            dialog.Run();
            dialog.Destroy();

            System.Diagnostics.Process.Start("xdg-open", outputPath);
        }
    }
}

