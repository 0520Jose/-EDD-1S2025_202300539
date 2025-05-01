# Manual técnico

- Autor: José Emanuel Monzón Lémus  
- Carnet: 202300539  
- Curso: Estructuras de datos  
- Repositorio: https://github.com/0520Jose/-EDD-Proyecto_202300539.git

## Introducción  

El presente manual cuenta con la función de informar sobre el proceso de desarrollo del proyecto AutoGestPro del curso Estructuras de Datos. En el presente se muestran todas las estructuras y objetos utilizados en el lenguaje C# para obtener un correcto funcionamiento del programa propuesto.  

## Objetivos  

### General  
+ Presentar y explicar el funcionamiento del código para el proyecto AutoGestPro.  

### Específicos  
+ Mostrar los módulos y estructuras utilizadas para el proyecto de Estructuras de Datos.  
+ Mostrar el funcionamiento de las distintas ventanas de acción con las que cuenta el usuario.  

## Entidades  

Las diferentes entidades que serán utilizadas para las distintas funcionalidades del proyecto AutoGestPro, el cual cuenta con la función de administrar un taller mecánico, fueron creadas para cubrir las necesidades que surgieron para el taller, siendo esta su primera fase.  

---

### Usuario  

Entidad que representa a los usuarios registrados en el sistema.  

#### Código:  
```csharp
using System;
using System.Text;
using System.Security.Cryptography;

namespace AutoGestPro.Models.Entidades
{
    public class UsuarioNodo
    {
        public int Index;
        public string Fecha;
        public int Id;
        public string Nombres;
        public string Apellidos;
        public string Correo;
        public int Edad;
        public string Contrasenia;
        public string HashAnterior;
        public string Hash;
        public int Nonce;
        public UsuarioNodo Siguiente;
        public UsuarioNodo Anterior;

        public string GenerateHash()
        {
            string data = $"{Index}{Fecha}{Id}{Nombres}{Apellidos}{Correo}{Edad}{Contrasenia}{Nonce}{HashAnterior}";
            byte[] bytes = Encoding.UTF8.GetBytes(data);
            byte[] hashBytes = SHA256.HashData(bytes);
            return "0000" + BitConverter.ToString(hashBytes).Replace("-", "").ToLower();
        }

        public void MineBlock()
        {
            int dificultad = 0;
            string target = new string('0', dificultad);
            while (!Hash.StartsWith(target))
            {
                Nonce++;
                Hash = GenerateHash();
            }
        }
    }
}
```

#### Explicación:  
- **Propiedades**:  
  - `Index`: Índice del nodo en la lista.  
  - `Fecha`: Fecha de creación del nodo.  
  - `Id`: Identificador único del usuario.  
  - `Nombres` y `Apellidos`: Información personal del usuario.  
  - `Correo`: Dirección de correo electrónico del usuario.  
  - `Edad`: Edad del usuario.  
  - `Contrasenia`: Contraseña del usuario.  
  - `HashAnterior`: Hash del nodo anterior en la lista.  
  - `Hash`: Hash actual del nodo.  
  - `Nonce`: Valor utilizado para minar el bloque.  
  - `Siguiente` y `Anterior`: Referencias al siguiente y anterior nodo en la lista.  

- **Métodos**:  
  - `GenerateHash`: Genera el hash del nodo utilizando el algoritmo SHA-256.  
  - `MineBlock`: Realiza el proceso de minería para encontrar un hash que cumpla con la dificultad establecida.  

Esta entidad implementa un sistema de minería y generación de hashes para garantizar la integridad de los datos en la lista de usuarios.

### Vehículo  

Entidad que representa los vehículos registrados en el sistema.  

#### Código:  
```csharp
using System;

namespace AutoGestPro.Models.Entidades
{
    public unsafe struct Vehiculo
    {
        public int Id { get; set; }
        public int Id_Usuario { get; set; }
        public string Marca { get; set; }   
        public string Modelo { get; set; }
        public string Placa { get; set; }
        public Vehiculo* siguiente;
        public Vehiculo* anterior;
        
        public Vehiculo(int id, int id_usuario, string marca, string modelo, string placa)
        {
            Id = id;
            Id_Usuario = id_usuario;
            Marca = marca;
            Modelo = modelo;
            Placa = placa;
        }
    }
}
```

#### Explicación:  
- **Propiedades**:  
  - `Id`: Identificador único del vehículo.  
  - `Id_Usuario`: Relación con el usuario propietario del vehículo.  
  - `Marca` y `Modelo`: Información del fabricante y modelo del vehículo.  
  - `Placa`: Matrícula del vehículo.  
  - `siguiente` y `anterior`: Referencias al siguiente y anterior nodo en la lista doble.  
- **Constructor**: Inicializa un nuevo vehículo con los datos proporcionados.  

---

### Servicio  

Entidad que representa los servicios realizados en el taller.  

#### Código:  
```csharp
using System;

namespace AutoGestPro.Models.Entidades
{
    unsafe struct Servicio
    {
        public int Id { get; set; }
        public int Id_Repuesto { get; set; }
        public int Id_Vehiculo { get; set; }
        public string Detalles { get; set; }
        public double Costo { get; set; }
        public Servicio(int id, int idRepuesto, int idVehiculo, string detalles, double costo)
        {
            Id = id;
            Id_Repuesto = idRepuesto;
            Id_Vehiculo = idVehiculo;
            Detalles = detalles;
            Costo = costo;
        }
    }
}
```

#### Explicación:  
- **Propiedades**:  
  - `Id`: Identificador único del servicio.  
  - `Id_Repuesto`: Relación con el repuesto utilizado en el servicio.  
  - `Id_Vehiculo`: Relación con el vehículo al que se le realizó el servicio.  
  - `Detalles`: Descripción del servicio realizado.  
  - `Costo`: Costo total del servicio.  
- **Constructor**: Inicializa un nuevo servicio con los datos proporcionados.  

---

### Repuesto  

Entidad que representa los repuestos disponibles en el taller.  

#### Código:  
```csharp
using System;

namespace AutoGestPro.Models.Entidades
{
    public class Repuesto
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Detalle { get; set; }   
        public float Costo { get; set; }

        public Repuesto(int id, string nombre, string detalle, float costo)
        {
            Id = id;
            Nombre = nombre;
            Detalle = detalle;
            Costo = costo;
        }
    }
}
```

#### Explicación:  
- **Propiedades**:  
  - `Id`: Identificador único del repuesto.  
  - `Nombre`: Nombre del repuesto.  
  - `Detalle`: Descripción del repuesto.  
  - `Costo`: Costo del repuesto.  
- **Constructor**: Inicializa un nuevo repuesto con los datos proporcionados.  

Esta entidad permite gestionar los repuestos utilizados en los servicios del taller, almacenando información relevante como su nombre, descripción y costo.

### Factura  

Entidad que representa las facturas generadas en el sistema.  

