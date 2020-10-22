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
    public partial class frmPresentacion : Form
    {
        private bool IsNuevo = false;
        private bool IsEditar = false;


        public frmPresentacion()
        {
            InitializeComponent();
            this.ttMensaje.SetToolTip(this.txtNombre,"Ingrese el Nombre de la Presentación");
        }

        public void MensajeOk(string cadena)
        {
            MessageBox.Show(cadena, "Sistema de Ventas", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void MensajeError(string cadena)
        {
            MessageBox.Show(cadena, "Sistema de Ventas", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void Limpiar()
        {
            this.txtIdPresentacion.Text = string.Empty;
            this.txtNombre.Text = string.Empty;
            this.txtDescripcion.Text = string.Empty;
        }

        //Habilitar los controles del formulario
        public void Habilitar(bool valor)
        {
            this.txtIdPresentacion.ReadOnly = !valor;
            this.txtNombre.ReadOnly = !valor;
            this.txtDescripcion.ReadOnly = !valor;
        }


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


        public void OcultarColumnas()
        {
            this.dataListado.Columns[0].Visible = false;
        }
        public void Consultar()
        {
            this.dataListado.DataSource = NPresentacion.Consultar();
            this.OcultarColumnas();
            lblTotal.Text = "Total de Registros: " + Convert.ToString(this.dataListado.Rows.Count);
        }


        //Método Consultar por Nombre
        public void ConsultarNombre()
        {
            this.dataListado.DataSource = NPresentacion.ConsultarNombre(this.txtBuscar.Text);
            this.OcultarColumnas();
            lblTotal.Text = "Total de Registros: " + Convert.ToString(this.dataListado.Rows.Count);

        }


        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void frmPresentacion_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            this.Botones();
            this.Consultar();
            this.Habilitar(false);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            this.ConsultarNombre();
        }

        private void txtBuscar_TextChanged(object sender, EventArgs e)
        {
            this.ConsultarNombre();
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            this.IsNuevo = true;
            this.Botones();
            this.Limpiar();
            this.txtNombre.Focus();

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string rpta = string.Empty;

                if (this.txtNombre.Text == string.Empty)
                {
                    this.MensajeError("Falta ingresar algunos datos, serán remarcados");
                    this.ErrorIcono.SetError(this.txtNombre, "Ingrese el nombre de la Presentación");
                }
                else
                {
                    if (this.IsNuevo)
                    {
                        rpta = NPresentacion.Insertar(this.txtNombre.Text.Trim().ToUpper(), this.txtDescripcion.Text.Trim().ToUpper());
                    }
                    else if (this.IsEditar)
                    {
                        rpta = NPresentacion.Modificar(Convert.ToInt32(this.txtIdPresentacion.Text.Trim()),
                            this.txtNombre.Text.Trim().ToUpper(),
                            this.txtDescripcion.Text.Trim().ToUpper());
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
            catch (Exception ex )
            {
                MessageBox.Show(ex.Message + ex.StackTrace);

            }
        }

        private void dataListado_DoubleClick(object sender, EventArgs e)
        {
            this.txtIdPresentacion.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["idPresentacion"].Value);
            this.txtNombre.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["Nombre"].Value);
            this.txtDescripcion.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["Descripcion"].Value);

            this.tabControl1.SelectedIndex = 1;
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (!txtIdPresentacion.Text.Equals(""))
            {
                this.IsEditar = true;
                this.Botones();
                this.Habilitar(true);
            }
            else
            {
                this.MensajeError("Debe seleccionar el registro a modificar");
                this.ErrorIcono.SetError(this.txtIdPresentacion, "Debe seleccionar un registro");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.IsNuevo = false;
            this.IsEditar = false;
            this.Botones();
            this.Limpiar();
            this.Habilitar(false);
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

        private void dataListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == dataListado.Columns["Eliminar"].Index)
            {
                DataGridViewCheckBoxCell ChkEliminar =
                    (DataGridViewCheckBoxCell)dataListado.Rows[e.RowIndex].Cells["Eliminar"];

                ChkEliminar.Value = !Convert.ToBoolean(ChkEliminar.Value);
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
                            rpta = NPresentacion.Eliminar(Convert.ToInt32(Codigo));

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
    }
}
