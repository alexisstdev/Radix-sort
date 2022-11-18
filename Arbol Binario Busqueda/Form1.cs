using System;
using System.Windows.Forms;

namespace Radix_Sort
{
    public partial class ABB : Form
    {
        public ABB()
        {
            InitializeComponent();
            lblComparaciones.Visible = false;
            lblIntercambios.Visible = false;
            lblTamano.Visible = false;
            lblTiempo.Visible = false;
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (!ValidarDatos(pnlDatos)) return;

            Estudiante estudiante = new()
            {
                Nombre = txtNombre.Text,
                Edad = (int)nudEdad.Value,
                Promedio = nudPromedio.Value,
                NumControl = (int)nudId.Value
            };

            dtgTablaDesordenada.Rows.Add(estudiante.Nombre, estudiante.NumControl, estudiante.Edad, estudiante.Promedio);
            ReiniciarControles(pnlDatos);
            MessageBox.Show($"El estudiante con NumControl {estudiante} Ha sido agregado correctamente", "Inserción exitosa");
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show($"¿Seguro que desea vaciar el arreglo? Esta acción eliminará todos los elementos.", "Vaciar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.No) return;

            dtgTablaDesordenada.Rows.Clear();
            dtgTablaOrdenada.Rows.Clear();
            lblComparaciones.Visible = false;
            lblIntercambios.Visible = false;
            lblTamano.Visible = false;
            lblTiempo.Visible = false;
            ReiniciarControles(pnlDatos);
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            /* if (dtgTabla.SelectedCells.Count > 0)
             {
                 DataGridViewRow selectedRow = dtgTabla.Rows[dtgTabla.SelectedCells[0].RowIndex];
                 Estudiante estudiante = new()
                 {
                     NumControl = selectedRow.Cells["NumControl"].Value.ToString(),
                 };
                 DialogResult dialogResult = MessageBox.Show($"Seguro que desea eliminar el estudiante con NumControl: {estudiante}", "Eliminar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                 if (dialogResult == DialogResult.No) return;

                 foreach (Estudiante item in estudiantes)
                 {
                     if (item.NumControl == estudiante.NumControl)
                     {
                         estudiantes.Remove(item);
                         break;
                     }
                 }

                 MessageBox.Show($"El estudiante {estudiante} Ha sido eliminado", "Eliminación confirmada");
             }
             ActualizarControles();*/
        }

        private void btnLlenar_Click(object sender, EventArgs e)
        {
            Random random = new();
            string[] nombreEstudiantes = { "Pedro", "Manuel", "Francisco", "Osvaldo", "Vanessa", "Leslie", "Rodrigo", "Maria" };

            txtNombre.Text = nombreEstudiantes[random.Next(nombreEstudiantes.Length)];
            nudId.Value = random.Next(1, 10000);
        }

        private void Ordenar()
        {
            dtgTablaOrdenada.Rows.Clear();
            int[] arregloEnteros = new int[dtgTablaDesordenada.Rows.Count];
            Estudiante[] estudiantes = new Estudiante[dtgTablaDesordenada.Rows.Count];
            foreach (DataGridViewRow row in dtgTablaDesordenada.Rows)
            {
                Estudiante estudiante = new()
                {
                    Nombre = row.Cells["Nombre"].Value.ToString(),
                    NumControl = (int)row.Cells["NumControl"].Value,
                    Promedio = (decimal)row.Cells["Promedio"].Value,
                    Edad = (int)row.Cells["Edad"].Value,
                };
                arregloEnteros[row.Index] = estudiante.NumControl;
                estudiantes[row.Index] = estudiante;
            }
            System.Diagnostics.Stopwatch Reloj = System.Diagnostics.Stopwatch.StartNew();
            try
            {
                Ordenador.RadixSort(estudiantes, rbtnAscendente.Checked);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Reloj.Stop();
            foreach (Estudiante estudiante in estudiantes)
            {
                dtgTablaOrdenada.Rows.Add(estudiante.Nombre, estudiante.NumControl, estudiante.Edad, estudiante.Promedio);
            }

            lblComparaciones.Visible = true;
            lblIntercambios.Visible = true;
            lblTiempo.Visible = true;
            lblTamano.Visible = true;
            lblTiempo.Text = "Tiempo: " + Reloj.Elapsed.TotalMilliseconds + "ms";
            lblTamano.Text = "Elementos: " + dtgTablaDesordenada.Rows.Count;
        }

        private void BtnOrdenar_Click(object sender, EventArgs e)
        {
            Ordenar();
        }

        private void BtnRandom_Click(object sender, EventArgs e)
        {
            Random random = new();
            string[] nombreEstudiantes = { "Pedro", "Manuel", "Francisco", "Osvaldo", "Vanessa", "Leslie", "Rodrigo", "Maria" };
            for (int i = 0; i < 3000; i++)
            {
                Estudiante estudiante = new()
                {
                    Nombre = nombreEstudiantes[random.Next(nombreEstudiantes.Length)],
                    NumControl = random.Next(10000, 100000),
                    Promedio = Math.Round((decimal)random.NextDouble() * 100, 2),
                    Edad = random.Next(0, 100),
                };

                dtgTablaDesordenada.Rows.Add(estudiante.Nombre, estudiante.NumControl, estudiante.Edad, estudiante.Promedio);

                ReiniciarControles(pnlDatos);
            }
        }

        private void DtgTabla_SelectionChanged(object sender, EventArgs e)
        {
            if (dtgTablaDesordenada.SelectedCells.Count <= 0) return;
            DataGridViewRow selectedRow = dtgTablaDesordenada.Rows[dtgTablaDesordenada.SelectedCells[0].RowIndex];
            txtNombre.Text = selectedRow.Cells["Nombre"].Value.ToString();
            nudId.Value = (int)selectedRow.Cells["NumControl"].Value;
            nudPromedio.Value = (decimal)selectedRow.Cells["Promedio"].Value;
            nudEdad.Value = (int)selectedRow.Cells["Edad"].Value;
        }

        private void ReiniciarControles(Panel pnlData)
        {
            foreach (Control control in pnlData.Controls)
            {
                NumericUpDown nud = control as NumericUpDown;
                if (control is TextBox || control is ComboBox) control.Text = "";
                if (control is NumericUpDown) nud.Value = 0;
            }
        }

        private bool ValidarDatos(Panel pnlData)
        {
            foreach (Control control in pnlData.Controls)
            {
                NumericUpDown nud = control as NumericUpDown;
                if (control.Text.Trim() == "")
                {
                    if (control is PictureBox) continue;
                    if (control is Panel) continue;
                    MessageBox.Show("Todos los campos son requeridos");
                    return false;
                }
                if (control is NumericUpDown && nud.Value <= 0)
                {
                    MessageBox.Show("Favor de verificar los campos numéricos");
                    return false;
                }
            }
            return true;
        }

        private void rbtnAscendente_CheckedChanged(object sender, EventArgs e)
        {
            Ordenar();
        }
    }
}