#### Código:  
```csharp
using System;
using System.Net.Http.Json;
using System.Security.Cryptography;
using System.Text;

namespace AutoGestPro.Models.Entidades
{
    public class Factura
    {
        public int Id { get; set; }
        public int Id_Servicio { get; set; }
        public double Total { get; set; }
        public string Fecha { get; set; }
        public string MetodoDePago { get; set; }

        public Factura(int id, int idServicio, double total, string metodoDePago)
        {
            Id = id;
            Id_Servicio = idServicio;
            Total = total;
            Fecha = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            MetodoDePago = metodoDePago;
        }

        public string GetHash()
        {
            string data = System.Text.Json.JsonSerializer.Serialize(this);
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(data));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
```

#### Explicación:  
- **Propiedades**:  
  - `Id`: Identificador único de la factura.  
  - `Id_Servicio`: Relación con el servicio asociado a la factura.  
  - `Total`: Monto total de la factura.  
  - `Fecha`: Fecha y hora de emisión de la factura.  
  - `MetodoDePago`: Método de pago utilizado para la factura.  

- **Constructor**:  
  - Inicializa una nueva factura con los datos proporcionados y asigna la fecha actual automáticamente.  

- **Métodos**:  
  - `GetHash`: Genera un hash único para la factura utilizando el algoritmo SHA-256, garantizando la integridad de los datos.  

Esta entidad permite gestionar las facturas generadas en el sistema, almacenando información relevante como el servicio asociado, el monto total, la fecha de emisión y el método de pago.

---

## Estructuras de Datos

En esta sección se describen las estructuras de datos utilizadas en el proyecto AutoGestPro para gestionar la información de manera eficiente.

---

### BlockChain
La estructura de datos **BlockChain** implementada en el proyecto AutoGestPro permite gestionar una lista doblemente enlazada de usuarios, donde cada nodo representa un bloque en la cadena. Esta estructura asegura la integridad de los datos mediante el uso de hashes generados con el algoritmo SHA-256 y un proceso de minería para validar los bloques.

#### Código:
```csharp
using System;
using System.Text;
using AutoGestPro.Models.Entidades;
using System.Security.Cryptography;

namespace AutoGestPro.Models.Estructuras
{
    public class BlockChain
    {
        public UsuarioNodo Inicio { get; set; }
        public UsuarioNodo Fin { get; set; }
        public int Tamanio { get; set; }

        public BlockChain()
        {
            Inicio = null;
            Fin = null;
            Tamanio = 0;
        }

        public void Insertar(int id, string nombres, string apellidos, string correo, int edad, string contrasenia)
        {
            string contraseniaEncriptada = GetSHA256(contrasenia);
            var nuevoUsuario = new UsuarioNodo
            {
                Index = Tamanio,
                Id = id,
                Nombres = nombres,
                Apellidos = apellidos,
                Correo = correo,
                Edad = edad,
                Contrasenia = contraseniaEncriptada,
                Fecha = DateTime.Now.ToString("dd-MM-yy::HH:mm:ss"),
                Nonce = 0
            };

            if (Inicio == null)
            {
                nuevoUsuario.HashAnterior = "00000";
            }
            else
            {
                nuevoUsuario.HashAnterior = Fin.Hash;
            }

            nuevoUsuario.Hash = nuevoUsuario.GenerateHash();

            if (Inicio == null)
            {
                Inicio = nuevoUsuario;
                Fin = nuevoUsuario;
            }
            else
            {
                Fin.Siguiente = nuevoUsuario;
                nuevoUsuario.Anterior = Fin;
                Fin = nuevoUsuario;
            }
            Tamanio++;
        }

        public void Minar(int id)
        {
            var usuarioActual = Inicio;
            while (usuarioActual != null)
            {
                if (usuarioActual.Id == id)
                {
                    usuarioActual.MineBlock();
                    break;
                }
                usuarioActual = usuarioActual.Siguiente;
            }
        }

        public void MinarTodo()
        {
            if (Inicio == null) return;

            try
            {
                UsuarioNodo actual = Inicio;
                actual.HashAnterior = "00000";
                actual.Nonce = 0;
                actual.MineBlock();

                UsuarioNodo anterior = actual;
                actual = actual.Siguiente;

                while (actual != null)
                {
                    actual.HashAnterior = anterior.Hash;
                    actual.Nonce = 0;
                    actual.MineBlock();

                    anterior = actual;
                    actual = actual.Siguiente;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al minar la cadena de bloques: " + ex.Message);
            }
        }




        public UsuarioNodo BuscarPorId(int id)
        {
            var usuarioActual = Inicio;
            while (usuarioActual != null)
            {
                if (usuarioActual.Id == id)
                {
                    return usuarioActual;
                }
                usuarioActual = usuarioActual.Siguiente;
            }
            return null;
        }

        public List<UsuarioNodo> ObtenerTodosLosUsuarios()
        {
            var usuarios = new List<UsuarioNodo>();
            var usuarioActual = Inicio;

            while (usuarioActual != null)
            {
                usuarios.Add(new UsuarioNodo
                {
                    Index = usuarioActual.Index,
                    Id = usuarioActual.Id,
                    Nombres = usuarioActual.Nombres,
                    Apellidos = usuarioActual.Apellidos,
                    Correo = usuarioActual.Correo,
                    Edad = usuarioActual.Edad,
                    Contrasenia = usuarioActual.Contrasenia,
                    Hash = usuarioActual.Hash,
                    Fecha = usuarioActual.Fecha,
                    Nonce = usuarioActual.Nonce,
                    HashAnterior = usuarioActual.HashAnterior,
                });

                usuarioActual = usuarioActual.Siguiente;
            }

            return usuarios;
        }

        public void CargarUsuarios(List<UsuarioNodo> usuarios)
        {
            Inicio = null;
            Fin = null;
            Tamanio = 0;

            foreach (var usuario in usuarios)
            {
                var nuevoNodo = new UsuarioNodo
                {
                    Index = usuario.Index,
                    Id = usuario.Id,
                    Nombres = usuario.Nombres,
                    Apellidos = usuario.Apellidos,
                    Correo = usuario.Correo,
                    Contrasenia = usuario.Contrasenia,
                    Edad = usuario.Edad,
                    Hash = usuario.Hash,
                    Fecha = usuario.Fecha,
                    Nonce = usuario.Nonce,
                    HashAnterior = usuario.HashAnterior
                };

                if (Inicio == null)
                {
                    Inicio = nuevoNodo;
                    Fin = nuevoNodo;
                }
                else
                {
                    Fin.Siguiente = nuevoNodo;
                    Fin = nuevoNodo;
                }

                Tamanio++;
            }
        }

        public bool EliminarBloque(int index)
        {
            if (index == 0) return false;

            var usuarioActual = Inicio;
            while (usuarioActual != null)
            {
                if (usuarioActual.Index == index)
                {
                    if (usuarioActual.Anterior != null)
                        usuarioActual.Anterior.Siguiente = usuarioActual.Siguiente;
                    if (usuarioActual.Siguiente != null)
                        usuarioActual.Siguiente.Anterior = usuarioActual.Anterior;
                    if (usuarioActual == Fin)
                        Fin = usuarioActual.Anterior;

                    recalcularChain(usuarioActual.Anterior, Fin);
                    Tamanio--;
                    return true;
                }
                usuarioActual = usuarioActual.Siguiente;
            }
            return false;
        }

        public void recalcularChain(UsuarioNodo usuario, UsuarioNodo fin)
        {
            var usuarioActual = usuario != null ? usuario.Siguiente : fin;
            var anterior = usuario;
            int index = usuario != null ? usuario.Index + 1 : 0;

            while (usuarioActual != null)
            {
                usuarioActual.Index = index;
                usuarioActual.HashAnterior = anterior != null ? anterior.Hash : "00000";
                usuarioActual.Hash = usuarioActual.GenerateHash();
                anterior = usuarioActual;
                usuarioActual = usuarioActual.Siguiente;
                index++;
            }
        }

        public void imprimir()
        {
            try
            {
                var usuarioActual = Inicio;
                while (usuarioActual != null)
                {
                    Console.WriteLine($"ID: {usuarioActual.Id}, Nombre: {usuarioActual.Nombres} {usuarioActual.Apellidos}, Correo: {usuarioActual.Correo}, Edad: {usuarioActual.Edad}, Hash: {usuarioActual.Hash}");
                    usuarioActual = usuarioActual.Siguiente;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error al imprimir la cadena de bloques: " + e.Message);
            }
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

        public string GenerarDot(int id)
        {
            var dot = new StringBuilder();
            dot.AppendLine("digraph G {");
            dot.AppendLine("node [shape=record];");
            dot.AppendLine("rankdir=TB;");
            dot.AppendLine("node [height=0.5];");
            dot.AppendLine("node [width=0.5];");
            dot.AppendLine("node [style=filled];");
            dot.AppendLine("node [fillcolor=\"#EEEEEE\"];");
            dot.AppendLine("node [fontname=\"Arial\"];");
            dot.AppendLine("edge [fontname=\"Arial\"];");
            dot.AppendLine("edge [fontsize=8];");
            dot.AppendLine("edge [fontcolor=\"#333333\"];");
            dot.AppendLine("edge [labelfloat=false];");
            dot.AppendLine("edge [decorate=true];");
            dot.AppendLine("edge [style=\"solid\"];");
            dot.AppendLine("edge [color=\"#333333\"];");
            dot.AppendLine("edge [dir=\"forward\"];");
            dot.AppendLine("edge [arrowhead=\"normal\"];");
            dot.AppendLine("edge [arrowsize=\"0.5\"];");
            dot.AppendLine("edge [arrowtail=\"normal\"];");
            dot.AppendLine("edge [taillabel=\"\"];");
            dot.AppendLine("edge [headlabel=\"\"];");
            dot.AppendLine("edge [label=\"\"];");
            dot.AppendLine("edge [weight=\"1\"];");

            var usuarioActual = Inicio;
            while (usuarioActual != null)
            {
                if (usuarioActual.Id == id)
                {   
                    string Id = id.ToString();
                    string id_ = Id.Replace("{", "\\{").Replace("}", "\\}").Replace("\"", "\\\"");
                    string nombres = usuarioActual.Nombres.Replace("{", "\\{").Replace("}", "\\}").Replace("\"", "\\\"");
                    string apellidos = usuarioActual.Apellidos.Replace("{", "\\{").Replace("}", "\\}").Replace("\"", "\\\"");
                    string correo = usuarioActual.Correo.Replace("{", "\\{").Replace("}", "\\}").Replace("\"", "\\\"");
                    string contrasenia = usuarioActual.Contrasenia.Replace("{", "\\{").Replace("}", "\\}").Replace("\"", "\\\"");
                    string hashAnterior = usuarioActual.HashAnterior.Replace("{", "\\{").Replace("}", "\\}").Replace("\"", "\\\"");
                    string hash = usuarioActual.Hash.Replace("{", "\\{").Replace("}", "\\}").Replace("\"", "\\\"");

                    dot.AppendLine($"\"{usuarioActual.Index}\" [label=\"{{");
                    dot.AppendLine($"INDEX: {usuarioActual.Index}\\l");
                    dot.AppendLine($"TIMESTAMP: {usuarioActual.Fecha}\\l");
                    dot.AppendLine($"DATA: \\{{ID: {id_}, NOMBRE: {nombres}, APELLIDO: {apellidos}, CORREO: {correo}, EDAD: {usuarioActual.Edad}, CONTRASEÑA: {contrasenia}\\}}\\l");
                    dot.AppendLine($"NONCE: {usuarioActual.Nonce}\\l");
                    dot.AppendLine($"PREVIOUS HASH: {hashAnterior}\\l");
                    dot.AppendLine($"HASH: {hash}\\l");
                    dot.AppendLine("}\"]");
                    break;
                }
                usuarioActual = usuarioActual.Siguiente;
            }

            dot.AppendLine("}");
            return dot.ToString();
        }

    }
}
```

