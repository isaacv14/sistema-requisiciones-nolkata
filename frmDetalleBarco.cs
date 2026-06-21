using System;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace Nolkata_Final
{
    public partial class frmDetalleBarco : Form
    {
        private int _id = -1;
        private bool _esEdicion = false;
        private TextBox txtNombre, txtMatricula, txtContacto;

        public frmDetalleBarco()
        {
            InitializeComponent();
            this.BackColor = ColoresSistema.ColorFondo;
            this.Text = "Agregar Barco";
            this.Size = new System.Drawing.Size(450, 350);
            this.StartPosition = FormStartPosition.CenterScreen;
            _esEdicion = false;
            InicializarFormulario();
        }

        public frmDetalleBarco(int id, string nombre, string matricula, string contacto)
        {
            InitializeComponent();
            this.BackColor = ColoresSistema.ColorFondo;
            this.Text = "Editar Barco";
            this.Size = new System.Drawing.Size(450, 350);
            this.StartPosition = FormStartPosition.CenterScreen;
            _id = id;
            _esEdicion = true;
            InicializarFormulario();
            txtNombre.Text = nombre;
            txtMatricula.Text = matricula;
            txtContacto.Text = contacto;
        }

        private void InicializarFormulario()
        {
            Label lblTitulo = new Label()
            {
                Text = _esEdicion ? "Editar Barco" : "Agregar Barco",
                Font = new System.Drawing.Font("Segoe UI", 14, System.Drawing.FontStyle.Bold),
                ForeColor = ColoresSistema.ColorPrincipal,
                Location = new System.Drawing.Point(20, 20),
                Size = new System.Drawing.Size(300, 30)
            };
            this.Controls.Add(lblTitulo);

            // Nombre
            this.Controls.Add(new Label() { Text = "Nombre:", Location = new System.Drawing.Point(30, 70), Size = new System.Drawing.Size(100, 25), ForeColor = ColoresSistema.ColorTextoNormal });
            txtNombre = new TextBox() { Location = new System.Drawing.Point(140, 70), Size = new System.Drawing.Size(250, 23) };
            this.Controls.Add(txtNombre);

            // Matrícula
            this.Controls.Add(new Label() { Text = "Matrícula (IMO):", Location = new System.Drawing.Point(30, 110), Size = new System.Drawing.Size(100, 25), ForeColor = ColoresSistema.ColorTextoNormal });
            txtMatricula = new TextBox() { Location = new System.Drawing.Point(140, 110), Size = new System.Drawing.Size(250, 23) };
            this.Controls.Add(txtMatricula);

            // Contacto
            this.Controls.Add(new Label() { Text = "Contacto:", Location = new System.Drawing.Point(30, 150), Size = new System.Drawing.Size(100, 25), ForeColor = ColoresSistema.ColorTextoNormal });
            txtContacto = new TextBox() { Location = new System.Drawing.Point(140, 150), Size = new System.Drawing.Size(250, 23) };
            this.Controls.Add(txtContacto);

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
                MessageBox.Show("Ingrese el nombre del barco", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string nombre = txtNombre.Text.Trim();
            string matricula = txtMatricula.Text.Trim();
            string contacto = txtContacto.Text.Trim();

            try
            {
                using (SqlConnection conn = ConexionDB.ObtenerConexion())
                {
                    conn.Open();

                    if (_esEdicion)
                    {
                        string query = "UPDATE Barcos SET Nombre = @nombre, Matricula = @matricula, Contacto = @contacto WHERE IdBarco = @id";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@nombre", nombre);
                        cmd.Parameters.AddWithValue("@matricula", matricula);
                        cmd.Parameters.AddWithValue("@contacto", contacto);
                        cmd.Parameters.AddWithValue("@id", _id);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Barco actualizado", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        string query = "INSERT INTO Barcos (Nombre, Matricula, Contacto) VALUES (@nombre, @matricula, @contacto)";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@nombre", nombre);
                        cmd.Parameters.AddWithValue("@matricula", matricula);
                        cmd.Parameters.AddWithValue("@contacto", contacto);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Barco agregado", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
