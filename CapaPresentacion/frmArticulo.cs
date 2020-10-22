using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using CapaNegocio;


namespace CapaPresentacion
{
    public partial class frmArticulo : Form
    {
        //Si se va a registrar
        private bool IsNuevo = false;

        //Si se va a editar
        private bool IsEditar = false;

        private static frmArticulo _Instancia;

        public static frmArticulo GetInstancia()
        {
            if (_Instancia == null)
            {
                _Instancia = new frmArticulo();
            }

            return _Instancia;
        }

        public void SetCategoria(string idcategoria, string nombre)
        {
            this.txtIdCategoria.Text = idcategoria;
            this.txtCategoria.Text = nombre;
        }


        public frmArticulo()
        {
            InitializeComponent();
            ttMensaje.SetToolTip(this.txtNombre, "Ingrese el Nombre del Artículo");
            ttMensaje.SetToolTip(this.pxImagen, "Selecciona la imagen del artículo");
            ttMensaje.SetToolTip(this.txtIdCategoria, "Selecciona la categoría del artículo");
            ttMensaje.SetToolTip(this.cbIdPresentacion, "Selecciona la presentación del artículo");

            this.txtIdCategoria.ReadOnly = true;
            this.txtCategoria.ReadOnly = true;
            this.ComboPresentacion();
            this.Consultar();
        }

        //Mostrar mensaje de Confirmación
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
            this.txtCodigoVenta.Text = string.Empty;
            this.txtNombre.Text = string.Empty;
            this.txtDescripcion.Text = string.Empty;
            this.txtIdCategoria.Text = string.Empty;
            this.txtCategoria.Text = string.Empty;
            //OTRA OPCIÓN PARA IMAGEN Y PARA INGRESAR UN IMAGEN CUANDO SE LIMPIA ES: 
            //this.pxImagen.Image = global::CapaPresentacion.Properties.Resources.file:
            //file es el nombre de la imagen
            this.pxImagen.Image = null;
            
        }

