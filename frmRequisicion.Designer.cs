namespace NolkataInc
{
    partial class frmRequisicion
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pnlHeader           = new System.Windows.Forms.Panel();
            this.lblTituloForm       = new System.Windows.Forms.Label();

            // Cabecera
            this.lblBarco            = new System.Windows.Forms.Label();
            this.txtBarco            = new System.Windows.Forms.TextBox();
            this.lblFecha            = new System.Windows.Forms.Label();
            this.dtpFecha            = new System.Windows.Forms.DateTimePicker();

            // Seccion agregar producto
            this.pnlAgregarProducto  = new System.Windows.Forms.Panel();
            this.lblSeccionProducto  = new System.Windows.Forms.Label();
            this.lblProducto         = new System.Windows.Forms.Label();
            this.txtProducto         = new System.Windows.Forms.TextBox();
            this.lblCantidad         = new System.Windows.Forms.Label();
            this.txtCantidad         = new System.Windows.Forms.TextBox();
            this.lblCostoUnitario    = new System.Windows.Forms.Label();
            this.txtCostoUnitario    = new System.Windows.Forms.TextBox();
            this.btnAgregarProducto  = new System.Windows.Forms.Button();

            // Grid de detalles
            this.lblListaProductos   = new System.Windows.Forms.Label();
            this.dgvDetalles         = new System.Windows.Forms.DataGridView();
            this.colDetProducto      = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDetCantidad      = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDetEstado        = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDetCosto         = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDetFecha         = new System.Windows.Forms.DataGridViewTextBoxColumn();

            // Total y botones
            this.lblTotalTitulo      = new System.Windows.Forms.Label();
            this.lblTotalValor       = new System.Windows.Forms.Label();
            this.btnGuardarRequisicion = new System.Windows.Forms.Button();
            this.btnCancelar         = new System.Windows.Forms.Button();

            this.pnlHeader.SuspendLayout();
            this.pnlAgregarProducto.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalles)).BeginInit();
            this.SuspendLayout();

            // pnlHeader
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(15, 52, 96);
            this.pnlHeader.Dock      = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Height    = 60;
            this.pnlHeader.Controls.Add(this.lblTituloForm);

            this.lblTituloForm.AutoSize  = false;
            this.lblTituloForm.Dock      = System.Windows.Forms.DockStyle.Fill;
            this.lblTituloForm.Font      = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTituloForm.ForeColor = System.Drawing.Color.White;
            this.lblTituloForm.Text      = "Nueva Requisicion";
            this.lblTituloForm.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;

            // Cabecera - barco y fecha
            this.lblBarco.AutoSize = true;
            this.lblBarco.Font     = new System.Drawing.Font("Segoe UI", 9F);
            this.lblBarco.Location = new System.Drawing.Point(20, 75);
            this.lblBarco.Text     = "Barco:";

            this.txtBarco.Font     = new System.Drawing.Font("Segoe UI", 10F);
            this.txtBarco.Location = new System.Drawing.Point(20, 93);
            this.txtBarco.Size     = new System.Drawing.Size(200, 26);

            this.lblFecha.AutoSize = true;
            this.lblFecha.Font     = new System.Drawing.Font("Segoe UI", 9F);
            this.lblFecha.Location = new System.Drawing.Point(240, 75);
            this.lblFecha.Text     = "Fecha:";

            this.dtpFecha.Font     = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpFecha.Location = new System.Drawing.Point(240, 93);
            this.dtpFecha.Size     = new System.Drawing.Size(200, 26);
            this.dtpFecha.Format   = System.Windows.Forms.DateTimePickerFormat.Short;

            // pnlAgregarProducto
            this.pnlAgregarProducto.BackColor  = System.Drawing.Color.FromArgb(235, 242, 250);
            this.pnlAgregarProducto.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlAgregarProducto.Location   = new System.Drawing.Point(20, 135);
            this.pnlAgregarProducto.Size       = new System.Drawing.Size(740, 110);
            this.pnlAgregarProducto.Controls.Add(this.lblSeccionProducto);
            this.pnlAgregarProducto.Controls.Add(this.lblProducto);
            this.pnlAgregarProducto.Controls.Add(this.txtProducto);
            this.pnlAgregarProducto.Controls.Add(this.lblCantidad);
            this.pnlAgregarProducto.Controls.Add(this.txtCantidad);
            this.pnlAgregarProducto.Controls.Add(this.lblCostoUnitario);
            this.pnlAgregarProducto.Controls.Add(this.txtCostoUnitario);
            this.pnlAgregarProducto.Controls.Add(this.btnAgregarProducto);

            this.lblSeccionProducto.AutoSize  = true;
            this.lblSeccionProducto.Font      = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblSeccionProducto.ForeColor = System.Drawing.Color.FromArgb(15, 52, 96);
            this.lblSeccionProducto.Location  = new System.Drawing.Point(10, 8);
            this.lblSeccionProducto.Text      = "Agregar Producto";

            this.lblProducto.AutoSize = true;
            this.lblProducto.Font     = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblProducto.Location = new System.Drawing.Point(10, 32);
            this.lblProducto.Text     = "Producto:";

            this.txtProducto.Font     = new System.Drawing.Font("Segoe UI", 9F);
            this.txtProducto.Location = new System.Drawing.Point(10, 50);
            this.txtProducto.Size     = new System.Drawing.Size(240, 22);

            this.lblCantidad.AutoSize = true;
            this.lblCantidad.Font     = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblCantidad.Location = new System.Drawing.Point(265, 32);
            this.lblCantidad.Text     = "Cantidad:";

            this.txtCantidad.Font     = new System.Drawing.Font("Segoe UI", 9F);
            this.txtCantidad.Location = new System.Drawing.Point(265, 50);
            this.txtCantidad.Size     = new System.Drawing.Size(80, 22);

            this.lblCostoUnitario.AutoSize = true;
            this.lblCostoUnitario.Font     = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblCostoUnitario.Location = new System.Drawing.Point(360, 32);
            this.lblCostoUnitario.Text     = "Costo Unitario ($):";

            this.txtCostoUnitario.Font     = new System.Drawing.Font("Segoe UI", 9F);
            this.txtCostoUnitario.Location = new System.Drawing.Point(360, 50);
            this.txtCostoUnitario.Size     = new System.Drawing.Size(120, 22);

            this.btnAgregarProducto.BackColor = System.Drawing.Color.FromArgb(15, 52, 96);
            this.btnAgregarProducto.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAgregarProducto.Font      = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnAgregarProducto.ForeColor = System.Drawing.Color.White;
            this.btnAgregarProducto.Location  = new System.Drawing.Point(580, 44);
            this.btnAgregarProducto.Size      = new System.Drawing.Size(140, 30);
            this.btnAgregarProducto.Text      = "+ Agregar";
            this.btnAgregarProducto.Click    += new System.EventHandler(this.btnAgregarProducto_Click);

            // lblListaProductos
            this.lblListaProductos.AutoSize  = true;
            this.lblListaProductos.Font      = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblListaProductos.ForeColor = System.Drawing.Color.FromArgb(15, 52, 96);
            this.lblListaProductos.Location  = new System.Drawing.Point(20, 258);
            this.lblListaProductos.Text      = "Productos en esta Requisicion";

            // dgvDetalles
            this.dgvDetalles.AllowUserToAddRows    = false;
            this.dgvDetalles.AllowUserToDeleteRows = false;
            this.dgvDetalles.AutoSizeColumnsMode   = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDetalles.BackgroundColor       = System.Drawing.Color.White;
            this.dgvDetalles.ColumnHeadersHeight   = 32;
            this.dgvDetalles.ColumnHeadersDefaultCellStyle.BackColor = System.Drawing.Color.FromArgb(15, 52, 96);
            this.dgvDetalles.ColumnHeadersDefaultCellStyle.ForeColor = System.Drawing.Color.White;
            this.dgvDetalles.ColumnHeadersDefaultCellStyle.Font      = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.dgvDetalles.EnableHeadersVisualStyles = false;
            this.dgvDetalles.Location          = new System.Drawing.Point(20, 282);
            this.dgvDetalles.ReadOnly          = true;
            this.dgvDetalles.RowHeadersVisible = false;
            this.dgvDetalles.SelectionMode     = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDetalles.Size              = new System.Drawing.Size(740, 200);
            this.dgvDetalles.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
                this.colDetProducto, this.colDetCantidad, this.colDetEstado,
                this.colDetCosto, this.colDetFecha });

            this.colDetProducto.HeaderText = "Producto";
            this.colDetProducto.Name       = "colDetProducto";
            this.colDetCantidad.HeaderText = "Cantidad";
            this.colDetCantidad.Name       = "colDetCantidad";
            this.colDetEstado.HeaderText   = "Estado";
            this.colDetEstado.Name         = "colDetEstado";
            this.colDetCosto.HeaderText    = "Costo Unit.";
            this.colDetCosto.Name          = "colDetCosto";
            this.colDetFecha.HeaderText    = "Ultimo Cambio";
            this.colDetFecha.Name          = "colDetFecha";

            // Total
            this.lblTotalTitulo.AutoSize  = true;
            this.lblTotalTitulo.Font      = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTotalTitulo.Location  = new System.Drawing.Point(560, 494);
            this.lblTotalTitulo.Text      = "Total:";

            this.lblTotalValor.AutoSize  = true;
            this.lblTotalValor.Font      = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTotalValor.ForeColor = System.Drawing.Color.FromArgb(15, 52, 96);
            this.lblTotalValor.Location  = new System.Drawing.Point(610, 492);
            this.lblTotalValor.Text      = "$0.00";

            // Botones finales
            this.btnGuardarRequisicion.BackColor = System.Drawing.Color.FromArgb(15, 52, 96);
            this.btnGuardarRequisicion.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuardarRequisicion.Font      = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnGuardarRequisicion.ForeColor = System.Drawing.Color.White;
            this.btnGuardarRequisicion.Location  = new System.Drawing.Point(440, 530);
            this.btnGuardarRequisicion.Size      = new System.Drawing.Size(160, 38);
            this.btnGuardarRequisicion.Text      = "Guardar Requisicion";
            this.btnGuardarRequisicion.Click    += new System.EventHandler(this.btnGuardarRequisicion_Click);

            this.btnCancelar.BackColor = System.Drawing.Color.FromArgb(180, 50, 50);
            this.btnCancelar.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCancelar.Font      = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancelar.ForeColor = System.Drawing.Color.White;
            this.btnCancelar.Location  = new System.Drawing.Point(615, 530);
            this.btnCancelar.Size      = new System.Drawing.Size(110, 38);
            this.btnCancelar.Text      = "Cancelar";
            this.btnCancelar.Click    += new System.EventHandler(this.btnCancelar_Click);

            // frmRequisicion
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode       = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor           = System.Drawing.Color.White;
            this.ClientSize          = new System.Drawing.Size(780, 590);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.lblBarco);
            this.Controls.Add(this.txtBarco);
            this.Controls.Add(this.lblFecha);
            this.Controls.Add(this.dtpFecha);
            this.Controls.Add(this.pnlAgregarProducto);
            this.Controls.Add(this.lblListaProductos);
            this.Controls.Add(this.dgvDetalles);
            this.Controls.Add(this.lblTotalTitulo);
            this.Controls.Add(this.lblTotalValor);
            this.Controls.Add(this.btnGuardarRequisicion);
            this.Controls.Add(this.btnCancelar);
            this.Name          = "frmRequisicion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text          = "NOLKATA INC - Nueva Requisicion";
            this.Load         += new System.EventHandler(this.frmRequisicion_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlAgregarProducto.ResumeLayout(false);
            this.pnlAgregarProducto.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDetalles)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Panel                      pnlHeader;
        private System.Windows.Forms.Label                      lblTituloForm;
        private System.Windows.Forms.Label                      lblBarco;
        private System.Windows.Forms.TextBox                    txtBarco;
        private System.Windows.Forms.Label                      lblFecha;
        private System.Windows.Forms.DateTimePicker             dtpFecha;
        private System.Windows.Forms.Panel                      pnlAgregarProducto;
        private System.Windows.Forms.Label                      lblSeccionProducto;
        private System.Windows.Forms.Label                      lblProducto;
        private System.Windows.Forms.TextBox                    txtProducto;
        private System.Windows.Forms.Label                      lblCantidad;
        private System.Windows.Forms.TextBox                    txtCantidad;
        private System.Windows.Forms.Label                      lblCostoUnitario;
        private System.Windows.Forms.TextBox                    txtCostoUnitario;
        private System.Windows.Forms.Button                     btnAgregarProducto;
        private System.Windows.Forms.Label                      lblListaProductos;
        private System.Windows.Forms.DataGridView               dgvDetalles;
        private System.Windows.Forms.DataGridViewTextBoxColumn  colDetProducto;
        private System.Windows.Forms.DataGridViewTextBoxColumn  colDetCantidad;
        private System.Windows.Forms.DataGridViewTextBoxColumn  colDetEstado;
        private System.Windows.Forms.DataGridViewTextBoxColumn  colDetCosto;
        private System.Windows.Forms.DataGridViewTextBoxColumn  colDetFecha;
        private System.Windows.Forms.Label                      lblTotalTitulo;
        private System.Windows.Forms.Label                      lblTotalValor;
        private System.Windows.Forms.Button                     btnGuardarRequisicion;
        private System.Windows.Forms.Button                     btnCancelar;
    }
}
