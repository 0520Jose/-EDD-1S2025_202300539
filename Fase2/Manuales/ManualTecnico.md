# Manual técnico

- Autor: José Emanuel Monzón Lémus  
- Carnet: 202300539  
- Curso: Estructuras de datos  
- Repositorio: [GitHub](https://github.com/0520Jose/-EDD-Proyecto_202300539.git)  

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

Entidad necesaria para la administración de los usuarios que estarán en la base de datos del taller.  

#### Código:  
```csharp
// filepath: /Models/Entidades/Usuario.cs
using System;

namespace AutoGestPro.Models
{
    unsafe struct Usuario
    {
        public int Id { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public string Correo { get; set; }
        public int Edad { get; set; }
        public string Contrasenia { get; set; }
        public Usuario* siguiente;

        public Usuario(int id, string nombre, string apellido, string correo, int edad, string contrasenia)
        {
            Id = id;
            Nombres = nombre;
            Apellidos = apellido;
            Correo = correo;
            Edad = edad;
            Contrasenia = contrasenia;
        }
    }
}
```

#### Explicación:  
- **Propiedades**:  
  - `Id`: Identificador único del usuario.  
  - `Nombres` y `Apellidos`: Información personal del usuario.  
  - `Correo`: Dirección de correo electrónico del usuario.  
  - `Edad`: Edad del usuario.  
  - `Contrasenia`: Contraseña para acceder al sistema.  
- **Puntero `siguiente`**: Permite enlazar esta estructura con otros usuarios, formando una lista enlazada.  
- **Constructor**: Inicializa un nuevo usuario con los datos proporcionados.  

---

### Vehículo  

Entidad que representa los vehículos registrados en el sistema.  

#### Código:  
```csharp
// filepath: /Models/Entidades/Vehiculo.cs
using System;

namespace AutoGestPro.Models
{
    unsafe struct Vehiculo
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
  - `Marca`, `Modelo` y `Placa`: Información del vehículo.  
- **Punteros `siguiente` y `anterior`**: Permiten enlazar esta estructura en una lista doblemente enlazada.  
- **Constructor**: Inicializa un nuevo vehículo con los datos proporcionados.  

---

### Servicio  

Entidad que representa los servicios realizados en el taller.  

#### Código:  
```csharp
// filepath: /Models/Entidades/Servicios.cs
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
  - `Id_Repuesto` y `Id_Vehiculo`: Relación con el repuesto y vehículo asociados.  
  - `Detalles`: Descripción del servicio realizado.  
  - `Costo`: Costo del servicio.  
- **Constructor**: Inicializa un nuevo servicio con los datos proporcionados.  

---

### Repuesto  

Entidad que representa los repuestos disponibles en el taller.  

#### Código:  
```csharp
// filepath: /Models/Entidades/Repuesto.cs
using System;

namespace AutoGestPro.Models
{
    class Repuesto
    {
        public int Id { get; set; }
        public string REpuesto { get; set; }
        public string Detalle { get; set; }
        public float Costo { get; set; }

        public Repuesto(int id, string repuesto, string detalle, float costo)
        {
            Id = id;
            REpuesto = repuesto;
            Detalle = detalle;
            Costo = costo;
        }
    }
}
```

#### Explicación:  
- **Propiedades**:  
  - `Id`: Identificador único del repuesto.  
  - `REpuesto`: Nombre del repuesto.  
  - `Detalle`: Descripción del repuesto.  
  - `Costo`: Precio del repuesto.  
- **Constructor**: Inicializa un nuevo repuesto con los datos proporcionados.  

---

### Factura  

Entidad que representa las facturas generadas en el sistema.  

#### Código:  
```csharp
// filepath: /Models/Entidades/Factura.cs
using System;

namespace AutoGestPro.Models.Entidades
{
    public class Factura
    {
        public int Id { get; set; }
        public int Id_Orden { get; set; }
        public double Total { get; set; }

        public Factura(int id, int idOrden, double total)
        {
            Id = id;
            Id_Orden = idOrden;
            Total = total;
        }
    }
}
```

#### Explicación:  
- **Propiedades**:  
  - `Id`: Identificador único de la factura.  
  - `Id_Orden`: Relación con la orden de servicio asociada.  
  - `Total`: Monto total de la factura.  
- **Constructor**: Inicializa una nueva factura con los datos proporcionados.  

---

---

## Estructuras de Datos

En esta sección se describen las estructuras de datos utilizadas en el proyecto AutoGestPro para gestionar la información de manera eficiente.

---

### Lista Simple

La lista simple se utiliza para almacenar y gestionar los usuarios del sistema. Esta estructura permite insertar y buscar usuarios de manera eficiente.

#### Código:
```csharp
// filepath: /Models/Listas/ListaSimple.cs
using System;
using System.Runtime.InteropServices;
using AutoGestPro.Models;

unsafe class ListaSimple
{
    public Usuario* inicio = null;

    public void Insertar(int id, string nombre, string apellido, string correo, int edad, string contrasenia)
    {
        if (inicio == null)
        {
            // Lógica para insertar el primer nodo
        }
        else
        {
            while (usuarioActual->siguiente != null)
            {
                // Lógica para recorrer e insertar en la lista
            }
        }
    }

    public Usuario buscarUsuario(int id)
    {
        while (usuarioActual != null)
        {
            if (usuarioActual->Id == id)
            {
                // Lógica para buscar un usuario
            }
        }
    }
}
```

#### Explicación:
- **Propiedades**:
  - `inicio`: Puntero al primer nodo de la lista.
- **Métodos**:
  - `Insertar`: Agrega un nuevo usuario a la lista.
  - `buscarUsuario`: Busca un usuario en la lista por su identificador.

---

### Lista Doble

La lista doble se utiliza para gestionar los vehículos registrados en el sistema. Esta estructura permite recorrer la lista en ambas direcciones.

#### Código:
```csharp
// filepath: /Models/Listas/ListaDoble.cs
using System;
using System.Runtime.InteropServices;
using AutoGestPro.Models;

unsafe class ListaDoble
{
    public Vehiculo* inicio = null;
    public int tamanio = 0;

    public void Insertar(int id, int IdUsuario, string marca, string modelo, string placa)
    {
        if (inicio == null)
        {
            // Lógica para insertar el primer nodo
        }
        else
        {
            while (actual != null && actual->Id < id)
            {
                // Lógica para recorrer e insertar en la lista
            }

            if (anterior == null)
            {
                // Lógica para insertar al inicio
            }
            else
            {
                if (actual != null)
                {
                    // Lógica para insertar en el medio o al final
                }
            }
        }
    }

    public Vehiculo buscarVehiculo(int id)
    {
        while (vehiculoActual != null)
        {
            if (vehiculoActual->Id == id)
            {
                // Lógica para buscar un vehículo
            }
        }
    }
}
```

#### Explicación:
- **Propiedades**:
  - `inicio`: Puntero al primer nodo de la lista.
  - `tamanio`: Tamaño de la lista.
- **Métodos**:
  - `Insertar`: Agrega un nuevo vehículo a la lista.
  - `buscarVehiculo`: Busca un vehículo en la lista por su identificador.

---

### Árbol Binario

El árbol binario se utiliza para gestionar los servicios realizados en el taller. Esta estructura permite realizar búsquedas rápidas y organizar los servicios jerárquicamente.

#### Nodo Binario:
```csharp
// filepath: /Models/Listas/Arbol_Binario/NodoBinario.cs
using System;
using AutoGestPro.Models.Entidades;

namespace AutoGestPro.Models.Listas.Arbol_Binario
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

#### Árbol Binario:
```csharp
// filepath: /Models/Listas/Arbol_Binario/ArbolBinario.cs
using System;
using AutoGestPro.Models.Entidades;

namespace AutoGestPro.Models.Listas.Arbol_Binario
{
    class ArbolBinario
    {
        public NodoBinario? Raiz { get; set; }

        public ArbolBinario()
        {
            // Constructor del árbol binario
        }

        public void Insertar(Servicio servicio)
        {
            // Lógica para insertar un servicio en el árbol
        }

        public NodoBinario? Buscar(int id)
        {
            // Lógica para buscar un servicio en el árbol
        }
    }
}
```

#### Explicación:
- **Nodo Binario**:
  - **Propiedades**:
    - `Izquierdo` y `Derecho`: Hijos del nodo.
    - `Servicio`: Servicio almacenado en el nodo.
  - **Constructor**: Inicializa un nodo con un servicio.
- **Árbol Binario**:
  - **Propiedades**:
    - `Raiz`: Nodo raíz del árbol.
  - **Métodos**:
    - `Insertar`: Agrega un nuevo servicio al árbol.
    - `Buscar`: Busca un servicio en el árbol por su identificador.

---

### Árbol AVL

El árbol AVL se utiliza para gestionar los repuestos disponibles en el taller. Esta estructura mantiene el equilibrio del árbol para garantizar búsquedas eficientes.

#### Nodo AVL:
```csharp
// filepath: /Models/Listas/Arbol_AVL/NodoAVL.cs
using System;

namespace AutoGestPro.Models.Listas.Arbol_AVL
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

#### Árbol AVL:
```csharp
// filepath: /Models/Listas/Arbol_AVL/ArbolAVL.cs
using System;
using AutoGestPro.Models;

namespace AutoGestPro.Models.Listas.Arbol_AVL
{
    class ArbolAVL
    {
        public NodoAVL Raiz { get; set; }

        public ArbolAVL()
        {
            // Constructor del árbol AVL
        }

        private int getAltura(NodoAVL nodo) => nodo?.Altura ?? 0;

        private int getBalance(NodoAVL nodo) => nodo == null ? 0 : getAltura(nodo.Izquierdo) - getAltura(nodo.Derecho);

        private NodoAVL RotarDerecha(NodoAVL y)
        {
            // Lógica para rotación a la derecha
        }

        private NodoAVL RotarIzquierda(NodoAVL x)
        {
            // Lógica para rotación a la izquierda
        }
    }
}
```

#### Explicación:
- **Nodo AVL**:
  - **Propiedades**:
    - `Izquierdo` y `Derecho`: Hijos del nodo.
    - `Repuesto`: Repuesto almacenado en el nodo.
    - `Altura`: Altura del nodo.
  - **Constructor**: Inicializa un nodo con un repuesto.
- **Árbol AVL**:
  - **Propiedades**:
    - `Raiz`: Nodo raíz del árbol.
  - **Métodos**:
    - `getAltura`: Obtiene la altura de un nodo.
    - `getBalance`: Calcula el factor de balance de un nodo.
    - `RotarDerecha` y `RotarIzquierda`: Realizan rotaciones para mantener el equilibrio del árbol.

---

### Árbol B (Orden 5)

El árbol B se utiliza para gestionar las facturas generadas en el sistema. Esta estructura permite almacenar grandes cantidades de datos de manera eficiente.

#### Nodo B:
```csharp
// filepath: /Models/Listas/Arbol_B5/NodoB.cs
using System;
using AutoGestPro.Models.Entidades;

namespace AutoGestPro.Models.Listas.Arbol_B5
{
    public class NodoB
    {
        public int n_facturas { get; set; }
        public Factura[] Facturas { get; set; }
        public NodoB[] hijos { get; set; }

        public NodoB(int Orden)
        {
            n_facturas = 0;
            Facturas = new Factura[Orden];
            hijos = new NodoB[Orden + 1];
        }
    }
}
```

#### Árbol B:
```csharp
// filepath: /Models/Listas/Arbol_B5/ArbolB5.cs
using System;
using AutoGestPro.Models.Entidades;

namespace AutoGestPro.Models.Listas.Arbol_B5
{
    public unsafe class ArbolB5
    {
        private int T;
        public int Tamaño { get; set; }
        public NodoB Raiz { get; set; }

        public ArbolB5(int Orden)
        {
            // Constructor del árbol B
        }

        public void Insertar(Factura nuevaFactura)
        {
            // Lógica para insertar una factura en el árbol
        }
    }
}
```

#### Explicación:
- **Nodo B**:
  - **Propiedades**:
    - `n_facturas`: Número de facturas almacenadas en el nodo.
    - `Facturas`: Arreglo de facturas.
    - `hijos`: Arreglo de nodos hijos.
  - **Constructor**: Inicializa un nodo con el orden del árbol.
- **Árbol B**:
  - **Propiedades**:
    - `T`: Orden del árbol.
    - `Tamaño`: Número de elementos en el árbol.
    - `Raiz`: Nodo raíz del árbol.
  - **Métodos**:
    - `Insertar`: Agrega una nueva factura al árbol.

---