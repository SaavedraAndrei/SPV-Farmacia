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

    public partial class frmTrabajador : Form
    {
        //Si se va a registrar
        private bool IsNuevo = false;

        //Si se va a editar
        private bool IsEditar = false;


        public frmTrabajador()
        {
            InitializeComponent();
            ttMensaje.SetToolTip(this.txtNombre, "Ingrese el nombre del trabajador");
            ttMensaje.SetToolTip(this.txtApellidos, "Ingrese el apellido del trabajador");
            ttMensaje.SetToolTip(this.txtNumDocumento, "Ingrese el número de documento del trabajador");
            ttMensaje.SetToolTip(this.txtUsuario, "Ingrese el usuario del trabajador");
            ttMensaje.SetToolTip(this.txtPassword, "Ingrese el password del trabajador");
            ttMensaje.SetToolTip(this.cbAcceso, "Seleccione el nivel de acceso del trabajador");

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
            this.txtIdTrabajador.Text = string.Empty;
            this.txtNombre.Text = string.Empty;
            this.txtApellidos.Text = string.Empty;
            this.txtNumDocumento.Text = string.Empty;
            this.txtDireccion.Text = string.Empty;
            this.txtTelefono.Text = string.Empty;
            this.txtEmail.Text = string.Empty;
            this.txtUsuario.Text = string.Empty;
            this.txtPassword.Text = string.Empty;
        }

        //Habilitar los controles del formulario
        public void Habilitar(bool valor)
        {

            this.txtIdTrabajador.ReadOnly = !valor;
            this.txtNombre.ReadOnly = !valor;
            this.txtApellidos.ReadOnly = !valor;
            this.txtNumDocumento.ReadOnly = !valor;
            this.txtDireccion.ReadOnly = !valor;
            this.txtTelefono.ReadOnly = !valor;
            this.txtEmail.ReadOnly = !valor;
            this.cbAcceso.Enabled = valor;
            this.cbSexo.Enabled = valor;
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
            this.dataListado.DataSource = NTrabajador.Consultar();
            this.OcultarColumnas();
            lblTotal.Text = "Total de Registros: " + Convert.ToString(this.dataListado.Rows.Count);
        }


        //Método Consultar por Nombre
        public void ConsultarApellidos()
        {
            this.dataListado.DataSource = NTrabajador.ConsultarApellidos(this.txtBuscar.Text);
            this.OcultarColumnas();
            lblTotal.Text = "Total de Registros: " + Convert.ToString(this.dataListado.Rows.Count);

        }


        public void ConsultarDocumento()
        {
            this.dataListado.DataSource = NTrabajador.ConsultarDocumento(this.txtBuscar.Text);
            this.OcultarColumnas();
            lblTotal.Text = "Total de Registros: " + Convert.ToString(this.dataListado.Rows.Count);

        }

        private void frmTrabajador_Load(object sender, EventArgs e)
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
                case "Apellidos":
                    ConsultarApellidos();
                    break;
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult opcion;
                opcion = MessageBox.Show("Realmente Desea Eliminar los Registros?", "Sistema de Ventas",
                    MessageBoxButtons.OKCancel
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
                            rpta = NTrabajador.Eliminar(Convert.ToInt32(Codigo));

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
            this.txtIdTrabajador.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["idTrabajador"].Value);
            this.txtNombre.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["Nombre"].Value);
            this.txtApellidos.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["Apellidos"].Value);
            this.cbSexo.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["Sexo"].Value);
            this.dtFechaNacimiento.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["Fecha_Nac"].Value);
            this.txtNumDocumento.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["Num_Documento"].Value);
            this.txtDireccion.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["Direccion"].Value);
            this.txtTelefono.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["Telefono"].Value);
            this.txtEmail.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["Email"].Value);
            this.cbAcceso.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["Acceso"].Value);
            this.txtUsuario.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["Usuario"].Value);
            this.txtPassword.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["Password"].Value);


            this.tabControl1.SelectedIndex = 1;
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string rpta = string.Empty;
                if (this.txtNombre.Text == string.Empty)
                {
                    MensajeError("Falta ingresar algunos datos, serán remarcados");
                    this.ErrorIcono.SetError(txtNombre, "Ingrese un Nombre");
                    this.ErrorIcono.SetError(txtApellidos, "Ingrese un Nombre");
                }
                else
                {
                    if (this.IsNuevo)
                    {
                        rpta = NTrabajador.Insertar(
                            this.txtNombre.Text.Trim().ToUpper(),
                            this.txtApellidos.Text.Trim().ToUpper(),
                            this.cbSexo.Text.Trim().ToUpper(),
                            (dtFechaNacimiento.Value),
                            this.txtNumDocumento.Text.Trim().ToUpper(),
                            this.txtDireccion.Text.Trim().ToUpper(),
                            this.txtTelefono.Text.Trim().ToUpper(),
                            this.txtEmail.Text.Trim().ToUpper(),
                            this.cbAcceso.Text.Trim().ToUpper(),
                            this.txtUsuario.Text.Trim().ToUpper(),
                            this.txtPassword.Text.Trim().ToUpper()

                            );
                    }
                    else if (this.IsEditar)
                    {
                        rpta = NTrabajador.Modificar(Convert.ToInt32(this.txtIdTrabajador.Text.Trim()),
                            this.txtNombre.Text.Trim().ToUpper(),
                            this.txtApellidos.Text.Trim().ToUpper(),
                            this.cbSexo.Text.Trim().ToUpper(),
                            (dtFechaNacimiento.Value),
                            this.txtNumDocumento.Text.Trim().ToUpper(),
                            this.txtDireccion.Text.Trim().ToUpper(),
                            this.txtTelefono.Text.Trim().ToUpper(),
                            this.txtEmail.Text.Trim().ToUpper(),
                            this.cbAcceso.Text.Trim().ToUpper(),
                            this.txtUsuario.Text.Trim().ToUpper(),
                            this.txtPassword.Text.Trim().ToUpper()
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

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            this.IsNuevo = true;
            this.Botones();
            this.Limpiar();
            this.txtNombre.Focus();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (!txtIdTrabajador.Text.Equals(""))
            {
                this.IsEditar = true;
                this.Botones();
                this.Habilitar(true);
            }
            else
            {
                this.MensajeError("Debe seleccionar el registro a modificar");
                this.ErrorIcono.SetError(this.txtIdTrabajador, "Debe seleccionar un registro");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.IsNuevo = false;
            this.IsEditar = false;
            this.Botones();
            this.Limpiar();
            this.Habilitar(false);
            this.txtIdTrabajador.Text = string.Empty;
        }
    }
}
