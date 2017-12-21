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
        private static Quaternion thetaRotation = Quaternion.Identity;
        private static Quaternion phiRotation = Quaternion.Identity;
        private static float theta = 0;
        private static float phi = 0;

        public static Vector3 Project(Vector3 point)
        {
            Vector3 transformedPoint = Vector3.Transform(Vector3.Transform(point,thetaRotation),phiRotation);
            return new Vector3(0.866f * (-transformedPoint.X + transformedPoint.Y), transformedPoint.Z + 0.5f * (transformedPoint.X + transformedPoint.Y),transformedPoint.X + transformedPoint.Y);
        }

        public static void SetAngle(float newTheta, float newPhi)
        {
            theta = newTheta;
            phi = newPhi;
            thetaRotation = Quaternion.CreateFromAxisAngle(new Vector3(0, 0, 1), theta);
            phiRotation = Quaternion.CreateFromAxisAngle(new Vector3(-0.707f, 0.707f, 0), phi);
        }

        public static float GetTheta()
        {
            return theta;
        }

        public static float GetPhi()
        {
            return phi;
        }
    }
}
