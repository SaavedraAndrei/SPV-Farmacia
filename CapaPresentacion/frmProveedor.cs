using CapaNegocio;
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
    public partial class frmProveedor : Form
    {

        //Si se va a registrar
        private bool IsNuevo = false;

        //Si se va a editar
        private bool IsEditar = false;


        public frmProveedor()
        {
            InitializeComponent();
            ttMensaje.SetToolTip(this.txtRazonSocial, "Ingrese la razón social del Proveedor");
            ttMensaje.SetToolTip(this.txtNumDocumento, "Ingrese el número de documento del Proveedor");
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
            this.txtIdProveedor.Text = string.Empty;
            this.txtRazonSocial.Text = string.Empty;
            this.txtNumDocumento.Text = string.Empty;
            this.txtDireccion.Text = string.Empty;
            this.txtTelefono.Text = string.Empty;
            this.txtEmail.Text = string.Empty;
            this.txtUrl.Text = string.Empty;

        }

        //Habilitar los controles del formulario
        public void Habilitar(bool valor)
        {
            
            this.txtIdProveedor.ReadOnly = !valor;
            this.txtRazonSocial.ReadOnly = !valor;
            this.txtNumDocumento.ReadOnly = !valor;
            this.txtDireccion.ReadOnly = !valor;
            this.txtTelefono.ReadOnly = !valor;
            this.txtEmail.ReadOnly = !valor;
            this.txtUrl.ReadOnly = !valor;
            this.cbSectorComercial.Enabled = valor;
            this.cbTipoDocumento.Enabled = valor;
        }

        //Habilitar botones
        public void Botones()
        {
            if (IsNuevo || IsEditar)
            {
                Habilitar(true);
                this.btnNuevo.Enabled = false;
                this.btnGuardar.Enabled = true;
                this.btnEditar.Enabled = false;
                this.btnCancelar.Enabled = true;
            }
            else
            {
                Habilitar(false);
                this.btnNuevo.Enabled = true;
                this.btnGuardar.Enabled = false;
                this.btnEditar.Enabled = true;
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

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void frmProveedor_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            this.Habilitar(false);
            this.Botones();
            this.Consultar();
            
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

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult opcion;
                opcion = MessageBox.Show("Realmente Desea Eliminar los Registros?", "Sistema de Ventas", MessageBoxButtons.OKCancel
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
                            rpta = NProveedor.Eliminar(Convert.ToInt32(Codigo));

                            if (rpta.Equals("OK"))
                            {
                                this.MensajeOk("Se eliminó correctamente");
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

        private void chkEliminar_CheckedChanged(object sender, EventArgs e)
        {
            if (chkEliminar.Checked)
            {
                this.dataListado.Columns[0].Visible = true;

            }
            else
            {
                this.dataListado.Columns[0].Visible = false;
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {

            this.IsNuevo = true;
            this.Botones();
            this.Limpiar();
            this.txtRazonSocial.Focus();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string rpta = string.Empty;
                if (this.txtRazonSocial.Text == string.Empty)
                {
                    MensajeError("Falta ingresar algunos datos, serán remarcados");
                    this.ErrorIcono.SetError(txtRazonSocial, "Ingrese un Nombre");
                }
                else
                {
                    if (this.IsNuevo)
                    {
                        rpta = NProveedor.Insertar(this.txtRazonSocial.Text.Trim().ToUpper(),
                            this.cbSectorComercial.Text.Trim().ToUpper(),
                            this.cbTipoDocumento.Text.Trim().ToUpper(),
                            this.txtNumDocumento.Text.Trim().ToUpper(),
                            this.txtDireccion.Text.Trim().ToUpper(),
                            this.txtTelefono.Text.Trim().ToUpper(),
                            this.txtEmail.Text.Trim().ToUpper(),
                            this.txtUrl.Text.Trim().ToUpper()
                            );
                    }
                    else if (this.IsEditar)
                    {
                        rpta = NProveedor.Modificar(Convert.ToInt32(this.txtIdProveedor.Text.Trim()), 
                            this.txtRazonSocial.Text.Trim().ToUpper(),
                            this.cbSectorComercial.Text.Trim().ToUpper(),
                            this.cbTipoDocumento.Text.Trim().ToUpper(),
                            this.txtNumDocumento.Text.Trim().ToUpper(),
                            this.txtDireccion.Text.Trim().ToUpper(),
                            this.txtTelefono.Text.Trim().ToUpper(),
                            this.txtEmail.Text.Trim().ToUpper(),
                            this.txtUrl.Text.Trim().ToUpper()
                            );


                    }

                    if (rpta.Equals("OK"))
                    {
                        if (this.IsNuevo)
                        {
                            this.MensajeOk("Se ingresó correctamente el registro");
                        }
                        else if (this.IsEditar)
                        {
                            this.MensajeOk("Se actualizó correctamente el registro");
                        }
                    }
                    else
                    {
                        this.MensajeError(rpta);
                    }

                    this.IsNuevo = false;
                    this.IsEditar = false;
                    this.Botones();
                    this.Limpiar();
                    this.Consultar();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message + ex.StackTrace);
            }


        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (!txtIdProveedor.Text.Equals(""))
            {
                this.IsEditar = true;
                this.Botones();
                this.Habilitar(true);
            }
            else
            {
                this.MensajeError("Debe seleccionar el registro a modificar");
                this.ErrorIcono.SetError(this.txtIdProveedor, "Debe seleccionar un registro");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.IsNuevo = false;
            this.IsEditar = false;
            this.Botones();
            this.Limpiar();
            this.Habilitar(false);
            this.txtIdProveedor.Text = string.Empty;
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

        private void dataListado_DoubleClick(object sender, EventArgs e)
        {
            this.txtIdProveedor.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["idProveedor"].Value);
            this.txtRazonSocial.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["Razon_Social"].Value);
            this.cbSectorComercial.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["Sector_Comercial"].Value);
            this.cbTipoDocumento.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["Tipo_Documento"].Value);
            this.txtNumDocumento.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["Num_Documento"].Value);
            this.txtDireccion.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["Direccion"].Value);
            this.txtTelefono.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["Telefono"].Value);
            this.txtEmail.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["Email"].Value);
            this.txtUrl.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["Url"].Value);

            this.tabControl1.SelectedIndex = 1;
        }


    }
}
