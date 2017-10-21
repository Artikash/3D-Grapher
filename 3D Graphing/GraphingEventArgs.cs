using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _3D_Graphing
{
    public class GraphingEventArgs : EventArgs
    {
        public float angle;
        public string function;
        public float minX;
        public float maxX;
        public float minY;
        public float maxY;
        public float step;

        public GraphingEventArgs(float Angle, string Function, float X1, float X2, float Y1, float Y2, float Step)
        {
            angle = Angle;
            function = Function;
            minX = X1;
            maxX = X2;
            minY = Y1;
            maxY = Y2;
            step = Step;
        }
    }
}
