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

        public static Vector2[] Project(Vector3[] points)
        {
            List<Vector2> projectedPoints = new List<Vector2>();
            foreach (Vector3 point in points)
            {
                Vector3 transformedPoint = Vector3.Transform(point,rotation);
                projectedPoints.Add(new Vector2(0.866f * (-transformedPoint.X + transformedPoint.Y), transformedPoint.Z + 0.5f * (transformedPoint.X + transformedPoint.Y)));
            }
            return projectedPoints.ToArray();
        }
    }
}