#### Explicación:
- **Propiedades**:
    - `Inicio` y `Fin`: Referencias al primer y último nodo de la cadena.
    - `Tamanio`: Número total de bloques en la cadena.

- **Métodos principales**:
    - `Insertar`: Agrega un nuevo bloque a la cadena, generando su hash y enlazándolo con el bloque anterior.
    - `Minar`: Realiza el proceso de minería para un bloque específico, ajustando su `Nonce` hasta cumplir con la dificultad.
    - `MinarTodo`: Minería de todos los bloques en la cadena, recalculando los hashes en caso de cambios.
    - `BuscarPorId`: Busca un bloque en la cadena por su identificador único.
    - `EliminarBloque`: Elimina un bloque de la cadena y recalcula los hashes de los bloques posteriores.
    - `ObtenerTodosLosUsuarios`: Devuelve una lista con todos los bloques de la cadena.
    - `CargarUsuarios`: Carga una lista de bloques en la cadena.
    - `imprimir`: Muestra los datos de todos los bloques en la consola.
    - `GenerarDot`: Genera un archivo DOT para visualizar la cadena de bloques en formato gráfico.

Esta implementación asegura que cualquier modificación en un bloque invalide los hashes de los bloques posteriores, garantizando la seguridad e integridad de los datos almacenados.


---

### Lista Doble

La estructura de datos **Lista Doble** implementada en el proyecto AutoGestPro permite gestionar una lista doblemente enlazada de vehículos. Esta estructura facilita la inserción, eliminación y búsqueda de elementos, además de permitir la generación de un archivo DOT para su visualización gráfica.

