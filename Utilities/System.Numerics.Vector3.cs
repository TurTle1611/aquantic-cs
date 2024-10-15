using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ZBase.Utilities
{
	public static class VectorExtensions
	{
		public static bool IsZero(this Vector3 vector)
		{
			return vector.X == 0 && vector.Y == 0 && vector.Z == 0;
		}

		public static Vector3 ToAngle(this Vector3 vector)
		{
			return new Vector3(
				(float)(Math.Atan2(-vector.Z, Math.Sqrt(vector.X * vector.X + vector.Y * vector.Y)) * (180.0f / Math.PI)),
				(float)(Math.Atan2(vector.Y, vector.X) * (180.0f / Math.PI)),
				0.0f
			);
		}

	}
}
