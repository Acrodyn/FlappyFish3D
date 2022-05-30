using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MovingObject
{
	// ------------------------------------------------------------------------------------------------------------------------------
	// [Editor]
	// ------------------------------------------------------------------------------------------------------------------------------
	// [Code - private]
	// ------------------------------------------------------------------------------------------------------------------------------
	public override void Interact(FlappyFish fish)
	{
		fish.KillFish();
	}
	// ------------------------------------------------------------------------------------------------------------------------------
}