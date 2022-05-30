using UnityEngine;

public class DestructionWall : MonoBehaviour
{
    // ------------------------------------------------------------------------------------------------------------------------------
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag(Consts.MOVABLE_TAG))
		{
            GameObject gameObject = Utils.GetMovingObjectRoot(collider.transform).gameObject;
            gameObject.SetActive(false);
        }
    }
    // ------------------------------------------------------------------------------------------------------------------------------
}
