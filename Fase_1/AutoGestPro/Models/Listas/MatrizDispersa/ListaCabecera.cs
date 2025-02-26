using System;
using System.Runtime.InteropServices;

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