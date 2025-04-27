using System;
using AutoGestPro.Models.Estructuras;
using AutoGestPro.Models;
using AutoGestPro.Models.Entidades;

namespace  AutoGestPro.Views
{
    public class Backup
    {
        public void GenerarBackup()
        {
            BlockChain usuarios = EstructurasGlobales.blockChain;

            var usuariosData = usuarios.ObtenerTodosLosUsuarios();
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(usuariosData, Newtonsoft.Json.Formatting.Indented);

            string filePath = "/home/emanuel/Escritorio/Proyectos/-EDD-Proyecto_202300539/-EDD-Proyecto_202300539/Fase3/AutoGestPro/Backup/usuarios.json";
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }
            File.WriteAllText(filePath, json);



            ListaDoble vehiculos = EstructurasGlobales.listaVehiculos;
            ArbolAVL repuestos = EstructurasGlobales.arbolRepuestos;

            string vehiculosTexto = vehiculos.ObtenerTexto();
            string repuestosTexto = repuestos.ObtenerTexto();

            var (vehiculosComprimidos, raizVehiculos) = HuffmanCompression.CompressWithTree(vehiculosTexto);
            var (repuestosComprimidos, raizRepuestos) = HuffmanCompression.CompressWithTree(repuestosTexto);

            string vehiculosFilePath = "/home/emanuel/Escritorio/Proyectos/-EDD-Proyecto_202300539/-EDD-Proyecto_202300539/Fase3/AutoGestPro/Backup/vehiculos.edd";
            string repuestosFilePath = "/home/emanuel/Escritorio/Proyectos/-EDD-Proyecto_202300539/-EDD-Proyecto_202300539/Fase3/AutoGestPro/Backup/repuestos.edd";

            var vehiculosData = new
            {
                Comprimido = vehiculosComprimidos,
                Arbol = raizVehiculos
            };

            var repuestosData = new
            {
                Comprimido = repuestosComprimidos,
                Arbol = raizRepuestos
            };

            string vehiculosJson = Newtonsoft.Json.JsonConvert.SerializeObject(vehiculosData, Newtonsoft.Json.Formatting.Indented);
            string repuestosJson = Newtonsoft.Json.JsonConvert.SerializeObject(repuestosData, Newtonsoft.Json.Formatting.Indented);

            if (File.Exists(vehiculosFilePath))
            {
                File.Delete(vehiculosFilePath);
            }
            File.WriteAllText(vehiculosFilePath, vehiculosJson);

            if (File.Exists(repuestosFilePath))
            {
                File.Delete(repuestosFilePath);
            }
            File.WriteAllText(repuestosFilePath, repuestosJson);

        }

        public void CargarBackup()
        {
            string filePath = "/home/emanuel/Escritorio/Proyectos/-EDD-Proyecto_202300539/-EDD-Proyecto_202300539/Fase3/AutoGestPro/Backup/usuarios.json";
            string vehiculosFilePath = "/home/emanuel/Escritorio/Proyectos/-EDD-Proyecto_202300539/-EDD-Proyecto_202300539/Fase3/AutoGestPro/Backup/vehiculos.edd";
            string repuestosFilePath = "/home/emanuel/Escritorio/Proyectos/-EDD-Proyecto_202300539/-EDD-Proyecto_202300539/Fase3/AutoGestPro/Backup/repuestos.edd";

            if (File.Exists(filePath))
            {
                string json = File.ReadAllText(filePath);
                var usuariosData = Newtonsoft.Json.JsonConvert.DeserializeObject<List<UsuarioNodo>>(json);
                if (usuariosData.Count > 0)
                {
                    string hashAnterior = "00000";
                    foreach (var usuario in usuariosData)
                    {
                        if (usuario.HashAnterior != hashAnterior)
                        {
                            using (var dialog = new Gtk.MessageDialog(
                                null,
                                Gtk.DialogFlags.Modal,
                                Gtk.MessageType.Error,
                                Gtk.ButtonsType.Ok,
                                "Error: La cadena de bloques está corrupta. No se puede cargar el backup."))
                            {
                                dialog.Run();
                                dialog.Destroy();
                            }
                            return;
                        }
                        hashAnterior = usuario.Hash;
                    }
                }
                EstructurasGlobales.blockChain.CargarUsuarios(usuariosData);
            }

            if (File.Exists(vehiculosFilePath))
            {
                string json = File.ReadAllText(vehiculosFilePath);
                var vehiculosData = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
                var vehiculosComprimidos = vehiculosData["Comprimido"].ToString();
                var raizVehiculos = Newtonsoft.Json.JsonConvert.DeserializeObject<HuffmanNode>(vehiculosData["Arbol"].ToString());
                var vehiculosDescomprimidos = HuffmanCompression.Descomprimir(vehiculosComprimidos, raizVehiculos);
                EstructurasGlobales.listaVehiculos.CargarDesdeTexto(vehiculosDescomprimidos);
            }

            if (File.Exists(repuestosFilePath))
            {
                string json = File.ReadAllText(repuestosFilePath);
                var repuestosData = Newtonsoft.Json.JsonConvert.DeserializeObject<Dictionary<string, object>>(json);
                var repuestosComprimidos = repuestosData["Comprimido"].ToString();
                var raizRepuestos = Newtonsoft.Json.JsonConvert.DeserializeObject<HuffmanNode>(repuestosData["Arbol"].ToString());
                var repuestosDescomprimidos = HuffmanCompression.Descomprimir(repuestosComprimidos, raizRepuestos);
                EstructurasGlobales.arbolRepuestos.CargarDesdeTexto(repuestosDescomprimidos);
            }
        }
    }
}