using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class frmPrincipal : Form
    {
        private int childFormNumber = 0;
        public string idTrabajador = "";
        public string Apellidos = "";
        public string Nombre = "";
        public string Acceso = "";


        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            Form childForm = new Form();
            childForm.MdiParent = this;
            childForm.Text = "Window " + childFormNumber++;
            childForm.Show();
        }

        private void OpenFile(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            openFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (openFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = openFileDialog.FileName;
            }
        }

        private void SaveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            saveFileDialog.Filter = "Text Files (*.txt)|*.txt|All Files (*.*)|*.*";
            if (saveFileDialog.ShowDialog(this) == DialogResult.OK)
            {
                string FileName = saveFileDialog.FileName;
            }
        }

        private void ExitToolsStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CutToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void CopyToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void PasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void ToolBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            toolStrip.Visible = toolBarToolStripMenuItem.Checked;
        }

        private void StatusBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            statusStrip.Visible = statusBarToolStripMenuItem.Checked;
        }

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        private void ArrangeIconsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.ArrangeIcons);
        }

        private void CloseAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }
        }

        private void almacénToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void optionsToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void salirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void categoríasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCategoria frm = new frmCategoria();
            frm.MdiParent = this;
            frm.Show();
        }

        private void presentacionesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmPresentacion frm = new frmPresentacion();
            frm.MdiParent = this;
            frm.Show();
        }

        private void artículosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //var _GetInstancia = new frmArticulo();

            //if (true)
            //{

            //}
            
            frmArticulo frm = frmArticulo.GetInstancia();
            frm.MdiParent = this;
            frm.Show();

        }

        private void proveedorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmProveedor frm = new frmProveedor();
            frm.MdiParent = this;
            frm.Show();
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmCliente frm = new frmCliente();
            frm.MdiParent = this;
            frm.Show();
        }

        private void trabajadoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTrabajador frm = new frmTrabajador();
            frm.MdiParent = this;
            frm.Show();
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            GestionUsuario();
        }

        private void GestionUsuario()
        {
            //Controlar los accesos
            if (Acceso == "ADMINISTRADOR")
            {
                this.MnuAlmacen.Enabled = true;
                this.MnuCompras.Enabled = true;
                this.MnuVentas.Enabled = true;
                this.MnuMantenimiento.Enabled = true;
                this.MnuConsultaReporte.Enabled = true;
                this.MnuTools.Enabled = true;
                this.tsCompras.Enabled = true;
                this.tsVentas.Enabled = true;
            }
            else if (Acceso == "VENDEDOR")
            {
                this.MnuAlmacen.Enabled = false;
                this.MnuCompras.Enabled = false;
                this.MnuVentas.Enabled = true;
                this.MnuMantenimiento.Enabled = false;
                this.MnuConsultaReporte.Enabled = true;
                this.MnuTools.Enabled = true;
                this.tsCompras.Enabled = true;
                this.tsVentas.Enabled = false;
            }
            else if (Acceso == "ALMACENERO")
            {
                this.MnuAlmacen.Enabled = true;
                this.MnuCompras.Enabled = true;
                this.MnuVentas.Enabled = false;
                this.MnuMantenimiento.Enabled = false;
                this.MnuConsultaReporte.Enabled = true;
                this.MnuTools.Enabled = true;
                this.tsCompras.Enabled = true;
                this.tsVentas.Enabled = false;
            }
            else
            {
                this.MnuAlmacen.Enabled = false;
                this.MnuCompras.Enabled = false;
                this.MnuVentas.Enabled = false;
                this.MnuMantenimiento.Enabled = false;
                this.MnuConsultaReporte.Enabled = false;
                this.MnuTools.Enabled = false;
                this.tsCompras.Enabled = false;
                this.tsVentas.Enabled = false;
            }
        }

        private void ingresosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmIngreso frm = frmIngreso.GetInstancia();
            frm.MdiParent = this;
            frm.Show();

            frm.IdTrabajador = Convert.ToInt32(this.idTrabajador);


        }

        private void ventasToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            frmVenta frm = frmVenta.GetInstancia();
            frm.MdiParent = this;
            frm.Show();
            frm.IdTrabajador = Convert.ToInt32(this.idTrabajador);
        }
    }
}
