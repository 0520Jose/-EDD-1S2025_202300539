using System;
using System.Runtime.InteropServices;

namespace AutoGestPro.Models.Listas.MatrizDispersa
{
    public unsafe class MatrizDispersa
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

        public void graficar()
        {
            string dotPath = "/usr/bin/dot";
            string outputPath = "/home/emanuel/Escritorio/Proyectos/-EDD-Proyecto_202300539/Fase_1/AutoGestPro/Reportes/MatrizDispersa->png";
            string dotFilePath = "/home/emanuel/Escritorio/Proyectos/-EDD-Proyecto_202300539/Fase_1/AutoGestPro/Reportes/MatrizDispersa->dot";

            string dotContent = "digraph G {\n";
            dotContent += "node [shape=record];\n";
            dotContent += "rankdir=UD;\n";

            NodoCabecera* filaactual = filas.primero;
            string idFila = "";
            string conexionesFilas = "";
            string nodosInteriores = "";
            string direccionInteriores = "";
            while (filaactual != null)
            {
                bool primero = true;
                NodoCelda* actual = filaactual->acceso;
                idFila += "";
            }


        }
    }
}