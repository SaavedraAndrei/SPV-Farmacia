﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CapaPresentacion
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
            lblHora.Text = DateTime.Now.ToString();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            lblHora.Text = DateTime.Now.ToString();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            DataTable datos = CapaNegocio.NTrabajador.Login(this.txtUsuario.Text.Trim(),
                this.txtPassword.Text.Trim());

            if (datos.Rows.Count == 0)
            {
                MessageBox.Show("No tiene Acceso al Sistema", "Sistema de Ventas", MessageBoxButtons.OK
                    , MessageBoxIcon.Error);
            }
            else
            {
                frmPrincipal frm = new frmPrincipal();
                frm.idTrabajador = datos.Rows[0][0].ToString();
                frm.Apellidos = datos.Rows[0][1].ToString();
                frm.Nombre = datos.Rows[0][2].ToString();
                frm.Acceso = datos.Rows[0][3].ToString();

                frm.Show();
                this.Hide();


            }
        }

        private void btnIngresar_Enter(object sender, EventArgs e)
        {
            DataTable datos = CapaNegocio.NTrabajador.Login(this.txtUsuario.Text.Trim(),
                this.txtPassword.Text.Trim());

            if (datos.Rows.Count == 0)
            {
                MessageBox.Show("No tiene Acceso al Sistema", "Sistema de Ventas", MessageBoxButtons.OK
                    , MessageBoxIcon.Error);
            }
            else
            {
                frmPrincipal frm = new frmPrincipal();
                frm.idTrabajador = datos.Rows[0][0].ToString();
                frm.Apellidos = datos.Rows[0][1].ToString();
                frm.Nombre = datos.Rows[0][2].ToString();
                frm.Acceso = datos.Rows[0][3].ToString();

                frm.Show();
                this.Hide();
            }
        }
    }
}