        //Habilitar los controles del formulario
        public void Habilitar(bool valor)
        {
            this.txtIdArticulo.ReadOnly = !valor;
            this.txtCodigoVenta.ReadOnly = !valor;
            this.txtNombre.ReadOnly = !valor;
            this.txtDescripcion.ReadOnly = !valor;
            this.btnBuscarCategoria.Enabled = valor;
            this.cbIdPresentacion.Enabled = valor;
            this.btnCargar.Enabled = valor;
            this.btnLimpiar.Enabled = valor;
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

            this.dataListado.Columns[6].Visible = false;
            this.dataListado.Columns[8].Visible = false;
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

        private void ComboPresentacion()
        {
            cbIdPresentacion.DataSource = NPresentacion.Consultar();
            cbIdPresentacion.ValueMember = "idPresentacion";
            cbIdPresentacion.DisplayMember = "Nombre";
        }


        private void pxImagen_Click(object sender, EventArgs e)
        {

        }

        private void frmArticulo_Load(object sender, EventArgs e)
        {
            this.CenterToScreen();
            this.Habilitar(false);
            this.Botones();
            this.Consultar();
        }

        private void btnCargar_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();

            DialogResult result = dialog.ShowDialog();

            if (result == DialogResult.OK)
            {
                this.pxImagen.SizeMode = PictureBoxSizeMode.StretchImage;
                this.pxImagen.Image = Image.FromFile(dialog.FileName);
            }
        }

        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            this.pxImagen.SizeMode = PictureBoxSizeMode.StretchImage;
            this.pxImagen.Image = null;
        }

        private void frmArticulo_Load_1(object sender, EventArgs e)
        {

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
            this.IsEditar = false;
            this.Botones();
            this.Limpiar();
            this.txtCodigoVenta.Focus();
            this.Habilitar(true);
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                string rpta = string.Empty;
                if (this.txtNombre.Text == string.Empty || this.txtIdCategoria.Text == string.Empty 
                    || this.txtCodigoVenta.Text == string.Empty)
                {
                    MensajeError("Falta ingresar algunos datos, serán remarcados");
                    this.ErrorIcono.SetError(txtNombre, "Ingrese un valor");
                    this.ErrorIcono.SetError(txtIdCategoria, "Ingrese un valor");
                    this.ErrorIcono.SetError(txtCodigoVenta, "Ingrese un valor");
                }
                else
                {
                    System.IO.MemoryStream ms = new System.IO.MemoryStream();
                    this.pxImagen.Image.Save(ms,System.Drawing.Imaging.ImageFormat.Png);

                    byte[] imagen = ms.GetBuffer();

                    if (this.IsNuevo)
                    {
                        rpta = NArticulo.Insertar(
                            this.txtCodigoVenta.Text.Trim().ToUpper(),
                            this.txtNombre.Text.Trim().ToUpper(),
                            this.txtDescripcion.Text.Trim().ToUpper(),
                            imagen,
                            Convert.ToInt32(this.txtIdCategoria.Text.Trim().ToUpper()),
                            Convert.ToInt32(this.cbIdPresentacion.SelectedValue));
                    }
                    else if (this.IsEditar)
                    {
                        rpta = NArticulo.Modificar(Convert.ToInt32(this.txtIdArticulo.Text.Trim()),
                            this.txtCodigoVenta.Text.Trim().ToUpper(),
                            this.txtNombre.Text.Trim().ToUpper(),
                            this.txtDescripcion.Text.Trim().ToUpper(),
                            imagen,
                            Convert.ToInt32(this.txtIdCategoria.Text.Trim().ToUpper()),
                            Convert.ToInt32(this.cbIdPresentacion.SelectedValue));


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
            if (!txtIdArticulo.Text.Equals(""))
            {
                this.IsEditar = true;
                this.Botones();
                this.Habilitar(true);
            }
            else
            {
                this.MensajeError("Debe seleccionar el registro a modificar");
                this.ErrorIcono.SetError(this.txtIdCategoria, "Debe seleccionar un registro");
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

        private void dataListado_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //PARA PODER MARCAR LOS CHECKBOX DE LA COLUMNA
            if (e.ColumnIndex == dataListado.Columns["Eliminar"].Index)
            {
                DataGridViewCheckBoxCell ChkEliminar =
                    (DataGridViewCheckBoxCell)dataListado.Rows[e.RowIndex].Cells["Eliminar"];

                ChkEliminar.Value = !Convert.ToBoolean(ChkEliminar.Value);
            }
        }

        private void dataListado_DoubleClick(object sender, EventArgs e)
        {
            this.txtIdArticulo.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["idArticulo"].Value);
            this.txtCodigoVenta.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["Codigo"].Value);
            this.txtNombre.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["Nombre"].Value);
            this.txtDescripcion.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["Descripcion"].Value);

            byte[] imagenBuffer = (byte[])this.dataListado.CurrentRow.Cells["Imagen"].Value;
            System.IO.MemoryStream ms = new System.IO.MemoryStream(imagenBuffer);

            this.pxImagen.Image = Image.FromStream(ms);
            this.pxImagen.SizeMode = PictureBoxSizeMode.StretchImage;

            this.txtIdCategoria.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["idCategoria"].Value);
            this.txtCategoria.Text = Convert.ToString(this.dataListado.CurrentRow.Cells["Categoria"].Value);
            this.cbIdPresentacion.SelectedValue = Convert.ToString(this.dataListado.CurrentRow.Cells["idPresentacion"].Value);

            this.tabControl1.SelectedIndex = 1;
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
                            rpta = NArticulo.Eliminar(Convert.ToInt32(Codigo));

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

        private void btnBuscarCategoria_Click(object sender, EventArgs e)
        {
            frmVistaCategoria formulario = new frmVistaCategoria();
            formulario.ShowDialog();
        }
    }


}
