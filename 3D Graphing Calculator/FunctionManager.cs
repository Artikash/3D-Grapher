using System;
using System.Collections.Generic;
using System.Numerics;

namespace _3D_Graphing
{
    public static class FunctionManager
    {
        public static Vector2[][] KeyPoints(string function, float minX, float maxX, float minY, float maxY, float step)
        {
            List<Vector2[]> pointArrays = new List<Vector2[]>();
            for (float x = minX; x < maxX; x += step)
            {
                for (float y = minY; y < maxY; y += step)
                {
                   pointArrays.Add(Projector.Project(new Vector3[4]
                    {
                        new Vector3(x + step, y, -MathParser.Evaluate(x + step, y,function )),
                        new Vector3(x - step, y, -MathParser.Evaluate(x - step, y,function)),
                        new Vector3(x, y + step, -MathParser.Evaluate(x, y + step,function)),
                        new Vector3(x, y - step, -MathParser.Evaluate(x, y - step,function))
                    }
                    ));
                }
            }
            return pointArrays.ToArray(); 
        }
    }

}
