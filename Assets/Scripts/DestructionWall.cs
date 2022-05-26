using UnityEngine;

public class DestructionWall : MonoBehaviour
{
    // ------------------------------------------------------------------------------------------------------------------------------
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag(Consts.MOVABLE_TAG))
		{
            GameObject gameObject = GetMovingObjectRoot(collider.transform);
            Destroy(gameObject);
		}
    }
    // ------------------------------------------------------------------------------------------------------------------------------
    private GameObject GetMovingObjectRoot(Transform transform)
	{
        if (transform.gameObject.GetComponent<MovingObject>())
        {
            return transform.gameObject;
		}

        return GetMovingObjectRoot(transform.parent);
	}
    // ------------------------------------------------------------------------------------------------------------------------------
}
