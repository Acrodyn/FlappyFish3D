using System.Collections.Generic;
using UnityEngine;

public class ObjectSpawner : MonoBehaviour
{
	// ------------------------------------------------------------------------------------------------------------------------------
	// [Editor]
	[SerializeField] private float ObstacleLimit;
	[SerializeField] private float PickupLimit;
	[SerializeField] private int ObstaclePoolSize;
	[SerializeField] private int PerPickupPoolSize;
	[SerializeField] private Transform SpawnedObjectsHolder;
	[SerializeField] private Transform SpawnWallTransform;
	[SerializeField] private GameObject ObstaclePrefab;
	[SerializeField] private List<GameObject> PickupPrefabs;
	// ------------------------------------------------------------------------------------------------------------------------------
	// [Code - private]
	private Transform _lastSpawnedObstacleTransform;
	private List<GameObject> _obstaclePool;
	private List<GameObject> _pickupPool;
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
				_lastSpawnedObstacleTransform.localPosition = new Vector3(SpawnWallTransform.localPosition.x, GetObstacleSpawnPosition(difficulty), ObstaclePrefab.transform.localPosition.z);
				obstacle.SetActive(true);
				return obstacle.transform;
			}
		}

		throw new System.Exception("Not enough obstacles in an obstacle pool!");
	}
	// ------------------------------------------------------------------------------------------------------------------------------
	public Transform ActivatePickupObject()
	{
		List<int> pickupIndices = Utils.GetRandomOrderingList(_pickupPool.Count);
		foreach (var pickupIndex in pickupIndices)
		{
			GameObject pickup = _pickupPool[pickupIndex];
			if (!pickup.activeSelf)
			{
				pickup.transform.localPosition = new Vector3(SpawnWallTransform.localPosition.x, Random.Range(-PickupLimit, PickupLimit), pickup.transform.localPosition.z);
				pickup.SetActive(true);
				return pickup.transform;
			}
		}

		throw new System.Exception("Not enough pickups in a pickup pool!");
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
		InitObstaclePool();
		InitPickupPool();
	}
	// ------------------------------------------------------------------------------------------------------------------------------
	private void InitObstaclePool()
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
	private void InitPickupPool()
	{
		_pickupPool = new List<GameObject>();
		GameObject pickupObject;

		foreach (var pickupPrefab in PickupPrefabs)
		{
			for (int i = 0; i < PerPickupPoolSize; ++i)
			{
				pickupObject = Instantiate(pickupPrefab, SpawnedObjectsHolder, false);
				pickupObject.SetActive(false);
				_pickupPool.Add(pickupObject);
			}
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
