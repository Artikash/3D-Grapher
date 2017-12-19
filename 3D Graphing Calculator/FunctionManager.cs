using System;
using System.Collections.Generic;
using System.Numerics;

namespace _3D_Graphing
{
    public static class FunctionManager // Singleton that takes a function and some other info, then returns what points need to be drawn.
    {
        public static Vector3[][] KeyPoints(string functions, float minX, float maxX, float minY, float maxY, float step)
        {
            if (step <= 0) { throw new FormatException(); }
            List<Vector3[]> pointArrays = new List<Vector3[]>();
            foreach (string function in functions.Split(','))
            {
                string formattedFunction = MathParser.ConvertToRPN(function);
                for (float x = minX; x < maxX; x += step)
                {
                    for (float y = minY; y < maxY; y += step)
                    {
                        pointArrays.Add(new Vector3[4] // These 4 points need lines between them, so they're grouped together.
                         {
                        new Vector3(x + step, y, -MathParser.Evaluate(x + step, y,formattedFunction)),
                        new Vector3(x - step, y, -MathParser.Evaluate(x - step, y,formattedFunction)),
                        new Vector3(x, y + step, -MathParser.Evaluate(x, y + step,formattedFunction)),
                        new Vector3(x, y - step, -MathParser.Evaluate(x, y - step,formattedFunction))
                         }
                         );
                    }
                }
            }
            return pointArrays.ToArray(); 
        }
    }

}
