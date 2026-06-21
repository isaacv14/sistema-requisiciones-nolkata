using System;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Microsoft.Data.SqlClient;

namespace Nolkata_Final
{
    public partial class frmReportes : Form
    {
        private DataGridView dgv;
        private Chart chart;
        private ComboBox cbTipoReporte;

        public frmReportes()
        {
            InitializeComponent();
            this.BackColor = ColoresSistema.ColorFondo;
            this.Text = "NOLKATA MARINE - Reportes";
            this.Size = new System.Drawing.Size(900, 700);
            this.StartPosition = FormStartPosition.CenterScreen;

            Label lblTitulo = new Label()
            {
                Text = "Reportes",
                Font = new System.Drawing.Font("Segoe UI", 16, System.Drawing.FontStyle.Bold),
                ForeColor = ColoresSistema.ColorPrincipal,
                Location = new System.Drawing.Point(20, 20),
                Size = new System.Drawing.Size(200, 35)
            };
            this.Controls.Add(lblTitulo);

            // Tipo de reporte
            Label lblTipo = new Label()
            {
                Text = "Tipo de Reporte:",
                Location = new System.Drawing.Point(20, 70),
                Size = new System.Drawing.Size(120, 25),
                ForeColor = ColoresSistema.ColorTextoNormal
            };
            this.Controls.Add(lblTipo);

            cbTipoReporte = new ComboBox()
            {
                Location = new System.Drawing.Point(150, 70),
                Size = new System.Drawing.Size(200, 23),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cbTipoReporte.Items.AddRange(new[] { "Pedidos por barco", "Productos más pedidos", "Pedidos atrasados" });
            cbTipoReporte.SelectedIndex = 0;
            this.Controls.Add(cbTipoReporte);

            // Botón Generar
            Button btnGenerar = new Button()
            {
                Text = "Generar",
                BackColor = ColoresSistema.ColorPrincipal,
                ForeColor = ColoresSistema.ColorTextoClaro,
                FlatStyle = FlatStyle.Flat,
                Location = new System.Drawing.Point(370, 68),
                Size = new System.Drawing.Size(100, 30)
            };
            btnGenerar.Click += (s, e) => GenerarReporte();
            this.Controls.Add(btnGenerar);

            // Botón Exportar
            Button btnExportar = new Button()
            {
                Text = "Exportar a Excel",
                BackColor = ColoresSistema.ColorAcento,
                ForeColor = ColoresSistema.ColorTextoClaro,
                FlatStyle = FlatStyle.Flat,
                Location = new System.Drawing.Point(490, 68),
                Size = new System.Drawing.Size(120, 30)
            };
            btnExportar.Click += (s, e) => ExportarExcel();
            this.Controls.Add(btnExportar);

            // DataGridView
            dgv = new DataGridView()
            {
                Location = new System.Drawing.Point(20, 120),
                Size = new System.Drawing.Size(400, 400),
                BackgroundColor = ColoresSistema.ColorBlanco,
                AllowUserToAddRows = false,
                ReadOnly = true,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill
            };
            this.Controls.Add(dgv);

            // Chart
            chart = new Chart()
            {
                Location = new System.Drawing.Point(440, 120),
                Size = new System.Drawing.Size(420, 400),
                BackColor = System.Drawing.Color.White
            };
            ChartArea chartArea = new ChartArea();
            chart.ChartAreas.Add(chartArea);
            this.Controls.Add(chart);

            // Botón Cerrar
            Button btnCerrar = new Button()
            {
                Text = "Cerrar",
                BackColor = ColoresSistema.ColorSecundario,
                ForeColor = ColoresSistema.ColorTextoClaro,
                FlatStyle = FlatStyle.Flat,
                Location = new System.Drawing.Point(750, 550),
                Size = new System.Drawing.Size(120, 35)
            };
            btnCerrar.Click += (s, e) => this.Close();
            this.Controls.Add(btnCerrar);
        }

        private void GenerarReporte()
        {
            dgv.Rows.Clear();
            dgv.Columns.Clear();
            chart.Series.Clear();

            string tipo = cbTipoReporte.SelectedItem.ToString();

            try
            {
                using (SqlConnection conn = ConexionDB.ObtenerConexion())
                {
                    conn.Open();

                    if (tipo == "Pedidos por barco")
                    {
                        dgv.ColumnCount = 2;
                        dgv.Columns[0].Name = "Barco";
                        dgv.Columns[1].Name = "Total Pedidos";

                        string query = @"
                            SELECT b.Nombre AS Barco, COUNT(r.IdRequisicion) AS Total
                            FROM Requisiciones r
                            INNER JOIN Barcos b ON r.IdBarco = b.IdBarco
                            GROUP BY b.Nombre
                            ORDER BY Total DESC";

                        SqlCommand cmd = new SqlCommand(query, conn);
                        SqlDataReader reader = cmd.ExecuteReader();

                        Series series = new Series("Pedidos por barco");
                        series.ChartType = SeriesChartType.Column;
                        series.Color = System.Drawing.Color.FromArgb(29, 112, 184);

                        while (reader.Read())
                        {
                            string barco = reader["Barco"].ToString();
                            int total = Convert.ToInt32(reader["Total"]);
                            dgv.Rows.Add(barco, total);
                            series.Points.AddXY(barco, total);
                        }
                        chart.Series.Add(series);
                    }
                    else if (tipo == "Productos más pedidos")
                    {
                        dgv.ColumnCount = 2;
                        dgv.Columns[0].Name = "Producto";
                        dgv.Columns[1].Name = "Veces Pedido";

                        string query = @"
                            SELECT Producto, SUM(Cantidad) AS Total
                            FROM DetalleRequisicion
                            GROUP BY Producto
                            ORDER BY Total DESC";

                        SqlCommand cmd = new SqlCommand(query, conn);
                        SqlDataReader reader = cmd.ExecuteReader();

                        Series series = new Series("Productos más pedidos");
                        series.ChartType = SeriesChartType.Bar;
                        series.Color = System.Drawing.Color.FromArgb(0, 113, 188);

                        while (reader.Read())
                        {
                            string producto = reader["Producto"].ToString();
                            int total = Convert.ToInt32(reader["Total"]);
                            dgv.Rows.Add(producto, total);
                            series.Points.AddXY(producto, total);
                        }
                        chart.Series.Add(series);
                    }
                    else if (tipo == "Pedidos atrasados")
                    {
                        dgv.ColumnCount = 3;
                        dgv.Columns[0].Name = "Barco";
                        dgv.Columns[1].Name = "Producto";
                        dgv.Columns[2].Name = "Días en espera";

                        string query = @"
                            SELECT b.Nombre AS Barco, d.Producto, DATEDIFF(day, d.FechaCambio, GETDATE()) AS Dias
                            FROM DetalleRequisicion d
                            INNER JOIN Requisiciones r ON d.IdRequisicion = r.IdRequisicion
                            INNER JOIN Barcos b ON r.IdBarco = b.IdBarco
                            WHERE d.Estado = 'Pendiente' AND DATEDIFF(day, d.FechaCambio, GETDATE()) > 3
                            ORDER BY Dias DESC";

                        SqlCommand cmd = new SqlCommand(query, conn);
                        SqlDataReader reader = cmd.ExecuteReader();

                        Series series = new Series("Pedidos atrasados");
                        series.ChartType = SeriesChartType.Column;
                        series.Color = System.Drawing.Color.FromArgb(239, 68, 68);

                        while (reader.Read())
                        {
                            string barco = reader["Barco"].ToString();
                            string producto = reader["Producto"].ToString();
                            int dias = Convert.ToInt32(reader["Dias"]);
                            dgv.Rows.Add(barco, producto, dias);
                            series.Points.AddXY($"{barco}\n{producto}", dias);
                        }
                        chart.Series.Add(series);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al generar reporte: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ExportarExcel()
        {
            if (dgv.Rows.Count == 0)
            {
                MessageBox.Show("No hay datos para exportar. Genere un reporte primero.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Archivos CSV (*.csv)|*.csv";
                sfd.FileName = $"Reporte_{DateTime.Now:yyyyMMdd_HHmmss}.csv";

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        using (StreamWriter sw = new StreamWriter(sfd.FileName))
                        {
                            string encabezados = "";
                            for (int i = 0; i < dgv.Columns.Count; i++)
                            {
                                encabezados += dgv.Columns[i].Name;
                                if (i < dgv.Columns.Count - 1) encabezados += ",";
                            }
                            sw.WriteLine(encabezados);

                            for (int i = 0; i < dgv.Rows.Count; i++)
                            {
                                string fila = "";
                                for (int j = 0; j < dgv.Columns.Count; j++)
                                {
                                    fila += dgv.Rows[i].Cells[j].Value?.ToString();
                                    if (j < dgv.Columns.Count - 1) fila += ",";
                                }
                                sw.WriteLine(fila);
                            }
                        }
                        MessageBox.Show("Reporte exportado correctamente", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error al exportar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }
    }
}