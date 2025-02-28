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

Entidad necesaria para la administracion de los usuarios que estaran en la bases de datos del taller.

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

Entidad necesaria para la administracion de los vehiculos a los cuales se realizara los registros.

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

Entidad necesaria para administrar los repuestos que se almacenan para los registros de los servicios.

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

Entidad necesaria para poder llevar los registros de todo lo realizado a vehiculos dentro del taller.

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

Entidad necesaria para poder almacenar los datos al momento de cancelar un servicio.

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

Las estructuras son todas las necesarias para poder almacenar todos los registro de las entidades utilizadas dentro del programa.

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

La matriz disperza necesita dos tipo de nodos cabecera y nodos celda para referenciar y almacenar los datos necesarios para su funcionamiento y una lista cabecera poder acceder a los datos contenidos y saber la pasion en la que se encuentran.

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

#### Lista Cabecera

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

#### Nodo Celda

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

## Views

Las posibles ventanas que podemos visualizar en el proyecto contanta de un menu de inicio de sesion, un menu principal de seleccion, ademas cada apartado cuenta con su propia interfaz para que todo pueda ser accedido de manera secuencial o no entre ventanas.

Las distintas implimentaciones para las ventanas requeridas fuerono separadas en namespaces con el objetivo que puedan acceder entre si de namera agil, ademas que cada ventana recibe como parametro la ventana anterior a la que se accedio para que no solo de pueda avanzar entre ventanas si no tambien retroceder y con ello poder acceder nuevamente al menu principal o hasta cerrar la sesion acutal.

A continuación se muestran las ventanas mas relevantes:

### Inicio de sesión

En esta ventana se muestra el espacio para que el usuario inicie sesión.

+ Código:

```csharp
namespace AutoGestPro.Views
{
    class IniciarSesion
    {
        public IniciarSesion()
        {
            Application.Init();
            Window ventana = new Window("AutoGestPro");
            ventana.Opacity = 0.75;
            ventana.SetDefaultSize(400, 300);
            ventana.DeleteEvent += delegate { Application.Quit(); };

            VBox contenedor = new VBox(false, 10);
            contenedor.BorderWidth = 20;
            ventana.Add(contenedor);

            Label titulo = new Label("<span size='xx-large' weight='bold'>Iniciar Sesión</span>");
            titulo.UseMarkup = true;
            contenedor.PackStart(titulo, false, false, 10);

            HBox usuarioBox = new HBox(false, 5);
            Label tituloUsuario = new Label("Usuario:");
            Entry txtUsuario = new Entry();
            usuarioBox.PackStart(tituloUsuario, false, false, 5);
            usuarioBox.PackStart(txtUsuario, true, true, 5);
            contenedor.PackStart(usuarioBox, false, false, 5);

            HBox contrasenaBox = new HBox(false, 5);
            Label tituloContrasena = new Label("Contraseña:");
            Entry txtContrasena = new Entry();
            txtContrasena.Visibility = false;
            contrasenaBox.PackStart(tituloContrasena, false, false, 5);
            contrasenaBox.PackStart(txtContrasena, true, true, 5);
            contenedor.PackStart(contrasenaBox, false, false, 5);

            Button iniciarSesion = new Button("Iniciar Sesión");
            iniciarSesion.Clicked += (sender, e) => {
                if (txtUsuario.Text == "root@gmail.com" && txtContrasena.Text == "root123")
                {
                    txtUsuario.Text = "";
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
                    txtUsuario.Text = "";
                    txtContrasena.Text = "";
                }
            };
            contenedor.PackStart(iniciarSesion, false, false, 20);

            ventana.ShowAll();
            Application.Run();
        }
    }
}
```

### Menu

El este espacio el usuario puede acceder a todas las opciones que tiene disponible.

+ Código:

