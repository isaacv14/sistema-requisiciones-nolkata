using System;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace Nolkata_Final
{
    public partial class frmAdminPrincipal : Form
    {
        private long _idAdminActual;
        private string _nombreAdminActual;
        private DataGridView dgv;

        public frmAdminPrincipal(long idAdmin, string nombreAdmin)
        {
            _idAdminActual = idAdmin;
            _nombreAdminActual = nombreAdmin;
            InitializeComponent();
            this.BackColor = ColoresSistema.ColorFondo;
            this.Text = "NOLKATA MARINE - Administrador";
            this.Size = new System.Drawing.Size(1300, 700);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Título
            Label lbl = new Label()
            {
                Text = $"Bienvenido Administrador, {_nombreAdminActual}",
                Font = new System.Drawing.Font("Segoe UI", 14, System.Drawing.FontStyle.Bold),
                ForeColor = ColoresSistema.ColorPrincipal,
                Location = new System.Drawing.Point(20, 20),
                Size = new System.Drawing.Size(400, 35)
            };
            this.Controls.Add(lbl);

            int yBotones = 70;
            int btnAncho = 160;
            int btnAlto = 45;

            // Botón Crear Usuario Barco
            Button btnCrearUsuario = new Button()
            {
                Text = "Crear Usuario Barco",
                BackColor = ColoresSistema.ColorAcento,
                ForeColor = ColoresSistema.ColorTextoClaro,
                FlatStyle = FlatStyle.Flat,
                Location = new System.Drawing.Point(20, yBotones),
                Size = new System.Drawing.Size(btnAncho, btnAlto)
            };
            btnCrearUsuario.Click += (s, e) =>
            {
                frmCrearUsuarioBarco form = new frmCrearUsuarioBarco(_idAdminActual, _nombreAdminActual);
                form.ShowDialog();
                CargarRequisiciones();
            };
            this.Controls.Add(btnCrearUsuario);

            // Botón Nueva Requisición
            Button btnNuevaReq = new Button()
            {
                Text = "Nueva Requisición",
                BackColor = ColoresSistema.ColorPrincipal,
                ForeColor = ColoresSistema.ColorTextoClaro,
                FlatStyle = FlatStyle.Flat,
                Location = new System.Drawing.Point(200, yBotones),
                Size = new System.Drawing.Size(btnAncho, btnAlto)
            };
            btnNuevaReq.Click += (s, e) =>
            {
                new frmNuevaRequisicion(_nombreAdminActual, true, _idAdminActual).ShowDialog();
                CargarRequisiciones();
            };
            this.Controls.Add(btnNuevaReq);

            // Botón Ver Detalles
            Button btnDetalle = new Button()
            {
                Text = "Ver Detalles",
                BackColor = ColoresSistema.ColorSecundario,
                ForeColor = ColoresSistema.ColorTextoClaro,
                FlatStyle = FlatStyle.Flat,
                Location = new System.Drawing.Point(380, yBotones),
                Size = new System.Drawing.Size(btnAncho, btnAlto)
            };
            btnDetalle.Click += (s, e) =>
            {
                if (dgv.CurrentRow != null)
                {
                    int id = Convert.ToInt32(dgv.CurrentRow.Cells["ID"].Value);
                    new frmDetalleRequisicion(id, true, _idAdminActual).ShowDialog();
                    CargarRequisiciones();
                }
                else
                    MessageBox.Show("Seleccione una requisición", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            };
            this.Controls.Add(btnDetalle);

            // Botón Reportes
            Button btnReportes = new Button()
            {
                Text = "Reportes",
                BackColor = ColoresSistema.ColorSecundario,
                ForeColor = ColoresSistema.ColorTextoClaro,
                FlatStyle = FlatStyle.Flat,
                Location = new System.Drawing.Point(560, yBotones),
                Size = new System.Drawing.Size(btnAncho, btnAlto)
            };
            btnReportes.Click += (s, e) => new frmReportes().ShowDialog();
            this.Controls.Add(btnReportes);

            // Botón Gestión Productos
            Button btnProductos = new Button()
            {
                Text = "Gestión Productos",
                BackColor = ColoresSistema.ColorAcento,
                ForeColor = ColoresSistema.ColorTextoClaro,
                FlatStyle = FlatStyle.Flat,
                Location = new System.Drawing.Point(740, yBotones),
                Size = new System.Drawing.Size(btnAncho, btnAlto)
            };
            btnProductos.Click += (s, e) => new frmGestionProductos().ShowDialog();
            this.Controls.Add(btnProductos);

            // Botón Gestión Barcos
            Button btnGestionBarcos = new Button()
            {
                Text = "Gestión Barcos",
                BackColor = ColoresSistema.ColorAcento,
                ForeColor = ColoresSistema.ColorTextoClaro,
                FlatStyle = FlatStyle.Flat,
                Location = new System.Drawing.Point(920, yBotones),
                Size = new System.Drawing.Size(btnAncho, btnAlto)
            };
            btnGestionBarcos.Click += (s, e) => new frmGestionBarcos().ShowDialog();
            this.Controls.Add(btnGestionBarcos);

            // Botón Salir
            Button btnSalir = new Button()
            {
                Text = "Salir",
                BackColor = ColoresSistema.ColorSecundario,
                ForeColor = ColoresSistema.ColorTextoClaro,
                FlatStyle = FlatStyle.Flat,
                Location = new System.Drawing.Point(1100, yBotones),
                Size = new System.Drawing.Size(120, btnAlto)
            };
            btnSalir.Click += (s, e) =>
            {
                new frmLogin().Show();
                this.Close();
            };
            this.Controls.Add(btnSalir);

            // DataGridView
            dgv = new DataGridView()
            {
                Location = new System.Drawing.Point(20, 140),
                Size = new System.Drawing.Size(1240, 480),
                BackgroundColor = ColoresSistema.ColorBlanco,
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnCount = 5;
            dgv.Columns[0].Name = "ID";
            dgv.Columns[0].Width = 80;
            dgv.Columns[1].Name = "Barco";
            dgv.Columns[1].Width = 250;
            dgv.Columns[2].Name = "Fecha";
            dgv.Columns[2].Width = 150;
            dgv.Columns[3].Name = "Estado";
            dgv.Columns[3].Width = 200;
            dgv.Columns[4].Name = "Costo Total";
            dgv.Columns[4].Width = 150;
            this.Controls.Add(dgv);

            CargarRequisiciones();
        }

        private void CargarRequisiciones()
        {
            dgv.Rows.Clear();
            try
            {
                using (SqlConnection conn = ConexionDB.ObtenerConexion())
                {
                    conn.Open();
                    string query = @"
                        SELECT r.IdRequisicion, b.Nombre AS Barco, r.Fecha, r.EstadoGeneral, r.CostoTotal 
                        FROM Requisiciones r
                        INNER JOIN Barcos b ON r.IdBarco = b.IdBarco
                        ORDER BY r.Fecha DESC";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        dgv.Rows.Add(
                            reader["IdRequisicion"].ToString(),
                            reader["Barco"].ToString(),
                            Convert.ToDateTime(reader["Fecha"]).ToShortDateString(),
                            reader["EstadoGeneral"].ToString(),
                            $"${Convert.ToDecimal(reader["CostoTotal"]):0.00}"
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar requisiciones: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}