using System;
using Gtk;

namespace AutoGestPro.Views
{
    class ControlLogueo
    {
        public ControlLogueo(Window regrear)
        {
            Window ventana = new Window("Control de logueo");
            ventana.SetDefaultSize(400, 300);
            ventana.SetPosition(WindowPosition.Center);
            ventana.DeleteEvent += (o, args) => {
                Application.Quit();
                args.RetVal = true;
            };

            VBox contenedor = new VBox(false, 5);
            ventana.Add(contenedor);

            Label titulo = new Label("<span size='xx-large'>Control de logueo</span>");
            titulo.UseMarkup = true;
            contenedor.PackStart(titulo, false, false, 20);

            Button generarLogueo = new Button("Generar logueo");
            generarLogueo.Clicked += (sender, e) => {
                GenerarDocumento();
            };
            contenedor.PackStart(generarLogueo, false, false, 10);

            Button regresar = new Button("Regresar");
            regresar.Clicked += (sender, e) => {
                regrear.Show();
                ventana.Destroy();
            };
            contenedor.PackStart(regresar, false, false, 10);

            ventana.ShowAll();
        }

        private void GenerarDocumento()
        {
            string filePath = "/home/emanuel/Escritorio/Proyectos/-EDD-Proyecto_202300539/Fase2/AutoGestPro/Logueos.json";

            if (System.IO.File.Exists(filePath))
            {
                System.Diagnostics.Process.Start("xdg-open", filePath);
            }
            else
            {
                MessageDialog dialog = new MessageDialog(
                    null,
                    DialogFlags.Modal,
                    MessageType.Error,
                    ButtonsType.Ok,
                    "El archivo no existe en la ruta especificada."
                );
                dialog.Run();
                dialog.Destroy();
            }
        }
    }
}