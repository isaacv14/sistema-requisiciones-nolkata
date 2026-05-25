namespace NolkataInc
{
    partial class frmPrincipal
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlHeader              = new System.Windows.Forms.Panel();
            this.lblEmpresa             = new System.Windows.Forms.Label();
            this.lblBienvenida          = new System.Windows.Forms.Label();
            this.lblTipoLabel           = new System.Windows.Forms.Label();
            this.lblTipoUsuario         = new System.Windows.Forms.Label();
            this.lblAvisoCosto          = new System.Windows.Forms.Label();
            this.pnlBotones             = new System.Windows.Forms.Panel();
            this.btnNuevaRequisicion    = new System.Windows.Forms.Button();
            this.btnSalir               = new System.Windows.Forms.Button();
            this.lblTituloLista         = new System.Windows.Forms.Label();
            this.dgvRequisiciones       = new System.Windows.Forms.DataGridView();
            this.colFecha               = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colBarco               = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProductos           = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colCosto               = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnlHeader.SuspendLayout();
            this.pnlBotones.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRequisiciones)).BeginInit();
            this.SuspendLayout();

            // pnlHeader
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(15, 52, 96);
            this.pnlHeader.Dock      = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Height    = 90;
            this.pnlHeader.Controls.Add(this.lblEmpresa);
            this.pnlHeader.Controls.Add(this.lblBienvenida);
            this.pnlHeader.Controls.Add(this.lblTipoLabel);
            this.pnlHeader.Controls.Add(this.lblTipoUsuario);

            this.lblEmpresa.AutoSize  = false;
            this.lblEmpresa.Font      = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblEmpresa.ForeColor = System.Drawing.Color.White;
            this.lblEmpresa.Location  = new System.Drawing.Point(20, 10);
            this.lblEmpresa.Size      = new System.Drawing.Size(300, 38);
            this.lblEmpresa.Text      = "NOLKATA INC";

            this.lblBienvenida.AutoSize  = false;
            this.lblBienvenida.Font      = new System.Drawing.Font("Segoe UI", 9F);
            this.lblBienvenida.ForeColor = System.Drawing.Color.LightSteelBlue;
            this.lblBienvenida.Location  = new System.Drawing.Point(20, 52);
            this.lblBienvenida.Size      = new System.Drawing.Size(400, 20);
            this.lblBienvenida.Text      = "---";

            this.lblTipoLabel.AutoSize  = true;
            this.lblTipoLabel.Font      = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTipoLabel.ForeColor = System.Drawing.Color.LightSteelBlue;
            this.lblTipoLabel.Location  = new System.Drawing.Point(650, 35);
            this.lblTipoLabel.Text      = "Rol:";

            this.lblTipoUsuario.AutoSize  = true;
            this.lblTipoUsuario.Font      = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTipoUsuario.ForeColor = System.Drawing.Color.Yellow;
            this.lblTipoUsuario.Location  = new System.Drawing.Point(680, 35);
            this.lblTipoUsuario.Text      = "---";

            // lblAvisoCosto - solo visible para UsuarioBarco
            this.lblAvisoCosto.AutoSize  = false;
            this.lblAvisoCosto.BackColor = System.Drawing.Color.FromArgb(255, 243, 205);
            this.lblAvisoCosto.Font      = new System.Drawing.Font("Segoe UI", 9F);
            this.lblAvisoCosto.ForeColor = System.Drawing.Color.FromArgb(133, 100, 4);
            this.lblAvisoCosto.Location  = new System.Drawing.Point(20, 100);
            this.lblAvisoCosto.Size      = new System.Drawing.Size(760, 28);
            this.lblAvisoCosto.Text      = "  ⚠  Los costos no son visibles para usuarios de barco.";
            this.lblAvisoCosto.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblAvisoCosto.Visible   = false;

            // pnlBotones
            this.pnlBotones.BackColor = System.Drawing.Color.FromArgb(235, 242, 250);
            this.pnlBotones.Location  = new System.Drawing.Point(20, 138);
            this.pnlBotones.Size      = new System.Drawing.Size(760, 50);
            this.pnlBotones.Controls.Add(this.btnNuevaRequisicion);
            this.pnlBotones.Controls.Add(this.btnSalir);

            this.btnNuevaRequisicion.BackColor = System.Drawing.Color.FromArgb(15, 52, 96);
            this.btnNuevaRequisicion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnNuevaRequisicion.Font      = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnNuevaRequisicion.ForeColor = System.Drawing.Color.White;
            this.btnNuevaRequisicion.Location  = new System.Drawing.Point(0, 8);
            this.btnNuevaRequisicion.Size      = new System.Drawing.Size(180, 34);
            this.btnNuevaRequisicion.Text      = "+ Nueva Requisicion";
            this.btnNuevaRequisicion.Click    += new System.EventHandler(this.btnNuevaRequisicion_Click);

            this.btnSalir.BackColor = System.Drawing.Color.FromArgb(180, 50, 50);
            this.btnSalir.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSalir.Font      = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSalir.ForeColor = System.Drawing.Color.White;
            this.btnSalir.Location  = new System.Drawing.Point(670, 8);
            this.btnSalir.Size      = new System.Drawing.Size(90, 34);
            this.btnSalir.Text      = "Salir";
            this.btnSalir.Click    += new System.EventHandler(this.btnSalir_Click);

            // lblTituloLista
            this.lblTituloLista.AutoSize = true;
            this.lblTituloLista.Font     = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTituloLista.ForeColor = System.Drawing.Color.FromArgb(15, 52, 96);
            this.lblTituloLista.Location = new System.Drawing.Point(20, 200);
            this.lblTituloLista.Text     = "Historial de Requisiciones";

            // dgvRequisiciones
            this.dgvRequisiciones.AllowUserToAddRows    = false;
            this.dgvRequisiciones.AllowUserToDeleteRows = false;
            this.dgvRequisiciones.AutoSizeColumnsMode   = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvRequisiciones.BackgroundColor       = System.Drawing.Color.White;
            this.dgvRequisiciones.BorderStyle           = System.Windows.Forms.BorderStyle.FixedSingle;
            this.dgvRequisiciones.ColumnHeadersHeight   = 35;
            this.dgvRequisiciones.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(15, 52, 96);
            this.dgvRequisiciones.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvRequisiciones.ColumnHeadersDefaultCellStyle.Font      = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.dgvRequisiciones.EnableHeadersVisualStyles = false;
            this.dgvRequisiciones.Location          = new System.Drawing.Point(20, 228);
            this.dgvRequisiciones.ReadOnly          = true;
            this.dgvRequisiciones.RowHeadersVisible = false;
            this.dgvRequisiciones.SelectionMode     = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvRequisiciones.Size              = new System.Drawing.Size(760, 320);
            this.dgvRequisiciones.RowPrePaint      += new System.Windows.Forms.DataGridViewRowPrePaintEventHandler(this.dgvRequisiciones_RowPrePaint);
            this.dgvRequisiciones.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colFecha, this.colBarco, this.colProductos, this.colCosto });

            this.colFecha.HeaderText = "Fecha";
            this.colFecha.Name       = "colFecha";

            this.colBarco.HeaderText = "Barco";
            this.colBarco.Name       = "colBarco";

            this.colProductos.HeaderText = "Productos";
            this.colProductos.Name       = "colProductos";

            this.colCosto.HeaderText = "Costo Total";
            this.colCosto.Name       = "colCosto";

            // frmPrincipal
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor           = System.Drawing.Color.FromArgb(235, 242, 250);
            this.ClientSize          = new System.Drawing.Size(800, 580);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.lblAvisoCosto);
            this.Controls.Add(this.pnlBotones);
            this.Controls.Add(this.lblTituloLista);
            this.Controls.Add(this.dgvRequisiciones);
            this.MinimumSize         = new System.Drawing.Size(800, 580);
            this.Name                = "frmPrincipal";
            this.StartPosition       = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text                = "NOLKATA INC - Panel Principal";
            this.Load               += new System.EventHandler(this.frmPrincipal_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlBotones.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRequisiciones)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Panel                  pnlHeader;
        private System.Windows.Forms.Label                  lblEmpresa;
        private System.Windows.Forms.Label                  lblBienvenida;
        private System.Windows.Forms.Label                  lblTipoLabel;
        private System.Windows.Forms.Label                  lblTipoUsuario;
        private System.Windows.Forms.Label                  lblAvisoCosto;
        private System.Windows.Forms.Panel                  pnlBotones;
        private System.Windows.Forms.Button                 btnNuevaRequisicion;
        private System.Windows.Forms.Button                 btnSalir;
        private System.Windows.Forms.Label                  lblTituloLista;
        private System.Windows.Forms.DataGridView           dgvRequisiciones;
        private System.Windows.Forms.DataGridViewTextBoxColumn colFecha;
        private System.Windows.Forms.DataGridViewTextBoxColumn colBarco;
        private System.Windows.Forms.DataGridViewTextBoxColumn colProductos;
        private System.Windows.Forms.DataGridViewTextBoxColumn colCosto;
    }
}
