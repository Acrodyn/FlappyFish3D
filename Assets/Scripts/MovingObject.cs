using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
	// ------------------------------------------------------------------------------------------------------------------------------
	// [Code - private]
	private LevelController _activeLevelController;
	// ------------------------------------------------------------------------------------------------------------------------------
	void Start()
	{
		_activeLevelController = Core.ActiveLevelController;
	}
	// ------------------------------------------------------------------------------------------------------------------------------
	void Update()
	{
		Move();
	}
	// ------------------------------------------------------------------------------------------------------------------------------
	private void Move()
	{
		transform.position += Vector3.left * Time.deltaTime * _activeLevelController.ObjectMovementSpeed;
	}
	// ------------------------------------------------------------------------------------------------------------------------------
}
