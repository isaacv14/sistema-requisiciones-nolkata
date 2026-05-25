using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using NolkataInc.Clases;

namespace NolkataInc
{
    public partial class frmPrincipal : Form
    {
        private Usuario usuarioActual;
        private List<Requisicion> listaRequisiciones = new List<Requisicion>();

        public frmPrincipal(Usuario usuarioQueViene)
        {
            InitializeComponent();
            this.usuarioActual = usuarioQueViene;
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            // Saludo personalizado
            lblBienvenida.Text = "Bienvenido/a, " + usuarioActual.Nombre;
            lblTipoUsuario.Text = usuarioActual is UsuarioOficina ? "Oficina" : "Barco";

            // Mostrar u ocultar columna de costos segun el tipo de usuario
            if (!usuarioActual.PuedeVerCostos())
            {
                colCosto.Visible     = false;
                lblAvisoCosto.Visible = true;
            }

            // Cargar datos de ejemplo
            CargarDatosEjemplo();
            RefrescarGrid();
        }

        // Carga datos de ejemplo para mostrar el sistema funcionando
        private void CargarDatosEjemplo()
        {
            Requisicion r1 = new Requisicion("Sol", DateTime.Now.AddDays(-5));
            r1.AgregarDetalle(new DetalleRequisicion("Valvula hidraulica", 2, 150.00));
            r1.AgregarDetalle(new DetalleRequisicion("Filtro de aceite", 4, 35.00));

            Requisicion r2 = new Requisicion("Poseidon", DateTime.Now.AddDays(-1));
            r2.AgregarDetalle(new DetalleRequisicion("Correa de motor", 1, 280.00));

            listaRequisiciones.Add(r1);
            listaRequisiciones.Add(r2);
        }

        private void RefrescarGrid()
        {
            dgvRequisiciones.Rows.Clear();

            foreach (Requisicion r in listaRequisiciones)
            {
                string costo = "$" + r.CalcularCostoTotal().ToString("F2");
                dgvRequisiciones.Rows.Add(
                    r.Fecha.ToShortDateString(),
                    r.Barco,
                    r.Detalles.Count + " producto(s)",
                    costo
                );
            }
        }

        private void btnNuevaRequisicion_Click(object sender, EventArgs e)
        {
            frmRequisicion frm = new frmRequisicion(usuarioActual, listaRequisiciones);
            frm.ShowDialog();
            RefrescarGrid();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            frmLogin login = new frmLogin();
            login.Show();
            this.Close();
        }

        // Colorear filas del DataGridView segun estado de productos atrasados
        private void dgvRequisiciones_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if (e.RowIndex < listaRequisiciones.Count)
            {
                Requisicion r = listaRequisiciones[e.RowIndex];
                bool hayAtrasado = false;
                foreach (DetalleRequisicion d in r.Detalles)
                {
                    if (d.EstaAtrasado())
                    {
                        hayAtrasado = true;
                        break;
                    }
                }
                if (hayAtrasado)
                    dgvRequisiciones.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.FromArgb(255, 220, 220);
            }
        }
    }
}
