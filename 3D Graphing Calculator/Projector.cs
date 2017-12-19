using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace _3D_Graphing
{
    public static class Projector // Singleton that maps 3D to 2D for graphing
    {
        private static Quaternion rotation = Quaternion.Identity;
        private static float angle = 0;

        public static Vector3 Project(Vector3 point)
        {
            Vector3 transformedPoint = Vector3.Transform(point,rotation);        
            return new Vector3(0.866f * (-transformedPoint.X + transformedPoint.Y), transformedPoint.Z + 0.5f * (transformedPoint.X + transformedPoint.Y),transformedPoint.X + transformedPoint.Y);
        }

        public static void SetAngle(float newAngle)
        {
            angle = newAngle;
            rotation = Quaternion.CreateFromAxisAngle(new Vector3(0, 0, 1), newAngle);
        }

        public static float GetAngle()
        {
            return angle;
        }
    }
}
