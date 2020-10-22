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
    public partial class frmVistaCategoria : Form
    {
        public frmVistaCategoria()
        {
            InitializeComponent();
        }
        public void OcultarColumnas()
        {
            this.dataListado.Columns[0].Visible = false;
        }

        public void ConsultarNombre()
        {
            this.dataListado.DataSource = NCategoria.ConsultarNombre(this.txtBuscar.Text);
            this.OcultarColumnas();
            lblTotal.Text = "Total de Registros: " + Convert.ToString(this.dataListado.Rows.Count);

        }

        public void Consultar()
        {
            this.dataListado.DataSource = NCategoria.Consultar();
            this.OcultarColumnas();
            lblTotal.Text = "Total de Registros: " + Convert.ToString(this.dataListado.Rows.Count);
        }

        private void frmVistaCategoria_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            this.Consultar();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            this.ConsultarNombre();
        }

        private void dataListado_DoubleClick(object sender, EventArgs e)
        {
            frmArticulo formulario = frmArticulo.GetInstancia();
            string par1, par2;

            par1 = Convert.ToString(this.dataListado.CurrentRow.Cells["idCategoria"].Value);
            par2 = Convert.ToString(this.dataListado.CurrentRow.Cells["Nombre"].Value);
            formulario.SetCategoria(par1, par2);
            this.Hide();
        }
    }
}
