using System;
using AutoGestPro.Models;
using AutoGestPro.Models.Entidades;
using AutoGestPro.Models.Estructuras;
using Gtk;

namespace AutoGestPro.Views
{
    class VerUsuario
    {
        public VerUsuario(Window gestionUsuarios)
        {
            Window ventana = new Window("Gestion de usuarios - Root");
            ventana.SetDefaultSize(600, 400);
            ventana.SetPosition(WindowPosition.Center);
            ventana.DeleteEvent += delegate { Application.Quit(); };

            VBox contenedor = new VBox(false, 10);
            ventana.Add(contenedor);

            Label titulo = new Label("<b>Visualizar usuarios</b>");
            titulo.UseMarkup = true;
            titulo.Justify = Justification.Center;
            contenedor.PackStart(titulo, false, false, 10);

            Table table = new Table(4, 3, false);
            table.ColumnSpacing = 10;
            table.RowSpacing = 10;
            contenedor.PackStart(table, true, true, 10);

            Label idLabel = new Label("ID:");
            table.Attach(idLabel, 0, 1, 0, 1);

            Entry idEntry = new Entry();
            table.Attach(idEntry, 1, 2, 0, 1);

            Button buscar = new Button("Buscar");
            table.Attach(buscar, 2, 3, 0, 1);

            Label nombre = new Label("Nombres:");
            table.Attach(nombre, 0, 1, 1, 2);

            Label nombreActual = new Label("Null");
            table.Attach(nombreActual, 1, 2, 1, 2);

            Label apellido = new Label("Apellidos:");
            table.Attach(apellido, 0, 1, 2, 3);

            Label apellidoActual = new Label("Null");
            table.Attach(apellidoActual, 1, 2, 2, 3);

            Label correo = new Label("Correo:");
            table.Attach(correo, 0, 1, 3, 4);

            Label correoActual = new Label("Null");
            table.Attach(correoActual, 1, 2, 3, 4);

            Label edad = new Label("Edad:");
            table.Attach(edad, 0, 1, 4, 5);

            Label edadActual = new Label("Null");
            table.Attach(edadActual, 1, 2, 4, 5);

            Label contrasenia = new Label("Contraseña:");
            table.Attach(contrasenia, 0, 1, 5, 6);

            Label contraseniaActual = new Label("Null");
            table.Attach(contraseniaActual, 1, 2, 5, 6);

            Label hash = new Label("Hash:");
            table.Attach(hash, 0, 1, 6, 7);

            Label hashActual = new Label("Null");
            table.Attach(hashActual, 1, 2, 6, 7);

            Label hashAnterior = new Label("Hash Anterior:");
            table.Attach(hashAnterior, 0, 1, 7, 8);

            Label hashAnteriorActual = new Label("Null");
            table.Attach(hashAnteriorActual, 1, 2, 7, 8);

            Label nonce = new Label("Nonce:");
            table.Attach(nonce, 0, 1, 8, 9);

            Label nonceActual = new Label("Null");
            table.Attach(nonceActual, 1, 2, 8, 9);

            Label index = new Label("Index:");
            table.Attach(index, 0, 1, 9, 10);

            Label indexActual = new Label("Null");
            table.Attach(indexActual, 1, 2, 9, 10);

            Label fecha = new Label("Fecha:");
            table.Attach(fecha, 0, 1, 10, 11);

            Label fechaActual = new Label("Null");
            table.Attach(fechaActual, 1, 2, 10, 11);

            buscar.Clicked += (sender, e) => {
                int id = int.Parse(idEntry.Text);
                UsuarioNodo usuario = EstructurasGlobales.blockChain.BuscarPorId(id);
                if (usuario != null)
                {
                    nombreActual.Text = usuario.Nombres;
                    apellidoActual.Text = usuario.Apellidos;
                    correoActual.Text = usuario.Correo;
                    edadActual.Text = usuario.Edad.ToString();
                    contraseniaActual.Text = usuario.Contrasenia;
                    hashActual.Text = usuario.Hash;
                    hashAnteriorActual.Text = usuario.HashAnterior;
                    nonceActual.Text = usuario.Nonce.ToString();
                    indexActual.Text = usuario.Index.ToString();
                    fechaActual.Text = usuario.Fecha;
                    idEntry.Text = usuario.Id.ToString();
                }
                else
                {
                    nombreActual.Text = "Usuario no encontrado";
                    apellidoActual.Text = "Usuario no encontrado";
                    correoActual.Text = "Usuario no encontrado";
                    edadActual.Text = "Usuario no encontrado";
                    contraseniaActual.Text = "Usuario no encontrado";
                    hashActual.Text = "Usuario no encontrado";
                    hashAnteriorActual.Text = "Usuario no encontrado";
                    nonceActual.Text = "Usuario no encontrado";
                    indexActual.Text = "Usuario no encontrado";
                    fechaActual.Text = "Usuario no encontrado";
                    idEntry.Text = "";
                }
            };

            HBox buttonContainer = new HBox(true, 10);
            contenedor.PackStart(buttonContainer, false, false, 10);

            Button regresar = new Button("Regresar");
            regresar.Clicked += (sender, e) => {
                gestionUsuarios.Show();
                ventana.Destroy();
            };
            contenedor.PackStart(regresar, false, false, 10);

            ventana.ShowAll();
        }
    }
}