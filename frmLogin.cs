using System;
using System.Windows.Forms;
using NolkataInc.Clases;

namespace NolkataInc
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnRegistrar_Click(object sender, EventArgs e)
        {
            // Validaciones
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("Por favor ingrese su nombre.", "Campo vacio",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(txtCorreo.Text))
            {
                MessageBox.Show("Por favor ingrese su correo.", "Campo vacio",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cmbTipoUsuario.SelectedIndex == -1)
            {
                MessageBox.Show("Seleccione el tipo de usuario.", "Campo vacio",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string nombre = txtNombre.Text.Trim();
            string correo = txtCorreo.Text.Trim();
            string tipo   = cmbTipoUsuario.Text;

            // Instanciar el usuario segun el tipo seleccionado
            Usuario miUsuario;

            if (tipo == "Usuario Oficina")
                miUsuario = new UsuarioOficina(nombre, correo);
            else
                miUsuario = new UsuarioBarco(nombre, correo);

            // Abrir el panel principal pasando el usuario
            frmPrincipal principal = new frmPrincipal(miUsuario);
            principal.Show();
            this.Hide();
        }
    }
}
