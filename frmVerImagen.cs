using System;
using System.Drawing;
using System.Windows.Forms;

namespace Nolkata_Final
{
    public partial class frmVerImagen : Form
    {
        public frmVerImagen(string rutaImagen)
        {
            InitializeComponent();
            this.BackColor = ColoresSistema.ColorFondo;
            this.Text = "Ver Imagen del Producto";
            this.Size = new System.Drawing.Size(500, 500);
            this.StartPosition = FormStartPosition.CenterScreen;

            PictureBox pb = new PictureBox()
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.Zoom,
                Image = Image.FromFile(rutaImagen)
            };
            this.Controls.Add(pb);

            Button btnCerrar = new Button()
            {
                Text = "Cerrar",
                BackColor = ColoresSistema.ColorSecundario,
                ForeColor = ColoresSistema.ColorTextoClaro,
                FlatStyle = FlatStyle.Flat,
                Location = new System.Drawing.Point(180, 420),
                Size = new System.Drawing.Size(100, 35)
            };
            btnCerrar.Click += (s, e) => this.Close();
            this.Controls.Add(btnCerrar);
        }
    }
}