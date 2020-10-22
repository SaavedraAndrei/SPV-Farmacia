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
    public partial class frmVistaArticuloVenta : Form
    {
        public frmVistaArticuloVenta()
        {
            InitializeComponent();
            

        }

        private void frmVistaArticuloVenta_Load(object sender, EventArgs e)
        {
            
        }   

        public void OcultarColumnas()
        {
            this.dataListado.Columns[0].Visible = false;
        }



        private void ConsultarArticulo_Venta_Nombre()
        {
            this.dataListado.DataSource = NVenta.ConsultarArticulo_Venta_Nombre(this.txtBuscar.Text);
            this.OcultarColumnas();
            lblTotal.Text = "Total de Registros: " + Convert.ToString(this.dataListado.Rows.Count);

        }

        private void ConsultarArticulo_Venta_Codigo()
        {
            this.dataListado.DataSource = NVenta.ConsultarArticulo_Venta_Codigo(this.txtBuscar.Text);
            this.OcultarColumnas();
            lblTotal.Text = "Total de Registros: " + Convert.ToString(this.dataListado.Rows.Count);

        }



        private void btnBuscar_Click(object sender, EventArgs e)
        {
            
        }


        private void btnBuscar_Click_1(object sender, EventArgs e)
        {
            switch (cbBuscar.SelectedItem)
            {
                case "Nombre":
                    this.ConsultarArticulo_Venta_Nombre();
                    break;
                case "Codigo":
                    this.ConsultarArticulo_Venta_Codigo();
                    break;
            }
        }

        private void dataListado_DoubleClick(object sender, EventArgs e)
        {
            frmVenta frm = frmVenta.GetInstancia();
            string par1, par2;
            Decimal par3, par4;
            int par5;
            DateTime par6;
            par1 = Convert.ToString(this.dataListado.CurrentRow.Cells["idDetalle_Ingreso"].Value);
            par2 = Convert.ToString(this.dataListado.CurrentRow.Cells["Nombre"].Value);
            par3 = Convert.ToDecimal(this.dataListado.CurrentRow.Cells["Precio_Compra"].Value);
            par4 = Convert.ToDecimal(this.dataListado.CurrentRow.Cells["Precio_Venta"].Value);
            par5 = Convert.ToInt32(this.dataListado.CurrentRow.Cells["Stock_Actual"].Value);
            par6 = Convert.ToDateTime(this.dataListado.CurrentRow.Cells["Fecha_Vencimiento"].Value);

            frm.setArticulo(par1, par2, par3, par4, par5, par6);
            this.Hide();

        }
    }
}
