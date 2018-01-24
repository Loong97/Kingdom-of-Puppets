using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Tool_Math
{
	public static void Swap<T>(ref T a, ref T b)
	{
		T temp;
		temp = a;
		a = b;
		b = temp;
	}

	public static int Fit(int x, int min, int max)
	{
		if (x < min) return min;
		if (x > max) return max;
		return x;
	}

	public static float Diagonal(float a, float b)
	{
		if (b <= 0) return a;
		if (a <= 0) return b;
		if (a < b) Swap(ref a, ref b);
		return a + b * b / a / 2;
	}

	public static float Diagonal(float a, float b, float c)
	{
		return Diagonal(Diagonal(a, b), c);
	}

	public static float Length(Vector3 vec)
	{
		return Diagonal(vec.x, vec.y, vec.z);
	}
}