```csharp
namespace AutoGestPro.Views
{
    class Menu
    {
        public Menu(Window cerrarSesion)
        {
            Window ventana = new Window("Menu - Root");
            ventana.SetDefaultSize(800, 600);
            ventana.SetPosition(WindowPosition.Center);
            ventana.DeleteEvent += delegate { Application.Quit(); };

            VBox contenedor = new VBox(false, 5);
            ventana.Add(contenedor);

            Label titulo = new Label("<span size='xx-large'>Menu</span>");
            titulo.UseMarkup = true;
            contenedor.PackStart(titulo, false, false, 20);

            Table table = new Table(3, 2, true);
            contenedor.PackStart(table, true, true, 10);

            Button cargaMasiva = new Button("Carga masiva");
            cargaMasiva.Clicked += (sender, e) => {
                CargaMasiva cargaMasiva = new CargaMasiva(ventana);
                ventana.Hide();
            };
            table.Attach(cargaMasiva, 0, 1, 0, 1);

            Button ingresoIndividual = new Button("Ingreso individual");
            ingresoIndividual.Clicked += (sender, e) => {
                IngresoIndividual ingresoIndividual = new IngresoIndividual(ventana);
                ventana.Hide();
            };
            table.Attach(ingresoIndividual, 1, 2, 0, 1);

            Button gestionDeUsuarios = new Button("Gestión de usuarios");
            gestionDeUsuarios.Clicked += (sender, e) => {
                GestionDeUsuarios gestionDeUsuarios = new GestionDeUsuarios(ventana);
                ventana.Hide();
            };
            table.Attach(gestionDeUsuarios, 0, 1, 1, 2);

            Button generarServicio = new Button("Generar servicio");
            generarServicio.Clicked += (sender, e) => {
                GenerarServicio generarServicio = new GenerarServicio(ventana);
                ventana.Hide();
            };
            table.Attach(generarServicio, 1, 2, 1, 2);

            Button cancelarFactura = new Button("Cancelar factura");
            cancelarFactura.Clicked += (sender, e) => {
                CancelarFactura cancelarFactura = new CancelarFactura(ventana);
                ventana.Hide();
            };
            table.Attach(cancelarFactura, 0, 1, 2, 3);

            Button reportes = new Button("Reportes");
            reportes.Clicked += (sender, e) => {
                Reportes reportes = new Reportes(ventana);
                ventana.Hide();
            };
            table.Attach(reportes, 1, 2, 2, 3);

            Button tops = new Button("Tops");
            tops.Clicked += (sender, e) => {
                TopVehiculos tops = new TopVehiculos(ventana);
                ventana.Hide();
            };
            table.Attach(tops, 0, 1, 3, 4);

            Button CerrarSesion = new Button("Cerrar sesión");
            CerrarSesion.Clicked += (sender, e) => {
                cerrarSesion.Show();
                ventana.Destroy();
            };
            contenedor.PackStart(CerrarSesion, false, false, 20);

            ventana.ShowAll();
        }
    }
}
```

### Carga masiva

El este espacio el usuario puede cargar todos los datos con los que se llevan los registros dentro del programa.

+ Código:

