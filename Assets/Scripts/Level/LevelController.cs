using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
	// ------------------------------------------------------------------------------------------------------------------------------
	// [Editor]
	[SerializeField] private bool AutoRestartOnDeath;
	[Range(0f, 1f)]
	[SerializeField] private float Difficulty;
	[SerializeField] private float SkyBoxMovementSpeed;
	[SerializeField] private float ObjectsSpeed;
	[SerializeField] private float StartSpawnDelay;
	[SerializeField] private float RegularSpawnDelay;
	[SerializeField] private float PickupTrailIntervalMin;
	[SerializeField] private float PickupTrailIntervalMax;
	[SerializeField] private float PickupSpawnDelayMin;
	[SerializeField] private float PickupSpawnDelayMax;
	[SerializeField] private float SpawnDifficultyModifier;
	[SerializeField] private Transform EndGameScreenTransform;
	[SerializeField] private ObserverEvent ResetLevelEvent;
	// ------------------------------------------------------------------------------------------------------------------------------
	// [Properties]
	public bool IsLevelMovementStopped => _isLevelMovementStopped;
	public float ObjectMovementSpeed => ObjectsSpeed;
	// ------------------------------------------------------------------------------------------------------------------------------
	// [Code - private]
	private ObjectSpawner _objectSpawner;
	private Material _skyBoxMaterial;
	private float _currentSkyBoxRotation = 0f;
	private float _spawnDelay;
	private float _pickupSpawnDelayGeneral;
	private float _pickupSpawnDelay;
	private bool _isLevelMovementStopped = false;
	private bool _isPickupReady = false;
	// ------------------------------------------------------------------------------------------------------------------------------
	void Start()
	{
		Core.SetActiveLevelController(this);
		_skyBoxMaterial = RenderSettings.skybox;
		_spawnDelay = StartSpawnDelay;
		_pickupSpawnDelayGeneral = StartSpawnDelay + GetPickupSpawnDelay();
		_objectSpawner = GetComponent<ObjectSpawner>();
	}
	// ------------------------------------------------------------------------------------------------------------------------------
	void Update()
	{
		if (!_isLevelMovementStopped)
		{
			RotateSkyBox();
			ObstacleSpawnCheck();
			PickupSpawnCheck();
		}
	}
	// ------------------------------------------------------------------------------------------------------------------------------
	void OnDestroy()
	{
		_skyBoxMaterial.SetFloat("_Rotation", 0f);
	}
	// ------------------------------------------------------------------------------------------------------------------------------ 
	public void ResetLevel()
	{
		_objectSpawner.DestroyAllObjects();
		_isLevelMovementStopped = false;
		_spawnDelay = StartSpawnDelay;
		_pickupSpawnDelayGeneral = StartSpawnDelay + GetPickupSpawnDelay();
		Core.ActiveFlappyFish.ReviveFish();
		EndGameScreenTransform.gameObject.SetActive(false);

		ResetLevelEvent.Trigger();
	}
	// ------------------------------------------------------------------------------------------------------------------------------ 
	public void OnFishDeath()
	{
		_isLevelMovementStopped = true;
		EndGameScreenTransform.gameObject.SetActive(true);

		if (AutoRestartOnDeath)
		{
			ResetLevel();
		}
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
	private void ObstacleSpawnCheck()
	{
		_spawnDelay -= Time.deltaTime;
		if (_spawnDelay <= 0f)
		{
			_objectSpawner.ActivateObstacleObject(Difficulty);
			_spawnDelay = RegularSpawnDelay - Difficulty * SpawnDifficultyModifier;
			CheckForTrailingPickup();
		}
	}
	// ------------------------------------------------------------------------------------------------------------------------------
	private void CheckForTrailingPickup()
	{
		if (!_isPickupReady && _pickupSpawnDelayGeneral <= 0f)
		{
			_pickupSpawnDelay = _spawnDelay * Random.Range(PickupTrailIntervalMin, PickupTrailIntervalMax);
			_isPickupReady = true;
		}
	}
	// ------------------------------------------------------------------------------------------------------------------------------
	private void PickupSpawnCheck()
	{
		if (_isPickupReady)
		{
			_pickupSpawnDelay -= Time.deltaTime;
			if (_pickupSpawnDelay <= 0f)
			{
				_objectSpawner.ActivatePickupObject();
				_pickupSpawnDelayGeneral = GetPickupSpawnDelay();
				_isPickupReady = false;
			}

			return;
		}

		_pickupSpawnDelayGeneral -= Time.deltaTime;
	}
	// ------------------------------------------------------------------------------------------------------------------------------
	private float GetPickupSpawnDelay()
	{
		return Random.Range(PickupSpawnDelayMin, PickupSpawnDelayMax);
	}
	// ------------------------------------------------------------------------------------------------------------------------------
}