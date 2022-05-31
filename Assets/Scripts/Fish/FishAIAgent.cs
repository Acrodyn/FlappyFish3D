using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishAIAgent : MonoBehaviour
{
	// ------------------------------------------------------------------------------------------------------------------------------
	// [Editor]
	[SerializeField] private float ActionDelay;
	[SerializeField] private float DetectionLength;
	[SerializeField] private float BottomOffset;
	// ------------------------------------------------------------------------------------------------------------------------------
	// [Code - private]
	private IEnumerator coroutine;
	private FlappyFish _flappyFish;
	private Obstacle _obstacleAhead;
	// ------------------------------------------------------------------------------------------------------------------------------
	void Start()
	{
		_flappyFish = GetComponent<FlappyFish>();
		coroutine = WaitAndReact(ActionDelay);
		StartCoroutine(coroutine);
	}
	// ------------------------------------------------------------------------------------------------------------------------------
	public void ResetAgent()
	{
		_obstacleAhead = null;
	}
	// ------------------------------------------------------------------------------------------------------------------------------
	private void CheckForObstacle()
	{
		if (_obstacleAhead != null)
		{
			return;
		}

		RaycastHit hit;
		if (Physics.Raycast(transform.position, transform.right, out hit, DetectionLength))
		{
			if (hit.collider.CompareTag(Consts.MOVABLE_TAG) || hit.collider.CompareTag(Consts.KILLER_TAG))
			{
				_obstacleAhead = Utils.GetComponentAtRoot<Obstacle>(hit.collider.transform);
			}
		}
	}
	// ------------------------------------------------------------------------------------------------------------------------------
	private void React()
	{
		if (_obstacleAhead == null)
		{
			MaintainMiddle();
		}
		else
		{
			AvoidObstacle();
		}
	}
	// ------------------------------------------------------------------------------------------------------------------------------
	private void MaintainMiddle()
	{
		if (_flappyFish.transform.position.y < 0f && _flappyFish.IsFalling())
		{
			_flappyFish.Jump();
		}
	}
	// ------------------------------------------------------------------------------------------------------------------------------
	private void AvoidObstacle()
	{
		Collider goalColllider = _obstacleAhead.PointsZoneColider;
		if (_flappyFish.transform.position.y < goalColllider.bounds.center.y - BottomOffset)
		{
			_flappyFish.Jump();
		}
	}
	// ------------------------------------------------------------------------------------------------------------------------------
	private IEnumerator WaitAndReact(float waitTime)
	{
		while (true)
		{
			yield return new WaitForSeconds(waitTime);
			CheckForObstacle();
			React();
		}
	}
	// ------------------------------------------------------------------------------------------------------------------------------
}
