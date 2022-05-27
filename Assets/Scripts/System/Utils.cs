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
    public static MovingObject GetMovingObjectRoot(Transform transform)
    {
        if (transform == null || transform.gameObject == null)
		{
            return null;
		}

        MovingObject movingObject = transform.gameObject.GetComponent<MovingObject>();
        if (movingObject != null)
        {
            return movingObject;
        }

        return GetMovingObjectRoot(transform.parent);
    }
    // ------------------------------------------------------------------------------------------------------------------------------
}
