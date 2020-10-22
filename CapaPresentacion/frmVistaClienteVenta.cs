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
    public partial class frmVistaClienteVenta : Form
    {
        public frmVistaClienteVenta()
        {
            InitializeComponent();
        }

        private void frmVistaClienteVenta_Load(object sender, EventArgs e)
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
            this.dataListado.DataSource = NCliente.Consultar();
            this.OcultarColumnas();
            lblTotal.Text = "Total de Registros: " + Convert.ToString(this.dataListado.Rows.Count);
        }


        //Método Consultar por Nombre
        public void ConsultarApellidos()
        {
            this.dataListado.DataSource = NCliente.ConsultarApellidos(this.txtBuscar.Text);
            this.OcultarColumnas();
            lblTotal.Text = "Total de Registros: " + Convert.ToString(this.dataListado.Rows.Count);

        }


        public void ConsultarDocumento()
        {
            this.dataListado.DataSource = NCliente.ConsultarDocumento(this.txtBuscar.Text);
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
                case "Apellidos":
                    ConsultarApellidos();
                    break;
            }
        }

        private void dataListado_DoubleClick(object sender, EventArgs e)
        {
            frmVenta form = frmVenta.GetInstancia();
            string par1, par2;
            par1 = Convert.ToString(this.dataListado.CurrentRow.Cells["idCliente"].Value);
            par2 = Convert.ToString(this.dataListado.CurrentRow.Cells["Nombre"].Value) + " "
                + Convert.ToString(this.dataListado.CurrentRow.Cells["Apellidos"].Value);
            form.setCliente(par1, par2);
            this.Hide();
        }
    }
}
