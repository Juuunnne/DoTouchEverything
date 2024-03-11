using UnityEngine;

public static class FloatExtensions
{
    public static bool IsEqualTo(this float a, float b, float tolerance = 0.0001f)
    {
        return Mathf.Abs(a - b) < tolerance;
    }
}