```csharp
namespace AutoGestPro.Views
{
    class CargaMasiva 
    {
        public CargaMasiva(Window menu)
        {
            Window ventana = new Window("Carga masiva - Root");
            ventana.SetDefaultSize(800, 600);
            ventana.SetPosition(WindowPosition.Center);
            ventana.DeleteEvent += delegate { Application.Quit(); };

            VBox contenedor = new VBox(false, 10);
            ventana.Add(contenedor);

            Label titulo = new Label("<span size='xx-large'>Carga masiva</span>");
            titulo.UseMarkup = true;
            contenedor.PackStart(titulo, false, false, 10);

            Table table = new Table(3, 2, false);
            contenedor.PackStart(table, false, false, 10);

            Label tituloOpciones = new Label("Seleccione una opción:");
            table.Attach(tituloOpciones, 0, 1, 0, 1, AttachOptions.Fill, AttachOptions.Fill, 5, 5);

            ComboBox opciones = new ComboBox();
            ListStore store = new ListStore(typeof(string));
            opciones.Model = store;

            CellRendererText celda = new CellRendererText();
            opciones.PackStart(celda, false);
            opciones.AddAttribute(celda, "text", 0);

            store.AppendValues("Usuarios");
            store.AppendValues("Vehiculos");
            store.AppendValues("Repuestos");

            opciones.Active = 0;
            string opcionSeleccionada = "Usuarios";
            opciones.Changed += (sender, e) => {
                TreeIter iter;
                if (((ComboBox)sender).GetActiveIter(out iter))
                {
                    opcionSeleccionada = (string)((ComboBox)sender).Model.GetValue(iter, 0);
                }
            };

            opciones.WidthRequest = 200;
            opciones.HeightRequest = 40;
            table.Attach(opciones, 1, 2, 0, 1, AttachOptions.Fill, AttachOptions.Fill, 5, 5);

            Button cargar = new Button("Cargar");
            cargar.WidthRequest = 100;
            cargar.HeightRequest = 40;
            cargar.Clicked += (sender, e) => {
                FileChooserDialog fileChooser = new FileChooserDialog(
                    "Seleccione un archivo .json",
                    null,
                    FileChooserAction.Open,
                    "Cancelar", ResponseType.Cancel,
                    "Abrir", ResponseType.Accept
                );

                FileFilter filter = new FileFilter();
                filter.AddPattern("*.json");
                fileChooser.Filter = filter;

                if (fileChooser.Run() == (int)ResponseType.Accept)
                {
                    string filePath = fileChooser.Filename;
                    if (opcionSeleccionada == "Usuarios")
                    {
                        string json = File.ReadAllText(filePath);
                        bool error = false;
                        using (JsonDocument doc = JsonDocument.Parse(json))
                        {
                            JsonElement root = doc.RootElement;
                            foreach (JsonElement element in root.EnumerateArray())
                            {
                                try
                                {
                                    int id = element.GetProperty("ID").GetInt32();
                                    string nombre = element.GetProperty("Nombres").GetString();
                                    string apellido = element.GetProperty("Apellidos").GetString();
                                    string correo = element.GetProperty("Correo").GetString();
                                    string contrasenia = element.GetProperty("Contrasenia").GetString();
                                    ListasGlobales.listaUsuarios.Insertar(id, nombre, apellido, correo, contrasenia);
                                }
                                catch (Exception ex)
                                {
                                    ListaSimple lista = new ListaSimple();
                                    ListasGlobales.listaUsuarios = lista;
                                    MostrarMensaje(ventana, $"Error al procesar el archivo: {ex.Message}");
                                    error = true;
                                    break;
                                }
                            }
                        }
                        if (!error)
                        {                            
                            MostrarMensaje(ventana, "Carga masiva exitosa");
                        }
                    }
                    else if (opcionSeleccionada == "Vehiculos")
                    {
                        string json = File.ReadAllText(filePath);
                        bool error = false;
                        using (JsonDocument doc = JsonDocument.Parse(json))
                        {
                            JsonElement root = doc.RootElement;
                            foreach (JsonElement element in root.EnumerateArray())
                            {
                                try
                                {
                                    int id = element.GetProperty("ID").GetInt32();
                                    int idUsuario = element.GetProperty("ID_Usuario").GetInt32();
                                    string marca = element.GetProperty("Marca").GetString();
                                    string modelo = element.GetProperty("Modelo").GetInt32().ToString();
                                    string placa = element.GetProperty("Placa").GetString();
                                    if (ListasGlobales.listaUsuarios.buscarUsuario(idUsuario).Id == null)
                                    {
                                        MostrarMensaje(ventana, $"Error al procesar el archivo: Usuario con ID {idUsuario} no existe");
                                        error = true;
                                        break;
                                    }
                                    ListasGlobales.listaVehiculos.Insertar(id, idUsuario, marca, modelo, placa);
                                }
                                catch (Exception ex)
                                {
                                    ListaDoble lista = new ListaDoble();
                                    ListasGlobales.listaVehiculos = lista;
                                    MostrarMensaje(ventana, $"Error al procesar el archivo: {ex.Message}");
                                    error = true;
                                    break;
                                }
                            }
                        }
                        if (!error)
                        {
                            MostrarMensaje(ventana, "Carga masiva exitosa");
                        }
                    }
                    else if (opcionSeleccionada == "Repuestos")
                    {
                        string json = File.ReadAllText(filePath);
                        bool error = false;
                        using (JsonDocument doc = JsonDocument.Parse(json))
                        {
                            JsonElement root = doc.RootElement;
                            foreach (JsonElement element in root.EnumerateArray())
                            {
                                try
                                {
                                    int id = element.GetProperty("ID").GetInt32();
                                    string repuesto = element.GetProperty("Repuesto").GetString();
                                    string detalles = element.GetProperty("Detalles").GetString();
                                    float costo = element.GetProperty("Costo").GetSingle();
                                    ListasGlobales.listaRepuestos.Insertar(id, repuesto, detalles, costo);
                                }
                                catch (Exception ex)
                                {
                                    ListaCircular lista = new ListaCircular(); 
                                    ListasGlobales.listaRepuestos = lista;
                                    MostrarMensaje(ventana, $"Error al procesar el archivo: {ex.Message}");
                                    error = true;
                                    break;
                                }
                            }
                        }
                        if (!error)
                        {
                            MostrarMensaje(ventana, "Carga masiva exitosa");
                        }
                    }
                    else
                    {
                        MostrarMensaje(ventana, "Error archivo no válido");
                    }
                }
                fileChooser.Destroy();
            };
            table.Attach(cargar, 0, 1, 1, 2, AttachOptions.Fill, AttachOptions.Fill, 5, 5);

            Button regresar = new Button("Regresar");
            regresar.WidthRequest = 100;
            regresar.HeightRequest = 40;
            regresar.Clicked += (sender, e) => {
                menu.Show();
                ventana.Destroy();
            };
            table.Attach(regresar, 1, 2, 1, 2, AttachOptions.Fill, AttachOptions.Fill, 5, 5);

            ventana.ShowAll();
        }

        private void MostrarMensaje(Window ventana, string mensaje)
        {
            MessageDialog dialog = new MessageDialog(ventana, 
                DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, mensaje);
            dialog.Run();
            dialog.Destroy();
        }
    }
}
```

