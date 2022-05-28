using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelController : MonoBehaviour
{
	// ------------------------------------------------------------------------------------------------------------------------------
	// [Editor]
	[Range(0f, 1f)]
	[SerializeField] private float Difficulty;
	[SerializeField] private float SkyBoxMovementSpeed;
	[SerializeField] private float ObjectsSpeed;
	[SerializeField] private float StartSpawnDelay;
	[SerializeField] private float RegularSpawnDelay;
	[SerializeField] private float SpawnDifficultyModifier;
	[SerializeField] private float ObstacleLimit;
	[SerializeField] private Transform SpawnedObjectsHolder;
	[SerializeField] private Transform EndGameScreenTransform;
 	[SerializeField] private GameObject ObstaclePrefab;
	[SerializeField] private ObserverEvent ResetLevelEvent;
	// ------------------------------------------------------------------------------------------------------------------------------
	// [Properties]
	public bool IsLevelMovementStopped => _isLevelMovementStopped;
	public float ObjectMovementSpeed => ObjectsSpeed;
	// ------------------------------------------------------------------------------------------------------------------------------
	// [Code - private]
	private Transform _lastSpawnedObstacleTransform;
	private Material _skyBoxMaterial;
	private float _currentSkyBoxRotation = 0f;
	private float _spawnDelay;
	private bool _isLevelMovementStopped = false;
	// ------------------------------------------------------------------------------------------------------------------------------
	void Start()
	{
		Core.SetActiveLevelController(this);
		_skyBoxMaterial = RenderSettings.skybox;
		_spawnDelay = StartSpawnDelay;
	}
	// ------------------------------------------------------------------------------------------------------------------------------
	void Update()
	{
		if (!_isLevelMovementStopped)
		{
			RotateSkyBox();
			SpawnCheck();
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
		DestroyAllObjects();
		_isLevelMovementStopped = false;
		_spawnDelay = StartSpawnDelay;
		Core.ActiveFlappyFish.ReviveFish();
		EndGameScreenTransform.gameObject.SetActive(false);

		ResetLevelEvent.Trigger();
	}
	// ------------------------------------------------------------------------------------------------------------------------------ 
	public void OnFishDeath()
	{
		_isLevelMovementStopped = true;
		EndGameScreenTransform.gameObject.SetActive(true);
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
	private void SpawnCheck()
	{
		_spawnDelay -= Time.deltaTime;
		if (_spawnDelay <= 0f)
		{
			_lastSpawnedObstacleTransform = Instantiate(ObstaclePrefab, SpawnedObjectsHolder, false).transform;
			_lastSpawnedObstacleTransform.position = new Vector3(_lastSpawnedObstacleTransform.position.x, GetObstacleSpawnPosition(), _lastSpawnedObstacleTransform.transform.position.z);
			_spawnDelay = RegularSpawnDelay - Difficulty * SpawnDifficultyModifier;
		}
	}
	// ------------------------------------------------------------------------------------------------------------------------------
	private float GetObstacleSpawnPosition()
	{
		float obstacleDownLimit;
		float obstacleUpLimit;

		if (_lastSpawnedObstacleTransform != null)
		{
			float lastObstaclePosition = _lastSpawnedObstacleTransform.position.y;

			obstacleDownLimit = Mathf.Clamp(lastObstaclePosition - ObstacleLimit * Difficulty, -ObstacleLimit, ObstacleLimit);
			obstacleUpLimit = Mathf.Clamp(lastObstaclePosition + ObstacleLimit * Difficulty, -ObstacleLimit, ObstacleLimit);
		}
		else
		{
			obstacleDownLimit = -ObstacleLimit;
			obstacleUpLimit = ObstacleLimit;
		}

		return Random.Range(obstacleDownLimit, obstacleUpLimit);
	}
	// ------------------------------------------------------------------------------------------------------------------------------
	private void DestroyAllObjects()
	{
		foreach (Transform child in SpawnedObjectsHolder)
		{
			Destroy(child.gameObject);
		}

		_lastSpawnedObstacleTransform = null;
	}
	// ------------------------------------------------------------------------------------------------------------------------------
}