using System;
using System.Collections.Generic;

namespace NolkataInc.Clases
{
    public class Requisicion
    {
        // Atributos privados
        private DateTime fecha;
        private string barco;
        private List<DetalleRequisicion> detalles;

        // Propiedades publicas
        public DateTime Fecha
        {
            get { return fecha; }
            set { fecha = value; }
        }

        public string Barco
        {
            get { return barco; }
            set { barco = value; }
        }

        public List<DetalleRequisicion> Detalles
        {
            get { return detalles; }
        }

        // Constructor
        public Requisicion(string barco, DateTime fecha)
        {
            this.barco   = barco;
            this.fecha   = fecha;
            this.detalles = new List<DetalleRequisicion>();
        }

        // Agrega un producto a la requisicion
        public void AgregarDetalle(DetalleRequisicion detalle)
        {
            detalles.Add(detalle);
        }

        // Calcula el costo total sumando todos los detalles
        public double CalcularCostoTotal()
        {
            double total = 0;
            foreach (DetalleRequisicion d in detalles)
                total += d.CalcularSubtotal();
            return total;
        }
    }
}
