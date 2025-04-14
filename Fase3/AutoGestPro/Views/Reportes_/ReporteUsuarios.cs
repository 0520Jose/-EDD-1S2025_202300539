using System;
using AutoGestPro.Models.Estructuras;
using System.Diagnostics;

namespace AutoGestPro.Views.Reportes_
{
    class ReporteUsuarios
    {
        public void Generar(int id)
        {
            BlockChain lista = EstructurasGlobales.blockChain;

            string dotPath = "/usr/bin/dot";
            string outputPath = "/home/emanuel/Escritorio/Proyectos/-EDD-Proyecto_202300539/-EDD-Proyecto_202300539/Fase3/AutoGestPro/Reportes/ReporteUsuarios.png";
            string dotFilePath = "/home/emanuel/Escritorio/Proyectos/-EDD-Proyecto_202300539/-EDD-Proyecto_202300539/Fase3/AutoGestPro/Reportes/ReporteUsuarios.dot";

            string dotContent = lista.GenerarDot(id);

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