using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Data;
using System.Data.SqlClient;

namespace CapaDatos
{
    public class DDetalle_Venta
    {

        private int _idDetalle_Venta;
        private int _idVenta;
        private int _idDetalle_Ingreso;
        private int _Cantidad;
        private decimal _Precio_Venta;
        private decimal _Descuento;

        public int IdDetalle_Venta { get => _idDetalle_Venta; set => _idDetalle_Venta = value; }
        public int IdVenta { get => _idVenta; set => _idVenta = value; }
        public int IdDetalle_Ingreso { get => _idDetalle_Ingreso; set => _idDetalle_Ingreso = value; }
        public int Cantidad { get => _Cantidad; set => _Cantidad = value; }
        public decimal Precio_Venta { get => _Precio_Venta; set => _Precio_Venta = value; }
        public decimal Descuento { get => _Descuento; set => _Descuento = value; }

        public DDetalle_Venta()
        {

        }

        public DDetalle_Venta(int iddetalle_venta, int idventa, int iddetalle_ingreso,
            int cantidad, decimal precio_venta, decimal descuento )
        {
            this.IdDetalle_Venta = iddetalle_venta;
            this.IdVenta = idventa;
            this.IdDetalle_Ingreso = iddetalle_ingreso;
            this.Cantidad = cantidad;
            this.Precio_Venta = precio_venta;
            this.Descuento = descuento;
        }

        public string Insertar(DDetalle_Venta detalle_venta, ref SqlConnection SqlCn,
            ref SqlTransaction SqlTra)
        {
            string rpta = string.Empty;
            try
            {


                SqlCommand cmd = new SqlCommand();
                cmd.Connection = SqlCn;
                cmd.Transaction = SqlTra;
                cmd.CommandText = "SP_A_TABLA_DETALLE_VENTA";
                cmd.CommandType = CommandType.StoredProcedure;

                SqlParameter paridDetalle_Venta = new SqlParameter();
                paridDetalle_Venta.ParameterName = "@iddetalle_venta";
                paridDetalle_Venta.SqlDbType = SqlDbType.Int;
                paridDetalle_Venta.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(paridDetalle_Venta);

                SqlParameter paridVenta = new SqlParameter();
                paridVenta.ParameterName = "@idventa";
                paridVenta.SqlDbType = SqlDbType.Int;
                paridVenta.Value = detalle_venta.IdVenta;
                cmd.Parameters.Add(paridVenta);

                SqlParameter parIdDetalle_Ingreso = new SqlParameter();
                parIdDetalle_Ingreso.ParameterName = "@iddetalle_ingreso";
                parIdDetalle_Ingreso.SqlDbType = SqlDbType.Int;
                parIdDetalle_Ingreso.Value = detalle_venta.IdDetalle_Ingreso;
                cmd.Parameters.Add(parIdDetalle_Ingreso);

                SqlParameter parCantidad = new SqlParameter();
                parCantidad.ParameterName = "@cantidad";
                parCantidad.SqlDbType = SqlDbType.Int;
                parCantidad.Value = detalle_venta.Cantidad;
                cmd.Parameters.Add(parCantidad);

                SqlParameter parPrecio_Venta = new SqlParameter();
                parPrecio_Venta.ParameterName = "@precio_venta";
                parPrecio_Venta.SqlDbType = SqlDbType.Money;
                parPrecio_Venta.Value = detalle_venta.Precio_Venta;
                cmd.Parameters.Add(parPrecio_Venta);


                SqlParameter parDescuento = new SqlParameter();
                parDescuento.ParameterName = "@descuento";
                parDescuento.SqlDbType = SqlDbType.Money;
                parDescuento.Value = detalle_venta.Descuento;
                cmd.Parameters.Add(parDescuento);

               


                rpta = cmd.ExecuteNonQuery() == 1 ? "OK" : "No se ingresó el registro";



            }
            catch (Exception ex)
            {
                rpta = ex.Message;

            }


            return rpta;
        }
    }
}
