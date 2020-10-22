using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using CapaNegocio;

namespace CapaPresentacion
{
    public partial class frmIngreso : Form
    {
        public int IdTrabajador;
        private bool IsNuevo;
        private DataTable dtDetalle;
        private decimal totalPagado = 0;
        

        private static frmIngreso _instancia;

        public static frmIngreso GetInstancia()
        {
            if (_instancia == null)
            {
                _instancia = new frmIngreso();
            }
            return _instancia;
        }
        
        public void setProveedor(string idproveedor, string nombre)
        {
            this.txtIdProveedor.Text = idproveedor;
            this.txtProveedor.Text = nombre;
        }

        public void setArticulo(string idarticulo, string nombre)
        {
            this.txtIdArticulo.Text = idarticulo;
            this.txtArticulo.Text = nombre;
        }


        public frmIngreso()
        {
            InitializeComponent();
            this.ttMensaje.SetToolTip(this.txtProveedor, "Seleccione un proveedor");
            this.ttMensaje.SetToolTip(this.txtSerie, "Ingrese la serie del comprobante");
            this.ttMensaje.SetToolTip(this.txtArticulo, "Seleccione un artículo");
            this.ttMensaje.SetToolTip(this.txtStock, "Ingrese la cantidad de compra");
        }

        public void MensajeOk(string mensaje)
        {
            MessageBox.Show(mensaje, "Sistema de Ventas", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        //Mostrar mensaje de Error
        public void MensajeError(string mensaje)
        {
            MessageBox.Show(mensaje, "Sistema de Ventas", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        //Limpiar los controles
        public void Limpiar()
        {
            this.txtIdArticulo.Text = string.Empty;
            this.txtIdProveedor.Text = string.Empty;
            this.txtProveedor.Text = string.Empty;
            this.txtSerie.Text = string.Empty;
            this.txtCorrelativo.Text = string.Empty;
            this.txtIgv.Text = string.Empty;
            this.lblTotalPagado.Text = "0.0";
            this.txtIgv.Text = "18";
            this.CrearTabla();

        }

        public void LimpiarDetalle()
        {
            this.txtIdArticulo.Text = string.Empty;
            this.txtArticulo.Text = string.Empty;
            this.txtPrecioCompra.Text = string.Empty;
            this.txtPrecioVenta.Text = string.Empty;
            this.txtStock.Text = string.Empty;
        }

        //Habilitar los controles del formulario
        public void Habilitar(bool valor)
        {
            this.txtIdIngreso.ReadOnly = !valor;
            this.txtSerie.ReadOnly = !valor;
            this.txtCorrelativo.ReadOnly = !valor;
            this.txtIgv.ReadOnly = !valor;
            this.dtFecha.Enabled = valor;
            this.cbTipoComprobante.Enabled = valor;
            this.txtStock.ReadOnly= !valor;
            this.txtPrecioCompra.ReadOnly = !valor;
            this.txtPrecioVenta.ReadOnly = !valor;
            this.dtFechaProduccion.Enabled = valor;
            this.dtFechaVencimiento.Enabled = valor;

            this.btnBuscarArtículo.Enabled = valor;
            this.btnBuscarProveedor.Enabled = valor;
            this.btnAgregar.Enabled = valor;
            this.btnQuitar.Enabled = valor;
            
        }

        //Habilitar botones
        public void Botones()
        {
            if (IsNuevo)
            {
                Habilitar(true);
                this.btnNuevo.Enabled = false;
                this.btnGuardar.Enabled = true;
                this.btnCancelar.Enabled = true;
            }
            else
            {
                Habilitar(false);
                this.btnNuevo.Enabled = true;
                this.btnGuardar.Enabled = false;
                this.btnCancelar.Enabled = false;
            }
        }

        //Método ocultar Columnas
        public void OcultarColumnas()
        {
            this.dataListado.Columns[0].Visible = false;
        }

        //Método Consultar
        public void Consultar()
        {
            this.dataListado.DataSource = NIngreso.Consultar();
            this.OcultarColumnas();
            lblTotal.Text = "Total de Registros: " + Convert.ToString(this.dataListado.Rows.Count);
        }   


        //Método Consultar por Nombre
        public void ConsultarFechas()
        {
            this.dataListado.DataSource = NIngreso.ConsultarFechas(this.dtFecha1.Value.ToString("dd/MM/yyyy")
                ,this.dtFecha2.Value.ToString("dd/MM/yyyy"));

            this.OcultarColumnas();
            lblTotal.Text = "Total de Registros: " + Convert.ToString(this.dataListado.Rows.Count);
            

        }

        public void ConsultarDetalles()
        {
            this.dataListadoDetalle.DataSource = NIngreso.ConsultarDetalle_Ingreso(this.txtIdIngreso.Text);
            
            //this.OcultarColumnas();
            //lblTotal.Text = "Total de Registros: " + Convert.ToString(dataListado.Rows.Count);
            //this.datalistadoDetalle.AutoGenerateColumns = false;
        }

        private void CrearTabla()
        {
            this.dtDetalle = new DataTable("Detalle");
            this.dtDetalle.Columns.Add("idArticulo", System.Type.GetType("System.Int32"));
            this.dtDetalle.Columns.Add("Articulo", System.Type.GetType("System.String"));
            this.dtDetalle.Columns.Add("Precio_Compra", System.Type.GetType("System.Decimal"));
            this.dtDetalle.Columns.Add("Precio_Venta", System.Type.GetType("System.Decimal"));
            this.dtDetalle.Columns.Add("Stock_Inicial", System.Type.GetType("System.Int32"));
            this.dtDetalle.Columns.Add("Fecha_Produccion", System.Type.GetType("System.DateTime"));
            this.dtDetalle.Columns.Add("Fecha_Vencimiento", System.Type.GetType("System.DateTime"));
            this.dtDetalle.Columns.Add("SubTotal", System.Type.GetType("System.Decimal"));

            //Relaciona DataGriedView
            this.dataListadoDetalle.DataSource = this.dtDetalle;

        }

        private void tabPage2_Click(object sender, EventArgs e)
        {

        }

        private void frmIngreso_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            this.Consultar();
            this.Habilitar(false);
            this.Botones();
            this.CrearTabla();
        }

        private void frmIngreso_FormClosing(object sender, FormClosingEventArgs e)
        {
            _instancia = null;
        }

        private void btnBuscarProveedor_Click(object sender, EventArgs e)
        {
            frmVistaProveedorIngreso frm = new frmVistaProveedorIngreso();
            frm.ShowDialog();

        }

        private void btnBuscarArtículo_Click(object sender, EventArgs e)
        {
            frmVistaArticuloIngreso frm = new frmVistaArticuloIngreso();
            frm.ShowDialog();
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.ConsultarFechas();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult opcion;
                opcion = MessageBox.Show("Realmente Desea Anular los Registros?", 
                    "Sistema de Ventas", MessageBoxButtons.OKCancel
                    , MessageBoxIcon.Question);

                if (opcion == DialogResult.OK)
                {
                    string Codigo;
                    string rpta = "";

                    foreach (DataGridViewRow row in dataListado.Rows)
                    {
                        if (Convert.ToBoolean(row.Cells[0].Value))
                        {
                            Codigo = Convert.ToString(row.Cells[1].Value);
                            rpta = NIngreso.Anular(Convert.ToInt32(Codigo));

                            if (rpta.Equals("OK"))
                            {
                                this.MensajeOk("Se anuló correctamente");
                            }
                            else
                            {
                                this.MensajeError(rpta);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace);
            }

            this.Consultar();
        }

        private void chkAnular_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAnular.Checked)
            {
                this.dataListado.Columns[0].Visible = true;

            }
            else
            {
                this.dataListado.Columns[0].Visible = false;
            }
        }

        private void dataListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataListado.Columns["Eliminar"].Index)
            {
                DataGridViewCheckBoxCell ChkEliminar =
                    (DataGridViewCheckBoxCell)dataListado.Rows[e.RowIndex].Cells["Eliminar"];

                ChkEliminar.Value = !Convert.ToBoolean(ChkEliminar.Value);
            }


        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            this.IsNuevo = true;
            this.Botones();
            this.Limpiar();
            this.txtSerie.Focus();
            this.Habilitar(true);
            this.LimpiarDetalle();

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.IsNuevo = false;
            this.Botones();
            this.Limpiar();
            this.Habilitar(false);
            this.LimpiarDetalle();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string rpta = string.Empty;
                if (this.txtSerie.Text == string.Empty || this.txtCorrelativo.Text == string.Empty
                    || this.txtIgv.Text == string.Empty)
                {
                    MensajeError("Falta ingresar algunos datos, serán remarcados");
                    this.ErrorIcono.SetError(txtSerie, "Ingrese un valor");
                    this.ErrorIcono.SetError(txtCorrelativo, "Ingrese un valor");
                    this.ErrorIcono.SetError(txtIgv, "Ingrese un valor");
                }
                else
                {
                    

                    if (this.IsNuevo)
                    {
                        rpta = NIngreso.Insertar(IdTrabajador,
                            Convert.ToInt32(this.txtIdProveedor.Text),
                            this.dtFecha.Value,
                            this.cbTipoComprobante.Text,
                            this.txtSerie.Text,
                            this.txtCorrelativo.Text,
                            Convert.ToDecimal(this.txtIgv.Text), "Emitidio",
                            dtDetalle);

                        ////Vamos a insertar un Ingreso 
                        //Rpta = NIngreso.Insertar(Idtrabajador, Convert.ToInt32(txtIdproveedor.Text),
                        //dtFecha.Value, cbTipo_Comprobante.Text,
                        //txtSerie.Text, txtCorrelativo.Text,
                        //Convert.ToDecimal(txtIgv.Text), "EMITIDO", dtDetalle);

                    }
                    

                    if (rpta.Equals("OK"))
                    {
                        if (this.IsNuevo)
                        {
                            this.MensajeOk("Se ingresó correctamente el registro");
                        }
                        
                    }
                    else
                    {
                        this.MensajeError(rpta);
                    }

                    this.IsNuevo = false;
                    this.Botones();
                    this.Limpiar();
                    this.Consultar();
                    this.LimpiarDetalle();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                
                if (this.txtIdArticulo.Text == string.Empty || this.txtStock.Text == string.Empty
                    || this.txtPrecioCompra.Text == string.Empty || this.txtPrecioVenta.Text == string.Empty)
                {
                    MensajeError("Falta ingresar algunos datos, serán remarcados");
                    this.ErrorIcono.SetError(txtIdArticulo, "Ingrese un valor");
                    this.ErrorIcono.SetError(txtStock, "Ingrese un valor");
                    this.ErrorIcono.SetError(txtPrecioVenta, "Ingrese un valor");
                    this.ErrorIcono.SetError(txtPrecioCompra, "Ingrese un valor");
                }
                else
                {
                    bool registrar = true;
                    foreach (DataRow row in dtDetalle.Rows)
                    {
                        if (Convert.ToInt32(row["idArticulo"]) == Convert.ToInt32(this.txtIdArticulo.Text))
                        {
                            registrar = false;
                            this.MensajeError("Ya se encuentra el artículo en el detalle");
                        }
                    }
                    if (registrar)
                    {
                        decimal SubTotal =
                            Convert.ToDecimal(this.txtStock.Text) * Convert.ToDecimal(this.txtPrecioCompra.Text);

                        totalPagado = totalPagado + SubTotal;
                        this.lblTotalPagado.Text = totalPagado.ToString("#0.00#");

                        DataRow row = this.dtDetalle.NewRow();
                        row["idArticulo"] = Convert.ToInt32(this.txtIdArticulo.Text);
                        row["Articulo"] = (this.txtArticulo.Text);
                        row["Precio_Compra"] = Convert.ToDecimal(this.txtPrecioCompra.Text);
                        row["Precio_Venta"] = Convert.ToDecimal(this.txtPrecioVenta.Text);
                        row["Stock_Inicial"] = Convert.ToInt32(this.txtStock.Text);
                        row["Fecha_produccion"] = dtFechaProduccion.Value;
                        row["Fecha_vencimiento"] = dtFechaVencimiento.Value;
                        row["SubTotal"] = SubTotal;
                        this.dtDetalle.Rows.Add(row);
                        this.LimpiarDetalle();
                    }
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace);
            }
        }

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            try
            {
                int indiceFila = this.dataListadoDetalle.CurrentCell.RowIndex;
                DataRow row = this.dtDetalle.Rows[indiceFila];
                //Disminuir pagado
                this.totalPagado = this.totalPagado - Convert.ToDecimal(row["SubTotal"].ToString());
                this.lblTotalPagado.Text = totalPagado.ToString("#0.00#");
                //removemos la fila
                this.dtDetalle.Rows.Remove(row);
            }
            catch (Exception ex)
            {
                MensajeError("No hay fila para remover");
            }
        }

