using System;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace Nolkata_Final
{
    public partial class frmBarcoPrincipal : Form
    {
        private string _usuario;
        private long _idUsuarioActual;
        private DataGridView dgv;

        public frmBarcoPrincipal(string usuario, long idUsuarioActual)
        {
            _usuario = usuario;
            _idUsuarioActual = idUsuarioActual;
            InitializeComponent();
            this.BackColor = ColoresSistema.ColorFondo;
            this.Text = "NOLKATA MARINE - Usuario Barco";
            this.Size = new System.Drawing.Size(1200, 650);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label lblBienvenida = new Label()
            {
                Text = $"Bienvenido, {_usuario}",
                Font = new System.Drawing.Font("Segoe UI", 14, System.Drawing.FontStyle.Bold),
                ForeColor = ColoresSistema.ColorPrincipal,
                Location = new System.Drawing.Point(20, 20),
                Size = new System.Drawing.Size(300, 30)
            };
            this.Controls.Add(lblBienvenida);

            Label lblAviso = new Label()
            {
                Text = "⚠️ Los costos no son visibles para usuarios de barco",
                ForeColor = ColoresSistema.EstadoAlerta,
                Location = new System.Drawing.Point(20, 55),
                Size = new System.Drawing.Size(400, 25),
                Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold)
            };
            this.Controls.Add(lblAviso);

            Button btnNuevaReq = new Button()
            {
                Text = "Nueva Requisición",
                BackColor = ColoresSistema.ColorPrincipal,
                ForeColor = ColoresSistema.ColorTextoClaro,
                FlatStyle = FlatStyle.Flat,
                Location = new System.Drawing.Point(20, 100),
                Size = new System.Drawing.Size(180, 45)
            };
            btnNuevaReq.Click += (s, e) =>
            {
                new frmNuevaRequisicion(_usuario, false, _idUsuarioActual).ShowDialog();
                CargarRequisiciones();
            };
            this.Controls.Add(btnNuevaReq);

            Button btnDetalle = new Button()
            {
                Text = "Ver Detalles",
                BackColor = ColoresSistema.ColorSecundario,
                ForeColor = ColoresSistema.ColorTextoClaro,
                FlatStyle = FlatStyle.Flat,
                Location = new System.Drawing.Point(220, 100),
                Size = new System.Drawing.Size(180, 45)
            };
            btnDetalle.Click += (s, e) =>
            {
                if (dgv.CurrentRow != null)
                {
                    int id = Convert.ToInt32(dgv.CurrentRow.Cells["ID"].Value);
                    new frmDetalleRequisicion(id, false, _idUsuarioActual).ShowDialog();
                    CargarRequisiciones();
                }
                else
                    MessageBox.Show("Seleccione una requisición", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            };
            this.Controls.Add(btnDetalle);

            Button btnSalir = new Button()
            {
                Text = "Salir",
                BackColor = ColoresSistema.ColorSecundario,
                ForeColor = ColoresSistema.ColorTextoClaro,
                FlatStyle = FlatStyle.Flat,
                Location = new System.Drawing.Point(20, 550),
                Size = new System.Drawing.Size(120, 45)
            };
            btnSalir.Click += (s, e) =>
            {
                new frmLogin().Show();
                this.Close();
            };
            this.Controls.Add(btnSalir);

            dgv = new DataGridView()
            {
                Location = new System.Drawing.Point(20, 170),
                Size = new System.Drawing.Size(1150, 350),
                BackgroundColor = ColoresSistema.ColorBlanco,
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnCount = 4;
            dgv.Columns[0].Name = "ID";
            dgv.Columns[0].Width = 80;
            dgv.Columns[1].Name = "Barco";
            dgv.Columns[1].Width = 250;
            dgv.Columns[2].Name = "Fecha";
            dgv.Columns[2].Width = 150;
            dgv.Columns[3].Name = "Estado";
            dgv.Columns[3].Width = 200;
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
                    int idBarco = 1;
                    SqlCommand cmdBarco = new SqlCommand("SELECT IdBarcoAsociado FROM Usuarios WHERE Nombre = @nombre", conn);
                    cmdBarco.Parameters.AddWithValue("@nombre", _usuario);
                    object result = cmdBarco.ExecuteScalar();
                    if (result != null && result != DBNull.Value)
                        idBarco = Convert.ToInt32(result);

                    string query = @"
                        SELECT r.IdRequisicion, b.Nombre AS Barco, r.Fecha, r.EstadoGeneral 
                        FROM Requisiciones r
                        INNER JOIN Barcos b ON r.IdBarco = b.IdBarco
                        WHERE r.IdBarco = @idBarco
                        ORDER BY r.Fecha DESC";

                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@idBarco", idBarco);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        dgv.Rows.Add(
                            reader["IdRequisicion"].ToString(),
                            reader["Barco"].ToString(),
                            Convert.ToDateTime(reader["Fecha"]).ToShortDateString(),
                            reader["EstadoGeneral"].ToString()
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