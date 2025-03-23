using System;
using Gtk;
using AutoGestPro.Models.Listas.Arbol_AVL;
using AutoGestPro.Models;
using AutoGestPro.Models.Listas;  

namespace AutoGestPro.Views
{
    class ActualizarRepuesto
    {
        public ActualizarRepuesto(Window gestionRepuestos)
        {
            Window ventana = new Window("Gestion de repuestos - Root");
            ventana.SetDefaultSize(600, 400);
            ventana.SetPosition(WindowPosition.Center);
            ventana.DeleteEvent += delegate { Application.Quit(); };

            VBox contenedor = new VBox(false, 10);
            ventana.Add(contenedor);

            Label titulo = new Label("<b>Editor de repuesto</b>");
            titulo.UseMarkup = true;
            titulo.Justify = Justification.Center;
            contenedor.PackStart(titulo, false, false, 10);

            Table table = new Table(4, 3, false);
            table.ColumnSpacing = 10;
            table.RowSpacing = 10;
            contenedor.PackStart(table, true, true, 10);

            Label id = new Label("ID:");
            table.Attach(id, 0, 1, 0, 1);

            Entry idEntry = new Entry();
            table.Attach(idEntry, 1, 2, 0, 1);

            Button buscar = new Button("Buscar");
            table.Attach(buscar, 2, 3, 0, 1);

            Label repuesto_ = new Label("Repuesto:");
            table.Attach(repuesto_, 0, 1, 1, 2);

            Label repuestoActual = new Label("Null");
            table.Attach(repuestoActual, 1, 2, 1, 2);

            Entry repuestoEntry = new Entry();
            table.Attach(repuestoEntry, 2, 3, 1, 2);

            Label detalles = new Label("Detalles:");
            table.Attach(detalles, 0, 1, 2, 3);

            Label detallesActual = new Label("Null");
            table.Attach(detallesActual, 1, 2, 2, 3);

            Entry detallesEntry = new Entry();
            table.Attach(detallesEntry, 2, 3, 2, 3);

            Label costo = new Label("Costo:");
            table.Attach(costo, 0, 1, 3, 4);

            Label costoActual = new Label("Null");
            table.Attach(costoActual, 1, 2, 3, 4);

            Entry costoEntry = new Entry();
            table.Attach(costoEntry, 2, 3, 3, 4);
            
            buscar.Clicked += (sender, e) => {
                int id = int.Parse(idEntry.Text);
                Repuesto repuesto = ListasGlobales.arbolRepuestos.Buscar(ListasGlobales.arbolRepuestos.Raiz, id).Repuesto;
                if (repuesto.Id != null)
                {
                    repuestoActual.Text = repuesto.REpuesto;
                    detallesActual.Text = repuesto.Detalle;
                    costoActual.Text = repuesto.Costo.ToString();
                }
                else
                {
                    repuestoActual.Text = "No encontrado";
                    detallesActual.Text = "No encontrado";
                    costoActual.Text = "No encontrado";
                }
            };

            HBox buttonContainer = new HBox(true, 10);
            contenedor.PackStart(buttonContainer, false, false, 10);

            Button actualizar = new Button("Actualizar");
            actualizar.Clicked += (sender, e) => {
                int Id_ = int.Parse(idEntry.Text);
                string Repuesto_ = repuestoEntry.Text;
                string Detalles_ = detallesEntry.Text;
                string Costo_ = costoEntry.Text;
                float Costo__ = float.Parse(Costo_);
                if (Repuesto_ != "")
                {
                    repuestoActual.Text = Repuesto_;
                } else {
                    Repuesto_ = repuestoActual.Text;
                }
                if (Detalles_ != "")
                { 
                    detallesActual.Text = Detalles_;
                } else {
                    Detalles_ = detallesActual.Text;
                }
                if (Costo_ != "")
                {
                    costoActual.Text = Costo_;
                } else {
                    Costo__ = float.Parse(costoActual.Text);
                }

                Repuesto repuesto = ListasGlobales.arbolRepuestos.Buscar(ListasGlobales.arbolRepuestos.Raiz, Id_).Repuesto;
                if (repuesto.Id == null)
                {
                    repuestoActual.Text = "No encontrado";
                    detallesActual.Text = "No encontrado";
                    costoActual.Text = "No encontrado";
                    return;
                }

                idEntry.Text = "";
                repuestoEntry.Text = "";
                detallesEntry.Text = "";
                costoEntry.Text = "";
                ListasGlobales.arbolRepuestos.ActualizarRepuesto(Id_, Repuesto_, Detalles_, Costo__);
            };
            buttonContainer.PackStart(actualizar, true, true, 0);

            Button regresar = new Button("Regresar");
            regresar.Clicked += (sender, e) => {
                gestionRepuestos.Show();
                ventana.Destroy();
            };
            buttonContainer.PackStart(regresar, true, true, 0);

            ventana.ShowAll();
        }
    }
}