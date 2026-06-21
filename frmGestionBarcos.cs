using System;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace Nolkata_Final
{
    public partial class frmGestionBarcos : Form
    {
        private DataGridView dgv;
        private TextBox txtBuscar;

        public frmGestionBarcos()
        {
            InitializeComponent();
            this.BackColor = ColoresSistema.ColorFondo;
            this.Text = "NOLKATA MARINE - Gestión de Barcos";
            this.Size = new System.Drawing.Size(900, 550);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Título
            Label lblTitulo = new Label()
            {
                Text = "Gestión de Barcos",
                Font = new System.Drawing.Font("Segoe UI", 16, System.Drawing.FontStyle.Bold),
                ForeColor = ColoresSistema.ColorPrincipal,
                Location = new System.Drawing.Point(20, 20),
                Size = new System.Drawing.Size(300, 35)
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
            btnBuscar.Click += (s, e) => CargarBarcos(txtBuscar.Text.Trim());
            this.Controls.Add(btnBuscar);

            // Botón Agregar
            Button btnAgregar = new Button()
            {
                Text = "Agregar Barco",
                BackColor = ColoresSistema.ColorPrincipal,
                ForeColor = ColoresSistema.ColorTextoClaro,
                FlatStyle = FlatStyle.Flat,
                Location = new System.Drawing.Point(700, 68),
                Size = new System.Drawing.Size(150, 30)
            };
            btnAgregar.Click += (s, e) =>
            {
                frmDetalleBarco detalle = new frmDetalleBarco();
                detalle.ShowDialog();
                CargarBarcos(txtBuscar.Text.Trim());
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
            dgv.Columns[1].Width = 250;
            dgv.Columns[2].Name = "Matrícula";
            dgv.Columns[2].Width = 150;
            dgv.Columns[3].Name = "Contacto";
            dgv.Columns[3].Width = 250;
            this.Controls.Add(dgv);

            // Botón Editar
            Button btnEditar = new Button()
            {
                Text = "Editar Barco",
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
                    string matricula = dgv.CurrentRow.Cells["Matrícula"].Value.ToString();
                    string contacto = dgv.CurrentRow.Cells["Contacto"].Value.ToString();
                    frmDetalleBarco detalle = new frmDetalleBarco(id, nombre, matricula, contacto);
                    detalle.ShowDialog();
                    CargarBarcos(txtBuscar.Text.Trim());
                }
                else
                    MessageBox.Show("Seleccione un barco", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            };
            this.Controls.Add(btnEditar);

            // Botón Eliminar
            Button btnEliminar = new Button()
            {
                Text = "Eliminar Barco",
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
                    DialogResult result = MessageBox.Show($"¿Eliminar el barco '{nombre}'?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                    if (result == DialogResult.Yes)
                    {
                        try
                        {
                            using (SqlConnection conn = ConexionDB.ObtenerConexion())
                            {
                                conn.Open();
                                string query = "DELETE FROM Barcos WHERE IdBarco = @id";
                                SqlCommand cmd = new SqlCommand(query, conn);
                                cmd.Parameters.AddWithValue("@id", id);
                                cmd.ExecuteNonQuery();
                            }
                            CargarBarcos(txtBuscar.Text.Trim());
                            MessageBox.Show("Barco eliminado", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                else
                    MessageBox.Show("Seleccione un barco", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

            CargarBarcos("");
        }

        private void CargarBarcos(string filtro)
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
                        query = "SELECT IdBarco, Nombre, Matricula, Contacto FROM Barcos ORDER BY Nombre";
                        cmd = new SqlCommand(query, conn);
                    }
                    else
                    {
                        query = "SELECT IdBarco, Nombre, Matricula, Contacto FROM Barcos WHERE Nombre LIKE @filtro ORDER BY Nombre";
                        cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@filtro", "%" + filtro + "%");
                    }

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        dgv.Rows.Add(
                            reader["IdBarco"].ToString(),
                            reader["Nombre"].ToString(),
                            reader["Matricula"].ToString(),
                            reader["Contacto"]?.ToString() ?? ""
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar barcos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
