using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using CapaDatos;
using System.Data;
using System.Runtime.CompilerServices;

namespace CapaNegocio
{
    public class NIngreso
    {
        public static string Insertar(int idtrabajador, int idproveedor, DateTime fecha, string tipo_comprobante
            , string serie, string correlativo, decimal igv, string estado, DataTable dtDetalles)
        {
            DIngreso ingreso = new DIngreso();
            ingreso.IdTrabajador = idtrabajador;
            ingreso.IdProveedor = idproveedor;
            ingreso.Fecha = fecha;
            ingreso.Tipo_Comprobante = tipo_comprobante;
            ingreso.Serie = serie;
            ingreso.Correlativo = correlativo;
            ingreso.Igv = igv;
            ingreso.Estado = estado;
            List<DDetalle_Ingreso> detalles = new List<DDetalle_Ingreso>();

            foreach(DataRow row in dtDetalles.Rows)
            {
                DDetalle_Ingreso detalle = new DDetalle_Ingreso();
                detalle.IdArticulo = Convert.ToInt32(row["idArticulo"].ToString());
                detalle.Precio_Compra = Convert.ToDecimal(row["Precio_Compra"].ToString());
                detalle.Precio_Venta = Convert.ToDecimal(row["Precio_Venta"].ToString());
                detalle.Stock_Inicial = Convert.ToInt32(row["Stock_Inicial"].ToString());
                detalle.Stock_Actual = Convert.ToInt32(row["Stock_Inicial"].ToString());
                detalle.Fecha_Produccion = Convert.ToDateTime(row["Fecha_Produccion"].ToString());
                detalle.Fecha_Vencimiento = Convert.ToDateTime(row["Fecha_Vencimiento"].ToString());
                detalles.Add(detalle);

            }

            return ingreso.Insertar(ingreso,detalles);
        }
 
        public static string Anular(int idingreso)
        {
            DIngreso ingreso= new DIngreso();
            ingreso.IdIngreso= idingreso;

            return ingreso.Anular(ingreso);
        }


        public static DataTable Consultar()
        {
            return new DIngreso().Consultar();
        }

        public static DataTable ConsultarFechas(string textobuscar1, string textobuscar2)
        {
            DIngreso ingreso = new DIngreso();
            return ingreso.ConsultarFechas(textobuscar1,textobuscar2);
        }

        public static DataTable ConsultarDetalle_Ingreso(string textobuscar)
        {
            DIngreso ingreso = new DIngreso();

            return ingreso.ConsultarDetalle_Ingreso(textobuscar);
        }

    }
}
