# Manual técnico

- Autor: José Emanuel Monzón Lémus
- Carnet: 202300539
- Curso: Estructuras de datos
- Repositorio: https://github.com/0520Jose/-EDD-Proyecto_202300539.git

## Introdución 

El presente manual cuenta con la función de informar sobre el proceso de desarrollo sobre el proyecto AutoGestPro de curso Estructuras de Datos, en el presente se muestran todas las estructuras y objetos utilizados en el lenguaje C# para obtener un correcto funcionamiento del programa propuesto.

## Objetivos

### General
+ Presentar y explicar el funcionamiento del cóigo para el proyecto AutoGestPro.

### Especificos
+ Mostrar los modulos y estructuras utlizadas para el proyecto de Esctructuras de datos.
+ Mostrar el funcionamiento de los distintos ventanas de acción con las que cuenta el usuario.


## Entidades

Las diferentes entidades que seran utilizadas para las distintas funcionalidades del proyecto AutoGestPro el cual cuenta con la función de administrar un taller mécanico por lo que las entidades requeridas fueron creadas para cubrir las necesidades que surgieron para el taller, siendo esta su primer fase.  

### Usuario

+ Código:
```csharp
namespace AutoGestPro.Models
{
    unsafe struct Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Correo { get; set; }
        public string Contrasenia { get; set;}
        public Usuario* siguiente;

        public Usuario(int id, string nombre, string apellido, string correo, string contrasenia)
        {
            Id = id;
            Nombre = nombre;
            Apellido = apellido;
            Correo = correo;
            Contrasenia = contrasenia;
        }
    }
}
```
### Vehiculo

+ Código:
```csharp
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

### Repuesto

+ Código:
```csharp
namespace AutoGestPro.Models
{
    unsafe struct Repuesto
    {
        public int Id { get; set; }
        public string repuesto { get; set; }
        public string detalle { get; set; }   
        public float costo { get; set; }


        public Repuesto* siguiente;
        public Repuesto* anterior;

