using Microsoft.Data.SqlClient;
using ProyectoPrograII.DatosAcceso;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoPrograII.Servicios
{
    internal class Utilidades
    {

        public static int menu_inicio()
        {
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("---------------------Control de Tolbas--------------------");
            Console.WriteLine();
            Console.WriteLine("----- 1.Gestion de producción (Calculo Grados brix) ------");
            Console.WriteLine();
            Console.WriteLine("----- 2.Informe  ------");
            Console.WriteLine();
            Console.WriteLine("----- 3.Salir ------------------");
            Console.WriteLine();
            Console.WriteLine("----------------------------------------------------------");
            Console.Write("Digite una opción: ");
            int opcion;

            if (!int.TryParse(Console.ReadLine(), out opcion))
            {
                opcion = 0;
            }

            return opcion;
        }

        public static int menu_produccion()
        {
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("----- 1.Clasificación en tiempo real ------");
            Console.WriteLine();
            Console.WriteLine("----- 2.Ajustar Rangos del calculo grados brix  ------");
            Console.WriteLine();
            Console.WriteLine("----- 3.Salir ------------------");
            Console.WriteLine();
            Console.WriteLine("----------------------------------------------------------");
            Console.Write("Digite una opción: ");
            int opcion;

            if (!int.TryParse(Console.ReadLine(), out opcion))
            {
                opcion = 0;
            }

            return opcion;
        }

        public static bool Acceso(string usuario, string contraseña)
        {

            using (SqlConnection con = Conexion.GetConexion())
            {
                if (con.State == ConnectionState.Closed)
                {
                    con.Open(); 
                }


                string query = @"SELECT COUNT(*) 
                             FROM inicio_sesion 
                             WHERE nombreUsuario = @usuario AND contraseña = contraseña";

                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@usuario", usuario);
                    cmd.Parameters.AddWithValue("@contraseña", contraseña);

                    int existe = (int)cmd.ExecuteScalar();
                    if (existe > 0)
                    {
                        
                        return true; 
                    }
                    else
                    {
                        return false; 
                    }
                }


            }
         
        }

    }

}
