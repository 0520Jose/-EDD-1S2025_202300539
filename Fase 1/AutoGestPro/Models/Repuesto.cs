using System;

class Repuesto
{
    public string Id { get; set; }
    public string repuesto { get; set; }
    public string detalles { get; set; }
    public string precio { get; set; }

    public Repuesto(string id, string repuesto, string detalles, string precio)
    {
        this.Id = id;
        this.repuesto = repuesto;
        this.detalles = detalles;
        this.precio = precio;
    }
}