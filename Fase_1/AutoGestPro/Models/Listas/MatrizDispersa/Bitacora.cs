using System;

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