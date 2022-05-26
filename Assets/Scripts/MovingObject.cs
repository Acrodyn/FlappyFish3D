using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingObject : MonoBehaviour
{
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
	private void Move()
	{
		transform.position += Vector3.left * Time.deltaTime * _activeLevelController.ObjectMovementSpeed;
		//CheckForDestruction();
	}
	// ------------------------------------------------------------------------------------------------------------------------------
	//private void CheckForDestruction()
	//{
	//	Vector3 screenPoint = _camera.WorldToViewportPoint(transform.position);
	//	if (screenPoint.x < 0)
	//	{
	//		Destroy(gameObject);
	//	}
	//}
	//// ------------------------------------------------------------------------------------------------------------------------------
}
