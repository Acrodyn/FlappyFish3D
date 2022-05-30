using UnityEngine;

public class DestructionWall : MonoBehaviour
{
    // ------------------------------------------------------------------------------------------------------------------------------
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag(Consts.MOVABLE_TAG))
		{
            GameObject gameObject = Utils.GetComponentAtRoot<MovingObject>(collider.transform).gameObject;
            gameObject.SetActive(false);
        }
    }
    // ------------------------------------------------------------------------------------------------------------------------------
}