#### Código:
```csharp
using System;
using System.Runtime.InteropServices;
using AutoGestPro.Models.Entidades;
unsafe class ListaDoble
{
    public Vehiculo* inicio = null;
    public int tamanio = 0;

    public void Insertar(int id, int IdUsuario, string marca, string modelo, string placa)
    {
        Vehiculo* nuevoVehiculo = (Vehiculo*)NativeMemory.Alloc((nuint)sizeof(Vehiculo));
        nuevoVehiculo->Id = id;
        nuevoVehiculo->Id_Usuario = IdUsuario;
        nuevoVehiculo->Marca = marca;
        nuevoVehiculo->Modelo = modelo;
        nuevoVehiculo->Placa = placa;
        nuevoVehiculo->siguiente = null;
        nuevoVehiculo->anterior = null;

        if (inicio == null)
        {
            inicio = nuevoVehiculo;
        }
        else
        {
            Vehiculo* actual = inicio;
            Vehiculo* anterior = null;

            while (actual != null && actual->Id < id)
            {
                anterior = actual;
                actual = actual->siguiente;
            }

            if (anterior == null)
            {
                nuevoVehiculo->siguiente = inicio;
                inicio->anterior = nuevoVehiculo;
                inicio = nuevoVehiculo;
            }
            else
            {
                nuevoVehiculo->siguiente = actual;
                nuevoVehiculo->anterior = anterior;
                anterior->siguiente = nuevoVehiculo;
                if (actual != null)
                {
                    actual->anterior = nuevoVehiculo;
                }
            }
        }
        tamanio++;
    }

    public Vehiculo buscarVehiculo(int id)
    {
        Vehiculo* vehiculoActual = inicio;
        while (vehiculoActual != null)
        {
            if (vehiculoActual->Id == id)
            {
                return *vehiculoActual;
            }
            vehiculoActual = vehiculoActual->siguiente;
        }
        return new Vehiculo();
    }

    public void CargarDesdeTexto(string texto)
    {
        string[] lineas = texto.Split(new[] { '\n' }, StringSplitOptions.RemoveEmptyEntries);
        foreach (string linea in lineas)
        {
            string[] partes = linea.Split(',');
            if (partes.Length == 5)
            {
                int id = int.Parse(partes[0]);
                int idUsuario = int.Parse(partes[1]);
                string marca = partes[2];
                string modelo = partes[3];
                string placa = partes[4];

                Insertar(id, idUsuario, marca, modelo, placa);
            }
        }
    }

    public string ObtenerTexto()
    {
        string texto = "";
        Vehiculo* vehiculoActual = inicio;
        while (vehiculoActual != null)
        {
            texto += $"{vehiculoActual->Id},{vehiculoActual->Id_Usuario},{vehiculoActual->Marca},{vehiculoActual->Modelo},{vehiculoActual->Placa}\n";
            vehiculoActual = vehiculoActual->siguiente;
        }
        return texto;
    }

    public void eliminarVehiculo(int id)
    {
        Vehiculo* vehiculoActual = inicio;
        while (vehiculoActual != null)
        {
            if (vehiculoActual->Id == id)
            {
                if (vehiculoActual->anterior != null)
                {
                    vehiculoActual->anterior->siguiente = vehiculoActual->siguiente;
                }
                else
                {
                    inicio = vehiculoActual->siguiente;
                }
                if (vehiculoActual->siguiente != null)
                {
                    vehiculoActual->siguiente->anterior = vehiculoActual->anterior;
                }

                NativeMemory.Free(vehiculoActual);
                tamanio--;
                return;
            }
            vehiculoActual = vehiculoActual->siguiente;
        }
    }

    public string GenerarDot()
    {
        string dot = "digraph G {\n";
        dot += "node [shape=record];\n";
        dot += "rankdir=LR;\n";

        Vehiculo* vehiculoActual = inicio;
        while (vehiculoActual != null)
        {
            dot += $"\"{vehiculoActual->Id}\" [label=\"{{Id: {vehiculoActual->Id} | Id Usuario: {vehiculoActual->Id_Usuario} | Marca: {vehiculoActual->Marca} | Modelo: {vehiculoActual->Modelo} | Placa: {vehiculoActual->Placa}}}\"];\n";
            if (vehiculoActual->siguiente != null)
            {
                dot += $"\"{vehiculoActual->Id}\" -> \"{vehiculoActual->siguiente->Id}\";\n";
                dot += $"\"{vehiculoActual->siguiente->Id}\" -> \"{vehiculoActual->Id}\";\n";
            }
            vehiculoActual = vehiculoActual->siguiente;
        }
        dot += "}";
        return dot;
    }
}
```

#### Explicación:
- **Propiedades**:
  - `inicio`: Referencia al primer nodo de la lista.
  - `tamanio`: Número total de elementos en la lista.

- **Métodos principales**:
  - `Insertar`: Agrega un nuevo vehículo a la lista en orden ascendente por su ID.
  - `buscarVehiculo`: Busca un vehículo en la lista por su ID.
  - `CargarDesdeTexto`: Carga vehículos desde un texto con formato CSV.
  - `ObtenerTexto`: Genera un texto con los datos de los vehículos en formato CSV.
  - `eliminarVehiculo`: Elimina un vehículo de la lista por su ID.
  - `GenerarDot`: Genera un archivo DOT para visualizar la lista doble en formato gráfico.

Esta implementación permite gestionar eficientemente los vehículos registrados en el sistema, asegurando un acceso rápido y una representación visual clara de la estructura.


---

### Árbol Binario

El Árbol Binario es una estructura de datos jerárquica utilizada en el proyecto AutoGestPro para gestionar los servicios realizados en el taller. Cada nodo del árbol representa un servicio, y los nodos están organizados de manera que los valores menores se encuentran en el subárbol izquierdo y los valores mayores en el subárbol derecho.

#### Código:

```csharp
using System;
using AutoGestPro.Models.Entidades;

namespace AutoGestPro.Models
{
    class NodoBinario 
    {
        public NodoBinario? Izquierdo { get; set; }
        public NodoBinario? Derecho { get; set; }
        public Servicio Servicio { get; set; }

        public NodoBinario(Servicio servicio)
        {
            Servicio = servicio;
            Izquierdo = null;
            Derecho = null;
        }
    }
}
```

