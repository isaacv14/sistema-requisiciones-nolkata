using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using NolkataInc.Clases;

namespace NolkataInc
{
    public partial class frmRequisicion : Form
    {
        private Usuario usuarioActual;
        private List<Requisicion> listaRequisiciones;
        private List<DetalleRequisicion> detallesTemp = new List<DetalleRequisicion>();

        public frmRequisicion(Usuario usuario, List<Requisicion> lista)
        {
            InitializeComponent();
            this.usuarioActual     = usuario;
            this.listaRequisiciones = lista;
        }

        private void frmRequisicion_Load(object sender, EventArgs e)
        {
            // Ocultar columna de costo si es UsuarioBarco
            if (!usuarioActual.PuedeVerCostos())
            {
                colDetCosto.Visible       = false;
                txtCostoUnitario.Visible  = false;
                lblCostoUnitario.Visible  = false;
            }

            dtpFecha.Value = DateTime.Now;
            RefrescarGridDetalles();
        }

        private void btnAgregarProducto_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProducto.Text) ||
                string.IsNullOrWhiteSpace(txtCantidad.Text))
            {
                MessageBox.Show("Complete el nombre del producto y la cantidad.", "Campos vacios",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int cantidad     = int.Parse(txtCantidad.Text);
            double costo     = 0;
            if (usuarioActual.PuedeVerCostos() && !string.IsNullOrWhiteSpace(txtCostoUnitario.Text))
                costo = double.Parse(txtCostoUnitario.Text);

            DetalleRequisicion detalle = new DetalleRequisicion(txtProducto.Text, cantidad, costo);
            detallesTemp.Add(detalle);

            txtProducto.Clear();
            txtCantidad.Clear();
            txtCostoUnitario.Clear();
            RefrescarGridDetalles();
        }

        private void RefrescarGridDetalles()
        {
            dgvDetalles.Rows.Clear();

            foreach (DetalleRequisicion d in detallesTemp)
            {
                int idx = dgvDetalles.Rows.Add(
                    d.Producto,
                    d.Cantidad,
                    d.Estado.ToString(),
                    "$" + d.CostoUnitario.ToString("F2"),
                    d.FechaUltimoCambio.ToShortDateString()
                );

                // Colorear la celda de estado segun el valor
                DataGridViewCell celdaEstado = dgvDetalles.Rows[idx].Cells["colDetEstado"];
                switch (d.Estado)
                {
                    case EstadoProducto.Pendiente:
                        celdaEstado.Style.BackColor = Color.FromArgb(255, 220, 100); // amarillo
                        celdaEstado.Style.ForeColor = Color.FromArgb(100, 70, 0);
                        break;
                    case EstadoProducto.EnProceso:
                        celdaEstado.Style.BackColor = Color.FromArgb(100, 160, 255); // azul
                        celdaEstado.Style.ForeColor = Color.White;
                        break;
                    case EstadoProducto.Entregado:
                        celdaEstado.Style.BackColor = Color.FromArgb(80, 200, 120);  // verde
                        celdaEstado.Style.ForeColor = Color.White;
                        break;
                }
            }

            // Actualizar total visible solo para oficina
            if (usuarioActual.PuedeVerCostos())
            {
                double total = 0;
                foreach (DetalleRequisicion d in detallesTemp)
                    total += d.CalcularSubtotal();
                lblTotalValor.Text = "$" + total.ToString("F2");
            }
        }

        private void btnGuardarRequisicion_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtBarco.Text))
            {
                MessageBox.Show("Ingrese el nombre del barco.", "Campo vacio",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (detallesTemp.Count == 0)
            {
                MessageBox.Show("Agregue al menos un producto.", "Sin productos",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Requisicion nueva = new Requisicion(txtBarco.Text, dtpFecha.Value);
            foreach (DetalleRequisicion d in detallesTemp)
                nueva.AgregarDetalle(d);

            listaRequisiciones.Add(nueva);
            MessageBox.Show("Requisicion guardada con exito.", "Exito",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
