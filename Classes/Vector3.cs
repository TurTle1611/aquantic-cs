using System;

namespace ZBase.Utilities
{
    public class Vector3
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public Vector3(float x = 0, float y = 0, float z = 0)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public static Vector3 operator -(Vector3 a, Vector3 b)
        {
            return new Vector3(a.X - b.X, a.Y - b.Y, a.Z - b.Z);
        }

        public static Vector3 operator +(Vector3 a, Vector3 b)
        {
            return new Vector3(a.X + b.X, a.Y + b.Y, a.Z + b.Z);
        }

        public static Vector3 operator /(Vector3 a, float factor)
        {
            return new Vector3(a.X / factor, a.Y / factor, a.Z / factor);
        }

        public static Vector3 operator *(Vector3 a, float factor)
        {
            return new Vector3(a.X * factor, a.Y * factor, a.Z * factor);
        }

        public Vector3 ToAngle()
        {
            return new Vector3(
                (float)(Math.Atan2(-Z, Math.Sqrt(X * X + Y * Y)) * (180.0f / Math.PI)),
                (float)(Math.Atan2(Y, X) * (180.0f / Math.PI)),
                0.0f
            );
        }

        public bool IsZero()
        {
            return X == 0 && Y == 0 && Z == 0;
        }
    }
}