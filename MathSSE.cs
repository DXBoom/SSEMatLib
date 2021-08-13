using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;

namespace Rasteryzer
{
	public static class MathSSE
	{
		public static float3 Add(float3 v1, float3 v2)
		{
			Vector128<float> a = Vector128.Create(v1.x, v1.y, v1.z, 0f);
			Vector128<float> b = Vector128.Create(v2.x, v2.y, v2.z, 0f);
			Vector128<float> result = Sse.Add(a, b);
			return new float3(result.GetElement(0), result.GetElement(1), result.GetElement(2));
		}

		public static float3 Substract(float3 v1, float3 v2)
		{
			Vector128<float> a = Vector128.Create(v1.x, v1.y, v1.z, 0f);
			Vector128<float> b = Vector128.Create(v2.x, v2.y, v2.z, 0f);
			Vector128<float> result = Sse.Subtract(a, b);
			return new float3(result.GetElement(0), result.GetElement(1), result.GetElement(2));
		}

		public static float3 Multiply(float3 v1, float3 v2)
		{
			Vector128<float> a = Vector128.Create(v1.x, v1.y, v1.z, 0f);
			Vector128<float> b = Vector128.Create(v2.x, v2.y, v2.z, 0f);
			Vector128<float> result = Sse.Multiply(a, b);

			return new float3(result.GetElement(0), result.GetElement(1), result.GetElement(2));
		}

		public static float3 Divide(float3 v1, float3 v2)
		{
			Vector128<float> a = Vector128.Create(v1.x, v1.y, v1.z, 0f);
			Vector128<float> b = Vector128.Create(v2.x, v2.y, v2.z, 0f);
			Vector128<float> result = Sse.Divide(a, b);

			return new float3(result.GetElement(0), result.GetElement(1), result.GetElement(2));
		}

		public static void Divide(float3 res, float3 v1, float3 v2)
		{
			Vector128<float> a = Vector128.Create(v1.x, v1.y, v1.z, 0f);
			Vector128<float> b = Vector128.Create(v2.x, v2.y, v2.z, 0f);
			Vector128<float> result = Sse.Divide(a, b);

			res.x = result.GetElement(0);
			res.y = result.GetElement(1);
			res.z = result.GetElement(2);
		}

		public static float3 Add(float3 v1, float f)
		{
			Vector128<float> a = Vector128.Create(v1.x, v1.y, v1.z, 0f);
			Vector128<float> b = Vector128.Create(f, f, f, 0f);
			Vector128<float> result = Sse.Add(a, b);

			return new float3(result.GetElement(0), result.GetElement(1), result.GetElement(2));
		}

		public static float3 Substract(float3 v1, float f)
		{
			Vector128<float> a = Vector128.Create(v1.x, v1.y, v1.z, 0f);
			Vector128<float> b = Vector128.Create(f, f, f, 0f);
			Vector128<float> result = Sse.Subtract(a, b);

			return new float3(result.GetElement(0), result.GetElement(1), result.GetElement(2));
		}

		public static float3 Multiply(float3 v1, float f)
		{
			Vector128<float> a = Vector128.Create(v1.x, v1.y, v1.z, 0f);
			Vector128<float> b = Vector128.Create(f, f, f, 0f);
			Vector128<float> result = Sse.Multiply(a, b);

			return new float3(result.GetElement(0), result.GetElement(1), result.GetElement(2));
		}

		public static float3 Divide(float3 v1, float f)
		{
			Vector128<float> a = Vector128.Create(v1.x, v1.y, v1.z, 0f);
			Vector128<float> b = Vector128.Create(f, f, f, 0f);
			Vector128<float> result = Sse.Divide(a, b);

			return new float3(result.GetElement(0), result.GetElement(1), result.GetElement(2));
		}

		public static void Divide(float3 res, float f, float3 v)
		{
			Vector128<float> a = Vector128.Create(v.x, v.y, v.z, 0f);
			Vector128<float> b = Vector128.Create(f, f, f, 0f);
			Vector128<float> result = Sse.Divide(a, b);

			res.x = result.GetElement(0);
			res.y = result.GetElement(1);
			res.z = result.GetElement(2);
		}

		public static float3 Normalize(float3 v)
		{
			float len = v.Magnitude;
			Vector128<float> a = Vector128.Create(v.x, v.y, v.z, 0f);
			Vector128<float> l = Vector128.Create(len, len, len, 0f);

			Vector128<float> result = Sse.Divide(a, l);

			return new float3(result.GetElement(0), result.GetElement(1), result.GetElement(2));
		}

		public static float Dot(float3 v1, float3 v2)
		{
			if(v1 == null || v2 == null)
				return 0;

			Vector128<float> a = Vector128.Create(v1.x, v1.y, v1.z, 0f);
			Vector128<float> b = Vector128.Create(v2.x, v2.y, v2.z, 0f);

			Vector128<float> result = Sse.Multiply(a, b);

			return result.GetElement(0) + result.GetElement(1) + result.GetElement(2);
		}

