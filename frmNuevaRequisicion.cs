using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace Nolkata_Final
{
    public partial class frmNuevaRequisicion : Form
    {
        private string _usuario;
        private bool _esAdmin;
        private long _idUsuarioActual;
        private List<DetalleProducto> _productosTemp;
        private DataGridView dgv;
        private ComboBox cbBarco, cbProducto;
        private NumericUpDown nudCantidad;
        private TextBox txtPrecio;
        private Label lblPrecio;
        private string _rutaImagenTemp = "";
        private PictureBox pbImagen;

        private class DetalleProducto
        {
            public string Producto { get; set; }
            public int Cantidad { get; set; }
            public decimal Precio { get; set; }
            public string RutaImagen { get; set; }
        }

        public frmNuevaRequisicion(string usuario, bool esAdmin, long idUsuarioActual)
        {
            _usuario = usuario;
            _esAdmin = esAdmin;
            _idUsuarioActual = idUsuarioActual;
            _productosTemp = new List<DetalleProducto>();
            InitializeComponent();
            this.BackColor = ColoresSistema.ColorFondo;
            this.Text = "Nueva Requisición";
            this.Size = new System.Drawing.Size(850, 650);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label lblTitulo = new Label()
            {
                Text = "Nueva Requisición",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = ColoresSistema.ColorPrincipal,
                Location = new Point(20, 20),
                Size = new Size(300, 35)
            };
            this.Controls.Add(lblTitulo);

            // Barco
            this.Controls.Add(new Label() { Text = "Barco:", Location = new Point(20, 70), Size = new Size(80, 25), ForeColor = ColoresSistema.ColorTextoNormal });
            cbBarco = new ComboBox() { Location = new Point(100, 70), Size = new Size(250, 23), DropDownStyle = ComboBoxStyle.DropDownList };
            CargarBarcos();
            this.Controls.Add(cbBarco);

            // Producto
            this.Controls.Add(new Label() { Text = "Producto:", Location = new Point(20, 110), Size = new Size(80, 25), ForeColor = ColoresSistema.ColorTextoNormal });
            cbProducto = new ComboBox() { Location = new Point(100, 110), Size = new Size(250, 23), DropDownStyle = ComboBoxStyle.DropDownList };
            CargarProductos();
            cbProducto.SelectedIndexChanged += (s, e) => CargarPrecioProducto();
            this.Controls.Add(cbProducto);

            // Cantidad
            this.Controls.Add(new Label() { Text = "Cantidad:", Location = new Point(20, 150), Size = new Size(80, 25), ForeColor = ColoresSistema.ColorTextoNormal });
            nudCantidad = new NumericUpDown() { Location = new Point(100, 150), Size = new Size(100, 23), Minimum = 1, Value = 1 };
            this.Controls.Add(nudCantidad);

            // Precio (solo admin)
            lblPrecio = new Label() { Text = "Precio:", Location = new Point(20, 190), Size = new Size(80, 25), ForeColor = ColoresSistema.ColorTextoNormal };
            this.Controls.Add(lblPrecio);
            txtPrecio = new TextBox() { Location = new Point(100, 190), Size = new Size(120, 23), Text = "0.00", ReadOnly = true };
            this.Controls.Add(txtPrecio);

            if (!_esAdmin)
            {
                lblPrecio.Visible = false;
                txtPrecio.Visible = false;
            }

            // Imagen
            this.Controls.Add(new Label() { Text = "Imagen:", Location = new Point(20, 230), Size = new Size(80, 25), ForeColor = ColoresSistema.ColorTextoNormal });
            pbImagen = new PictureBox() { Location = new Point(100, 230), Size = new Size(100, 80), BorderStyle = BorderStyle.FixedSingle, BackColor = System.Drawing.Color.LightGray, SizeMode = PictureBoxSizeMode.Zoom };
            this.Controls.Add(pbImagen);

            Button btnAdjuntarFoto = new Button()
            {
                Text = "Adjuntar Foto",
                BackColor = ColoresSistema.ColorAcento,
                ForeColor = ColoresSistema.ColorTextoClaro,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(220, 260),
                Size = new Size(120, 30)
            };
            btnAdjuntarFoto.Click += (s, e) => AdjuntarFoto();
            this.Controls.Add(btnAdjuntarFoto);

            // Botón Agregar
            Button btnAgregar = new Button()
            {
                Text = "Agregar Producto",
                BackColor = ColoresSistema.ColorPrincipal,
                ForeColor = ColoresSistema.ColorTextoClaro,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(400, 110),
                Size = new Size(150, 30)
            };
            btnAgregar.Click += (s, e) => AgregarProducto();
            this.Controls.Add(btnAgregar);

            // DataGridView
            dgv = new DataGridView()
            {
                Location = new Point(20, 330),
                Size = new Size(800, 220),
                BackgroundColor = ColoresSistema.ColorBlanco,
                AllowUserToAddRows = false,
                ReadOnly = true
            };
            dgv.ColumnCount = 5;
            dgv.Columns[0].Name = "Producto";
            dgv.Columns[0].Width = 200;
            dgv.Columns[1].Name = "Cantidad";
            dgv.Columns[1].Width = 80;
            dgv.Columns[2].Name = "Precio";
            dgv.Columns[2].Width = 100;
            dgv.Columns[3].Name = "Subtotal";
            dgv.Columns[3].Width = 100;
            dgv.Columns[4].Name = "Imagen";
            dgv.Columns[4].Width = 150;
            this.Controls.Add(dgv);

            // Botón Guardar
            Button btnGuardar = new Button()
            {
                Text = "Guardar Requisición",
                BackColor = ColoresSistema.ColorPrincipal,
                ForeColor = ColoresSistema.ColorTextoClaro,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(180, 570),
                Size = new Size(150, 40)
            };
            btnGuardar.Click += (s, e) => GuardarRequisicion();
            this.Controls.Add(btnGuardar);

            // Botón Cancelar
            Button btnCancelar = new Button()
            {
                Text = "Cancelar",
                BackColor = ColoresSistema.ColorSecundario,
                ForeColor = ColoresSistema.ColorTextoClaro,
                FlatStyle = FlatStyle.Flat,
                Location = new Point(380, 570),
                Size = new Size(150, 40)
            };
            btnCancelar.Click += (s, e) => this.Close();
            this.Controls.Add(btnCancelar);
        }

        private void CargarBarcos()
        {
            try
            {
                using (SqlConnection conn = ConexionDB.ObtenerConexion())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT Nombre FROM Barcos ORDER BY Nombre", conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                        cbBarco.Items.Add(reader["Nombre"].ToString());
                }
                if (cbBarco.Items.Count > 0) cbBarco.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar barcos: " + ex.Message);
            }
        }

        private void CargarProductos()
        {
            try
            {
                using (SqlConnection conn = ConexionDB.ObtenerConexion())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT Nombre FROM Productos ORDER BY Nombre", conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                        cbProducto.Items.Add(reader["Nombre"].ToString());
                }
                if (cbProducto.Items.Count > 0) cbProducto.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar productos: " + ex.Message);
            }
        }

        private void CargarPrecioProducto()
        {
            if (_esAdmin && cbProducto.SelectedItem != null)
            {
                try
                {
                    using (SqlConnection conn = ConexionDB.ObtenerConexion())
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("SELECT Precio FROM Productos WHERE Nombre = @nombre", conn);
                        cmd.Parameters.AddWithValue("@nombre", cbProducto.SelectedItem.ToString());
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                            txtPrecio.Text = Convert.ToDecimal(result).ToString("0.00");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message);
                }
            }
        }

        private void AdjuntarFoto()
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Archivos de imagen|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
                ofd.Title = "Seleccionar imagen del producto";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        string dirImagenes = Path.Combine(Application.StartupPath, "Imagenes");
                        if (!Directory.Exists(dirImagenes))
                            Directory.CreateDirectory(dirImagenes);

                        string nombreArchivo = $"prod_{DateTime.Now:yyyyMMddHHmmss}_{Path.GetFileName(ofd.FileName)}";
                        string rutaDestino = Path.Combine(dirImagenes, nombreArchivo);
                        File.Copy(ofd.FileName, rutaDestino, true);
                        _rutaImagenTemp = rutaDestino;
                        pbImagen.Image = Image.FromFile(rutaDestino);
                        MessageBox.Show("Imagen adjuntada correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al adjuntar imagen: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void AgregarProducto()
        {
            if (cbProducto.SelectedItem == null)
            {
                MessageBox.Show("Seleccione un producto", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string producto = cbProducto.SelectedItem.ToString();
            int cantidad = (int)nudCantidad.Value;
            decimal precio = 0;

            try
            {
                using (SqlConnection conn = ConexionDB.ObtenerConexion())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT Precio FROM Productos WHERE Nombre = @nombre", conn);
                    cmd.Parameters.AddWithValue("@nombre", producto);
                    object result = cmd.ExecuteScalar();
                    if (result != null) precio = Convert.ToDecimal(result);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al obtener precio: " + ex.Message);
                return;
            }

            _productosTemp.Add(new DetalleProducto
            {
                Producto = producto,
                Cantidad = cantidad,
                Precio = precio,
                RutaImagen = _rutaImagenTemp
            });

            ActualizarGrid();
            nudCantidad.Value = 1;
            _rutaImagenTemp = "";
            pbImagen.Image = null;
        }

        private void ActualizarGrid()
        {
            dgv.Rows.Clear();
            foreach (var p in _productosTemp)
            {
                string img = string.IsNullOrEmpty(p.RutaImagen) ? "Sin imagen" : "📷 Con imagen";
                dgv.Rows.Add(p.Producto, p.Cantidad, $"${p.Precio:0.00}", $"${p.Cantidad * p.Precio:0.00}", img);
            }
        }

        private void GuardarRequisicion()
        {
            if (_productosTemp.Count == 0)
            {
                MessageBox.Show("Agregue al menos un producto", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string barcoSeleccionado = cbBarco.SelectedItem?.ToString();
            if (string.IsNullOrWhiteSpace(barcoSeleccionado))
            {
                MessageBox.Show("Seleccione un barco", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            decimal total = 0;
            foreach (var p in _productosTemp)
                total += p.Cantidad * p.Precio;

            int nuevaId = 0;

            try
            {
                using (SqlConnection conn = ConexionDB.ObtenerConexion())
                {
                    conn.Open();

                    // Obtener IdBarco
                    int idBarco = 1;
                    SqlCommand cmdBarco = new SqlCommand("SELECT IdBarco FROM Barcos WHERE Nombre = @nombre", conn);
                    cmdBarco.Parameters.AddWithValue("@nombre", barcoSeleccionado);
                    object idResult = cmdBarco.ExecuteScalar();
                    if (idResult != null) idBarco = Convert.ToInt32(idResult);

                    // Insertar requisición
                    string query = "INSERT INTO Requisiciones (IdBarco, Barco, Fecha, EstadoGeneral, CostoTotal, CreadoPor) VALUES (@idBarco, @barco, @fecha, 'Pendiente', @total, @creadoPor); SELECT SCOPE_IDENTITY();";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@idBarco", idBarco);
                    cmd.Parameters.AddWithValue("@barco", barcoSeleccionado);
                    cmd.Parameters.AddWithValue("@fecha", DateTime.Now);
                    cmd.Parameters.AddWithValue("@total", total);
                    cmd.Parameters.AddWithValue("@creadoPor", _usuario);
                    nuevaId = Convert.ToInt32(cmd.ExecuteScalar());

                    // Insertar detalles
                    foreach (var p in _productosTemp)
                    {
                        string queryDetalle = "INSERT INTO DetalleRequisicion (IdRequisicion, Producto, Cantidad, Precio, Estado, FechaCambio, Imagen) VALUES (@idReq, @producto, @cantidad, @precio, 'Pendiente', @fecha, @imagen)";
                        SqlCommand cmdDetalle = new SqlCommand(queryDetalle, conn);
                        cmdDetalle.Parameters.AddWithValue("@idReq", nuevaId);
                        cmdDetalle.Parameters.AddWithValue("@producto", p.Producto);
                        cmdDetalle.Parameters.AddWithValue("@cantidad", p.Cantidad);
                        cmdDetalle.Parameters.AddWithValue("@precio", p.Precio);
                        cmdDetalle.Parameters.AddWithValue("@fecha", DateTime.Now);
                        cmdDetalle.Parameters.AddWithValue("@imagen", p.RutaImagen ?? "");
                        cmdDetalle.ExecuteNonQuery();
                    }

                    // ========== AUDITORÍA: REGISTRAR CREACIÓN DE REQUISICIÓN ==========
                    RegistrarAuditoria(_idUsuarioActual, $"Creó requisición #{nuevaId} para el barco {barcoSeleccionado} (Total: ${total:0.00})", "Requisiciones");

                    MessageBox.Show($"Requisición guardada exitosamente.\nTotal: ${total:0.00}", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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