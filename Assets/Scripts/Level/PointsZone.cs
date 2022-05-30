using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointsZone : MonoBehaviour
{
    // ------------------------------------------------------------------------------------------------------------------------------
    // [Editor]
    [SerializeField] private ObserverEvent PointsZoneTriggered;
    [SerializeField] private AudioClip PointSound;
    // ------------------------------------------------------------------------------------------------------------------------------
    void OnTriggerExit(Collider collider)
    {
        if (collider.gameObject.CompareTag(Consts.PLAYER_TAG))
        {
            if (!Core.ActiveFlappyFish.IsDead)
			{
                PointsZoneTriggered.Trigger();
                AudioSource.PlayClipAtPoint(PointSound, transform.position);
			}
        }
    }
    // ------------------------------------------------------------------------------------------------------------------------------
}
