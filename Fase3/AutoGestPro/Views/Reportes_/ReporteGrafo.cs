using System;
using AutoGestPro.Models;
using AutoGestPro.Models.Estructuras;
using AutoGestPro.Models.Estructuras.Grafos;
using System.Diagnostics;

namespace AutoGestPro.Views.Reportes_
{
    class ReporteGrafo
    {
        public void Generar()
        {
            Grafo arbol = EstructurasGlobales.grafoVehiculos_Repuestos;

            string dotPath = "/usr/bin/dot";
            string outputPath = "/home/emanuel/Escritorio/Proyectos/-EDD-Proyecto_202300539/-EDD-Proyecto_202300539/Fase3/AutoGestPro/Reportes/ReporteGrafo.png";
            string dotFilePath = "/home/emanuel/Escritorio/Proyectos/-EDD-Proyecto_202300539/-EDD-Proyecto_202300539/Fase3/AutoGestPro/Reportes/ReporteGrafo.dot";

            string dotContent = arbol.Graficar();

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

            Process.Start("xdg-open", outputPath);
        }
    }
}