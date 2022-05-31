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
	[SerializeField] private Transform StartGameScreenTransform;
	[SerializeField] private Transform EndGameScreenTransform;
	[SerializeField] private ObserverEvent ResetLevelEvent;
	// ------------------------------------------------------------------------------------------------------------------------------
	// [Properties]
	public bool IsAutoRestartActivated => AutoRestartOnDeath;
	public bool IsLevelMovementStopped => _isLevelMovementStopped;
	public float ObjectMovementSpeed => ObjectsSpeed * _speedModifier;
	public float ModifierValue => _modifierValue;
	// ------------------------------------------------------------------------------------------------------------------------------
	// [Code - private]
	private ObjectSpawner _objectSpawner;
	private Material _skyBoxMaterial;
	private float _currentSkyBoxRotation = 0f;
	private float _spawnDelay;
	private float _pickupSpawnDelayGeneral;
	private float _pickupSpawnDelay;
	private float _speedModifier = 1f;
	private bool _hasGameStarted = false;
	private bool _isLevelMovementStopped = false;
	private bool _isPickupReady = false;
	private bool _isModifierActive;
	private float _modifierDuration;
	private float _modifierValue;
	// ------------------------------------------------------------------------------------------------------------------------------
	void Start()
	{
		Core.SetActiveLevelController(this);
		_skyBoxMaterial = RenderSettings.skybox;
		_spawnDelay = StartSpawnDelay;
		_pickupSpawnDelayGeneral = StartSpawnDelay + GetPickupSpawnDelay();
		_objectSpawner = GetComponent<ObjectSpawner>();
		ResetModifierData();
		Core.ShowCursor(false);

		if (AutoRestartOnDeath)
		{
			StartRun();
		}
	}
	// ------------------------------------------------------------------------------------------------------------------------------
	void Update()
	{
		if (!_hasGameStarted)
		{
			return;
		}

		if (!_isLevelMovementStopped)
		{
			RotateSkyBox();
			ObstacleSpawnCheck();
			PickupSpawnCheck();
		}

		CheckModifier();
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
		Core.ShowCursor(false);
		EndGameScreenTransform.gameObject.SetActive(false);

		if (!AutoRestartOnDeath)
		{
			_hasGameStarted = false;
			StartGameScreenTransform.gameObject.SetActive(true);
		}

		ResetLevelEvent.Trigger();
	}
	// ------------------------------------------------------------------------------------------------------------------------------ 
	public void OnFishDeath()
	{
		_isLevelMovementStopped = true;
		EndGameScreenTransform.gameObject.SetActive(true);
		Core.ShowCursor(true);
		ResetModifierData();

		if (AutoRestartOnDeath)
		{
			ResetLevel();
		}
	}
	// ------------------------------------------------------------------------------------------------------------------------------
	public void SetSpeedModifier(float modifier)
	{
		_speedModifier = modifier;
	}
	// ------------------------------------------------------------------------------------------------------------------------------
	public void ResetSpeedModifier()
	{
		_speedModifier = 1f;
	}
	// ------------------------------------------------------------------------------------------------------------------------------
	public void ApplyModifierData(ModifierData data)
	{
		_isModifierActive = true;
		_modifierDuration = data.ModifierDuration;
		_modifierValue = data.ModifierPointScale;
	}
	// ------------------------------------------------------------------------------------------------------------------------------
	public void ResetModifierData()
	{
		_isModifierActive = false;
		_modifierDuration = 0f;
		_modifierValue = 1f;
		ResetSpeedModifier();
	}
	// ------------------------------------------------------------------------------------------------------------------------------
	public void StartRun()
	{
		_hasGameStarted = true;
		StartGameScreenTransform.gameObject.SetActive(false);
	}
	// ------------------------------------------------------------------------------------------------------------------------------
	private void RotateSkyBox()
	{
		_currentSkyBoxRotation = Mathf.MoveTowards(_currentSkyBoxRotation, Consts.FULL_REVOLUTION, Time.deltaTime * SkyBoxMovementSpeed * _speedModifier);
		if (Utils.AreNearlyEqual(_currentSkyBoxRotation, Consts.FULL_REVOLUTION))
		{
			_currentSkyBoxRotation = 0f;
		}

		_skyBoxMaterial.SetFloat("_Rotation", _currentSkyBoxRotation);
	}
	// ------------------------------------------------------------------------------------------------------------------------------
	private void ObstacleSpawnCheck()
	{
		_spawnDelay -= (Time.deltaTime * _speedModifier);
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
			_pickupSpawnDelay -= (Time.deltaTime * _speedModifier);
			if (_pickupSpawnDelay <= 0f)
			{
				_objectSpawner.ActivatePickupObject();
				_pickupSpawnDelayGeneral = GetPickupSpawnDelay();
				_isPickupReady = false;
			}

			return;
		}

		_pickupSpawnDelayGeneral -= (Time.deltaTime * _speedModifier);
	}
	// ------------------------------------------------------------------------------------------------------------------------------
	private float GetPickupSpawnDelay()
	{
		return Random.Range(PickupSpawnDelayMin, PickupSpawnDelayMax);
	}
	// ------------------------------------------------------------------------------------------------------------------------------
	private void CheckModifier()
	{
		if (!_isModifierActive)
		{
			return;
		}

		_modifierDuration -= Time.deltaTime;
		if (_modifierDuration <= 0f)
		{
			ResetModifierData();
		}
	}
	// ------------------------------------------------------------------------------------------------------------------------------
}