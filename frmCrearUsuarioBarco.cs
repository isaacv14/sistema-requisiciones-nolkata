using System;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace Nolkata_Final
{
    public partial class frmCrearUsuarioBarco : Form
    {
        private long _idAdmin;
        private string _nombreAdmin;

        public frmCrearUsuarioBarco(long idAdmin, string nombreAdmin)
        {
            _idAdmin = idAdmin;
            _nombreAdmin = nombreAdmin;
            InitializeComponent();
            this.BackColor = ColoresSistema.ColorFondo;
            this.Text = "Crear Usuario Barco";
            this.Size = new System.Drawing.Size(450, 350);
            this.StartPosition = FormStartPosition.CenterScreen;

            // Título
            Label lblTitulo = new Label()
            {
                Text = "Crear Usuario Barco",
                Font = new System.Drawing.Font("Segoe UI", 14, System.Drawing.FontStyle.Bold),
                ForeColor = ColoresSistema.ColorPrincipal,
                Location = new System.Drawing.Point(20, 20),
                Size = new System.Drawing.Size(300, 30)
            };
            this.Controls.Add(lblTitulo);

            // Campo Nombre
            Label lblNombre = new Label()
            {
                Text = "Nombre:",
                Location = new System.Drawing.Point(30, 70),
                Size = new System.Drawing.Size(80, 25),
                ForeColor = ColoresSistema.ColorTextoNormal
            };
            this.Controls.Add(lblNombre);

            TextBox txtNombre = new TextBox()
            {
                Name = "txtNombre",
                Location = new System.Drawing.Point(120, 70),
                Size = new System.Drawing.Size(250, 23)
            };
            this.Controls.Add(txtNombre);

            // Campo Contraseña
            Label lblContrasena = new Label()
            {
                Text = "Contraseña:",
                Location = new System.Drawing.Point(30, 110),
                Size = new System.Drawing.Size(80, 25),
                ForeColor = ColoresSistema.ColorTextoNormal
            };
            this.Controls.Add(lblContrasena);

            TextBox txtContrasena = new TextBox()
            {
                Name = "txtContrasena",
                Location = new System.Drawing.Point(120, 110),
                Size = new System.Drawing.Size(250, 23),
                UseSystemPasswordChar = true
            };
            this.Controls.Add(txtContrasena);

            // Campo Barco
            Label lblBarco = new Label()
            {
                Text = "Barco:",
                Location = new System.Drawing.Point(30, 150),
                Size = new System.Drawing.Size(80, 25),
                ForeColor = ColoresSistema.ColorTextoNormal
            };
            this.Controls.Add(lblBarco);

            ComboBox cbBarco = new ComboBox()
            {
                Name = "cbBarco",
                Location = new System.Drawing.Point(120, 150),
                Size = new System.Drawing.Size(250, 23),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            this.Controls.Add(cbBarco);

            CargarBarcos(cbBarco);

            // Botón Crear
            Button btnCrear = new Button()
            {
                Text = "Crear",
                BackColor = ColoresSistema.ColorPrincipal,
                ForeColor = ColoresSistema.ColorTextoClaro,
                FlatStyle = FlatStyle.Flat,
                Location = new System.Drawing.Point(80, 220),
                Size = new System.Drawing.Size(100, 35)
            };
            btnCrear.Click += (s, e) =>
            {
                string nombre = txtNombre.Text.Trim();
                string contrasena = txtContrasena.Text.Trim();

                if (string.IsNullOrWhiteSpace(nombre) || string.IsNullOrWhiteSpace(contrasena))
                {
                    MessageBox.Show("Complete todos los campos", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (cbBarco.SelectedItem == null)
                {
                    MessageBox.Show("Seleccione un barco", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                int idBarco = (int)((dynamic)cbBarco.SelectedItem).Id;

                try
                {
                    using (SqlConnection conn = ConexionDB.ObtenerConexion())
                    {
                        conn.Open();
                        string query = "INSERT INTO Usuarios (Nombre, Contrasena, EsAdmin, IdBarcoAsociado) VALUES (@nombre, @contrasena, 0, @idBarco)";
                        SqlCommand cmd = new SqlCommand(query, conn);
                        cmd.Parameters.AddWithValue("@nombre", nombre);
                        cmd.Parameters.AddWithValue("@contrasena", contrasena);
                        cmd.Parameters.AddWithValue("@idBarco", idBarco);
                        cmd.ExecuteNonQuery();

                        // ========== AUDITORÍA: REGISTRAR CREACIÓN DE USUARIO BARCO ==========
                        RegistrarAuditoria(_idAdmin, $"Creó usuario barco: {nombre} (ID Barco: {idBarco})", "Usuarios");
                    }
                    MessageBox.Show($"Usuario '{nombre}' creado exitosamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.Close();
                }
                catch (Exception ex)
                {
                    if (ex.Message.Contains("duplicate"))
                        MessageBox.Show("El nombre de usuario ya existe", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    else
                        MessageBox.Show("Error al crear usuario: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };
            this.Controls.Add(btnCrear);

            // Botón Cancelar
            Button btnCancelar = new Button()
            {
                Text = "Cancelar",
                BackColor = ColoresSistema.ColorSecundario,
                ForeColor = ColoresSistema.ColorTextoClaro,
                FlatStyle = FlatStyle.Flat,
                Location = new System.Drawing.Point(200, 220),
                Size = new System.Drawing.Size(100, 35)
            };
            btnCancelar.Click += (s, e) => this.Close();
            this.Controls.Add(btnCancelar);
        }

        private void CargarBarcos(ComboBox cb)
        {
            try
            {
                using (SqlConnection conn = ConexionDB.ObtenerConexion())
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT IdBarco, Nombre FROM Barcos ORDER BY Nombre", conn);
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        cb.Items.Add(new { Id = reader["IdBarco"], Nombre = reader["Nombre"] });
                        cb.DisplayMember = "Nombre";
                        cb.ValueMember = "Id";
                    }
                }
                if (cb.Items.Count > 0) cb.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar barcos: " + ex.Message);
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