using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using CapaDatos;

namespace CapaNegocio
{
    public class NVenta
    {
        public static string Insertar(int idcliente,int idtrabajador, DateTime fecha, string tipo_comprobante
           , string serie, string correlativo, decimal igv, DataTable dtDetalles)
        {
            DVenta venta = new DVenta();
            venta.IdCliente =idcliente;
            venta.IdTrabajador= idtrabajador;
            venta.Fecha = fecha;
            venta.Tipo_Comprobante = tipo_comprobante;
            venta.Serie = serie;
            venta.Correlativo = correlativo;
            venta.Igv = igv;
            List<DDetalle_Venta> detalles = new List<DDetalle_Venta>();

            foreach (DataRow row in dtDetalles.Rows)
            {
                DDetalle_Venta detalle = new DDetalle_Venta();
                detalle.IdDetalle_Ingreso = Convert.ToInt32(row["idDetalle_Ingreso"].ToString());
                detalle.Cantidad = Convert.ToInt32(row["Cantidad"].ToString());
                detalle.Precio_Venta = Convert.ToDecimal(row["Precio_Venta"].ToString());
                detalle.Descuento = Convert.ToInt32(row["Descuento"].ToString());
                detalles.Add(detalle);

            }

            return venta.Insertar(venta, detalles);
        }

        public static string Eliminar(int idventa)
        {
            DVenta venta = new DVenta();
            venta.IdVenta = idventa;

            return venta.Eliminar(venta);



        }
        public static DataTable Consultar()
        {
            return new DVenta().Consultar();
        }

        public static DataTable ConsultarFechas(string textobuscar1, string textobuscar2)
        {
            DVenta venta = new DVenta();
            return venta.ConsultarFechas(textobuscar1, textobuscar2);
        }

        public static DataTable ConsultarDetalle_Venta(string textobuscar)
        {
            DVenta venta = new DVenta();

            return venta.ConsultarDetalle_Venta(textobuscar);
        }

        public static DataTable ConsultarArticulo_Venta_Nombre(string textobuscar)
        {
            DVenta venta = new DVenta();

            return venta.ConsultarArticulo_Venta_Nombre(textobuscar);
        }

        public static DataTable ConsultarArticulo_Venta_Codigo(string textobuscar)
        {
            DVenta venta = new DVenta();

            return venta.ConsultarArticulo_Venta_Codigo(textobuscar);
        }



    }
}
