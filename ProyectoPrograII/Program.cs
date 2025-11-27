using Microsoft.Data.SqlClient;
using Microsoft.Identity.Client;
using ProyectoPrograII.DatosAcceso;
using ProyectoPrograII.Servicios;
using System;
using System.ComponentModel;

namespace Proyecto_I
{
    internal class Program
    {
        static void Main(string[] args)
        {
            bool continuarSistema = true;

            while (continuarSistema)
            {
                Console.Clear();
                Utilidades.EscribirConColor("\n----SISTEMA DE CLASIFICACIÓN POR TOLVA---", ConsoleColor.DarkCyan);

                Console.Write("\nIngrese su Usuario: ");
                string usuario = Console.ReadLine();
                Console.Write("Ingrese su Contraseña: ");
                string contraseña = Console.ReadLine();

                bool ingreso = Utilidades.Acceso(usuario, contraseña);

                if (ingreso)
                {
                    bool mostrarMenuPrincipal = true;

                    while (mostrarMenuPrincipal)
                    {
                        Console.Clear();
                        int opcion = Utilidades.menu_inicio();

                        switch (opcion)
                        {
                            case 1: 
                                Console.Clear();
                                int opcProduccion = Utilidades.menu_produccion();

                                if (opcProduccion == 1)
                                {
                                    bool continuar = true;

                                    while (continuar)
                                    {
                                        Utilidades.EjecutarClasificacionTiempoReal();

                                        Utilidades.EscribirConColor("\n¿Desea continuar con una siguiente tolva?",ConsoleColor.DarkCyan);
                                        Console.WriteLine("\n1 - Si");
                                        Console.WriteLine("2 - No");
                                        Console.Write("Opción: ");
                                        string entrada = Console.ReadLine();

                                        if (entrada == "1")
                                        {
                                            continuar = true;
                                        }
                                        else if (entrada == "2")
                                        {
                                            continuar = false;
                                        }
                                        else
                                        {
                                            Console.WriteLine("Opción no válida. Intente nuevamente.");
                                        }
                                    }

                                    Console.WriteLine("\nPresione cualquier tecla para continuar...");
                                    Console.ReadKey();
                                }
                                else if (opcProduccion == 2)
                                {
                                    Utilidades.RangosBrix();
                                    Console.WriteLine("\nPresione cualquier tecla para continuar...");
                                    Console.ReadKey();
                                }
                                else
                                {
                                    
                                    Console.WriteLine("Regresando al menú principal...");
                                }
                                break;

                            case 2:
                                Utilidades.EscribirConColor("SEBASTIAN PENDIENTE DE DESAROLLAR INFORMES",ConsoleColor.Red);
                                Console.WriteLine("\nPresione cualquier tecla para continuar...");
                                Console.ReadKey();
                                break;


                            case 3: 
                                mostrarMenuPrincipal = false;
                                continuarSistema = false;
                                Console.WriteLine("Saliendo del sistema...");
                                break;

                            default:
                                Console.WriteLine("Opción no válida.");
                                Console.ReadKey();
                                break;
                        }
                    }
                }
                else
                {
                    Console.WriteLine("\n Usuario o contraseña incorrectos.");
                    Console.WriteLine("Presione cualquier tecla para intentar nuevamente...");
                    Console.ReadKey();
                }
            }

            Utilidades.EscribirConColor("\nGracias por usar el sistema. ¡Hasta pronto <3 !", ConsoleColor.DarkGray);
        }
    }
}

           