        public Repuesto(int id, string repuesto, string detalle, float costo)
        {
            Id = id;
            repuesto = repuesto;
            detalle = detalle;
            costo = costo;
        }
    }
}
```

### Servicio

+ Código:
```csharp
namespace AutoGestPro.Models.Entidades
{
    unsafe struct Servicio
    {
        public int Id { get; set; }
        public int Id_Repuesto { get; set; }
        public int Id_Vehiculo { get; set; }
        public string Detalles { get; set; }
        public double Costo { get; set; }
        public Servicio* siguiente;

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

### Factura

+ Código:
```csharp
namespace AutoGestPro.Models.Entidades
{
    unsafe struct Factura
    {
        public int Id { get; set; }
        public int Id_Orden { get; set; }
        public float Total { get; set; }
        public Factura* abajo;
        public Factura(int id, int idOrden, float total)
        {
            Id = id;
            Id_Orden = idOrden;
            Total = total;
        }
    }
}
```

## Estructuras

### Lista simple - Lista Usuarios

+ Código:
```csharp
unsafe class ListaSimple
{
    public Usuario* inicio = null;

    public void Insertar(int id, string nombre, string apellido, string correo, string contrasenia)
    {
        Usuario* nuevoUsuario = (Usuario*)NativeMemory.Alloc((nuint)sizeof(Usuario));
        nuevoUsuario->Id = id;
        nuevoUsuario->Nombre = nombre;
        nuevoUsuario->Apellido = apellido;
        nuevoUsuario->Correo = correo;
        nuevoUsuario->Contrasenia = contrasenia;
        nuevoUsuario->siguiente = null;
        if (inicio == null)
        {
            inicio = nuevoUsuario;
        }
        else
        {
            Usuario* usuarioActual = inicio;
            while (usuarioActual->siguiente != null)
            {
                usuarioActual = usuarioActual->siguiente;
            }
            usuarioActual->siguiente = nuevoUsuario;
        }
    }

    public Usuario buscarUsuario(int id)
    {
        Usuario* usuarioActual = inicio;
        while (usuarioActual != null)
        {
            if (usuarioActual->Id == id)
            {
                return *usuarioActual;
            }
            usuarioActual = usuarioActual->siguiente;
        }
        return new Usuario();
    }

    public void ActualizarUsuario(int id, string nombre, string apellido, string correo)
    {
        Usuario* usuarioActual = inicio;
        while (usuarioActual != null)
        {
            if (usuarioActual->Id == id)
            {
                if (nombre != "") 
                {
                    usuarioActual->Nombre = nombre;
                }
                if (apellido != "") 
                {
                    usuarioActual->Apellido = apellido;
                }
                if (correo != "") 
                {
                    usuarioActual->Correo = correo;
                }
                return;
            }
            usuarioActual = usuarioActual->siguiente;
        }
    }

    public void EliminarUsuario(int id)
    {
        Usuario* usuarioActual = inicio;
        Usuario* usuarioAnterior = null;
        while (usuarioActual != null)
        {
            if (usuarioActual->Id == id)
            {
                if (usuarioAnterior == null)
                {
                    inicio = usuarioActual->siguiente;
                    NativeMemory.Free(usuarioActual);
                }
                else
                {
                    usuarioAnterior->siguiente = usuarioActual->siguiente;
                    NativeMemory.Free(usuarioActual);
                }
                return;
            }
            usuarioAnterior = usuarioActual;
            usuarioActual = usuarioActual->siguiente;
        }
    }

}
```

### Lista doble - Lista Vehiculos

+ Código:
```csharp
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
        nuevoVehiculo->siguiente = inicio;
        if (inicio != null)
        {
            inicio->anterior = nuevoVehiculo;
        }
        inicio = nuevoVehiculo;
        nuevoVehiculo->anterior = null;
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
}
```

### Lista circular - Lista Repuestos

+ Código:
```csharp
unsafe class ListaCircular
{
    public Repuesto* inicio = null;
    public int tamanio = 0;

    public void Insertar(int id, string repuesto, string detalles, float costo)
    {
        Repuesto* nuevoRepuesto = (Repuesto*)NativeMemory.Alloc((nuint)sizeof(Repuesto));
        nuevoRepuesto->Id = id;
        nuevoRepuesto->repuesto = repuesto;
        nuevoRepuesto->detalle = detalles;
        nuevoRepuesto->costo = costo;
        if (inicio == null)
        {
            inicio = nuevoRepuesto;
            inicio->siguiente = inicio;
        }
        else
        {
            Repuesto* ultimo = inicio;
            while (ultimo->siguiente != inicio)
            {
                ultimo = ultimo->siguiente;
            }
            nuevoRepuesto->siguiente = inicio;
            ultimo->siguiente = nuevoRepuesto;
        }
        tamanio++;
    }

    public Repuesto buscarRepuesto(int id)
    {
        Repuesto* repuestoActual = inicio;
        while (repuestoActual != null)
        {
            if (repuestoActual->Id == id)
            {
                return *repuestoActual;
            }
            repuestoActual = repuestoActual->siguiente;
            if (repuestoActual == inicio)
            {
                return new Repuesto();
            }
        }
        return new Repuesto();
    }
}
```

### Cola - Servicios

+ Código:
```csharp
namespace AutoGestPro.Models.Listas
{
    unsafe class Cola
    {
        public Servicio* inicio = null;
        public Servicio* fin = null;

        public void Encolar(int id, int Id_Repuesto, int Id_Vehiculo, string detalles, double costo)
        {
            Servicio* nuevoServicio = (Servicio*)NativeMemory.Alloc((nuint)sizeof(Servicio));
            nuevoServicio->Id = id;
            nuevoServicio->Id_Repuesto = Id_Repuesto;
            nuevoServicio->Id_Vehiculo = Id_Vehiculo;
            nuevoServicio->Detalles = detalles;
            nuevoServicio->Costo = costo;
            nuevoServicio->siguiente = null;
            
            if (inicio == null)
            {
                inicio = nuevoServicio;
                fin = nuevoServicio;
            }
            else
            {
                fin->siguiente = nuevoServicio;
                fin = nuevoServicio;
            }
        }

        public Servicio Desencolar()
        {
            if (inicio != null)
            {
            Servicio* servicioDesencolado = inicio;
            inicio = inicio->siguiente;
            Servicio servicio = *servicioDesencolado;
            NativeMemory.Free(servicioDesencolado);
            return servicio;
            }
            return new Servicio();
        }

        public Servicio buscar(int id)
        {
            Servicio* actual = inicio;
            while (actual != null)
            {
                if (actual->Id == id)
                {
                    return *actual;
                }
                actual = actual->siguiente;
            }
            return new Servicio();
        }
    }
}
```

### Pila - Facturas

+ Código:
```csharp
namespace AutoGestPro.Models.Listas
{
    unsafe class Pila
    {
        public Factura* sima = null;

        public void Apilar(int id, int idOrden, float total)
        {
            Factura* nuevaFactura = (Factura*)NativeMemory.Alloc((nuint)sizeof(Factura));
            nuevaFactura->Id = id;
            nuevaFactura->Id_Orden = idOrden;
            nuevaFactura->Total = total;
            
            if (sima == null)
            {
                sima = nuevaFactura;
                sima->abajo = null;
            }
            else
            {
                nuevaFactura->abajo = sima;
                sima = nuevaFactura;
            }
        }

        public Factura Desapilar()
        {
            if (sima != null)
            {
            Factura* factura = sima;
            sima = sima->abajo;
            Factura result = *factura;
            NativeMemory.Free(factura);
            return result;
            }
            return new Factura();
        }

        public Factura* DesapilarFactura()
        {
            if (sima != null)
            {
                Factura* factura = sima;
                sima = sima->abajo;
                return factura;
            }
            return null;
        }
    }
}
```

### Matriz disperza - Bitacoras

+ Código:
```csharp
namespace AutoGestPro.Models.Listas.MatrizDispersa
{
    public unsafe class Matriz_Dispersa
    {
        public ListaCabecera filas = new ListaCabecera("fila");
        public ListaCabecera columnas = new ListaCabecera("columna");

        public void insertar(int x, int y, Bitacora bitacora)
        {
            NodoCelda* nuevo = (NodoCelda*)NativeMemory.Alloc((nuint)sizeof(NodoCelda));
            *nuevo = new NodoCelda(x, y, bitacora);
            NodoCabecera* celda_x = filas.buscar(x);
            NodoCabecera* celda_y = columnas.buscar(y);

            if (celda_x == null)
            {
                celda_x = (NodoCabecera*)NativeMemory.Alloc((nuint)sizeof(NodoCabecera));
                *celda_x = new NodoCabecera(x);
                filas.insertar(celda_x);
            }
            if (celda_y == null)
            {
                celda_y = (NodoCabecera*)NativeMemory.Alloc((nuint)sizeof(NodoCabecera));
                *celda_y = new NodoCabecera(y);
                columnas.insertar(celda_y);
            }
            if (celda_x->acceso == null)
            {
                celda_x->acceso = nuevo;
            }
            else
            {
                if (nuevo->y < celda_x->acceso->y)
                {
                    nuevo->derecha = celda_x->acceso;
                    celda_x->acceso->izquierda = nuevo;
                    celda_x->acceso = nuevo;
                }
                else
                {
                    NodoCelda* actual = celda_x->acceso;
                    while (actual != null)
                    {
                        if (nuevo->y < actual->y)
                        {
                            nuevo->derecha = actual;
                            nuevo->izquierda = actual->izquierda;
                            actual->izquierda->derecha = nuevo;
                            actual->izquierda = nuevo;
                            break;
                        }
                        else if (nuevo->x == actual->x && nuevo->y == actual->y)
                        {
                            actual->bitacora = nuevo->bitacora;
                            break;
                        }
                        else
                        {
                            if (actual->derecha == null)
                            {
                                actual->derecha = nuevo;
                                nuevo->izquierda = actual;
                                break;
                            }
                            else
                            {
                                actual = actual->derecha;
                            }
                        }
                    }
                }
            }
            if (celda_y->acceso == null)
            {
                celda_y->acceso = nuevo;
            }
            else
            {
                if (nuevo->x < celda_y->acceso->x)
                {
                    nuevo->abajo = celda_y->acceso;
                    celda_y->acceso->arriba = nuevo;
                    celda_y->acceso = nuevo;
                }
                else
                {
                    NodoCelda* actual = celda_y->acceso;
                    while (actual != null)
                    {
                        if (nuevo->x < actual->x)
                        {
                            nuevo->abajo = actual;
                            nuevo->arriba = actual->arriba;
                            actual->arriba->abajo = nuevo;
                            actual->arriba = nuevo;
                            break;
                        }
                        else if (nuevo->x == actual->x && nuevo->y == actual->y)
                        {
                            actual->bitacora = nuevo->bitacora;
                            break;
                        }
                        else
                        {
                            if (actual->abajo == null)
                            {
                                actual->abajo = nuevo;
                                nuevo->arriba = actual;
                                break;
                            }
                            else
                            {
                                actual = actual->abajo;
                            }
                        }
                    }
                }
            }
        }
    }
}
```

La matriz disperza necesita dos tipo de ondos y un lista cabecera poder acceder a los datos contenidos

#### NodoCabecera

+ Código:
```csharp
amespace AutoGestPro.Models.Listas.MatrizDispersa
{
    public unsafe class NodoCabecera
    {
        public int id { get; set; }
        public NodoCabecera* siguiente;
        public NodoCabecera* anterior;
        public NodoCelda* acceso;

        public NodoCabecera(int Id)
        {
            id = Id;
            siguiente = null;
            anterior = null;
            acceso = null;
        }
    }
}
```

### Lista Cabecera

+ Código:
```csharp
namespace AutoGestPro.Models.Listas.MatrizDispersa
{
    public unsafe class ListaCabecera
    {
        public string coordenada { get; set; }
        public NodoCabecera* primero;
        public NodoCabecera* ultimo;
        public int tamanio;

        public ListaCabecera(string Coordenada)
        {
            coordenada = Coordenada;
            primero = null;
            ultimo = null;
            tamanio = 0;
        }

        public int tamanioLista()
        {
            return tamanio;
        }

        public void insertar(NodoCabecera* nuevo)
        {
            if (primero == null && ultimo == null)
            {
                primero = nuevo;
                ultimo = nuevo;
            }
            else
            {
                if (nuevo->id < primero->id)
                {
                    nuevo->siguiente = primero;
                    primero->anterior = nuevo;
                    primero = nuevo;
                }
                else if (nuevo->id > ultimo->id)
                {
                    ultimo->siguiente = nuevo;
                    nuevo->anterior = ultimo;
                    ultimo = nuevo;
                }
                else
                {
                    NodoCabecera* actual = primero;
                    while (actual != null)
                    {
                        if (nuevo->id < actual->id)
                        {
                            nuevo->siguiente = actual;
                            nuevo->anterior = actual->anterior;
                            actual->anterior->siguiente = nuevo;
                            actual->anterior = nuevo;
                            break;
                        }
                        else if (nuevo->id > actual->id)
                        {
                            actual = actual->siguiente;
                        }
                        else
                        {
                            break;
                        }
                        
                    }
                }
            }
            tamanio++;
        }

        public NodoCabecera* buscar(int id)
        {
            NodoCabecera* actual = primero;
            while (actual != null)
            {
                if (actual->id == id)
                {
                    return actual;
                }
                actual = actual->siguiente;
            }
            return null;
        }

        public void mostrarCabeceras()
        {
            NodoCabecera* actual = primero;
            while (actual != null)
            {
                Console.WriteLine(actual->id);
                actual = actual->siguiente;
            }
        }

        public void LiberarMemoria()
        {
            NodoCabecera* actual = primero;
            while (actual != null)
            {
                NodoCabecera* siguiente = actual->siguiente;
                Marshal.FreeHGlobal((IntPtr)actual);
                actual = siguiente;
            }
        }
    }
}
```

### Nodo Celda

+ Código:
```csharp
namespace AutoGestPro.Models.Listas.MatrizDispersa
{
    public unsafe class NodoCelda
    {
        public int x { get; set; }
        public int y { get; set; }
        public Bitacora bitacora { get; set; }
        public NodoCelda* arriba;
        public NodoCelda* abajo;
        public NodoCelda* derecha;
        public NodoCelda* izquierda;

        public NodoCelda(int X, int Y, Bitacora Bitacora)
        {
            x = X;
            y = Y;
            bitacora = Bitacora;
            arriba = null;
            abajo = null;
            derecha = null;
            izquierda = null;
        }
    }
}
```

#### Bitacora

+ Código:
```csharp
namespace AutoGestPro.Models.Listas.MatrizDispersa
{
    public class Bitacora
    {
        public string Detalle { get; set; }
        public int Id_Vehiculo { get; set; }
        public int Id_Repuesto { get; set; }

        public Bitacora(string detalle, int idVehiculo, int idRepuesto)
        {
            Detalle = detalle;
            Id_Vehiculo = idVehiculo;
            Id_Repuesto = idRepuesto;
        }

    }
}
```