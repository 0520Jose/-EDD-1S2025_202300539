using System;
using AutoGestPro.Models;
using AutoGestPro.Models.Listas;
using Gtk;

namespace AutoGestPro.Views
{
    class IngresoUsuarios
    {
        public IngresoUsuarios(Window ingresoIndividual)
        {
            Window ventana = new Window("Ingreso individual - Root");
            ventana.SetDefaultSize(400, 300);
            ventana.SetPosition(WindowPosition.Center);
            ventana.DeleteEvent += delegate { Application.Quit(); };

            VBox contenedor = new VBox(false, 10);
            ventana.Add(contenedor);

            Label titulo = new Label("<b>Ingreso de usuario</b>");
            titulo.UseMarkup = true;
            titulo.Justify = Justification.Center;
            contenedor.PackStart(titulo, false, false, 10);

            Table table = new Table(5, 2, false);
            table.ColumnSpacing = 10;
            table.RowSpacing = 10;
            contenedor.PackStart(table, true, true, 10);

            Label id = new Label("ID:");
            table.Attach(id, 0, 1, 0, 1);

            Entry idEntry = new Entry();
            table.Attach(idEntry, 1, 2, 0, 1);

            Label nombre = new Label("Nombre:");
            table.Attach(nombre, 0, 1, 1, 2);

            Entry nombreEntry = new Entry();
            table.Attach(nombreEntry, 1, 2, 1, 2);

            Label apellido = new Label("Apellido:");
            table.Attach(apellido, 0, 1, 2, 3);

            Entry apellidoEntry = new Entry();
            table.Attach(apellidoEntry, 1, 2, 2, 3);

            Label correo = new Label("Correo:");
            table.Attach(correo, 0, 1, 3, 4);

            Entry correoEntry = new Entry();
            table.Attach(correoEntry, 1, 2, 3, 4);

            Label contrasenia = new Label("Contraseña:");
            table.Attach(contrasenia, 0, 1, 4, 5);

            Entry contraseniaEntry = new Entry();
            table.Attach(contraseniaEntry, 1, 2, 4, 5);

            HBox buttonContainer = new HBox(true, 10);
            contenedor.PackStart(buttonContainer, false, false, 10);

            Button guardar = new Button("Guardar");
            guardar.Clicked += (sender, e) => {
                string nombre = nombreEntry.Text;
                string apellido = apellidoEntry.Text;
                string correo = correoEntry.Text;
                string contrasenia = contraseniaEntry.Text;
                int id = int.Parse(idEntry.Text);
                ListasGlobales.listaUsuarios.Insertar(id, nombre, apellido, correo, contrasenia);
                MessageDialog dialog = new MessageDialog(ventana, 
                    DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, 
                    "Usuario guardado exitosamente");
                dialog.Run();
                dialog.Destroy();
            };
            buttonContainer.PackStart(guardar, true, true, 0);

            Button regresar = new Button("Regresar");
            regresar.Clicked += (sender, e) => {
                ingresoIndividual.Show();
                ventana.Destroy();
            };
            buttonContainer.PackStart(regresar, true, true, 0);

            ventana.ShowAll();
        }
    }
}