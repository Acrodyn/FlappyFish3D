using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifierObject : MonoBehaviour
{
	// ------------------------------------------------------------------------------------------------------------------------------
	// [Editor]
	[SerializeField] private ModifierData ModifierData;
	// ------------------------------------------------------------------------------------------------------------------------------
	// [Properties]
	//public ModifierData Data;
	// ------------------------------------------------------------------------------------------------------------------------------
	void OnTriggerEnter(Collider collider)
	{
		if (collider.gameObject.CompareTag(Consts.PLAYER_TAG))
		{
			// changes point modifier 
			Core.ScoreManager.ApplyModifierData(ModifierData);

			// adds effect to fish

			// Some flashy effect before destruction, particles?
			Destroy(gameObject);
		}
	}
	// ------------------------------------------------------------------------------------------------------------------------------
}
