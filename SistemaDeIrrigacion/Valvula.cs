using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaDeIrrigacion
{
    public class Valvula : Node
    {
        public override void Flow(int i)
        {

            if(Directions[i] == "L")
            {
                Left.WaterInput += WaterInput;
                WaterInput = 0;
                Left.Flow(i);
            }
            else
            {
                Right.WaterInput += WaterInput;
                WaterInput = 0;
                Right.Flow(i);
            }
        }



    }
}
