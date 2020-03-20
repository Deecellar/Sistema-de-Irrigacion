using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaDeIrrigacion
{
    public class Salida : Node
    {
        public int ID;
        public override void Flow(int i)
        {
        }

        public string GetString()
        {
            return $"Salida # {ID} : flujo {WaterInput} galones/min\n";
        }



    }
}