```csharp
using System;
using AutoGestPro.Models.Entidades;
using System.Text;

namespace AutoGestPro.Models
{
    class ArbolBinario
    {
        public NodoBinario? Raiz { get; set; }

        public ArbolBinario()
        {
            Raiz = null;
        }

        public void Insertar(Servicio servicio)
        {
            NodoBinario nuevoNodo = new NodoBinario(servicio);
            if (Raiz == null)
            {
                Raiz = nuevoNodo;
            }
            else
            {
                NodoBinario anterior = null, reco;
                reco = Raiz;
                while (reco != null)
                {
                    anterior = reco;
                    if (servicio.Id < reco.Servicio.Id)
                    {
                        reco = reco.Izquierdo;
                    }
                    else
                    {
                        reco = reco.Derecho;
                    }
                }
                if (servicio.Id < anterior.Servicio.Id)
                {
                    anterior.Izquierdo = nuevoNodo;
                }
                else
                {
                    anterior.Derecho = nuevoNodo;
                }
            }
        }

        public bool Buscar(int id)
        {
            return BuscarRecursivo(Raiz, id);
        }

        private bool BuscarRecursivo(NodoBinario? nodo, int id)
        {
            if (nodo == null)
            {
                return false;
            }

            if (nodo.Servicio.Id == id)
            {
                return true;
            }

            if (id < nodo.Servicio.Id)
            {
                return BuscarRecursivo(nodo.Izquierdo, id);
            }
            else
            {
                return BuscarRecursivo(nodo.Derecho, id);
            }
        }

        public NodoBinario? BuscarNodo(int id)
        {
            return BuscarNodoRecursivo(Raiz, id);
        }

        private NodoBinario? BuscarNodoRecursivo(NodoBinario? nodo, int id)
        {
            if (nodo == null)
            {
                return null;
            }

            if (nodo.Servicio.Id == id)
            {
                return nodo;
            }

            if (id < nodo.Servicio.Id)
            {
                return BuscarNodoRecursivo(nodo.Izquierdo, id);
            }
            else
            {
                return BuscarNodoRecursivo(nodo.Derecho, id);
            }
        }

        public void ActualizarServicio(Servicio servicio)
        {
            ActualizarServicioRecursivo(Raiz, servicio);
        }

        private void ActualizarServicioRecursivo(NodoBinario? nodo, Servicio servicio)
        {
            if (nodo == null) return;

            if (nodo.Servicio.Id == servicio.Id)
            {
                nodo.Servicio = servicio;
                return;
            }

            if (servicio.Id < nodo.Servicio.Id)
            {
                ActualizarServicioRecursivo(nodo.Izquierdo, servicio);
            }
            else
            {
                ActualizarServicioRecursivo(nodo.Derecho, servicio);
            }
        }

        public string GenerarDot()
        {
            var dot = new StringBuilder();
            dot.AppendLine("digraph G {");
            dot.AppendLine("node [shape=record];");
            dot.AppendLine("rankdir=TB;");
            
            GenerarDotRecursivo(Raiz, dot);

            dot.AppendLine("}");
            return dot.ToString();
        }

        private void GenerarDotRecursivo(NodoBinario? nodo, StringBuilder dot)
        {
            if (nodo == null) return;

            dot.AppendLine($"n{nodo.Servicio.Id} [label=\"<f0> |<f1> ID: {nodo.Servicio.Id}\\nId repuesto: {nodo.Servicio.Id_Repuesto}\\nId vehiculo: {nodo.Servicio.Id_Vehiculo}\\nDetalles: {nodo.Servicio.Detalles}\\nCosto: {nodo.Servicio.Costo}|<f2>\"];");

            if (nodo.Izquierdo != null)
            {
                dot.AppendLine($"n{nodo.Servicio.Id} -> n{nodo.Izquierdo.Servicio.Id};");
            }

            if (nodo.Derecho != null)
            {
                dot.AppendLine($"n{nodo.Servicio.Id} -> n{nodo.Derecho.Servicio.Id};");
            }

            GenerarDotRecursivo(nodo.Izquierdo, dot);
            GenerarDotRecursivo(nodo.Derecho, dot);
        }
    }
}
```

#### Explicación:

- **Propiedades**:
  - `Raiz`: Nodo raíz del árbol binario.

- **Métodos principales**:
  - `Insertar`: Agrega un nuevo nodo al árbol, manteniendo el orden binario.
  - `Buscar`: Verifica si un servicio con un ID específico existe en el árbol.
  - `BuscarNodo`: Devuelve el nodo que contiene el servicio con el ID especificado.
  - `ActualizarServicio`: Actualiza los datos de un servicio existente en el árbol.
  - `GenerarDot`: Genera un archivo DOT para visualizar el árbol binario en formato gráfico.

Esta implementación permite gestionar los servicios de manera eficiente, facilitando la búsqueda, inserción y actualización de datos, además de proporcionar una representación visual del árbol.

---

### Árbol AVL

El Árbol AVL es una estructura de datos balanceada que garantiza un tiempo de búsqueda, inserción y eliminación eficiente. En el proyecto AutoGestPro, se utiliza para gestionar los repuestos disponibles en el taller, asegurando que la estructura permanezca equilibrada después de cada operación.

#### Código:

```csharp
using System;
using AutoGestPro.Models.Entidades;

namespace AutoGestPro.Models.Estructuras
{
    class NodoAVL
    {
        public NodoAVL? Izquierdo;
        public NodoAVL? Derecho;
        public Repuesto Repuesto;
        public int Altura;

        public NodoAVL(Repuesto repuesto)
        {
            Repuesto = repuesto;
            Izquierdo = null;
            Derecho = null;
            Altura = 1;
        }
    }
}
```

```csharp
using System;
using AutoGestPro.Models.Estructuras;
using AutoGestPro.Models.Entidades;

namespace AutoGestPro.Models
{
    class ArbolAVL
    {
        public NodoAVL Raiz { get; set; }

        public ArbolAVL()
        {
            Raiz = null;
        }

        private int getAltura(NodoAVL nodo) => nodo?.Altura ?? 0;

        private int getBalance(NodoAVL nodo) => nodo == null ? 0 : getAltura(nodo.Izquierdo) - getAltura(nodo.Derecho);

        private NodoAVL RotarDerecha(NodoAVL y)
        {
            NodoAVL x = y.Izquierdo;
            NodoAVL T2 = x.Derecho;
            x.Derecho = y;
            y.Izquierdo = T2;
            y.Altura = Math.Max(getAltura(y.Izquierdo), getAltura(y.Derecho)) + 1;
            x.Altura = Math.Max(getAltura(x.Izquierdo), getAltura(x.Derecho)) + 1;
            return x;
        }

        private NodoAVL RotarIzquierda(NodoAVL x)
        {
            NodoAVL y = x.Derecho;
            NodoAVL T2 = y.Izquierdo;
            y.Izquierdo = x;
            x.Derecho = T2;
            x.Altura = Math.Max(getAltura(x.Izquierdo), getAltura(x.Derecho)) + 1;
            y.Altura = Math.Max(getAltura(y.Izquierdo), getAltura(y.Derecho)) + 1;
            return y;
        }

        public NodoAVL Insertar(NodoAVL raiz, Repuesto repuesto)
        {
            if (raiz == null)
                return new NodoAVL(repuesto);

            if (repuesto.Id < raiz.Repuesto.Id)
                raiz.Izquierdo = Insertar(raiz.Izquierdo, repuesto);
            else if (repuesto.Id > raiz.Repuesto.Id)
                raiz.Derecho = Insertar(raiz.Derecho, repuesto);
            else
                return raiz;

            raiz.Altura = 1 + Math.Max(getAltura(raiz.Izquierdo), getAltura(raiz.Derecho));

            int balance = getBalance(raiz);

            if (balance > 1 && repuesto.Id < raiz.Izquierdo.Repuesto.Id)
                return RotarDerecha(raiz);

            if (balance < -1 && repuesto.Id > raiz.Derecho.Repuesto.Id)
                return RotarIzquierda(raiz);

            if (balance > 1 && repuesto.Id > raiz.Izquierdo.Repuesto.Id)
            {
                raiz.Izquierdo = RotarIzquierda(raiz.Izquierdo);
                return RotarDerecha(raiz);
            }

            if (balance < -1 && repuesto.Id < raiz.Derecho.Repuesto.Id)
            {
                raiz.Derecho = RotarDerecha(raiz.Derecho);
                return RotarIzquierda(raiz);
            }

            return raiz;
        }

        public string GenerarDot()
        {
            string dot = "digraph G {\n";
            dot += "node [shape=record];\n";
            dot += "rankdir=TB;\n";

            void GenerarDotRecursivo(NodoAVL nodo)
            {
                if (nodo != null)
                {
                    dot += $"nodo{nodo.Repuesto.Id} [label=\"<f0> |<f1> ID: {nodo.Repuesto.Id}\\nRepuesto: {nodo.Repuesto.Nombre}\\nDetalles: {nodo.Repuesto.Detalle}\\nCosto: {nodo.Repuesto.Costo}|<f2>\"];\n";

                    if (nodo.Izquierdo != null)
                    {
                        dot += $"nodo{nodo.Repuesto.Id}:f0 -> nodo{nodo.Izquierdo.Repuesto.Id}:f1;\n";
                        GenerarDotRecursivo(nodo.Izquierdo);
                    }

                    if (nodo.Derecho != null)
                    {
                        dot += $"nodo{nodo.Repuesto.Id}:f2 -> nodo{nodo.Derecho.Repuesto.Id}:f1;\n";
                        GenerarDotRecursivo(nodo.Derecho);
                    }
                }
            }

            GenerarDotRecursivo(Raiz);

            dot += "}";
            return dot;
        }
    }
}
```

