using System;
using System.Collections.Generic;
using System.Numerics;

namespace _3D_Graphing
{
    public static class FunctionManager // Singleton that takes a function and some other info, then returns what points need to be drawn.
    {
        public static Vector3[][] KeyPoints(string functions, float minX, float maxX, float minY, float maxY, float step)
        {
            if (step <= 0 || minX > maxX || minY > maxY) { throw new FormatException(); }
            List<Vector3[]> pointArrays = new List<Vector3[]>();
            foreach (string function in functions.Split(','))
            {
                string formattedFunction = MathParser.ConvertToRPN(function);
                for (float x = minX; x < maxX; x += step)
                {
                    int i = 0;
                    for (float y = minY; y < maxY || ++i % 2 == 1; y += step)
                    {
                        pointArrays.Add(new Vector3[4] // These 4 points define the created polygon, so they're grouped together.
                        {
                            new Vector3(x + step / 2, y + step / 2, -MathParser.Evaluate(x + step / 2, y + step / 2, formattedFunction)),
                            new Vector3(x - step / 2, y + step / 2, -MathParser.Evaluate(x - step / 2, y + step / 2, formattedFunction)),
                            new Vector3(x - step / 2, y - step / 2, -MathParser.Evaluate(x - step / 2, y - step / 2, formattedFunction)),
                            new Vector3(x + step / 2, y - step / 2, -MathParser.Evaluate(x + step / 2, y - step / 2, formattedFunction))
                        });
                    }
                }
            }
            return pointArrays.ToArray();
        }
    }

}
