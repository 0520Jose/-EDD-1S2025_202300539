using System;
using AutoGestPro.Models;
using AutoGestPro.Models.Estructuras;
using System.Diagnostics;

namespace AutoGestPro.Views.Reportes_
{
    class ReporteRepuestos
    {
        public void Generar()
        {
            ArbolAVL arbol = EstructurasGlobales.arbolRepuestos;

            string dotPath = "/usr/bin/dot";
            string outputPath = "/home/emanuel/Escritorio/Proyectos/-EDD-Proyecto_202300539/-EDD-Proyecto_202300539/Fase3/AutoGestPro/Reportes/ReporteRepuestos.png";
            string dotFilePath = "/home/emanuel/Escritorio/Proyectos/-EDD-Proyecto_202300539/-EDD-Proyecto_202300539/Fase3/AutoGestPro/Reportes/ReporteRepuestos.dot";

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

            Process.Start("xdg-open", outputPath);
        }
    }
}