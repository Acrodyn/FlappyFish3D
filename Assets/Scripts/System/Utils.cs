using UnityEngine;

public static class Utils
{
    // ------------------------------------------------------------------------------------------------------------------------------
    public static bool AreNearlyEqual(float a, float b, float epsilon = Consts.TINY_NUMBER)
	{
        return Mathf.Abs(a - b) < epsilon;
	}
    // ------------------------------------------------------------------------------------------------------------------------------
    public static bool IsNearlyZero(float a, float epsilon = Consts.TINY_NUMBER)
    {
        return AreNearlyEqual(a, 0.0f, epsilon);
    }
    // ------------------------------------------------------------------------------------------------------------------------------
}
