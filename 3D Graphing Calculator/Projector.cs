using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace _3D_Graphing
{
    public static class Projector
    {
        public static Quaternion rotation = Quaternion.Identity;

        public static Vector2 Project(Vector3 point)
        {
            Vector3 transformedPoint = Vector3.Transform(point,rotation);        
            return new Vector2(0.866f * (-transformedPoint.X + transformedPoint.Y), transformedPoint.Z + 0.5f * (transformedPoint.X + transformedPoint.Y));
        }
    }
}
