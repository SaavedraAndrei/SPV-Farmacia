using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CapaNegocio;

namespace CapaPresentacion
{
    public partial class frmVistaProveedorIngreso : Form
    {
        public frmVistaProveedorIngreso()
        {
            InitializeComponent();
        }

        private void frmVistaProveedorIngreso_Load(object sender, EventArgs e)
        {
            this.Consultar();
        }

        public void OcultarColumnas()
        {
            this.dataListado.Columns[0].Visible = false;
        }

        //Método Consultar
        public void Consultar()
        {
            this.dataListado.DataSource = NProveedor.Consultar();
            this.OcultarColumnas();
            lblTotal.Text = "Total de Registros: " + Convert.ToString(this.dataListado.Rows.Count);
        }


        //Método Consultar por Nombre
        public void ConsultarRazonSocial()
        {
            this.dataListado.DataSource = NProveedor.ConsultarRazonSocial(this.txtBuscar.Text);
            this.OcultarColumnas();
            lblTotal.Text = "Total de Registros: " + Convert.ToString(this.dataListado.Rows.Count);

        }

        public void ConsultarDocumento()
        {
            this.dataListado.DataSource = NProveedor.ConsultarDocumento(this.txtBuscar.Text);
            this.OcultarColumnas();
            lblTotal.Text = "Total de Registros: " + Convert.ToString(this.dataListado.Rows.Count);

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            switch (cbBuscar.SelectedItem)
            {
                case "Documento":
                    ConsultarDocumento();
                    break;
                case "Razón Social":
                    ConsultarRazonSocial();
                    break;
            }
        }

        private void dataListado_DoubleClick(object sender, EventArgs e)
        {
            frmIngreso form = frmIngreso.GetInstancia();
            string par1, par2;
            par1 = Convert.ToString(this.dataListado.CurrentRow.Cells["idProveedor"].Value);
            par2 = Convert.ToString(this.dataListado.CurrentRow.Cells["Razon_Social"].Value);
            form.setProveedor(par1, par2);
            this.Hide();
        }
    }
}
