using UnityEngine;

public class Obstacle : MovingObject
{
	// ------------------------------------------------------------------------------------------------------------------------------
	// [Editor]
	[SerializeField] private Collider PointsZoneColiderReference;
	// ------------------------------------------------------------------------------------------------------------------------------
	// [Properties]
	public Collider PointsZoneColider => PointsZoneColiderReference;
	// ------------------------------------------------------------------------------------------------------------------------------
	// [Code - private]
	// ------------------------------------------------------------------------------------------------------------------------------
	public override void Interact(FlappyFish fish)
	{
		//fish.KillFish();
	}
	// ------------------------------------------------------------------------------------------------------------------------------
}
