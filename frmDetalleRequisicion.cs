using System;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace Nolkata_Final
{
    public partial class frmDetalleRequisicion : Form
    {
        private int _reqId;
        private bool _esAdmin;
        private long _idUsuarioActual;
        private DataGridView dgv;

        public frmDetalleRequisicion(int id, bool esAdmin, long idUsuarioActual)
        {
            _reqId = id;
            _esAdmin = esAdmin;
            _idUsuarioActual = idUsuarioActual;
            InitializeComponent();
            this.BackColor = ColoresSistema.ColorFondo;
            this.Text = $"Detalle de Requisición #{_reqId}";
            this.Size = new System.Drawing.Size(1000, 600);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label lblTitulo = new Label()
            {
                Text = $"Detalle de Requisición #{_reqId}",
                Font = new System.Drawing.Font("Segoe UI", 16, System.Drawing.FontStyle.Bold),
                ForeColor = ColoresSistema.ColorPrincipal,
                Location = new System.Drawing.Point(20, 20),
                Size = new System.Drawing.Size(400, 35)
            };
            this.Controls.Add(lblTitulo);

            dgv = new DataGridView()
            {
                Location = new System.Drawing.Point(20, 70),
                Size = new System.Drawing.Size(950, 400),
                BackgroundColor = ColoresSistema.ColorBlanco,
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnCount = 6;
            dgv.Columns[0].Name = "Producto";
            dgv.Columns[0].Width = 250;
            dgv.Columns[1].Name = "Cantidad";
            dgv.Columns[1].Width = 80;
            dgv.Columns[2].Name = "Precio";
            dgv.Columns[2].Width = 100;
            dgv.Columns[3].Name = "Estado";
            dgv.Columns[3].Width = 120;
            dgv.Columns[4].Name = "Fecha Cambio";
            dgv.Columns[4].Width = 120;
            dgv.Columns[5].Name = "Imagen";
            dgv.Columns[5].Width = 100;
            this.Controls.Add(dgv);

            // ComboBox nuevo estado
            this.Controls.Add(new Label() { Text = "Nuevo Estado:", Location = new System.Drawing.Point(20, 490), Size = new System.Drawing.Size(100, 25), ForeColor = ColoresSistema.ColorTextoNormal });
            ComboBox cbEstado = new ComboBox()
            {
                Location = new System.Drawing.Point(130, 490),
                Size = new System.Drawing.Size(120, 23),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cbEstado.Items.AddRange(new[] { "Pendiente", "En Proceso", "Entregado" });
            cbEstado.SelectedIndex = 0;
            this.Controls.Add(cbEstado);

            // Botón Cambiar Estado
            Button btnCambiar = new Button()
            {
                Text = "Cambiar Estado",
                BackColor = ColoresSistema.ColorPrincipal,
                ForeColor = ColoresSistema.ColorTextoClaro,
                FlatStyle = FlatStyle.Flat,
                Location = new System.Drawing.Point(270, 490),
                Size = new System.Drawing.Size(130, 30)
            };
            btnCambiar.Click += (s, e) =>
            {
                if (dgv.CurrentRow != null)
                {
                    int rowIndex = dgv.CurrentRow.Index;
                    string nuevoEstado = cbEstado.SelectedItem.ToString();
                    string producto = dgv.Rows[rowIndex].Cells[0].Value.ToString();
                    string estadoAnterior = dgv.Rows[rowIndex].Cells[3].Value.ToString();

                    try
                    {
                        using (SqlConnection conn = ConexionDB.ObtenerConexion())
                        {
                            conn.Open();
                            string query = "UPDATE DetalleRequisicion SET Estado = @estado, FechaCambio = @fecha WHERE IdRequisicion = @idReq AND Producto = @producto";
                            SqlCommand cmd = new SqlCommand(query, conn);
                            cmd.Parameters.AddWithValue("@estado", nuevoEstado);
                            cmd.Parameters.AddWithValue("@fecha", DateTime.Now);
                            cmd.Parameters.AddWithValue("@idReq", _reqId);
                            cmd.Parameters.AddWithValue("@producto", producto);
                            cmd.ExecuteNonQuery();

                            // Actualizar estado general de la requisición
                            string queryUpdateGeneral = @"
                                UPDATE Requisiciones 
                                SET EstadoGeneral = CASE 
                                    WHEN NOT EXISTS (SELECT 1 FROM DetalleRequisicion WHERE IdRequisicion = @idReq AND Estado != 'Entregado') 
                                    THEN 'Entregado'
                                    ELSE 'En Proceso'
                                END
                                WHERE IdRequisicion = @idReq";
                            SqlCommand cmdGeneral = new SqlCommand(queryUpdateGeneral, conn);
                            cmdGeneral.Parameters.AddWithValue("@idReq", _reqId);
                            cmdGeneral.ExecuteNonQuery();

                            // ========== AUDITORÍA: REGISTRAR CAMBIO DE ESTADO ==========
                            RegistrarAuditoria(_idUsuarioActual, $"Cambió estado de producto '{producto}' de '{estadoAnterior}' a '{nuevoEstado}' en requisición #{_reqId}", "DetalleRequisicion");
                        }
                        CargarDetalle();
                        MessageBox.Show($"Estado cambiado a {nuevoEstado}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                    MessageBox.Show("Seleccione un producto", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            };
            this.Controls.Add(btnCambiar);

            // Botón Ver Imagen
            Button btnVerImagen = new Button()
            {
                Text = "Ver Imagen",
                BackColor = ColoresSistema.ColorAcento,
                ForeColor = ColoresSistema.ColorTextoClaro,
                FlatStyle = FlatStyle.Flat,
                Location = new System.Drawing.Point(420, 490),
                Size = new System.Drawing.Size(120, 30)
            };
            btnVerImagen.Click += (s, e) =>
            {
                if (dgv.CurrentRow != null)
                {
                    string producto = dgv.CurrentRow.Cells[0].Value.ToString();
                    string rutaImagen = "";

                    using (SqlConnection conn = ConexionDB.ObtenerConexion())
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("SELECT Imagen FROM DetalleRequisicion WHERE IdRequisicion = @idReq AND Producto = @producto", conn);
                        cmd.Parameters.AddWithValue("@idReq", _reqId);
                        cmd.Parameters.AddWithValue("@producto", producto);
                        object result = cmd.ExecuteScalar();
                        if (result != null && !string.IsNullOrEmpty(result.ToString()))
                        {
                            rutaImagen = result.ToString();
                            if (System.IO.File.Exists(rutaImagen))
                            {
                                frmVerImagen frm = new frmVerImagen(rutaImagen);
                                frm.ShowDialog();
                            }
                            else
                                MessageBox.Show("El archivo de imagen no existe.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else
                            MessageBox.Show("Este producto no tiene imagen adjunta.", "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            };
            this.Controls.Add(btnVerImagen);

            // Botón Cerrar
            Button btnCerrar = new Button()
            {
                Text = "Cerrar",
                BackColor = ColoresSistema.ColorSecundario,
                ForeColor = ColoresSistema.ColorTextoClaro,
                FlatStyle = FlatStyle.Flat,
                Location = new System.Drawing.Point(850, 490),
                Size = new System.Drawing.Size(100, 30)
            };
            btnCerrar.Click += (s, e) => this.Close();
            this.Controls.Add(btnCerrar);

            CargarDetalle();
        }

        private void CargarDetalle()
        {
            dgv.Rows.Clear();
            try
            {
                using (SqlConnection conn = ConexionDB.ObtenerConexion())
                {
                    conn.Open();
                    string query = "SELECT Producto, Cantidad, Precio, Estado, FechaCambio, Imagen FROM DetalleRequisicion WHERE IdRequisicion = @idReq ORDER BY IdDetalle";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@idReq", _reqId);
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        string producto = reader["Producto"].ToString();
                        int cantidad = Convert.ToInt32(reader["Cantidad"]);
                        decimal precio = Convert.ToDecimal(reader["Precio"]);
                        string estado = reader["Estado"].ToString();
                        DateTime fechaCambio = Convert.ToDateTime(reader["FechaCambio"]);
                        string imagen = reader["Imagen"]?.ToString();
                        string imgDisplay = string.IsNullOrEmpty(imagen) ? "❌" : "📷";

                        int rowIndex = dgv.Rows.Add(producto, cantidad, $"${precio:0.00}", estado, fechaCambio.ToShortDateString(), imgDisplay);
                        int colEstadoIndex = 3;

                        if (estado == "Pendiente")
                        {
                            dgv.Rows[rowIndex].Cells[colEstadoIndex].Style.BackColor = ColoresSistema.EstadoPendiente;
                            dgv.Rows[rowIndex].Cells[colEstadoIndex].Style.ForeColor = System.Drawing.Color.Black;
                        }
                        else if (estado == "En Proceso")
                        {
                            dgv.Rows[rowIndex].Cells[colEstadoIndex].Style.BackColor = ColoresSistema.EstadoEnProceso;
                            dgv.Rows[rowIndex].Cells[colEstadoIndex].Style.ForeColor = System.Drawing.Color.Black;
                        }
                        else if (estado == "Entregado")
                        {
                            dgv.Rows[rowIndex].Cells[colEstadoIndex].Style.BackColor = ColoresSistema.EstadoEntregado;
                            dgv.Rows[rowIndex].Cells[colEstadoIndex].Style.ForeColor = System.Drawing.Color.Black;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar detalle: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ========== MÉTODO DE AUDITORÍA ==========
        private void RegistrarAuditoria(long idUsuario, string accion, string tablaAfectada)
        {
            try
            {
                using (SqlConnection conn = ConexionDB.ObtenerConexion())
                {
                    conn.Open();
                    string query = "INSERT INTO Auditoria (IdUsuario, Accion, TablaAfectada, FechaHora) VALUES (@idUsuario, @accion, @tabla, @fechaHora)";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@idUsuario", idUsuario);
                    cmd.Parameters.AddWithValue("@accion", accion);
                    cmd.Parameters.AddWithValue("@tabla", tablaAfectada);
                    cmd.Parameters.AddWithValue("@fechaHora", DateTime.Now);
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al registrar auditoría: " + ex.Message);
            }
        }
    }
}