#### Explicación:

- **Propiedades**:
  - `Raiz`: Nodo raíz del árbol AVL.

- **Métodos principales**:
  - `Insertar`: Agrega un nuevo nodo al árbol, asegurando que permanezca balanceado.
  - `RotarDerecha` y `RotarIzquierda`: Realizan rotaciones para mantener el balance del árbol.
  - `GenerarDot`: Genera un archivo DOT para visualizar el árbol AVL en formato gráfico.

Esta implementación asegura que el árbol AVL permanezca equilibrado, lo que garantiza un rendimiento óptimo para las operaciones de búsqueda, inserción y eliminación.

---

### Árbol Merkle

El Árbol Merkle es una estructura de datos utilizada para garantizar la integridad y consistencia de los datos. En el proyecto AutoGestPro, se emplea para gestionar las facturas generadas en el sistema, permitiendo verificar la autenticidad de cada factura mediante el uso de hashes.

#### Código:

```csharp
using System;
using AutoGestPro.Models.Entidades;
using System.Security.Cryptography;
using System.Text;

namespace AutoGestPro.Models.Estructuras
{
    public class Nodo
    {
        public Nodo Izquierdo;
        public Nodo Derecho;
        public Factura Factura;
        public string Hash;

        public Nodo(Factura factura)
        {
            Izquierdo = null;
            Derecho = null;
            Factura = factura;
            Hash = factura.GetHash();
        }

        public Nodo(Nodo izquierdo, Nodo derecho)
        {
            Factura = null;
            Izquierdo = izquierdo;
            Derecho = derecho;
            Hash = CalcularHash(izquierdo.Hash, derecho?.Hash);
        }

        private string CalcularHash(string leftHash, string rightHash)
        {

            string combined = leftHash + (rightHash ?? leftHash);
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(combined));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }

        }

    }
}
```

```csharp
using System.Text;
using System.Security.Cryptography;
using AutoGestPro.Models.Estructuras;
using AutoGestPro.Models.Entidades;

namespace AutoGestPro.Models
{
    public class ArbolMerkle
    {
        public Nodo? Raiz;
        public List<Nodo> Facturas;

        public ArbolMerkle()
        {
            Raiz = null;
            Facturas = new List<Nodo>();
        }

        public void Insertar(int id, int id_servicio, double total, string metodoDePago)
        {
            foreach(var factura in Facturas)
            {
                if(factura.Factura.Id == id)
                {
                    Console.WriteLine("Error: Ya existe una factura con el ID:", Convert.ToString(id));
                }
            }

            Factura factura_ = new Factura(id, id_servicio, total, metodoDePago);

            Nodo nuevoNodo = new Nodo(factura_);
            Facturas.Add(nuevoNodo);


            CrearArbol();
        }

        public void CrearArbol()
        {
            if(Facturas.Count == 0)
            {
                Raiz = null;
                return;
            }


            List<Nodo> nivelActual = new List<Nodo>(Facturas);

            while(nivelActual.Count > 1)
            {
                List<Nodo> siguienteNivel = new List<Nodo>();

                for(int i = 0; i < nivelActual.Count; i +=2)
                {

                    Nodo izquierdo = nivelActual[i];
                    Nodo derecho = (i + 1 < nivelActual.Count) ? nivelActual[i + 1] : null;
                    Nodo padre = new Nodo(izquierdo, derecho);

                    siguienteNivel.Add(padre);

                }

                nivelActual = siguienteNivel;
            }

            Raiz = nivelActual[0];

        }

        public bool VerificarIntegridad(Factura factura)
        {
            if (Raiz == null) return false;

            string hashFactura = factura.GetHash();
            Nodo nodoEncontrado = EncontrarNodo(Raiz, factura);

            if (nodoEncontrado == null) return false;

            if (nodoEncontrado.Hash != hashFactura) return false;

            return true;   
        }

        public Nodo EncontrarNodo(Nodo nodo, Factura factura)
        {
            if (nodo == null) return null;
            if (nodo.Factura.Id == factura.Id)
            {
                return nodo;
            }
            Nodo encontradoIzquierdo = EncontrarNodo(nodo.Izquierdo, factura);
            if (encontradoIzquierdo != null) return encontradoIzquierdo;
            Nodo encontradoDerecho = EncontrarNodo(nodo.Derecho, factura);
            if (encontradoDerecho != null) return encontradoDerecho;
            return null;
        }

        public string GenerarDot()
        {
            if (Raiz == null)
            {
                return "digraph G {\n  // Árbol vacío\n}";
            }

            var dot = new StringBuilder();
            dot.AppendLine("digraph G {");
            dot.AppendLine("nodo [shape=record];");
            dot.AppendLine("rankdir=BT;");
            dot.AppendLine("nodo [height=0.5];");
            dot.AppendLine("nodo [width=0.5];");
            dot.AppendLine("nodo [style=filled];");
            dot.AppendLine("nodo [fillcolor=\"#EEEEEE\"];");
            dot.AppendLine("nodo [fontname=\"Arial\"];");
            dot.AppendLine("edge [fontname=\"Arial\"];");
            dot.AppendLine("edge [fontsize=8];");
            dot.AppendLine("edge [fontcolor=\"#333333\"];");
            dot.AppendLine("edge [labelfloat=false];");
            dot.AppendLine("edge [decorate=true];");
            dot.AppendLine("edge [style=\"solid\"];");
            dot.AppendLine("edge [color=\"#333333\"];");
            dot.AppendLine("edge [dir=\"forward\"];");
            dot.AppendLine("edge [arrowhead=\"normal\"];");
            dot.AppendLine("edge [arrowsize=\"0.5\"];");
            dot.AppendLine("edge [arrowtail=\"normal\"];");
            dot.AppendLine("edge [taillabel=\"\"];");
            dot.AppendLine("edge [headlabel=\"\"];");
            dot.AppendLine("edge [label=\"\"];");
            dot.AppendLine("edge [weight=\"1\"];");

            var nodeIds = new Dictionary<string, int>();
            int idCounter = 0;

            GenerarDotRecursivo(Raiz, dot, nodeIds, ref idCounter);

            dot.AppendLine("}");
            return dot.ToString();
        }

        private void GenerarDotRecursivo(Nodo nodo, StringBuilder dot, Dictionary<string, int> nodeIds, ref int idCounter)
        {
            if (nodo == null) return;

            if (!nodeIds.ContainsKey(nodo.Hash))
            {
                nodeIds[nodo.Hash] = idCounter++;
            }

            int nodeId = nodeIds[nodo.Hash];

            string label = nodo.Factura != null
                ? $"\"Factura {nodo.Factura.Id}\\nTotal: {nodo.Factura.Total}\\nHash: {nodo.Hash.Substring(0, 8)}...\""
                : $"\"Hash: {nodo.Hash.Substring(0, 8)}...\"";

            dot.AppendLine($"  nodo{nodeId} [label={label}];");

            if (nodo.Izquierdo != null)
            {
                if (!nodeIds.ContainsKey(nodo.Izquierdo.Hash))
                {
                    nodeIds[nodo.Izquierdo.Hash] = idCounter++;
                }
                int leftId = nodeIds[nodo.Izquierdo.Hash];
                dot.AppendLine($" nodo{leftId} -> nodo{nodeId};");
                GenerarDotRecursivo(nodo.Izquierdo, dot, nodeIds, ref idCounter);
            }

            if (nodo.Derecho != null)
            {
                if (!nodeIds.ContainsKey(nodo.Derecho.Hash))
                {
                    nodeIds[nodo.Derecho.Hash] = idCounter++;
                }
                int rightId = nodeIds[nodo.Derecho.Hash];
                dot.AppendLine($" nodo{rightId} -> nodo{nodeId};");
                GenerarDotRecursivo(nodo.Derecho, dot, nodeIds, ref idCounter);
            }
        }
    }
}

```