		public static float3 CrossProduct(float3 v1, float3 v2)
		{
			Vector128<float> a1 = Vector128.Create(v1.y, v1.z, v1.z, v1.x);
			Vector128<float> a2 = Vector128.Create(v1.x, v1.y, 0f, 0f);
			Vector128<float> b1 = Vector128.Create(v2.z, v2.y, v2.x, v2.z);
			Vector128<float> b2 = Vector128.Create(v2.y, v2.x, 0f, 0f);

			Vector128<float> result1 = Sse.Multiply(a1, b1);
			Vector128<float> result2 = Sse.Multiply(a2, b2);

			return new float3(result1.GetElement(0) - result1.GetElement(1), 
							result1.GetElement(2) - result1.GetElement(3),
							result2.GetElement(0) - result2.GetElement(1));

		}

		public static float3 Reflect(float3 incoming, float3 normal)
		{
			return incoming - (normal * 2.0f * MathUtils.Dot(incoming, normal));
		}


		public static float3 Saturate(float3 v)
		{
			Vector128<float> a = Vector128.Create(v.x, v.y, v.z, 0f);
			Vector128<float> min = Vector128.Create(1f, 1f, 1f, 0f);
			Vector128<float> max = Vector128.Create(0f, 0f, 0f, 0f);
			Vector128<float> result = Sse.Max(Sse.Min(a, min), max);
			return new float3(result.GetElement(0), result.GetElement(1), result.GetElement(2));
		}

		public static float4 Mul(float4x4 matrix, float4 v)
		{
			Vector128<float> a1 = Vector128.Create(matrix[0][0], matrix[1][0], matrix[2][0], matrix[3][0]);
			Vector128<float> a2 = Vector128.Create(matrix[0][1], matrix[1][1], matrix[2][1], matrix[3][1]);
			Vector128<float> a3 = Vector128.Create(matrix[0][2], matrix[1][2], matrix[2][2], matrix[3][2]);
			Vector128<float> a4 = Vector128.Create(matrix[0][3], matrix[1][3], matrix[2][3], matrix[3][3]);

			Vector128<float> b = Vector128.Create(v.x, v.y, v.z, v.w);

			Vector128<float> res1 = Sse.Multiply(a1, b);
			Vector128<float> res2 = Sse.Multiply(a2, b);
			Vector128<float> res3 = Sse.Multiply(a3, b);
			Vector128<float> res4 = Sse.Multiply(a4, b);

			float4 result = new float4(res1.GetElement(0) + res1.GetElement(1) + res1.GetElement(2) + res1.GetElement(3),
										res2.GetElement(0) + res2.GetElement(1) + res2.GetElement(2) + res2.GetElement(3),
										res3.GetElement(0) + res3.GetElement(1) + res3.GetElement(2) + res3.GetElement(3),
										res4.GetElement(0) + res4.GetElement(1) + res4.GetElement(2) + res4.GetElement(3));

			return result;
		}

		public static float4x4 Mul(float4x4 matrix1, float4x4 matrix2)
		{
			float4x4 result = new float4x4(Mul(matrix1, matrix2[0]), Mul(matrix1, matrix2[1]),
											Mul(matrix1, matrix2[2]), Mul(matrix1, matrix2[3]));

			return result;
		}

		public static float4 Add(float4 v1, float4 v2)
		{
			Vector128<float> a = Vector128.Create(v1.x, v1.y, v1.z, v1.w);
			Vector128<float> b = Vector128.Create(v2.x, v2.y, v2.z, v2.w);
			Vector128<float> result = Sse.Add(a, b);
			return new float4(result.GetElement(0), result.GetElement(1), result.GetElement(2), result.GetElement(3));
		}

		public static float4 Substract(float4 v1, float4 v2)
		{
			Vector128<float> a = Vector128.Create(v1.x, v1.y, v1.z, v1.w);
			Vector128<float> b = Vector128.Create(v2.x, v2.y, v2.z, v2.w);
			Vector128<float> result = Sse.Subtract(a, b);
			return new float4(result.GetElement(0), result.GetElement(1), result.GetElement(2), result.GetElement(3));
		}

		public static float4 Multiply(float4 v1, float4 v2)
		{
			Vector128<float> a = Vector128.Create(v1.x, v1.y, v1.z, v1.w);
			Vector128<float> b = Vector128.Create(v2.x, v2.y, v2.z, v2.w);
			Vector128<float> result = Sse.Multiply(a, b);

			return new float4(result.GetElement(0), result.GetElement(1), result.GetElement(2), result.GetElement(3));
		}

		public static float4 Divide(float4 v1, float4 v2)
		{
			Vector128<float> a = Vector128.Create(v1.x, v1.y, v1.z, v1.w);
			Vector128<float> b = Vector128.Create(v2.x, v2.y, v2.z, v2.w);
			Vector128<float> result = Sse.Divide(a, b);

			return new float4(result.GetElement(0), result.GetElement(1), result.GetElement(2), result.GetElement(3));
		}

		public static float4 Normalize(float4 v)
		{
			float len = v.Magnitude;
			Vector128<float> a = Vector128.Create(v.x, v.y, v.z, v.w);
			Vector128<float> l = Vector128.Create(len, len, len, len);

			Vector128<float> result = Sse.Divide(a, l);

			return new float4(result.GetElement(0), result.GetElement(1), result.GetElement(2), result.GetElement(3));
		}
	}
}
