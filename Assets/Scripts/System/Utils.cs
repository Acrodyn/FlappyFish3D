using System.Collections.Generic;
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
    public static T GetComponentAtRoot<T>(Transform transform)
    {
        if (transform == null || transform.gameObject == null)
		{
            return default(T);
		}

        T movingObject = transform.gameObject.GetComponent<T>();
        if (movingObject != null)
        {
            return movingObject;
        }

        return GetComponentAtRoot<T>(transform.parent);
    }
    // ------------------------------------------------------------------------------------------------------------------------------
    public static List<int> GetRandomOrderingList(int higherNumber)
    {
        return GetRandomOrderingList(0, higherNumber);
    }
    // ------------------------------------------------------------------------------------------------------------------------------
    public static List<int> GetRandomOrderingList(int lowerNumber, int higherNumber)
    {
        List<int> randomOrdering = new List<int>();
        for (int i = lowerNumber; i < higherNumber; ++i)
        {
            randomOrdering.Add(i);
        }

        var count = randomOrdering.Count;
        for (var i = 0; i < count; ++i)
        {
            int random = Random.Range(i, count);
            var tmp = randomOrdering[i];
            randomOrdering[i] = randomOrdering[random];
            randomOrdering[random] = tmp;
        }

        return randomOrdering;
    }
    // ------------------------------------------------------------------------------------------------------------------------------
}
