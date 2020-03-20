using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaDeIrrigacion
{
    public interface INode
    {
        INode Left { get; set; }
        INode Right { get; set; }
        public string Name { get; set; }
        public StringBuilder toString(StringBuilder prefix, bool isTail, StringBuilder sb);

        string[] Directions { get; set; }
        int WaterInput { get; set; }
        void Flow(int i);
        void Reset();
    }
}
