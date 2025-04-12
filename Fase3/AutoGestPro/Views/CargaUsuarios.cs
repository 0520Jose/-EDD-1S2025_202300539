using System;
using Gtk;

namespace AutoGestPro.Views
{
    class CargaUsuarios
    {
        public CargaUsuarios(Window ventana)
        {
            Window ventanaCarga = new Window("Carga de Usuarios");
            ventanaCarga.SetDefaultSize(500, 400);
            ventanaCarga.SetPosition(WindowPosition.Center);
            ventanaCarga.DeleteEvent += (o, args) => {
                Application.Quit();
                args.RetVal = true;
            };

            VBox contenedor = new VBox(false, 10);
            contenedor.BorderWidth = 15;
            ventanaCarga.Add(contenedor);

            Label titulo = new Label("<span size='xx-large' weight='bold'>Carga de Usuarios</span>");
            titulo.UseMarkup = true;
            contenedor.PackStart(titulo, false, false, 20);

            Table table = new Table(6, 2, false);
            table.RowSpacing = 10;
            table.ColumnSpacing = 10;
            contenedor.PackStart(table, true, true, 10);

            Label textId = new Label("ID:");
            textId.Xalign = 1;
            table.Attach(textId, 0, 1, 0, 1);

            Entry txtId = new Entry();
            table.Attach(txtId, 1, 2, 0, 1);

            Label textNombre = new Label("Nombres:");
            textNombre.Xalign = 1;
            table.Attach(textNombre, 0, 1, 1, 2);

            Entry txtNombre = new Entry();
            table.Attach(txtNombre, 1, 2, 1, 2);

            Label textApellido = new Label("Apellidos:");
            textApellido.Xalign = 1;
            table.Attach(textApellido, 0, 1, 2, 3);

            Entry txtApellido = new Entry();
            table.Attach(txtApellido, 1, 2, 2, 3);

            Label textCorreo = new Label("Correo:");
            textCorreo.Xalign = 1;
            table.Attach(textCorreo, 0, 1, 3, 4);

            Entry txtCorreo = new Entry();
            table.Attach(txtCorreo, 1, 2, 3, 4);

            Label textEdad = new Label("Edad:");
            textEdad.Xalign = 1;
            table.Attach(textEdad, 0, 1, 4, 5);

            Entry txtEdad = new Entry();
            table.Attach(txtEdad, 1, 2, 4, 5);

            Label textContrasenia = new Label("Contraseña:");
            textContrasenia.Xalign = 1;
            table.Attach(textContrasenia, 0, 1, 5, 6);

            Entry txtContrasenia = new Entry();
            txtContrasenia.Visibility = false;
            txtContrasenia.InvisibleChar = '*';
            table.Attach(txtContrasenia, 1, 2, 5, 6);

            HBox botones = new HBox(true, 10);
            contenedor.PackStart(botones, false, false, 20);

            Button cargarUsuariosButton = new Button("Insertar usuario");
            cargarUsuariosButton.WidthRequest = 150;
            cargarUsuariosButton.Clicked += (sender, e) => {
                Console.WriteLine("Cargando usuarios...");
            };
            botones.PackStart(cargarUsuariosButton, true, true, 0);

            Button regresarButton = new Button("Regresar");
            regresarButton.WidthRequest = 150;
            regresarButton.Clicked += (sender, e) => {
                ventana.Show();
                ventanaCarga.Destroy();
            };
            botones.PackStart(regresarButton, true, true, 0);

            ventanaCarga.ShowAll();
        }
    }
}
