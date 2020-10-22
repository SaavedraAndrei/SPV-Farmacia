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
    public partial class frmVistaArticuloIngreso : Form
    {



        public frmVistaArticuloIngreso()
        {
            InitializeComponent();
        }

        private void frmVistaArticuloIngreso_Load(object sender, EventArgs e)
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
            this.dataListado.DataSource = NArticulo.Consultar();
            this.OcultarColumnas();
            lblTotal.Text = "Total de Registros: " + Convert.ToString(this.dataListado.Rows.Count);
        }


        //Método Consultar por Nombre
        public void ConsultarNombre()
        {
            this.dataListado.DataSource = NArticulo.ConsultarNombre(this.txtBuscar.Text);
            this.OcultarColumnas();
            lblTotal.Text = "Total de Registros: " + Convert.ToString(this.dataListado.Rows.Count);

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.ConsultarNombre();
        }

        private void dataListado_DoubleClick(object sender, EventArgs e)
        {
            frmIngreso from = frmIngreso.GetInstancia();
            string par1, par2;
            par1 = Convert.ToString(this.dataListado.CurrentRow.Cells["idArticulo"].Value);
            par2 = Convert.ToString(this.dataListado.CurrentRow.Cells["Nombre"].Value);
            from.setArticulo(par1, par2);
            this.Hide();


        }
    }
}
