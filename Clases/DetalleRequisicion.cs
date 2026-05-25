using System;

namespace NolkataInc.Clases
{
    public class DetalleRequisicion
    {
        // Atributos privados
        private string producto;
        private int cantidad;
        private EstadoProducto estado;
        private DateTime fechaUltimoCambio;
        private double costoUnitario;

        // Propiedades publicas
        public string Producto
        {
            get { return producto; }
            set { producto = value; }
        }

        public int Cantidad
        {
            get { return cantidad; }
            set { cantidad = value; }
        }

        public EstadoProducto Estado
        {
            get { return estado; }
            set
            {
                estado = value;
                // Cada vez que cambia el estado se registra la fecha
                fechaUltimoCambio = DateTime.Now;
            }
        }

        public DateTime FechaUltimoCambio
        {
            get { return fechaUltimoCambio; }
        }

        public double CostoUnitario
        {
            get { return costoUnitario; }
            set { costoUnitario = value; }
        }

        // Constructor
        public DetalleRequisicion(string producto, int cantidad, double costoUnitario)
        {
            this.producto        = producto;
            this.cantidad        = cantidad;
            this.costoUnitario   = costoUnitario;
            this.estado          = EstadoProducto.Pendiente;
            this.fechaUltimoCambio = DateTime.Now;
        }

        // Calcula el subtotal de este detalle
        public double CalcularSubtotal()
        {
            return cantidad * costoUnitario;
        }

        // Verifica si lleva mas de 3 dias pendiente (alerta)
        public bool EstaAtrasado()
        {
            if (estado == EstadoProducto.Pendiente)
                return (DateTime.Now - fechaUltimoCambio).TotalDays > 3;
            return false;
        }
    }
}