### Reportse

En este espacio el usuario podra generar reportes sobre todas las estructuras que son utilizadas dentro del programa.

+ Código:

```csharp
namespace AutoGestPro.Views
{
    unsafe class Reportes 
    {
        public Reportes(Window menu)
        {
            Window ventana = new Window("Reportes - Root");
            ventana.SetDefaultSize(800, 600);
            ventana.SetPosition(WindowPosition.Center);
            ventana.DeleteEvent += delegate { Application.Quit(); };

            VBox contenedor = new VBox(false, 5);
            ventana.Add(contenedor);

            Label titulo = new Label("Generar reportes");
            titulo.ModifyFont(Pango.FontDescription.FromString("Arial 24"));
            contenedor.PackStart(titulo, false, false, 10);

            Table tabla = new Table(3, 2, true);
            contenedor.PackStart(tabla, true, true, 10);

            Button reporteUsuarios = new Button("Reporte de usuarios");
            reporteUsuarios.Clicked += (sender, e) => {
                GenerarReporteUsuarios();
            };
            tabla.Attach(reporteUsuarios, 0, 1, 0, 1);

            Button reporteVehiculos = new Button("Reporte de vehiculos");
            reporteVehiculos.Clicked += (sender, e) => {
                GenerarReporteVehiculos();
            };
            tabla.Attach(reporteVehiculos, 1, 2, 0, 1);

            Button reporteRepuestos = new Button("Reporte de repuestos");
            reporteRepuestos.Clicked += (sender, e) => {
                GenerarReporteRepuestos();
            };
            tabla.Attach(reporteRepuestos, 0, 1, 1, 2);

            Button reporteServicios = new Button("Reporte de servicios");
            reporteServicios.Clicked += (sender, e) => {
                GenerarReporteServicios();
            };
            tabla.Attach(reporteServicios, 1, 2, 1, 2);

            Button reporteFacturas = new Button("Reporte de facturas");
            reporteFacturas.Clicked += (sender, e) => {
                GenerarReporteFacturas();
            };
            tabla.Attach(reporteFacturas, 0, 1, 2, 3);

            Button reporteOrdenes = new Button("Reporte de ordenes");
            reporteOrdenes.Clicked += (sender, e) => {
                GenerarReporteOrdenes();
            };
            tabla.Attach(reporteOrdenes, 1, 2, 2, 3);

            Button regresar = new Button("Regresar");
            regresar.Clicked += (sender, e) => {
                menu.Show();
                ventana.Destroy();
            };
            contenedor.PackStart(regresar, false, false, 10);

            ventana.ShowAll();
        }
        void GenerarReporteUsuarios()
        {
            try
            {
            string dotPath = "/usr/bin/dot";
            string outputPath = "/home/emanuel/Escritorio/Proyectos/-EDD-Proyecto_202300539/Fase_1/AutoGestPro/Reportes/reporte_usuarios.png";
            string dotFilePath = "/home/emanuel/Escritorio/Proyectos/-EDD-Proyecto_202300539/Fase_1/AutoGestPro/Reportes/reporte_usuarios.dot";

            string dotContent = "digraph G {\n";
            dotContent += "node [shape=rect];\n";
            ListaSimple listaUsuarios = ListasGlobales.listaUsuarios;
            Usuario* actual = listaUsuarios.inicio;
            dotContent += "rankdir=LR;\n";
            while (actual != null)
            {
                String nombre = actual->Nombre;
                String apellido = actual->Apellido;
                String correo = actual->Correo;
                string id = actual->Id.ToString();
                dotContent += "node" + id + "[label=\"ID: " + id + " \nNombre y Apellido: " + nombre + " " + apellido + " \nCorreo: " + correo + "\", shape=rect, width=3];\n";
                if (actual->siguiente != null)
                {
                    dotContent += $"node{actual->Id} -> node{actual->siguiente->Id};\n";
                }
                actual = actual->siguiente;
            }
            dotContent += "}";

            File.WriteAllText(dotFilePath, dotContent);

            ProcessStartInfo startInfo = new ProcessStartInfo(dotPath)
            {
                Arguments = $"-Tpng {dotFilePath} -o {outputPath}",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = Process.Start(startInfo))
            {
                process.WaitForExit();
            }

            MessageDialog dialog = new MessageDialog(null, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "Reporte de usuarios generado exitosamente.");
            dialog.Run();
            dialog.Destroy();

            Process.Start("xdg-open", outputPath);
            }
            catch (Exception e)
            {
                MessageDialog dialog = new MessageDialog(null, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "Error al generar el reporte de usuarios.");
                dialog.Run();
                dialog.Destroy();
            }
        }

        void GenerarReporteVehiculos()
        {
            try
            {
            string dotPath = "/usr/bin/dot";
            string outputPath = "/home/emanuel/Escritorio/Proyectos/-EDD-Proyecto_202300539/Fase_1/AutoGestPro/Reportes/reporte_usuarios.png";
            string dotFilePath = "/home/emanuel/Escritorio/Proyectos/-EDD-Proyecto_202300539/Fase_1/AutoGestPro/Reportes/reporte_usuarios.dot";

            string dotContent = "digraph G {\n";
            dotContent += "node [shape=rect];\n";

            ListaDoble listaVehiculos = ListasGlobales.listaVehiculos;
            Vehiculo* actual = listaVehiculos.inicio;
            dotContent += "rankdir=LR;\n";

            while (actual != null)
            {
                String marca = actual->Marca;
                String modelo = actual->Modelo;
                String placa = actual->Placa;
                string id = actual->Id.ToString();
                dotContent += "node" + id + "[label=\"ID: " + id + "  \nMarca: " + marca + "  \nModelo: " + modelo + "  \nPlaca: " + placa + "\", shape=rect];\n";
                if (actual->siguiente != null)
                {
                    dotContent += $"node{actual->Id} -> node{actual->siguiente->Id};\n";
                    dotContent += $"node{actual->siguiente->Id} -> node{actual->Id};\n";
                }
                actual = actual->siguiente;
            }

            dotContent += "}";
            File.WriteAllText(dotFilePath, dotContent);
            ProcessStartInfo startInfo = new ProcessStartInfo(dotPath)
            {
                Arguments = $"-Tpng {dotFilePath} -o {outputPath}",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = Process.Start(startInfo))
            {
                process.WaitForExit();
            }

            MessageDialog dialog = new MessageDialog(null, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "Reporte de vehiculos generado exitosamente.");
            dialog.Run();
            dialog.Destroy();   

            Process.Start("xdg-open", outputPath);
            }
            catch (Exception e)
            {
                MessageDialog dialog = new MessageDialog(null, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "Error al generar el reporte de vehiculos.");
                dialog.Run();
                dialog.Destroy();
            }
        }

        void GenerarReporteRepuestos()
        {
            try
            {
            ListaCircular listaRepuestos = ListasGlobales.listaRepuestos;
            string dotPath = "/usr/bin/dot";
            string outputPath = "/home/emanuel/Escritorio/Proyectos/-EDD-Proyecto_202300539/Fase_1/AutoGestPro/Reportes/reporte_repuestos.png";
            string dotFilePath = "/home/emanuel/Escritorio/Proyectos/-EDD-Proyecto_202300539/Fase_1/AutoGestPro/Reportes/reporte_repuestos.dot";

            string dotContent = "digraph G {\n";
            dotContent += "node [shape=rect];\n";

            Repuesto* nodoActual = listaRepuestos.inicio;

            dotContent += "rankdir=LR;\n";
            while (nodoActual->siguiente != listaRepuestos.inicio)
            {
                String repuesto = nodoActual->repuesto;
                String detalles = nodoActual->detalle;
                String costo = nodoActual->costo.ToString();
                string id = nodoActual->Id.ToString();
                dotContent += "node" + id + "[label=\"ID: " + id + "  \nRepuesto: " + repuesto + "  \nDetalles: " + detalles + "  \nCosto: " + costo + "\", shape=rect];\n";
                if (nodoActual->siguiente != listaRepuestos.inicio)
                {
                    dotContent += $"node{id} -> node{nodoActual->siguiente->Id};\n";
                }
                nodoActual = nodoActual->siguiente;
            }
            if (nodoActual->siguiente == listaRepuestos.inicio)
            {
                dotContent += $"node{nodoActual->Id} -> node{listaRepuestos.inicio->Id};\n";
            }

            dotContent += "}";

            File.WriteAllText(dotFilePath, dotContent);

            ProcessStartInfo startInfo = new ProcessStartInfo(dotPath)
            {
                Arguments = $"-Tpng {dotFilePath} -o {outputPath}",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = Process.Start(startInfo))
            {
                process.WaitForExit();
            }

            MessageDialog dialog = new MessageDialog(null, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "Reporte de repuestos generado exitosamente.");

            dialog.Run();
            dialog.Destroy();

            Process.Start("xdg-open", outputPath);
            }
            catch (Exception e)
            {
                MessageDialog dialog = new MessageDialog(null, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "Error al generar el reporte de repuestos.");
                dialog.Run();
                dialog.Destroy();
            }
        }

        void GenerarReporteServicios()
        {
            try
            {
            Cola colaServicios = ListasGlobales.colaServicios;
            string dotPath = "/usr/bin/dot";

            string outputPath = "/home/emanuel/Escritorio/Proyectos/-EDD-Proyecto_202300539/Fase_1/AutoGestPro/Reportes/reporte_servicios.png";
            string dotFilePath = "/home/emanuel/Escritorio/Proyectos/-EDD-Proyecto_202300539/Fase_1/AutoGestPro/Reportes/reporte_servicios.dot";

            Servicio* actual = colaServicios.inicio;
            string dotContent = "digraph G {\n";
            dotContent += "node [shape=rect];\n";
            dotContent += "rankdir=LR;\n";

            while (actual != null)
            {
                String id = actual->Id.ToString();
                String idRepuesto = actual->Id_Repuesto.ToString();
                String idVehiculo = actual->Id_Vehiculo.ToString();
                String detalles = actual->Detalles;
                String costo = actual->Costo.ToString();
                dotContent += "node" + id + "[label=\"ID: " + id + "  \nID Repuesto: " + idRepuesto + "  \nID Vehiculo: " + idVehiculo + "  \nDetalles: " + detalles + "  \nCosto: " + costo + "\", shape=rect];\n";
                if (actual->siguiente != null)
                {
                    dotContent += $"node{id} -> node{actual->siguiente->Id};\n";
                }
                actual = actual->siguiente;
            }

            dotContent += "}";

            File.WriteAllText(dotFilePath, dotContent);

            ProcessStartInfo startInfo = new ProcessStartInfo(dotPath)
            {
                Arguments = $"-Tpng {dotFilePath} -o {outputPath}",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = Process.Start(startInfo))
            {
                process.WaitForExit();
            }

            MessageDialog dialog = new MessageDialog(null, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "Reporte de servicios generado exitosamente.");
            dialog.Run();
            dialog.Destroy();

            Process.Start("xdg-open", outputPath);
            }
            catch (Exception e)
            {
                MessageDialog dialog = new MessageDialog(null, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "Error al generar el reporte de servicios.");
                dialog.Run();
                dialog.Destroy();
            }
        }

        void GenerarReporteFacturas()
        {
            try{
            Pila pilaFacturas = ListasGlobales.pilaFacturas;
            string dotPath = "/usr/bin/dot";
            string outputPath = "/home/emanuel/Escritorio/Proyectos/-EDD-Proyecto_202300539/Fase_1/AutoGestPro/Reportes/reporte_facturas.png";
            string dotFilePath = "/home/emanuel/Escritorio/Proyectos/-EDD-Proyecto_202300539/Fase_1/AutoGestPro/Reportes/reporte_facturas.dot";

            string dotContent = "digraph G {\n";
            dotContent += "node [shape=rect];\n";
            dotContent += "rankdir=TB;\n";

            Factura* actual = pilaFacturas.sima;

            while (actual != null)
            {
                String id = actual->Id.ToString();
                String idOrden = actual->Id_Orden.ToString();
                String total = actual->Total.ToString();
                dotContent += "node" + id + "[label=\"ID: " + id + "  \nID Orden: " + idOrden + "  \nTotal: " + total + "\", shape=rect];\n";
                if (actual->abajo != null)
                {
                    dotContent += $"node{id} -> node{actual->abajo->Id};\n";
                }
                actual = actual->abajo;
            }

            dotContent += "}";
            File.WriteAllText(dotFilePath, dotContent);

            ProcessStartInfo startInfo = new ProcessStartInfo(dotPath)
            {
                Arguments = $"-Tpng {dotFilePath} -o {outputPath}",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (Process process = Process.Start(startInfo))
            {
                process.WaitForExit();
            }

            MessageDialog dialog = new MessageDialog(null, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "Reporte de facturas generado exitosamente.");
            dialog.Run();
            dialog.Destroy();

            Process.Start("xdg-open", outputPath);
            }
            catch (Exception e)
            {
                MessageDialog dialog = new MessageDialog(null, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, "Error al generar el reporte de facturas.");
                dialog.Run();
                dialog.Destroy();
            }
        }
        void GenerarReporteOrdenes()
        {
            ListasGlobales.matrizDispersa.graficar();
        }
    }
}
```

