using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifierObject : MonoBehaviour
{
	// ------------------------------------------------------------------------------------------------------------------------------
	// [Editor]
	[SerializeField] private ModifierData ModifierData;
	// ------------------------------------------------------------------------------------------------------------------------------
	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.CompareTag(Consts.PLAYER_TAG))
		{
			// changes point modifier 
			Core.ActiveLevelController.ApplyModifierData(ModifierData);
			ApplyEffect();
			gameObject.SetActive(false);
		}
	}
	// ------------------------------------------------------------------------------------------------------------------------------
	protected virtual void ApplyEffect()
	{
		
	}
	// ------------------------------------------------------------------------------------------------------------------------------
}
