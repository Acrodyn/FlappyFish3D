using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
	// ------------------------------------------------------------------------------------------------------------------------------
	// [Editor]
	[SerializeField] private float SkyBoxMovementSpeed;
	[SerializeField] private float ObjectsSpeed;
	// ------------------------------------------------------------------------------------------------------------------------------
	// [Properties]
	public float ObjectMovementSpeed => ObjectsSpeed;
	// ------------------------------------------------------------------------------------------------------------------------------
	// [Code - private]
	private Material _skyBoxMaterial;
	private float _currentSkyBoxRotation = 0f;
	// ------------------------------------------------------------------------------------------------------------------------------
	void Start()
	{
		Core.SetActiveLevelController(this);
		_skyBoxMaterial = RenderSettings.skybox;
	}
	// ------------------------------------------------------------------------------------------------------------------------------
	void Update()
	{
		RotateSkyBox();
	}
	// ------------------------------------------------------------------------------------------------------------------------------
	private void RotateSkyBox()
	{
		_currentSkyBoxRotation = Mathf.MoveTowards(_currentSkyBoxRotation, Consts.FULL_REVOLUTION, Time.deltaTime * SkyBoxMovementSpeed);
		if (Utils.AreNearlyEqual(_currentSkyBoxRotation, Consts.FULL_REVOLUTION))
		{
			_currentSkyBoxRotation = 0f;
		}

		_skyBoxMaterial.SetFloat("_Rotation", _currentSkyBoxRotation);
	}
	// ------------------------------------------------------------------------------------------------------------------------------
}
