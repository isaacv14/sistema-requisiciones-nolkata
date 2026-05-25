namespace NolkataInc
{
    partial class frmLogin
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlHeader         = new System.Windows.Forms.Panel();
            this.lblEmpresa        = new System.Windows.Forms.Label();
            this.lblSubtitulo      = new System.Windows.Forms.Label();
            this.pnlFormulario     = new System.Windows.Forms.Panel();
            this.lblTituloForm     = new System.Windows.Forms.Label();
            this.lblNombre         = new System.Windows.Forms.Label();
            this.txtNombre         = new System.Windows.Forms.TextBox();
            this.lblCorreo         = new System.Windows.Forms.Label();
            this.txtCorreo         = new System.Windows.Forms.TextBox();
            this.lblTipoUsuario    = new System.Windows.Forms.Label();
            this.cmbTipoUsuario    = new System.Windows.Forms.ComboBox();
            this.btnRegistrar      = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.pnlFormulario.SuspendLayout();
            this.SuspendLayout();

            // pnlHeader - azul marino como el mar
            this.pnlHeader.BackColor  = System.Drawing.Color.FromArgb(15, 52, 96);
            this.pnlHeader.Dock       = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Height     = 110;
            this.pnlHeader.Controls.Add(this.lblEmpresa);
            this.pnlHeader.Controls.Add(this.lblSubtitulo);

            // lblEmpresa
            this.lblEmpresa.AutoSize  = false;
            this.lblEmpresa.Dock      = System.Windows.Forms.DockStyle.None;
            this.lblEmpresa.Font      = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold);
            this.lblEmpresa.ForeColor = System.Drawing.Color.White;
            this.lblEmpresa.Location  = new System.Drawing.Point(0, 15);
            this.lblEmpresa.Size      = new System.Drawing.Size(500, 45);
            this.lblEmpresa.Text      = "NOLKATA INC";
            this.lblEmpresa.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // lblSubtitulo
            this.lblSubtitulo.AutoSize  = false;
            this.lblSubtitulo.Font      = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtitulo.ForeColor = System.Drawing.Color.LightSteelBlue;
            this.lblSubtitulo.Location  = new System.Drawing.Point(0, 62);
            this.lblSubtitulo.Size      = new System.Drawing.Size(500, 25);
            this.lblSubtitulo.Text      = "Sistema de Gestion de Requisiciones Maritimas";
            this.lblSubtitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // pnlFormulario
            this.pnlFormulario.BackColor  = System.Drawing.Color.White;
            this.pnlFormulario.Location   = new System.Drawing.Point(50, 135);
            this.pnlFormulario.Size       = new System.Drawing.Size(400, 320);
            this.pnlFormulario.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlFormulario.Controls.Add(this.lblTituloForm);
            this.pnlFormulario.Controls.Add(this.lblNombre);
            this.pnlFormulario.Controls.Add(this.txtNombre);
            this.pnlFormulario.Controls.Add(this.lblCorreo);
            this.pnlFormulario.Controls.Add(this.txtCorreo);
            this.pnlFormulario.Controls.Add(this.lblTipoUsuario);
            this.pnlFormulario.Controls.Add(this.cmbTipoUsuario);
            this.pnlFormulario.Controls.Add(this.btnRegistrar);

            // lblTituloForm
            this.lblTituloForm.AutoSize  = false;
            this.lblTituloForm.Font      = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblTituloForm.ForeColor = System.Drawing.Color.FromArgb(15, 52, 96);
            this.lblTituloForm.Location  = new System.Drawing.Point(0, 18);
            this.lblTituloForm.Size      = new System.Drawing.Size(398, 30);
            this.lblTituloForm.Text      = "Registro de Usuario";
            this.lblTituloForm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // lblNombre
            this.lblNombre.AutoSize = true;
            this.lblNombre.Font     = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNombre.Location = new System.Drawing.Point(30, 72);
            this.lblNombre.Text     = "Nombre completo:";

            // txtNombre
            this.txtNombre.Font     = new System.Drawing.Font("Segoe UI", 10F);
            this.txtNombre.Location = new System.Drawing.Point(30, 92);
            this.txtNombre.Size     = new System.Drawing.Size(338, 26);

            // lblCorreo
            this.lblCorreo.AutoSize = true;
            this.lblCorreo.Font     = new System.Drawing.Font("Segoe UI", 9F);
            this.lblCorreo.Location = new System.Drawing.Point(30, 135);
            this.lblCorreo.Text     = "Correo electronico:";

            // txtCorreo
            this.txtCorreo.Font     = new System.Drawing.Font("Segoe UI", 10F);
            this.txtCorreo.Location = new System.Drawing.Point(30, 155);
            this.txtCorreo.Size     = new System.Drawing.Size(338, 26);

            // lblTipoUsuario
            this.lblTipoUsuario.AutoSize = true;
            this.lblTipoUsuario.Font     = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTipoUsuario.Location = new System.Drawing.Point(30, 198);
            this.lblTipoUsuario.Text     = "Tipo de usuario:";

            // cmbTipoUsuario
            this.cmbTipoUsuario.DropDownStyle     = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoUsuario.Font              = new System.Drawing.Font("Segoe UI", 10F);
            this.cmbTipoUsuario.FormattingEnabled = true;
            this.cmbTipoUsuario.Items.AddRange(new object[] { "Usuario Oficina", "Usuario Barco" });
            this.cmbTipoUsuario.Location          = new System.Drawing.Point(30, 218);
            this.cmbTipoUsuario.Size              = new System.Drawing.Size(338, 26);

            // btnRegistrar
            this.btnRegistrar.BackColor = System.Drawing.Color.FromArgb(15, 52, 96);
            this.btnRegistrar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRegistrar.Font      = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnRegistrar.ForeColor = System.Drawing.Color.White;
            this.btnRegistrar.Location  = new System.Drawing.Point(100, 268);
            this.btnRegistrar.Size      = new System.Drawing.Size(200, 38);
            this.btnRegistrar.Text      = "Ingresar al Sistema";
            this.btnRegistrar.Click    += new System.EventHandler(this.btnRegistrar_Click);

            // frmLogin
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor           = System.Drawing.Color.FromArgb(235, 242, 250);
            this.ClientSize          = new System.Drawing.Size(500, 490);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.pnlFormulario);
            this.FormBorderStyle     = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox         = false;
            this.Name                = "frmLogin";
            this.StartPosition       = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text                = "NOLKATA INC - Registro";
            this.pnlHeader.ResumeLayout(false);
            this.pnlFormulario.ResumeLayout(false);
            this.pnlFormulario.PerformLayout();
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Panel      pnlHeader;
        private System.Windows.Forms.Label      lblEmpresa;
        private System.Windows.Forms.Label      lblSubtitulo;
        private System.Windows.Forms.Panel      pnlFormulario;
        private System.Windows.Forms.Label      lblTituloForm;
        private System.Windows.Forms.Label      lblNombre;
        private System.Windows.Forms.TextBox    txtNombre;
        private System.Windows.Forms.Label      lblCorreo;
        private System.Windows.Forms.TextBox    txtCorreo;
        private System.Windows.Forms.Label      lblTipoUsuario;
        private System.Windows.Forms.ComboBox   cmbTipoUsuario;
        private System.Windows.Forms.Button     btnRegistrar;
    }
}
