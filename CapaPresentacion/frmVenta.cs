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
    public partial class frmVenta : Form
    {
        public int IdTrabajador;
        private bool IsNuevo = false;
        private DataTable dtDetalle;
        private decimal totalPagado = 0;

        private static frmVenta _instancia;

        public static frmVenta GetInstancia()
        {
            if (_instancia == null)
            {
                _instancia = new frmVenta();
            }

            return _instancia;
        }

        public void setCliente(string idcliente, string nombre)
        {
            this.txtIdCliente.Text = idcliente;
            this.txtCliente.Text = nombre;
        }

        public void setArticulo(string iddetalle_ingreso, string nombre,
            decimal precio_compra, decimal precio_venta, int stock, DateTime fecha_vencimiento)
        {
            this.txtIdArticulo.Text = iddetalle_ingreso;
            this.txtArticulo.Text = nombre;
            this.txtPrecioCompra.Text =Convert.ToString(precio_compra);
            this.txtPrecioVenta.Text = Convert.ToString(precio_venta);
            this.txtStockActual.Text = Convert.ToString(stock);
            this.dtFechaVencimiento.Value = fecha_vencimiento;

        }


        public frmVenta()
        {
            InitializeComponent();
            this.ttMensaje.SetToolTip(this.txtCliente, "Seleccione un cliente");
            this.ttMensaje.SetToolTip(this.txtSerie, "Ingrese la serie del comprobante");
            this.ttMensaje.SetToolTip(this.txtCorrelativo, "Seleccione un correlativo del comprobante");
            this.ttMensaje.SetToolTip(this.txtArticulo, "Seleccione un artículo");
            this.dtFecha.Enabled = false;
            this.txtPrecioCompra.ReadOnly = true;
            this.txtStockActual.ReadOnly = true;
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
            this.txtIdVenta.Text = string.Empty;
            this.txtIdCliente.Text = string.Empty;
            this.txtIdCliente.Text = string.Empty;
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
            this.txtStockActual.Text = string.Empty;
            this.txtCantidad.Text = string.Empty;
            this.txtDescuento.Text = string.Empty;
        }

        //Habilitar los controles del formulario
        public void Habilitar(bool valor)
        {
            this.txtIdVenta.ReadOnly = !valor;
            this.txtSerie.ReadOnly = !valor;
            this.txtCorrelativo.ReadOnly = !valor;
            this.txtIgv.ReadOnly = !valor;
            this.dtFecha.Enabled = valor;
            this.cbTipoComprobante.Enabled = valor;
            this.txtCantidad.ReadOnly = !valor;
            this.txtPrecioCompra.ReadOnly = !valor;
            this.txtPrecioVenta.ReadOnly = !valor;
            this.dtFechaVencimiento.Enabled = valor;
            this.txtStockActual.ReadOnly = !valor;

            this.btnBuscarArtículo.Enabled = valor;
            this.btnBuscarCliente.Enabled = valor;
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
            this.dataListado.DataSource = NVenta.Consultar();
            this.OcultarColumnas();
            lblTotal.Text = "Total de Registros: " + Convert.ToString(this.dataListado.Rows.Count);
        }


        //Método Consultar por Nombre
        public void ConsultarFechas()
        {
            this.dataListado.DataSource = NVenta.ConsultarFechas(this.dtFecha1.Value.ToString("dd/MM/yyyy")
                , this.dtFecha2.Value.ToString("dd/MM/yyyy"));

            this.OcultarColumnas();
            lblTotal.Text = "Total de Registros: " + Convert.ToString(this.dataListado.Rows.Count);


        }

        public void ConsultarDetalles()
        {
            this.dataListadoDetalle.DataSource = NVenta.ConsultarDetalle_Venta(this.txtIdVenta.Text);

            //this.OcultarColumnas();
            //lblTotal.Text = "Total de Registros: " + Convert.ToString(dataListado.Rows.Count);
            //this.datalistadoDetalle.AutoGenerateColumns = false;
        }

        private void CrearTabla()
        {
            this.dtDetalle = new DataTable("Detalle");
            this.dtDetalle.Columns.Add("idDetalle_Ingreso", System.Type.GetType("System.Int32"));
            this.dtDetalle.Columns.Add("Articulo", System.Type.GetType("System.String"));
            this.dtDetalle.Columns.Add("Cantidad", System.Type.GetType("System.Int32"));
            this.dtDetalle.Columns.Add("Precio_Venta", System.Type.GetType("System.Decimal"));
            this.dtDetalle.Columns.Add("Descuento", System.Type.GetType("System.Decimal")); 
            this.dtDetalle.Columns.Add("SubTotal", System.Type.GetType("System.Decimal"));

            //Relaciona DataGriedView
            this.dataListadoDetalle.DataSource = this.dtDetalle;

        }

        private void label14_Click(object sender, EventArgs e)
        {

        }

        private void frmVenta_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            this.Consultar();
            this.Habilitar(false);
            this.Botones();
            this.CrearTabla();
        }

        private void frmVenta_FormClosing(object sender, FormClosingEventArgs e)
        {
            _instancia = null;
        }

        private void btnBuscarCliente_Click(object sender, EventArgs e)
        {
            frmVistaClienteVenta vista = new frmVistaClienteVenta();
            vista.ShowDialog();
        }

        private void btnBuscarArtículo_Click(object sender, EventArgs e)
        {
            frmVistaArticuloVenta vista = new frmVistaArticuloVenta();
            vista.ShowDialog();
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
                opcion = MessageBox.Show("Realmente Desea Eliminar los Registros?",
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
                            rpta = NVenta.Eliminar(Convert.ToInt32(Codigo));

                            if (rpta.Equals("OK"))
                            {
                                this.MensajeOk("Se eliminió correctamente");
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

        private void dataListado_DoubleClick(object sender, EventArgs e)
        {
            this.txtIdVenta.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["idVenta"].Value);
            this.txtCliente.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["Cliente"].Value);
            this.dtFecha.Value = Convert.ToDateTime(this.dataListado.CurrentRow.Cells["Fecha"].Value);
            this.cbTipoComprobante.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["Tipo_Comprobante"].Value);
            this.txtSerie.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["Serie"].Value);
            this.txtCorrelativo.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["Correlativo"].Value);
            this.lblTotalPagado.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["Total"].Value);
            this.ConsultarDetalles();
            this.tabControl1.SelectedIndex = 1;
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
            this.LimpiarDetalle();
            this.Habilitar(true);
            this.txtSerie.Focus();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.IsNuevo = false;
            this.Botones();
            this.Limpiar();
            this.LimpiarDetalle();
            this.Habilitar(false);
            this.txtSerie.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string rpta = string.Empty;
                if (this.txtSerie.Text == string.Empty || this.txtCorrelativo.Text == string.Empty
                    || this.txtIgv.Text == string.Empty || this.txtCliente.Text == string.Empty)
                {
                    MensajeError("Falta ingresar algunos datos, serán remarcados");
                    this.ErrorIcono.SetError(txtSerie, "Ingrese un valor");
                    this.ErrorIcono.SetError(txtCorrelativo, "Ingrese un valor");
                    this.ErrorIcono.SetError(txtIgv, "Ingrese un valor");
                    this.ErrorIcono.SetError(txtCliente, "Ingrese un valor");
                }
                else
                {


                    if (this.IsNuevo)
                    {
                        rpta = NVenta.Insertar(
                            Convert.ToInt32(this.txtIdCliente.Text), IdTrabajador,
                            this.dtFecha.Value,
                            this.cbTipoComprobante.Text,
                            this.txtSerie.Text,
                            this.txtCorrelativo.Text,
                            Convert.ToDecimal(this.txtIgv.Text),
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

                if (this.txtIdCliente.Text == string.Empty || this.txtIdVenta.Text == string.Empty
                    || this.txtPrecioCompra.Text == string.Empty || this.txtPrecioVenta.Text == string.Empty)
                {
                    MensajeError("Falta ingresar algunos datos, serán remarcados");
                    this.ErrorIcono.SetError(txtIdArticulo, "Ingrese un valor");
                    this.ErrorIcono.SetError(txtIdVenta, "Ingrese un valor");
                    this.ErrorIcono.SetError(txtPrecioVenta, "Ingrese un valor");
                    this.ErrorIcono.SetError(txtPrecioCompra, "Ingrese un valor");
                    this.ErrorIcono.SetError(txtCliente, "Ingrese un valor");
                }
                else
                {
                    bool registrar = true;
                    foreach (DataRow row in dtDetalle.Rows)
                    {
                        if (Convert.ToInt32(row["idDetalle_Ingreso"]) == Convert.ToInt32(this.txtIdArticulo.Text))
                        {
                            registrar = false;
                            this.MensajeError("Ya se encuentra el artículo en el detalle");
                        }
                    }
                    if (registrar && 
                        Convert.ToInt32(this.txtCantidad.Text) <= Convert.ToInt32(this.txtStockActual.Text))
                    {
                        decimal SubTotal =
                            Convert.ToDecimal(this.txtCantidad.Text) * Convert.ToDecimal(this.txtPrecioVenta.Text) -
                            Convert.ToDecimal(this.txtDescuento.Text);

                        totalPagado = totalPagado + SubTotal;
                        this.lblTotalPagado.Text = totalPagado.ToString("#0.00#");

                        DataRow row = this.dtDetalle.NewRow();
                        row["idDetalle_Ingreso"] = Convert.ToInt32(this.txtIdArticulo.Text);
                        row["Articulo"] = (this.txtArticulo.Text);
                        row["Cantidad"] = Convert.ToInt32(this.txtCantidad.Text);
                        row["Precio_Venta"] = Convert.ToDecimal(this.txtPrecioVenta.Text);
                        row["Descuento"] = Convert.ToDecimal(this.txtDescuento.Text);
                        row["SubTotal"] = SubTotal;
                        this.dtDetalle.Rows.Add(row);
                        this.LimpiarDetalle();
                    }
                    else
                    {
                        MensajeError("No hay Stock Suficiente");
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
    }
}
