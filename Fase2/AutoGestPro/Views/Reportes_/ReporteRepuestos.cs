using System;
using Gtk;
using AutoGestPro.Models.Listas.Arbol_AVL;
using AutoGestPro.Models;
using AutoGestPro.Models.Listas;
using System.Diagnostics;
using System.IO;

namespace AutoGestPro.Views.Reportes
{
    class ReporteRepuestos
    {
        public void Generar()
        {
            ArbolAVL arbol = ListasGlobales.arbolRepuestos;

            string dotPath = "/usr/bin/dot";
            string outputPath = "/home/emanuel/Escritorio/Proyectos/-EDD-Proyecto_202300539/Fase2/AutoGestPro/Reportes/ReporteRepuestos.png";
            string dotFilePath = "/home/emanuel/Escritorio/Proyectos/-EDD-Proyecto_202300539/Fase2/AutoGestPro/Reportes/ReporteRepuestos.dot";

            string dotContent = arbol.GenerarDot();

            File.WriteAllText(dotFilePath, dotContent);

            ProcessStartInfo processInfo = new ProcessStartInfo
            {
                FileName = dotPath,
                Arguments = $"-Tpng {dotFilePath} -o {outputPath}",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = Process.Start(processInfo))
            {
                process.WaitForExit();
                if (process.ExitCode != 0)
                {
                    string error = process.StandardError.ReadToEnd();
                    throw new Exception($"Error al ejecutar: {error}");
                }
            }

        }
    }
}