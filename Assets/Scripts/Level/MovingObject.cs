using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
	// ------------------------------------------------------------------------------------------------------------------------------
	// [Editor]
	[SerializeField] private bool StopOnLevelEnd;
	// ------------------------------------------------------------------------------------------------------------------------------
	// [Properties]
	// ------------------------------------------------------------------------------------------------------------------------------
	// [Code - private]
	private LevelController _activeLevelController;
	private Renderer _renderer;
	private Camera _camera;
	// ------------------------------------------------------------------------------------------------------------------------------
	void Start()
	{
		_activeLevelController = Core.ActiveLevelController;
		_renderer = GetComponent<Renderer>();
		_camera = Camera.main;
	}
	// ------------------------------------------------------------------------------------------------------------------------------
	void Update()
	{
		Move();
	}
	// ------------------------------------------------------------------------------------------------------------------------------
	public virtual void Interact(FlappyFish fish)
	{
		
	}
	// ------------------------------------------------------------------------------------------------------------------------------
	private void Move()
	{
		if (StopOnLevelEnd && _activeLevelController.IsLevelMovementStopped)
		{
			return;
		}

		transform.position += Vector3.left * Time.deltaTime * _activeLevelController.ObjectMovementSpeed;
	}
	// ------------------------------------------------------------------------------------------------------------------------------
}