        private void dataListado_DoubleClick(object sender, EventArgs e)
        {
            this.txtIdIngreso.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["idIngreso"].Value);
            this.txtIdProveedor.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["Proveedor"].Value);
            this.dtFecha.Value = Convert.ToDateTime(this.dataListado.CurrentRow.Cells["Fecha"].Value);
            this.cbTipoComprobante.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["Tipo_Comprobante"].Value);
            this.txtSerie.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["Serie"].Value);
            this.txtCorrelativo.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["Correlativo"].Value);
            this.lblTotalPagado.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["Total"].Value);
            this.ConsultarDetalles();
            this.tabControl1.SelectedIndex = 1;

            //this.txtIdingreso.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["idingreso"].Value);
            //this.txtProveedor.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["proveedor"].Value);
            //this.dtFecha.Value = Convert.ToDateTime(this.dataListado.CurrentRow.Cells["fecha"].Value);
            //this.cbTipo_Comprobante.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["tipo_comprobante"].Value);
            //this.txtSerie.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["serie"].Value);
            //this.txtCorrelativo.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["correlativo"].Value);
            //this.lblTotalPagado.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["Total"].Value);
            //this.MostrarDetalles();
            //this.tabControl1.SelectedIndex = 1;

        }

        private void txtIdProveedor_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
