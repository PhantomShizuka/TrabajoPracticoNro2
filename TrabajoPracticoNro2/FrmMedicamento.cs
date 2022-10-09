using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrabajoPracticoNro2
{
    public partial class FrmMedicamento : Form
    {
        private readonly Medicamento Medicamento = new Medicamento("BD_YOPLAC_NAVARRO");
        private readonly List<Control> ListDatos = new List<Control>();
  
        public FrmMedicamento()
        {  
            InitializeComponent();     
        }
        private void FrmMedicamento_Load(object sender, EventArgs e)
        {
            AgruparDatos();
            Clear();
            Width = 290;
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                Medicamento.AgregarMedicamento(ListDatos);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            Clear();
        }

        private void BtnMostrar_Click(object sender, EventArgs e)
        {
            if (btnMostrar.Text == "Mostrar Lista")
            {
                btnMostrar.Text = "Ocultar Lista";

                while (Width < 1080)
                {
                    Width += 10;
                }
            }
            else
            {
                btnMostrar.Text = "Mostrar Lista";

                while (Width > 290)
                {
                    Width -= 10;
                }
            }
        }

        private void Clear()
        {
            Medicamento.GenerarNuevoId(txtCodigo);
            txtNombre.Clear();
            txtProveedor.Clear();
            txtStock.Clear();
            txtPrecio.Clear();
            txtPresentacion.Clear();
            dtpFechaVen.Value = DateTime.Now;
            dgvMedicamento.DataSource = Medicamento.MostrarDatos();
            txtNombre.Focus();
        }

        private void AgruparDatos()
        {
            ListDatos.Add(txtCodigo);
            ListDatos.Add(txtNombre);
            ListDatos.Add(txtProveedor);
            ListDatos.Add(txtStock);
            ListDatos.Add(txtPrecio);
            ListDatos.Add(txtPresentacion);
            ListDatos.Add(dtpFechaVen);
        }
    }
}
