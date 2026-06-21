using System;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace Nolkata_Final
{
    public partial class frmDetalleProducto : Form
    {
        private int _id = -1;
        private bool _esEdicion = false;

        // Constructor para AGREGAR
        public frmDetalleProducto()
        {
            InitializeComponent();
            this.BackColor = ColoresSistema.ColorFondo;
            this.Text = "Agregar Producto";
            this.Size = new System.Drawing.Size(450, 350);
            this.StartPosition = FormStartPosition.CenterScreen;
            _esEdicion = false;
            InicializarFormulario();
        }

        // Constructor para EDITAR
        public frmDetalleProducto(int id, string nombre, int stock, decimal precio)
        {
            InitializeComponent();
            this.BackColor = ColoresSistema.ColorFondo;
            this.Text = "Editar Producto";
            this.Size = new System.Drawing.Size(450, 350);
            this.StartPosition = FormStartPosition.CenterScreen;
            _id = id;
            _esEdicion = true;
            InicializarFormulario();
            txtNombre.Text = nombre;
            nudStock.Value = stock;
            txtPrecio.Text = precio.ToString("0.00");
        }

        private TextBox txtNombre;
        private NumericUpDown nudStock;
        private TextBox txtPrecio;

        private void InicializarFormulario()
        {
            Label lblTitulo = new Label()
            {
                Text = _esEdicion ? "Editar Producto" : "Agregar Producto",
                Font = new System.Drawing.Font("Segoe UI", 14, System.Drawing.FontStyle.Bold),
                ForeColor = ColoresSistema.ColorPrincipal,
                Location = new System.Drawing.Point(20, 20),
                Size = new System.Drawing.Size(400, 30)
            };
            this.Controls.Add(lblTitulo);

            // Nombre
            this.Controls.Add(new Label() { Text = "Nombre:", Location = new System.Drawing.Point(30, 70), Size = new System.Drawing.Size(100, 25), ForeColor = ColoresSistema.ColorTextoNormal });
            txtNombre = new TextBox() { Location = new System.Drawing.Point(140, 70), Size = new System.Drawing.Size(250, 23) };
            this.Controls.Add(txtNombre);

            // Stock
            this.Controls.Add(new Label() { Text = "Stock:", Location = new System.Drawing.Point(30, 110), Size = new System.Drawing.Size(100, 25), ForeColor = ColoresSistema.ColorTextoNormal });
            nudStock = new NumericUpDown() { Location = new System.Drawing.Point(140, 110), Size = new System.Drawing.Size(120, 23), Minimum = 0, Maximum = 99999 };
            this.Controls.Add(nudStock);

            // Precio
            this.Controls.Add(new Label() { Text = "Precio:", Location = new System.Drawing.Point(30, 150), Size = new System.Drawing.Size(100, 25), ForeColor = ColoresSistema.ColorTextoNormal });
            txtPrecio = new TextBox() { Location = new System.Drawing.Point(140, 150), Size = new System.Drawing.Size(120, 23), Text = "0.00" };
            this.Controls.Add(txtPrecio);

            // Botón Guardar
            Button btnGuardar = new Button()
            {
                Text = "Guardar",
                BackColor = ColoresSistema.ColorPrincipal,
                ForeColor = ColoresSistema.ColorTextoClaro,
                FlatStyle = FlatStyle.Flat,
                Location = new System.Drawing.Point(100, 220),
                Size = new System.Drawing.Size(100, 35)
            };
            btnGuardar.Click += (s, e) => Guardar();
            this.Controls.Add(btnGuardar);

            // Botón Cancelar
            Button btnCancelar = new Button()
            {
                Text = "Cancelar",
                BackColor = ColoresSistema.ColorSecundario,
                ForeColor = ColoresSistema.ColorTextoClaro,
                FlatStyle = FlatStyle.Flat,
                Location = new System.Drawing.Point(220, 220),
                Size = new System.Drawing.Size(100, 35)
            };
            btnCancelar.Click += (s, e) => this.Close();
            this.Controls.Add(btnCancelar);
        }

        private void Guardar()
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Ingrese el nombre del producto", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (!decimal.TryParse(txtPrecio.Text, out decimal precio))
            {
                MessageBox.Show("Ingrese un precio válido", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string nombre = txtNombre.Text.Trim();
            int stock = (int)nudStock.Value;

            try
            {
                using (SqlConnection conn = ConexionDB.ObtenerConexion())
                {
                    conn.Open();

                    if (_esEdicion)
                    {
                        // EDITAR producto existente
                        string query = "UPDATE Productos SET Nombre = @nombre, Stock = @stock, Precio = @precio WHERE IdProducto = @id";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@nombre", nombre);
                        cmd.Parameters.AddWithValue("@stock", stock);
                        cmd.Parameters.AddWithValue("@precio", precio);
                        cmd.Parameters.AddWithValue("@id", _id);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Producto actualizado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // AGREGAR nuevo producto
                        string query = "INSERT INTO Productos (Nombre, Stock, Precio) VALUES (@nombre, @stock, @precio)";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@nombre", nombre);
                        cmd.Parameters.AddWithValue("@stock", stock);
                        cmd.Parameters.AddWithValue("@precio", precio);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Producto agregado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al guardar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}