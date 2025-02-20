using System;
using System.Runtime.InteropServices;
using AutoGestPro.Models;
using AutoGestPro.Models.Listas;
using Gtk;

namespace AutoGestPro.Views
{
    class IngresoRepuestos
    {
        public IngresoRepuestos(Window ingresoIndividual)
        {
            Window ventana = new Window("Ingreso individual - Root");
            ventana.SetDefaultSize(600, 400);
            ventana.SetPosition(WindowPosition.Center);
            ventana.DeleteEvent += delegate { Application.Quit(); };

            VBox contenedor = new VBox(false, 10);
            ventana.Add(contenedor);

            Label titulo = new Label("<b>Ingreso de Repuesto</b>");
            titulo.UseMarkup = true;
            titulo.Justify = Justification.Center;
            contenedor.PackStart(titulo, false, false, 10);

            Table table = new Table(4, 2, false);
            table.RowSpacing = 10;
            table.ColumnSpacing = 10;
            contenedor.PackStart(table, true, true, 10);

            Label idLabel = new Label("ID:");
            table.Attach(idLabel, 0, 1, 0, 1);

            Entry idEntry = new Entry();
            table.Attach(idEntry, 1, 2, 0, 1);

            Label repuestoLabel = new Label("Repuesto:");
            table.Attach(repuestoLabel, 0, 1, 1, 2);

            Entry repuestoEntry = new Entry();
            table.Attach(repuestoEntry, 1, 2, 1, 2);

            Label detallesLabel = new Label("Detalles:");
            table.Attach(detallesLabel, 0, 1, 2, 3);

            Entry detallesEntry = new Entry();
            table.Attach(detallesEntry, 1, 2, 2, 3);

            Label costoLabel = new Label("Costo:");
            table.Attach(costoLabel, 0, 1, 3, 4);

            Entry costoEntry = new Entry();
            table.Attach(costoEntry, 1, 2, 3, 4);

            HButtonBox buttonBox = new HButtonBox();
            buttonBox.Layout = ButtonBoxStyle.End;
            buttonBox.Spacing = 10;
            contenedor.PackStart(buttonBox, false, false, 10);

            Button guardarButton = new Button("Guardar");
            guardarButton.Clicked += (sender, e) => {
                string repuesto = repuestoEntry.Text;
                string detalles = detallesEntry.Text;
                if (repuesto == "" || detalles == "" || costoEntry.Text=="" || idEntry.Text=="")
                {
                    MessageDialog errorDialog = new MessageDialog(ventana, 
                        DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, 
                        "Por favor, llene todos los campos");
                    errorDialog.Run();
                    errorDialog.Destroy();
                    return;
                }
                float costo = 0;
                int id = 0;
                try {
                    costo = float.Parse(costoEntry.Text);
                    id = int.Parse(idEntry.Text);
                } catch (FormatException) {
                    MessageDialog errorDialog = new MessageDialog(ventana, 
                        DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, 
                        "Por favor, ingrese un número en el campo de costo");
                    errorDialog.Run();
                    errorDialog.Destroy();
                    return;
                }
                if (repuesto == null || detalles == null || costo == null || id == null)
                {
                    MessageDialog errorDialog = new MessageDialog(ventana, 
                        DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, 
                        "Por favor, llene todos los campos");
                    errorDialog.Run();
                    errorDialog.Destroy();
                    return;
                }
                if (ListasGlobales.listaRepuestos.buscarRepuesto(id).Id == id)
                {
                    MessageDialog errorDialog = new MessageDialog(ventana, 
                        DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, 
                        "El repuesto ya existe");
                    errorDialog.Run();
                    errorDialog.Destroy();
                    return;
                }
                idEntry.Text = "";
                repuestoEntry.Text = "";
                detallesEntry.Text = "";
                costoEntry.Text = "";
                ListasGlobales.listaRepuestos.Insertar(id, repuesto, detalles, costo);
                MessageDialog successDialog = new MessageDialog(ventana, 
                    DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, 
                    "Carga exitosa de repuestos");
                successDialog.Run();
                successDialog.Destroy();
            };
            buttonBox.PackStart(guardarButton, false, false, 0);

            Button regresarButton = new Button("Regresar");
            regresarButton.Clicked += (sender, e) => {
                ingresoIndividual.Show();
                ventana.Destroy();
            };
            buttonBox.PackStart(regresarButton, false, false, 0);

            ventana.ShowAll();
        }
    }
}