#### Explicación:

- **Propiedades**:
    - `Raiz`: Nodo raíz del árbol Merkle, que contiene el hash combinado de todos los nodos hoja.
    - `Facturas`: Lista de nodos hoja que representan las facturas individuales.

- **Métodos principales**:
    - `Insertar`: Agrega una nueva factura al árbol y actualiza la estructura para reflejar los cambios.
    - `CrearArbol`: Construye el árbol Merkle a partir de las facturas existentes, combinando los hashes de los nodos hoja.
    - `VerificarIntegridad`: Comprueba si una factura específica es válida y no ha sido alterada.
    - `GenerarDot`: Genera un archivo DOT para visualizar el árbol Merkle en formato gráfico.

- **Funcionamiento**:
    - Cada nodo hoja del árbol representa una factura y contiene su hash.
    - Los nodos intermedios combinan los hashes de sus nodos hijos para formar un nuevo hash.
    - La raíz del árbol contiene el hash combinado de todas las facturas, lo que permite verificar la integridad de los datos de manera eficiente.

Esta implementación asegura que cualquier modificación en una factura sea detectada, ya que alteraría los hashes de los nodos ascendentes hasta la raíz. Esto hace que el Árbol Merkle sea una herramienta confiable para la verificación de datos en el sistema AutoGestPro.



### Grafo

#### Clase `Nodo1`

```csharp
using System;
using AutoGestPro.Models.Entidades;

namespace AutoGestPro.Models.Estructuras.Grafos
{
    public class Nodo1
    {
        public int Id { get; set; }
        
        public Nodo1(int id){
            Id = id;
        }
    }
}
```

#### Clase `Nodo2`

```csharp
using System;
using AutoGestPro.Models.Entidades;

namespace AutoGestPro.Models.Estructuras.Grafos
{
    public class Nodo2
    {
        public int Id { get; set; }
        public Nodo2 siguiente { get; set; }
        public List<Nodo1> hijos { get; set; }
        
        public Nodo2 (int id) {
            Id = id;
            siguiente = null;
            hijos = new List<Nodo1> {};
        }

        public void AgregarHijo(Nodo1 hijo) {
            hijos.Add(hijo);
        }

        public bool buscarHijo(int id) {
            foreach (Nodo1 hijo in hijos) {
                if (hijo.Id == id) {
                    return true;
                }
            }
            return false;
        }
    }
}
```

#### Clase `Grafo`

```csharp
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Text;

namespace AutoGestPro.Models.Estructuras.Grafos
{
    public class Grafo
    {
        public Nodo2 Raiz { get; set; }

        public Grafo () {
            Raiz = null;
        }

        public void insertar(int id2, int id1)
        {
            Nodo2 nuevoNodo2 = new Nodo2(id2);
            Nodo1 nuevoNodo1 = new Nodo1(id1);

            if (Raiz == null) {
                Raiz = nuevoNodo2;
                nuevoNodo2.AgregarHijo(nuevoNodo1);
                return;
            }

            Nodo2 temp = Raiz;
            while (temp != null)
            {
                if (id2 == temp.Id) {
                    break;
                }
                temp = temp.siguiente;
            }

            if (temp != null)
            {
                bool existe = temp.buscarHijo(id1);

                if (existe)
                {
                    return;
                }
                else {
                    temp.AgregarHijo(nuevoNodo1);
                }
            }
            else
            {
                Nodo2 temp2 = Raiz;
                while (temp2.siguiente != null) {
                    temp2 = temp2.siguiente;
                }
                temp2.siguiente = nuevoNodo2;
                nuevoNodo2.AgregarHijo(nuevoNodo1);
            }
        }

        public void imprimir()
        {
            Nodo2 temp = Raiz;
            while (temp != null)
            {
                Console.WriteLine("Nodo: " + Convert.ToString(temp.Id) + " hijos: ");
                foreach (Nodo1 hijo in temp.hijos) {
                    Console.WriteLine(hijo.Id);
                }
                temp = temp.siguiente;
            }
        }

        public string Graficar()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("graph G {");
            sb.AppendLine("    rankdir=LR;");
            sb.AppendLine("    node [shape=circle];");

            Nodo2 temp = Raiz;
            while (temp != null)
            {
                string nodoPadreId = $"V{temp.Id}";
                sb.AppendLine($"    {nodoPadreId} [label=\"{nodoPadreId}\"];");

                foreach (Nodo1 hijo in temp.hijos)
                {
                    string nodoHijoId = $"R{hijo.Id}";
                    sb.AppendLine($"    {nodoHijoId} [label=\"{nodoHijoId}\"];");
                    sb.AppendLine($"    {nodoPadreId} -- {nodoHijoId};");
                }
                temp = temp.siguiente;
            }

            sb.AppendLine("}");
            return sb.ToString();
        }
    }
}
```

#### Explicación

- **Clase `Nodo1`:**
  - Representa los nodos hijos en el grafo.
  - Contiene un identificador único (`Id`).

- **Clase `Nodo2`:**
  - Representa los nodos principales del grafo.
  - Contiene un identificador único (`Id`), una referencia al siguiente nodo principal (`siguiente`), y una lista de nodos hijos (`hijos`).

- **Clase `Grafo`:**
  - Representa el grafo completo.
  - Contiene un nodo raíz (`Raiz`) y métodos para insertar nodos, imprimir la estructura y generar una representación gráfica en formato DOT.




### HuffmanCompresion

