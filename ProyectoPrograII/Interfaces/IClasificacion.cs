using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProyectoPrograII.Interfaces
{
    internal interface IClasificacion
    {
        decimal CalcularPromedioBrix(decimal[] muestras);
        string ClasificarDestino(decimal promedioBrix);
    }
}
