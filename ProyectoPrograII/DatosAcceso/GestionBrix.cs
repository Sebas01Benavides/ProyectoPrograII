using Microsoft.Data.SqlClient;
using ProyectoPrograII.Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoPrograII.DatosAcceso
{
    internal class GestionBrix
    {
        // Método para establecer o actualizar los rangos de clasificación
        public static bool EstablecerRangos(decimal minExportacion, decimal minJugo)
        {
            try
            {
                SqlConnection conn = Conexion.GetConexion();

                // Validación para que tenga sentido los valores colocados
                if (minJugo >= minExportacion)
                {
                    Console.WriteLine("Error: El mínimo para Jugo debe ser menor que el mínimo para Exportación.");
                    return false;
                }

                // Borrar lo anterior
                string limpiar = "delete from ParametrosBrix";
                new SqlCommand(limpiar, conn).ExecuteNonQuery();

                // Exportación
                string q1 = @"insert into ParametrosBrix (rangoMinimo, rangoMaximo, destino)
                      values (@minE, 15.00, 'Exportacion')";

                SqlCommand c1 = new SqlCommand(q1, conn);
                c1.Parameters.AddWithValue("@minE", minExportacion);
                c1.ExecuteNonQuery();

                // Jugo
                string q2 = @"insert into ParametrosBrix (rangoMinimo, rangoMaximo, destino)
                      values (@minJ, @maxJ, 'Jugo')";

                SqlCommand c2 = new SqlCommand(q2, conn);
                c2.Parameters.AddWithValue("@minJ", minJugo);
                c2.Parameters.AddWithValue("@maxJ", minExportacion - 0.01m); //se resta eso para que no llegue al numero exacto, sino que quede en el decimal antes
                c2.ExecuteNonQuery();

                // Descarte
                string q3 = @"insert into ParametrosBrix (rangoMinimo, rangoMaximo, destino)
                      values (0.00, @maxD, 'Descarte')";

                SqlCommand c3 = new SqlCommand(q3, conn);
                c3.Parameters.AddWithValue("@maxD", minJugo - 0.01m);
                c3.ExecuteNonQuery();

                Console.WriteLine("Rangos establecidos correctamente.");
                return true;
            }
            catch (SqlException e)
            {
                Console.WriteLine("Error al establecer rangos: " + e.Message);
                return false;
            }
        }
    }
}
