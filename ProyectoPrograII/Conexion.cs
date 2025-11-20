using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace Proyecto_I
{
    internal class Conexion
    {
        public static SqlConnection GetConexion()
        {
            string cadenaConexion =
                "Server=localhost,1433;" +
                "Database=Upala_Agricola;" +
                "User Id=colaboradores;" +
                "Password=123;" +
                "Encrypt=True;" +
                "TrustServerCertificate=True;";

            try
            {
                SqlConnection conexion = new SqlConnection(cadenaConexion);
                conexion.Open();
                return conexion;
            }
            catch (SqlException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                return null;
            }
        }

        public static void VerTabla(string nombreTabla)
        {
            using (SqlConnection con = GetConexion())
            {
                string query = $"SELECT * FROM {nombreTabla}";

                SqlCommand cmd = new SqlCommand(query, con);
                SqlDataReader dr = cmd.ExecuteReader();

                Console.WriteLine($"\n--- TABLA: {nombreTabla} ---\n");

                while (dr.Read())
                {
                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        Console.Write($"{dr.GetName(i)}: {dr[i]}  |  ");
                    }
                    Console.WriteLine();
                }
            }
        }
    }
}
