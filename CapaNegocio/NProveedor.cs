using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using CapaDatos;

namespace CapaNegocio
{
    public class NProveedor
    {
        public static string Insertar(string razon_social, string sector_comercial, string tipo_documento, string num_documento,
            string direccion, string telefono, string email, string url)
        {
            DProveedor proveedor = new DProveedor();
            proveedor.Razon_Social = razon_social;
            proveedor.Sector_Comercial = sector_comercial;
            proveedor.Tipo_Documento = tipo_documento;
            proveedor.Num_Documento = num_documento;
            proveedor.Direccion = direccion;
            proveedor.Telefono = telefono;
            proveedor.Email = email;
            proveedor.Url = url;

            return proveedor.Insertar(proveedor);
        }

        public static string Modificar(int idproveedor, string razon_social, string sector_comercial, string tipo_documento, string num_documento,
            string direccion, string telefono, string email, string url)
        {
            DProveedor proveedor = new DProveedor();
            proveedor.IdProveedor = idproveedor;
            proveedor.Razon_Social = razon_social;
            proveedor.Sector_Comercial = sector_comercial;
            proveedor.Tipo_Documento = tipo_documento;
            proveedor.Num_Documento = num_documento;
            proveedor.Direccion = direccion;
            proveedor.Telefono = telefono;
            proveedor.Email = email;
            proveedor.Url = url;

            return proveedor.Modificar(proveedor);
        }

        public static string Eliminar(int idproveedor)
        {
            DProveedor proveedor = new DProveedor();
            proveedor.IdProveedor = idproveedor;

            return proveedor.Eliminar(proveedor);
        }


        public static DataTable Consultar()
        {
            return new DProveedor().Consultar();
        }

        public static DataTable ConsultarRazonSocial(string textobuscar)
        {
            DProveedor proveedor = new DProveedor();
            proveedor.TextoBuscar = textobuscar;

            return proveedor.ConsultarRazonSocial(proveedor);
        }

        public static DataTable ConsultarDocumento(string textobuscar)
        {
            DProveedor proveedor = new DProveedor();
            proveedor.TextoBuscar = textobuscar;

            return proveedor.ConsultarDocumento(proveedor);
        }

    }
}
