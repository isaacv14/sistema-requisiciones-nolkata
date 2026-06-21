using System;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace Nolkata_Final
{
    public partial class frmGestionProductos : Form
    {
        private DataGridView dgv;
        private TextBox txtBuscar;

        public frmGestionProductos()
        {
            InitializeComponent();
            this.BackColor = ColoresSistema.ColorFondo;
            this.Text = "NOLKATA MARINE - Gestión de Productos";
            this.Size = new System.Drawing.Size(900, 550);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Título
            Label lblTitulo = new Label()
            {
                Text = "Gestión de Productos (Inventario)",
                Font = new System.Drawing.Font("Segoe UI", 16, System.Drawing.FontStyle.Bold),
                ForeColor = ColoresSistema.ColorPrincipal,
                Location = new System.Drawing.Point(20, 20),
                Size = new System.Drawing.Size(400, 35)
            };
            this.Controls.Add(lblTitulo);

            // Campo de búsqueda
            Label lblBuscar = new Label()
            {
                Text = "Buscar:",
                Location = new System.Drawing.Point(20, 70),
                Size = new System.Drawing.Size(60, 25),
                ForeColor = ColoresSistema.ColorTextoNormal
            };
            this.Controls.Add(lblBuscar);

            txtBuscar = new TextBox()
            {
                Name = "txtBuscar",
                Location = new System.Drawing.Point(80, 70),
                Size = new System.Drawing.Size(200, 23)
            };
            this.Controls.Add(txtBuscar);

            Button btnBuscar = new Button()
            {
                Text = "Buscar",
                BackColor = ColoresSistema.ColorSecundario,
                ForeColor = ColoresSistema.ColorTextoClaro,
                FlatStyle = FlatStyle.Flat,
                Location = new System.Drawing.Point(290, 68),
                Size = new System.Drawing.Size(80, 27)
            };
            btnBuscar.Click += (s, e) => CargarProductos(txtBuscar.Text.Trim());
            this.Controls.Add(btnBuscar);

            // Botón Agregar
            Button btnAgregar = new Button()
            {
                Text = "Agregar Producto",
                BackColor = ColoresSistema.ColorPrincipal,
                ForeColor = ColoresSistema.ColorTextoClaro,
                FlatStyle = FlatStyle.Flat,
                Location = new System.Drawing.Point(700, 68),
                Size = new System.Drawing.Size(150, 30)
            };
            btnAgregar.Click += (s, e) =>
            {
                frmDetalleProducto detalle = new frmDetalleProducto();
                detalle.ShowDialog();
                CargarProductos(txtBuscar.Text.Trim());  // Recargar después de cerrar
            };
            this.Controls.Add(btnAgregar);

            // DataGridView
            dgv = new DataGridView()
            {
                Location = new System.Drawing.Point(20, 110),
                Size = new System.Drawing.Size(850, 300),
                BackgroundColor = ColoresSistema.ColorBlanco,
                AllowUserToAddRows = false,
                ReadOnly = true,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };
            dgv.ColumnCount = 4;
            dgv.Columns[0].Name = "ID";
            dgv.Columns[0].Width = 80;
            dgv.Columns[1].Name = "Nombre";
            dgv.Columns[1].Width = 350;
            dgv.Columns[2].Name = "Stock";
            dgv.Columns[2].Width = 100;
            dgv.Columns[3].Name = "Precio";
            dgv.Columns[3].Width = 120;
            this.Controls.Add(dgv);

            // Botón Editar
            Button btnEditar = new Button()
            {
                Text = "Editar Producto",
                BackColor = ColoresSistema.ColorAcento,
                ForeColor = ColoresSistema.ColorTextoClaro,
                FlatStyle = FlatStyle.Flat,
                Location = new System.Drawing.Point(20, 440),
                Size = new System.Drawing.Size(150, 35)
            };
            btnEditar.Click += (s, e) =>
            {
                if (dgv.CurrentRow != null)
                {
                    int id = Convert.ToInt32(dgv.CurrentRow.Cells["ID"].Value);
                    string nombre = dgv.CurrentRow.Cells["Nombre"].Value.ToString();
                    int stock = Convert.ToInt32(dgv.CurrentRow.Cells["Stock"].Value);
                    decimal precio = Convert.ToDecimal(dgv.CurrentRow.Cells["Precio"].Value.ToString().Replace("$", ""));
                    frmDetalleProducto detalle = new frmDetalleProducto(id, nombre, stock, precio);
                    detalle.ShowDialog();
                    CargarProductos(txtBuscar.Text.Trim());  // Recargar después de cerrar
                }
                else
                    MessageBox.Show("Seleccione un producto", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            };
            this.Controls.Add(btnEditar);

            // Botón Eliminar
            Button btnEliminar = new Button()
            {
                Text = "Eliminar Producto",
                BackColor = ColoresSistema.EstadoAlerta,
                ForeColor = ColoresSistema.ColorTextoClaro,
                FlatStyle = FlatStyle.Flat,
                Location = new System.Drawing.Point(190, 440),
                Size = new System.Drawing.Size(150, 35)
            };
            btnEliminar.Click += (s, e) =>
            {
                if (dgv.CurrentRow != null)
                {
                    int id = Convert.ToInt32(dgv.CurrentRow.Cells["ID"].Value);
                    string nombre = dgv.CurrentRow.Cells["Nombre"].Value.ToString();
                    DialogResult result = MessageBox.Show($"¿Eliminar '{nombre}'?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            using (SqlConnection conn = ConexionDB.ObtenerConexion())
                            {
                                conn.Open();
                                string query = "DELETE FROM Productos WHERE IdProducto = @id";
                                SqlCommand cmd = new SqlCommand(query, conn);
                                cmd.Parameters.AddWithValue("@id", id);
                                cmd.ExecuteNonQuery();
                            }
                            CargarProductos(txtBuscar.Text.Trim());
                            MessageBox.Show("Producto eliminado", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                    MessageBox.Show("Seleccione un producto", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            };
            this.Controls.Add(btnEliminar);

            // Botón Cerrar
            Button btnCerrar = new Button()
            {
                Text = "Cerrar",
                BackColor = ColoresSistema.ColorSecundario,
                ForeColor = ColoresSistema.ColorTextoClaro,
                FlatStyle = FlatStyle.Flat,
                Location = new System.Drawing.Point(750, 440),
                Size = new System.Drawing.Size(120, 35)
            };
            btnCerrar.Click += (s, e) => this.Close();
            this.Controls.Add(btnCerrar);

            CargarProductos("");
        }

        private void CargarProductos(string filtro)
        {
            dgv.Rows.Clear();
            try
            {
                using (SqlConnection conn = ConexionDB.ObtenerConexion())
                {
                    conn.Open();
                    string query;
                    SqlCommand cmd;

                    if (string.IsNullOrWhiteSpace(filtro))
                    {
                        query = "SELECT IdProducto, Nombre, Stock, Precio FROM Productos ORDER BY Nombre";
                        cmd = new SqlCommand(query, conn);
                    }
                    else
                    {
                        query = "SELECT IdProducto, Nombre, Stock, Precio FROM Productos WHERE Nombre LIKE @filtro ORDER BY Nombre";
                        cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@filtro", "%" + filtro + "%");
                    }

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        dgv.Rows.Add(
                            reader["IdProducto"].ToString(),
                            reader["Nombre"].ToString(),
                            reader["Stock"].ToString(),
                            $"${Convert.ToDecimal(reader["Precio"]):0.00}"
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar productos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}