```csharp
using System;
using System.Collections.Generic;
using System.Text;

class HuffmanNode : IComparable<HuffmanNode>
{

    public char Caracter;
    public int Frecuencia;
    public HuffmanNode Izquierdo;
    public HuffmanNode Derecho;

    public int CompareTo(HuffmanNode? otro)
    {
        if (otro == null) return 1;
        return this.Frecuencia.CompareTo(otro.Frecuencia);
    }


}
```
```csharp
using System;
using System.Collections.Generic;
using System.Text;

class HuffmanCompression
{

    public static (string comprssed, HuffmanNode raiz) CompressWithTree(string input)
    {
        Dictionary<char, int> frecuencies = new Dictionary<char, int>();

        foreach(char c in input)
        {
            if(frecuencies.ContainsKey(c))
            {
                frecuencies[c]++;
            } else {
                frecuencies[c] = 1;
            }
        }

        PriorityQueue<HuffmanNode> priorityQueue = new PriorityQueue<HuffmanNode>();
        foreach(var kp in frecuencies)
        {
            priorityQueue.Enqueue(new HuffmanNode{Caracter = kp.Key, Frecuencia = kp.Value});
        }

        while(priorityQueue.Count > 1)
        {
            HuffmanNode izquierdo = priorityQueue.Dequeue();
            HuffmanNode derecho = priorityQueue.Dequeue();

            HuffmanNode parent = new HuffmanNode
            {
                Frecuencia = izquierdo.Frecuencia + derecho.Frecuencia,
                Izquierdo = izquierdo,
                Derecho = derecho

            };

            priorityQueue.Enqueue(parent);
        }
        HuffmanNode raiz = priorityQueue.Dequeue();
        Dictionary<char, string> codes = GenerarCodigosHuffman(raiz);

        StringBuilder compressed = new StringBuilder();
        
        foreach (char c in input)
        {
            compressed.Append(codes[c]);
        }

        return (compressed.ToString(), raiz);
    }

    public static string Descomprimir(string compressed, HuffmanNode raiz)
    {
        StringBuilder decompressed = new StringBuilder();
        HuffmanNode actual = raiz;
        foreach (char bit in compressed)
        {
            if (bit == '0')
                actual = actual.Izquierdo;
            else if (bit == '1')
                actual = actual.Derecho;

            if (actual.Caracter != '\0')
            {
                decompressed.Append(actual.Caracter);
                actual = raiz;
            }
        }

        return decompressed.ToString();
    }


    private static Dictionary<char, string> GenerarCodigosHuffman(HuffmanNode raiz)
    {
        var codes = new Dictionary<char, string>();
        GenerarCodigosHuffman(raiz, "", codes);
        return codes;
    }

    private static void GenerarCodigosHuffman(HuffmanNode node, string code, Dictionary<char, string> codes)
    {
        if (node == null)
            return;

        if (node.Caracter != '\0')
            codes[node.Caracter] = code;

        GenerarCodigosHuffman(node.Izquierdo, code + "0", codes);
        GenerarCodigosHuffman(node.Derecho, code + "1", codes);
    }
    
}

class PriorityQueue<T> where T : IComparable<T>
{
    private List<T> list = new List<T>();

    public int Count => list.Count;

    public void Enqueue(T item)
    {
        list.Add(item);
        int i = list.Count - 1;

        while (i > 0)
        {
            int parent = (i - 1) / 2;
            if (list[i].CompareTo(list[parent]) >= 0)
                break;
            Swap(i, parent);
            i = parent;
        }
    }

    public T Dequeue()
    {
        if (list.Count == 0)
            throw new InvalidOperationException("Queue is empty");
        T front = list[0];
        list[0] = list[list.Count - 1];
        list.RemoveAt(list.Count - 1);
        int actual = 0;

        while (true)
        {
            int izquierdo = 2 * actual + 1;
            int derecho = 2 * actual + 2;
            int masPequenio = actual;
            if (izquierdo < list.Count && list[izquierdo].CompareTo(list[masPequenio]) < 0)
                masPequenio = izquierdo;
            if (derecho < list.Count && list[derecho].CompareTo(list[masPequenio]) < 0)
                masPequenio = derecho;
            if (masPequenio == actual)
                break;
            Swap(actual, masPequenio);
            actual = masPequenio;
        }
        return front;
    }

    private void Swap(int i, int j)
    {
        T temp = list[i];
        list[i] = list[j];
        list[j] = temp;
    }

}
```

#### Explicación

El algoritmo de compresión de Huffman es una técnica de compresión sin pérdida que asigna códigos binarios más cortos a los caracteres más frecuentes en un texto. A continuación, se explica cada componente del código:

#### **Clase `HuffmanNode`**
- Representa un nodo en el árbol de Huffman.
- **Propiedades:**
  - `Caracter`: El carácter asociado al nodo (solo para nodos hoja).
  - `Frecuencia`: La frecuencia del carácter en el texto de entrada.
  - `Izquierdo` y `Derecho`: Referencias a los nodos hijos izquierdo y derecho.
- **Método `CompareTo`:**
  - Permite comparar nodos por su frecuencia, lo que es esencial para construir el árbol de Huffman utilizando una cola de prioridad.

#### **Clase `HuffmanCompression`**
- Implementa la lógica para comprimir y descomprimir texto utilizando el algoritmo de Huffman.

##### **Método `CompressWithTree`**
- **Entrada:** Una cadena de texto.
- **Salida:** Una tupla que contiene la cadena comprimida y la raíz del árbol de Huffman.
- **Funcionamiento:**
  1. Calcula las frecuencias de cada carácter en el texto.
  2. Construye una cola de prioridad con nodos de Huffman basados en las frecuencias.
  3. Construye el árbol de Huffman combinando los nodos con las frecuencias más bajas.
  4. Genera códigos binarios para cada carácter utilizando el árbol.
  5. Reemplaza cada carácter en el texto con su código binario correspondiente para obtener la cadena comprimida.

##### **Método `Descomprimir`**
- **Entrada:** Una cadena comprimida y la raíz del árbol de Huffman.
- **Salida:** La cadena original descomprimida.
- **Funcionamiento:**
  1. Recorre la cadena comprimida bit por bit.
  2. Navega por el árbol de Huffman (hacia la izquierda para `0` y hacia la derecha para `1`).
  3. Cuando se alcanza un nodo hoja, se agrega el carácter correspondiente a la cadena descomprimida.
  4. Repite hasta procesar toda la cadena comprimida.

##### **Método `GenerarCodigosHuffman`**
- **Entrada:** La raíz del árbol de Huffman.
- **Salida:** Un diccionario que asigna a cada carácter su código binario.
- **Funcionamiento:**
  - Recorre el árbol de Huffman de manera recursiva.
  - Asigna un `0` para moverse hacia la izquierda y un `1` para moverse hacia la derecha.
  - Al llegar a un nodo hoja, almacena el código generado para el carácter correspondiente.

#### **Clase `PriorityQueue<T>`**
- Implementa una cola de prioridad genérica basada en un heap binario.
- **Propiedades:**
  - `Count`: Devuelve el número de elementos en la cola.
- **Métodos:**
  - `Enqueue`: Inserta un elemento en la cola, manteniendo el orden de prioridad.
  - `Dequeue`: Elimina y devuelve el elemento con la mayor prioridad (el más pequeño en este caso).
  - `Swap`: Intercambia dos elementos en el heap para mantener la propiedad de orden.