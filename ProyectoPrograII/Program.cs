using ProyectoPrograII.DatosAcceso;
using ProyectoPrograII.Servicios;

namespace Proyecto_I
{
    internal class Program
    {
        static void Main(string[] args)
        {

            /* bool continuarSistema = true;

             while (continuarSistema)
             {
                 Console.WriteLine("Ingrese su Usuariossfgs:");
                 String usuario = Console.ReadLine();

                 Console.WriteLine("Ingrese su Contraseña:");
                 String contraseña = Console.ReadLine();

                 bool ingreso = Utilidades.Acceso(usuario, contraseña);

                 if (ingreso == true)
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
                                     Console.WriteLine("Control en tiempo real");
                                     Console.ReadKey();
                                 }
                                 else if (opcProduccion == 2)
                                 {
                                     Console.WriteLine("Ajuste de rangos");
                                     Console.ReadKey();
                                 }
                                 else
                                 {
                                     Console.Clear();
                                     break;
                                 }

                                 break;

                         }


                     }
                 }
             }

         */

            //-------------------------

            Console.WriteLine("-----Rangos de Parametros Brix ----");

            Console.Write("Ingrese el mínimo de brix para Exportación: ");
            decimal minExport = Convert.ToDecimal(Console.ReadLine());

            Console.Write("Ingrese el mínimo de brix para Jugo: ");
            decimal minJugo = Convert.ToDecimal(Console.ReadLine());

            bool resultado = GestionBrix.EstablecerRangos(minExport, minJugo);

            if (resultado)
            {
                Console.WriteLine("Los rangos se guardaron correctamente");
            }
            else
            {
                Console.WriteLine("Hubo un error al guardar los rangos.");
            }

            Console.WriteLine("Presione una tecla para salir...");
            Console.ReadKey();
        }
    }
}