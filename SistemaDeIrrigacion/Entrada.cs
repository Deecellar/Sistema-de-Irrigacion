using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaDeIrrigacion
{
    public class Entrada : Node
    {
        public override void Flow(int i)
        {
            Left.WaterInput = WaterInput;
            Left.Flow(i);
        }
        public override void Reset()
        {
            Left?.Reset();
        }



    }
}
