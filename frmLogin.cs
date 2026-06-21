using System;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace Nolkata_Final
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
            this.BackColor = ColoresSistema.ColorFondo;
            this.Text = "NOLKATA MARINE - Login";
            this.Size = new System.Drawing.Size(420, 350);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // Título
            Label lblTitulo = new Label()
            {
                Text = "NOLKATA MARINE",
                Font = new System.Drawing.Font("Segoe UI", 20, System.Drawing.FontStyle.Bold),
                ForeColor = ColoresSistema.ColorPrincipal,
                Location = new System.Drawing.Point(60, 25),
                Size = new System.Drawing.Size(300, 45),
              
            };
            this.Controls.Add(lblTitulo);

            // Línea separadora
            Panel line = new Panel()
            {
                Location = new System.Drawing.Point(40, 80),
                Size = new System.Drawing.Size(340, 2),
                BackColor = System.Drawing.Color.LightGray
            };
            this.Controls.Add(line);

            // Usuario
            Label lblUsuario = new Label()
            {
                Text = "Usuario:",
                Location = new System.Drawing.Point(50, 105),
                Size = new System.Drawing.Size(80, 25),
                ForeColor = ColoresSistema.ColorTextoNormal,
                Font = new System.Drawing.Font("Segoe UI", 10)
            };
            this.Controls.Add(lblUsuario);

            TextBox txtUsuario = new TextBox()
            {
                Name = "txtUsuario",
                Location = new System.Drawing.Point(140, 105),
                Size = new System.Drawing.Size(220, 25),
                Font = new System.Drawing.Font("Segoe UI", 10)
            };
            this.Controls.Add(txtUsuario);

            // Contraseña
            Label lblPass = new Label()
            {
                Text = "Contraseña:",
                Location = new System.Drawing.Point(50, 145),
                Size = new System.Drawing.Size(80, 25),
                ForeColor = ColoresSistema.ColorTextoNormal,
                Font = new System.Drawing.Font("Segoe UI", 10)
            };
            this.Controls.Add(lblPass);

            TextBox txtPass = new TextBox()
            {
                Name = "txtPass",
                Location = new System.Drawing.Point(140, 145),
                Size = new System.Drawing.Size(220, 25),
                UseSystemPasswordChar = true,
                Font = new System.Drawing.Font("Segoe UI", 10)
            };
            this.Controls.Add(txtPass);

            // Botón Ingresar
            Button btnIngresar = new Button()
            {
                Text = "Ingresar",
                BackColor = ColoresSistema.ColorPrincipal,
                ForeColor = ColoresSistema.ColorTextoClaro,
                FlatStyle = FlatStyle.Flat,
                Location = new System.Drawing.Point(80, 200),
                Size = new System.Drawing.Size(110, 40),
                Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold)
            };
            btnIngresar.Click += btnIngresar_Click;
            this.Controls.Add(btnIngresar);

            // Botón Salir
            Button btnSalir = new Button()
            {
                Text = "Salir",
                BackColor = ColoresSistema.ColorSecundario,
                ForeColor = ColoresSistema.ColorTextoClaro,
                FlatStyle = FlatStyle.Flat,
                Location = new System.Drawing.Point(210, 200),
                Size = new System.Drawing.Size(110, 40),
                Font = new System.Drawing.Font("Segoe UI", 10, System.Drawing.FontStyle.Bold)
            };
            btnSalir.Click += (s, e) => Application.Exit();
            this.Controls.Add(btnSalir);

            // Recuperar contraseña
            Button btnRecuperar = new Button()
            {
                Text = "¿Olvidó su contraseña?",
                BackColor = System.Drawing.Color.Transparent,
                ForeColor = ColoresSistema.ColorPrincipal,
                FlatStyle = FlatStyle.Flat,
                Location = new System.Drawing.Point(125, 260),
                Size = new System.Drawing.Size(150, 30),
                Font = new System.Drawing.Font("Segoe UI", 9)
            };
            btnRecuperar.Click += (s, e) => new frmRecuperarContrasena().ShowDialog();
            this.Controls.Add(btnRecuperar);
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            TextBox txtUsuario = (TextBox)this.Controls["txtUsuario"];
            TextBox txtPass = (TextBox)this.Controls["txtPass"];

            string usuario = txtUsuario.Text;
            string contrasena = txtPass.Text;

            if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(contrasena))
            {
                MessageBox.Show("Ingrese usuario y contraseña", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                using (SqlConnection conn = ConexionDB.ObtenerConexion())
                {
                    conn.Open();
                    string query = "SELECT IdUsuario, Nombre, EsAdmin FROM Usuarios WHERE Nombre = @nombre AND Contrasena = @contrasena";
                    SqlCommand cmd = new SqlCommand(query, conn);
                    cmd.Parameters.AddWithValue("@nombre", usuario);
                    cmd.Parameters.AddWithValue("@contrasena", contrasena);

                    SqlDataReader reader = cmd.ExecuteReader();
                    if (reader.Read())
                    {
                        long idUsuario = Convert.ToInt64(reader["IdUsuario"]);
                        string nombre = reader["Nombre"].ToString();
                        bool esAdmin = Convert.ToBoolean(reader["EsAdmin"]);

                        // ========== AUDITORÍA: REGISTRAR INICIO DE SESIÓN ==========
                        RegistrarAuditoria(idUsuario, $"Inicio de sesión: {nombre}", "Usuarios");

                        if (esAdmin)
                        {
                            frmAdminPrincipal admin = new frmAdminPrincipal(idUsuario, nombre);
                            admin.Show();
                            this.Hide();
                        }
                        else
                        {
                            frmBarcoPrincipal barco = new frmBarcoPrincipal(nombre, idUsuario);
                            barco.Show();
                            this.Hide();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Usuario o contraseña incorrectos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al conectar con la base de datos: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                // No mostrar error al usuario, solo registrar silenciosamente
                System.Diagnostics.Debug.WriteLine("Error al registrar auditoría: " + ex.Message);
            }
        }
    }
}
