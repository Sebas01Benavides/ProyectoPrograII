using Microsoft.Data.SqlClient;
using ProyectoPrograII.DatosAcceso;
using ProyectoPrograII.Entidades;
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
            Console.WriteLine("---------------------Control de Tolvas--------------------");
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
            Console.WriteLine("-------------------Gestion Producción---------------------");
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


        public static void EjecutarClasificacionTiempoReal()
        {
            Console.Clear();
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("-       CLASIFICACIÓN EN TIEMPO REAL - GRADOS BRIX       -");
            Console.WriteLine("----------------------------------------------------------\n");

            try
            {
                Console.Write("Ingrese el número de serie de la tolva: ");
                string identificadorTolva = Console.ReadLine();

                decimal[] muestras = new decimal[5];
                Console.WriteLine("\nIngrese las mediciones de las 5 muestras de piñas:");

                for (int i = 0; i < 5; i++)
                {
                    bool entradaValida = false;
                    while (!entradaValida)
                    {
                        Console.Write($"  Muestra {i + 1} (°Brix): ");
                        string entrada = Console.ReadLine();

                        if (decimal.TryParse(entrada, out decimal valor) && valor >= 0)
                        {
                            muestras[i] = valor;
                            entradaValida = true;
                        }
                        else
                        {
                            Console.WriteLine("  Entrada inválida. Ingrese un número válido mayor o igual a 0.");
                        }
                    }
                }

                MedicionBrix medicion = new MedicionBrix();
                bool exito = medicion.ProcesarMedicionCompleta(identificadorTolva, muestras);

                if (exito)
                {
                    medicion.MostrarResultados();
                    Console.WriteLine(" Medición procesada y guardada exitosamente.");
                }
                else
                {
                    Console.WriteLine(" Error al procesar la medición.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n✗ Error: {ex.Message}");
            }

            Console.WriteLine("\nPresione Enter para continuar...");
            Console.ReadLine();
        }
    }

}
