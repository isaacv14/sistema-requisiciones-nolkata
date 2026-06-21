using System;
using System.Windows.Forms;
using Microsoft.Data.SqlClient;

namespace Nolkata_Final
{
    public partial class frmRecuperarContrasena : Form
    {
        public frmRecuperarContrasena()
        {
            InitializeComponent();
            this.BackColor = ColoresSistema.ColorFondo;
            this.Text = "Recuperar Contraseña";
            this.Size = new System.Drawing.Size(400, 250);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label lblTitulo = new Label()
            {
                Text = "Recuperar Contraseña",
                Font = new System.Drawing.Font("Segoe UI", 14, System.Drawing.FontStyle.Bold),
                ForeColor = ColoresSistema.ColorPrincipal,
                Location = new System.Drawing.Point(20, 20),
                Size = new System.Drawing.Size(300, 30)
            };
            this.Controls.Add(lblTitulo);

            Label lblUsuario = new Label()
            {
                Text = "Usuario:",
                Location = new System.Drawing.Point(30, 70),
                Size = new System.Drawing.Size(80, 25),
                ForeColor = ColoresSistema.ColorTextoNormal
            };
            this.Controls.Add(lblUsuario);

            TextBox txtUsuario = new TextBox()
            {
                Location = new System.Drawing.Point(120, 70),
                Size = new System.Drawing.Size(200, 23)
            };
            this.Controls.Add(txtUsuario);

            Button btnRecuperar = new Button()
            {
                Text = "Recuperar",
                BackColor = ColoresSistema.ColorPrincipal,
                ForeColor = ColoresSistema.ColorTextoClaro,
                FlatStyle = FlatStyle.Flat,
                Location = new System.Drawing.Point(80, 130),
                Size = new System.Drawing.Size(100, 35)
            };
            btnRecuperar.Click += (s, e) =>
            {
                try
                {
                    using (SqlConnection conn = ConexionDB.ObtenerConexion())
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("SELECT Contrasena FROM Usuarios WHERE Nombre = @nombre", conn);
                        cmd.Parameters.AddWithValue("@nombre", txtUsuario.Text);
                        object result = cmd.ExecuteScalar();
                        if (result != null)
                        {
                            MessageBox.Show($"Su contraseña es: {result.ToString()}\n\nRecomendamos cambiarla después de iniciar sesión.", "Contraseña recuperada", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                        {
                            MessageBox.Show("Usuario no encontrado", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };
            this.Controls.Add(btnRecuperar);

            Button btnCancelar = new Button()
            {
                Text = "Cancelar",
                BackColor = ColoresSistema.ColorSecundario,
                ForeColor = ColoresSistema.ColorTextoClaro,
                FlatStyle = FlatStyle.Flat,
                Location = new System.Drawing.Point(200, 130),
                Size = new System.Drawing.Size(100, 35)
            };
            btnCancelar.Click += (s, e) => this.Close();
            this.Controls.Add(btnCancelar);
        }
    }
}
