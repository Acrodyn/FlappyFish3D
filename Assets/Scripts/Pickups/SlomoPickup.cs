using UnityEngine;

public class SlomoPickup : ModifierObject
{
	// ------------------------------------------------------------------------------------------------------------------------------
	// [Editor]
	[SerializeField] private float SpeedModifier;
	// ------------------------------------------------------------------------------------------------------------------------------
	protected override void ApplyEffect()
	{
		Core.ActiveLevelController.SetSpeedModifier(SpeedModifier);
	}
	// ------------------------------------------------------------------------------------------------------------------------------
}
