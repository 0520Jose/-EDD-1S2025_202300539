using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Gtk;

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

        public void graficar()
        {
            try
            {
            string dotPath = "/usr/bin/dot";
            string outputPath = "/home/emanuel/Escritorio/Proyectos/-EDD-Proyecto_202300539/Fase_1/AutoGestPro/Reportes/MatrizDispersa.png";
            string dotFilePath = "/home/emanuel/Escritorio/Proyectos/-EDD-Proyecto_202300539/Fase_1/AutoGestPro/Reportes/MatrizDispersa.dot";

            string dotContent = "digraph G {\n";
            dotContent += "graph [pad=\"0.5\", nodesep=\"0.5\", ranksep=\"0.5\"]\n";
            dotContent += "node [shape=box, height=0.8]\n";

            NodoCabecera* filaactual = filas.primero;
            string idFila = "";
            string conexionesFilas = "";
            string nodosInteriores = "";
            string direccionesInteriores = "";
            while (filaactual != null)
            {
                bool primero = true;
                NodoCelda* actual = filaactual->acceso;
                idFila += $"\tFila{actual->x}[style=\"filled\", fillcolor=\"white\", label=\"V {filaactual->id}\", group=0];\n";
                if (filaactual->siguiente != null)
                {
                conexionesFilas += $"\tFila{actual->x} -> Fila{filaactual->siguiente->acceso->x} [dir=\"both\"];\n";
                }
                string actual_x = actual->x.ToString();
                direccionesInteriores += "\t{rank = same; Fila" + actual_x + ";";
                while (actual != null)
                {
                nodosInteriores += $"\tNodo{actual->x}_{actual->y}[style=\"filled\", fillcolor=\"lightblue\", label=\"{actual->bitacora.Detalle}\", group =\"{actual->y}\"];\n";
                actual_x = actual->x.ToString();
                string actual_y = actual->y.ToString();
                direccionesInteriores += $"\tNodo" + actual_x + "_" + actual_y + ";";
                if (primero)
                {
                    nodosInteriores += $"\tFila{actual->x} -> Nodo{actual->x}_{actual->y} [dir=\"both\"];\n";
                    if (actual->derecha != null)
                    {
                    nodosInteriores += $"\tNodo{actual->x}_{actual->y} -> Nodo{actual->derecha->x}_{actual->derecha->y} [dir=\"both\"];\n";
                    }
                    primero = false;
                }
                else
                {
                    if (actual->derecha != null)
                    {
                    nodosInteriores += $"\tNodo{actual->x}_{actual->y} -> Nodo{actual->derecha->x}_{actual->derecha->y} [dir=\"both\"];\n";
                    }
                }
                actual = actual->derecha;
                }
                filaactual = filaactual->siguiente;
                direccionesInteriores += "}\n";
            }
            dotContent += idFila + conexionesFilas;

            NodoCabecera* columnaactual = columnas.primero;
            string idColumna = "";
            string conexionesColumnas = "";
            string direccionesInteriores2 = "\t{rank = same;";
            while (columnaactual != null)
            {
                bool primero = true;
                NodoCelda* actual = columnaactual->acceso;
                idColumna += $"\tColumna{actual->y}[style=\"filled\", fillcolor=\"lightblue\", label=\"R {actual->y}\"];\n";
                string actual_y = actual->y.ToString();
                direccionesInteriores2 += "Columna" + actual_y + ";";
                if (columnaactual->siguiente != null)
                {
                conexionesColumnas += $"\tColumna{actual->y} -> Columna{columnaactual->siguiente->acceso->y} [dir=\"both\"];\n";
                }
                while (actual != null)
                {
                if (primero)
                {
                    dotContent += $"\tColumna{actual->y} -> Nodo{actual->x}_{actual->y} [dir=\"both\"];\n";
                    if (actual->abajo != null)
                    {
                    dotContent += $"\tNodo{actual->x}_{actual->y} -> Nodo{actual->abajo->x}_{actual->abajo->y} [dir=\"both\"];\n";
                    }
                    primero = false;
                }
                else
                {
                    if (actual->abajo != null)
                    {
                    dotContent += $"\tNodo{actual->x}_{actual->y} -> Nodo{actual->abajo->x}_{actual->abajo->y} [dir=\"both\"];\n";
                    }
                }
                actual = actual->abajo;
                }
                columnaactual = columnaactual->siguiente;
            }
            direccionesInteriores2 += "}\n";
            dotContent += idColumna + conexionesColumnas + direccionesInteriores2 + nodosInteriores + direccionesInteriores;

            dotContent += "}";

            File.WriteAllText(dotFilePath, dotContent);

            ProcessStartInfo startInfo = new ProcessStartInfo(dotPath)
            {
                Arguments = $"-Tpng {dotFilePath} -o {outputPath}",
                UseShellExecute = false,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                CreateNoWindow = true,
            };

            using (Process process = Process.Start(startInfo))
            {
                process.WaitForExit();
            }

            MessageDialog dialog = new MessageDialog(null, DialogFlags.Modal, MessageType.Info, ButtonsType.Ok, "Reporte generado con éxito");
            dialog.Run();
            dialog.Destroy();

            Process.Start("xdg-open", outputPath);
            }
            catch (Exception e)
            {
            MessageDialog dialog = new MessageDialog(null, DialogFlags.Modal, MessageType.Error, ButtonsType.Ok, e.Message);
            dialog.Run();
            dialog.Destroy();
            }
        }

    }
}
