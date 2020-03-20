using System;
using System.Collections.Generic;
using System.Text;

namespace SistemaDeIrrigacion
{
    public abstract class Node : INode
    {
        public INode Left { get; set; } = null;
        public INode Right { get; set; } = null;
        public string[] Directions { get; set; }
        public int WaterInput { get; set; } = 0;
        public string Name { get; set; } = "";

        public abstract void Flow(int i);
        public virtual void Reset()
        {
            WaterInput = 0;
            Left?.Reset();
            Right?.Reset();
        }
        public override String ToString()
        {
            return this.toString(new StringBuilder(), true, new StringBuilder()).ToString();
        }
 
        public StringBuilder toString(StringBuilder prefix, bool isTail, StringBuilder sb)
        {
            if (Right != null)
            {
                Right.toString(new StringBuilder().Append(prefix).Append(isTail ? "│   " : "    "), false, sb);
            }
            sb.Append(prefix).Append(isTail ? "└── " : "┌── ").Append(Name).Append("\n");
            if (Left != null)
            {
                Left.toString(new StringBuilder().Append(prefix).Append(isTail ? "    " : "│   "), true, sb);
            }
            return sb;
        }
    }
}
