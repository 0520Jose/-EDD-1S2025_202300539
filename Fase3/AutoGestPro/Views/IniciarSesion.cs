using System;
using Gtk;
using System.Security.Cryptography;
using System.Text;

using AutoGestPro.Models.Entidades;
using AutoGestPro.Models.Estructuras;
using AutoGestPro.Views.Usuario;

namespace AutoGestPro.Views
{
    public class IniciarSesion
    {
        bool usuario_Encontrado;
        UsuarioNodo usuario_;

        public IniciarSesion()
        {
            Application.Init();
            Window ventana = new Window("AutoGestPro");
            ventana.Opacity = 0.75;
            ventana.SetDefaultSize(400, 300);
            ventana.DeleteEvent += (o, args) => {
                Application.Quit();
                args.RetVal = true;
            };

            VBox contenedor = new VBox(false, 10);
            contenedor.BorderWidth = 20;
            ventana.Add(contenedor);

            Label titulo = new Label("<span size='xx-large' weight='bold'>Iniciar Sesión</span>");
            titulo.UseMarkup = true;
            contenedor.PackStart(titulo, false, false, 10);

            HBox usuario_Box = new HBox(false, 5);
            Label titulousuario_ = new Label("Usuario:");
            Entry txtusuario_ = new Entry();
            usuario_Box.PackStart(titulousuario_, false, false, 5);
            usuario_Box.PackStart(txtusuario_, true, true, 5);
            contenedor.PackStart(usuario_Box, false, false, 5);

            HBox contrasenaBox = new HBox(false, 5);
            Label tituloContrasena = new Label("Contraseña:");
            Entry txtContrasena = new Entry();
            txtContrasena.Visibility = false;
            contrasenaBox.PackStart(tituloContrasena, false, false, 5);
            contrasenaBox.PackStart(txtContrasena, true, true, 5);
            contenedor.PackStart(contrasenaBox, false, false, 5);

            txtContrasena.Text = "admint123";
            txtusuario_.Text = "admin@usac.com";

            Backup backup = new Backup();
            backup.CargarBackup();

            Button iniciarSesion = new Button("Iniciar Sesión");
            iniciarSesion.Clicked += (sender, e) => {
                BlockChain listaUsuarios = EstructurasGlobales.blockChain;
                usuario_Encontrado = false;
                usuario_ = listaUsuarios.Inicio;
                string contraseniaEncriptada = GetSHA256(txtContrasena.Text);
                while (usuario_ != null)
                {
                    if (usuario_.Correo == txtusuario_.Text && usuario_.Contrasenia == contraseniaEncriptada)
                    {
                        usuario_Encontrado = true;
                        break;
                    }
                    usuario_ = usuario_.Siguiente;
                }
                if (usuario_Encontrado)
                {
                    txtusuario_.Text = "";
                    txtContrasena.Text = "";
                    MessageDialog mensaje = new MessageDialog(ventana, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "Bienvenido");
                    mensaje.Run();
                    string filePath = "/home/emanuel/Escritorio/Proyectos/-EDD-Proyecto_202300539/-EDD-Proyecto_202300539/Fase3/AutoGestPro/Logueos.json";
                    string userData;

                    if (System.IO.File.Exists(filePath))
                    {
                        string existingData = System.IO.File.ReadAllText(filePath);
                        var existingUsers = System.Text.Json.JsonSerializer.Deserialize<List<dynamic>>(existingData) ?? new List<dynamic>();
                        existingUsers.Add(new 
                        { 
                            usuario = usuario_.Correo,
                            entrada = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                        });
                        userData = System.Text.Json.JsonSerializer.Serialize(existingUsers);
                    }
                    else
                    {
                        var newUserList = new List<dynamic> { new 
                        { 
                            usuario = usuario_.Correo,
                            entrada = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                        } };
                        userData = System.Text.Json.JsonSerializer.Serialize(newUserList);
                    }

                    System.IO.File.WriteAllText(filePath, userData);
                    MenuUsuario menuUsuario= new MenuUsuario(ventana, usuario_);
                    ventana.Hide();
                    mensaje.Destroy();
                }
                else if (txtusuario_.Text == "admin@usac.com" && txtContrasena.Text == "admint123")
                {
                    txtusuario_.Text = "";
                    txtContrasena.Text = "";
                    MessageDialog mensaje = new MessageDialog(ventana, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "Bienvenido");
                    mensaje.Run();
                    Menu menu = new Menu(ventana);
                    ventana.Hide();
                    mensaje.Destroy();
                }
                else
                {
                    MessageDialog mensaje = new MessageDialog(ventana, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "Usuario o contraseña incorrectos");
                    mensaje.Run();
                    mensaje.Destroy();
                    txtusuario_.Text = "";
                    txtContrasena.Text = "";
                }
            };
            contenedor.PackStart(iniciarSesion, false, false, 20);

            ventana.ShowAll();
            Application.Run();
        }

        public static string GetSHA256(string str)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder sb = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(str));
            for (int i = 0; i < stream.Length; i++) sb.AppendFormat("{0:x2}", stream[i]);
            return sb.ToString();
        }
    }
}