### Top vehiculos

En este espacio el usuario puede visualizar los top mas relevantes sobre los vehiculos almacenados dentro del programa.

+ Código:

```csharp
namespace AutoGestPro.Views
{
    unsafe class TopVehiculos
    {
        public TopVehiculos(Window menu)
        {
            Window ventana = new Window("Top Vehiculos");
            ventana.SetDefaultSize(800, 600);
            ventana.SetPosition(WindowPosition.Center);
            ventana.DeleteEvent += delegate { Application.Quit(); };

            VBox container = new VBox(false, 10);
            ventana.Add(container);

            Label title = new Label("<span size='xx-large'>Top Vehiculos</span>");
            title.UseMarkup = true;
            container.PackStart(title, false, false, 10);

            Label masServicios = new Label("Vehiculos con más servicios:");
            container.PackStart(masServicios, false, false, 10);

            Label top1 = new Label("");
            container.PackStart(top1, false, false, 10);

            Label top2 = new Label("");
            container.PackStart(top2, false, false, 10);

            Label top3 = new Label("");
            container.PackStart(top3, false, false, 10);

            Label top4 = new Label("");
            container.PackStart(top4, false, false, 10);

            Label top5 = new Label("");
            container.PackStart(top5, false, false, 10);

            Label masAntiguo = new Label("Vehiculos más antiguos:");
            container.PackStart(masAntiguo, false, false, 10);

            Label top1Antiguo = new Label("");
            container.PackStart(top1Antiguo, false, false, 10);

            Label top2Antiguo = new Label("");
            container.PackStart(top2Antiguo, false, false, 10);

            Label top3Antiguo = new Label("");
            container.PackStart(top3Antiguo, false, false, 10);

            Label top4Antiguo = new Label("");
            container.PackStart(top4Antiguo, false, false, 10);

            Label top5Antiguo = new Label("");
            container.PackStart(top5Antiguo, false, false, 10);


            Dictionary<int, int> vehiculoServicios = new Dictionary<int, int>();

            Cola servicios = ListasGlobales.colaServicios;
            ListaDoble listaVehiculos = ListasGlobales.listaVehiculos;
            Vehiculo* actual = listaVehiculos.inicio;
            Servicio* aux = servicios.inicio;

            while (aux != null)
            {
                if (vehiculoServicios.ContainsKey(aux->Id_Vehiculo))
                {
                    vehiculoServicios[aux->Id_Vehiculo]++;
                }
                else
                {
                    vehiculoServicios[aux->Id_Vehiculo] = 1;
                }
                aux = aux->siguiente;
            }

            var topVehiculos = vehiculoServicios.OrderByDescending(v => v.Value).Take(5).ToList();

            if (topVehiculos.Count > 0) top1.Text = "1. " + topVehiculos[0].Key + " con " + topVehiculos[0].Value + " servicios";
            if (topVehiculos.Count > 1) top2.Text = "2. " + topVehiculos[1].Key + " con " + topVehiculos[1].Value + " servicios";
            if (topVehiculos.Count > 2) top3.Text = "3. " + topVehiculos[2].Key + " con " + topVehiculos[2].Value + " servicios";
            if (topVehiculos.Count > 3) top4.Text = "4. " + topVehiculos[3].Key + " con " + topVehiculos[3].Value + " servicios";
            if (topVehiculos.Count > 4) top5.Text = "5. " + topVehiculos[4].Key + " con " + topVehiculos[4].Value + " servicios";


            Vehiculo* actual2 = listaVehiculos.inicio;
            Dictionary<int, int> vehiculoAntiguedad = new Dictionary<int, int>();

            while (actual2 != null)
            {
                vehiculoAntiguedad[actual2->Id] = DateTime.Now.Year - int.Parse(actual2->Modelo);
                actual2 = actual2->siguiente;
            }

            var topVehiculosAntiguedad = vehiculoAntiguedad.OrderByDescending(v => v.Value).Take(5).ToList();

            if (topVehiculosAntiguedad.Count > 0) top1Antiguo.Text = "1. " + topVehiculosAntiguedad[0].Key + " con " + topVehiculosAntiguedad[0].Value + " años";
            if (topVehiculosAntiguedad.Count > 1) top2Antiguo.Text = "2. " + topVehiculosAntiguedad[1].Key + " con " + topVehiculosAntiguedad[1].Value + " años";
            if (topVehiculosAntiguedad.Count > 2) top3Antiguo.Text = "3. " + topVehiculosAntiguedad[2].Key + " con " + topVehiculosAntiguedad[2].Value + " años";
            if (topVehiculosAntiguedad.Count > 3) top4Antiguo.Text = "4. " + topVehiculosAntiguedad[3].Key + " con " + topVehiculosAntiguedad[3].Value + " años";
            if (topVehiculosAntiguedad.Count > 4) top5Antiguo.Text = "5. " + topVehiculosAntiguedad[4].Key + " con " + topVehiculosAntiguedad[4].Value + " años";

            Button regresar = new Button("Regresar");
            regresar.WidthRequest = 100;
            regresar.HeightRequest = 40;
            regresar.Clicked += (sender, e) => {
                menu.Show();
                ventana.Destroy();
            };
            container.PackStart(regresar, false, false, 10);
            
            ventana.ShowAll();

        }
    }
}
```
