using ProyectoPrograII;

namespace Proyecto_I
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Conexion.VerTabla("productosAgricolas"); //Nombre de la tabla
            //Console.ReadLine();


            Console.WriteLine("=== PRUEBA GENERACIÓN DE CARNET ===\n");

            Contenedor c1 = new Contenedor("", "Atlantico");
            Console.WriteLine($"Contenedor: {c1.Carnet} | Nombre: {c1.Nombre}");

            Contenedor c2 = new Contenedor("", "Pacifico");
            Console.WriteLine($"Contenedor: {c2.Carnet} | Nombre: {c2.Nombre}");

            Contenedor c3 = new Contenedor("", "Oceania");
            Console.WriteLine($"Contenedor: {c3.Carnet} | Nombre: {c3.Nombre}");

            Console.ReadKey();
        }


    }
}
