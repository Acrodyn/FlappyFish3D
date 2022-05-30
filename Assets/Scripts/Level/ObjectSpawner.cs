using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
	// ------------------------------------------------------------------------------------------------------------------------------
	// [Editor]
	[SerializeField] private float ObstacleLimit;
	[SerializeField] private int ObstaclePoolSize;
	[SerializeField] private Transform SpawnedObjectsHolder;
	[SerializeField] private GameObject ObstaclePrefab;
	// ------------------------------------------------------------------------------------------------------------------------------
	// [Code - private]
	private Transform _lastSpawnedObstacleTransform;
	private List<GameObject> _obstaclePool;
	// ------------------------------------------------------------------------------------------------------------------------------
	void Start()
	{
		InitObjectPools();
	}
	// ------------------------------------------------------------------------------------------------------------------------------
	public Transform ActivateObstacleObject(float difficulty)
	{
		foreach (var obstacle in _obstaclePool)
		{
			if (!obstacle.activeSelf)
			{
				_lastSpawnedObstacleTransform = obstacle.transform;
				_lastSpawnedObstacleTransform.position = new Vector3(ObstaclePrefab.transform.position.x, GetObstacleSpawnPosition(difficulty), ObstaclePrefab.transform.position.z);
				obstacle.SetActive(true);
				return obstacle.transform;
			}
		}

		throw new System.Exception("Not enough obstacles in an obstacle pool!");
	}
	// ------------------------------------------------------------------------------------------------------------------------------
	public void DestroyAllObjects()
	{
		foreach (Transform child in SpawnedObjectsHolder)
		{
			child.gameObject.SetActive(false);
		}

		_lastSpawnedObstacleTransform = null;
	}
	// ------------------------------------------------------------------------------------------------------------------------------
	private void InitObjectPools()
	{
		_obstaclePool = new List<GameObject>();
		GameObject obstacleObject;

		for (int i = 0; i < ObstaclePoolSize; ++i)
		{
			obstacleObject = Instantiate(ObstaclePrefab, SpawnedObjectsHolder, false);
			obstacleObject.SetActive(false);
			_obstaclePool.Add(obstacleObject);
		}
	}
	// ------------------------------------------------------------------------------------------------------------------------------
	private float GetObstacleSpawnPosition(float difficulty)
	{
		float obstacleDownLimit;
		float obstacleUpLimit;

		if (_lastSpawnedObstacleTransform != null)
		{
			float lastObstaclePosition = _lastSpawnedObstacleTransform.position.y;

			obstacleDownLimit = Mathf.Clamp(lastObstaclePosition - ObstacleLimit * difficulty, -ObstacleLimit, ObstacleLimit);
			obstacleUpLimit = Mathf.Clamp(lastObstaclePosition + ObstacleLimit * difficulty, -ObstacleLimit, ObstacleLimit);
		}
		else
		{
			obstacleDownLimit = -ObstacleLimit;
			obstacleUpLimit = ObstacleLimit;
		}

		return Random.Range(obstacleDownLimit, obstacleUpLimit);
	}
	// ------------------------------------------------------------------------------------------------------------------------------
}
