using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyFish : MonoBehaviour
{
    // ------------------------------------------------------------------------------------------------------------------------------
    // [Editor]
    [SerializeField] private float DefaultJumpStrength;
    [SerializeField] private float GravityAcceleration;
    [SerializeField] private float DeathForce;
    [SerializeField] private float DeathSideForce;
    [SerializeField] private ObserverEvent FishDeathEvent;
    [SerializeField] private Rigidbody FishRigidBody;
    // ------------------------------------------------------------------------------------------------------------------------------
    // [Code - private]
    private float _jumpStrength;
    private bool _isDead;
	// ------------------------------------------------------------------------------------------------------------------------------
	void Start()
	{
        _jumpStrength = DefaultJumpStrength;
        AssignInputActions();
    }
	// ------------------------------------------------------------------------------------------------------------------------------
	void Update()
	{
        AddGravity();
    }
    // ------------------------------------------------------------------------------------------------------------------------------
    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.CompareTag(Consts.KILLER_TAG))
        {
            KillFish();
            return;
        }

        if (collider.gameObject.CompareTag(Consts.MOVABLE_TAG))
		{
            MovingObject movingObject = Utils.GetMovingObjectRoot(collider.transform);
            if (movingObject == null)
			{
                throw new System.Exception(string.Format("Object {0} is marked as movable but isn't part of a hiararchy that includes movable component", collider.gameObject.ToString()));
			}

            movingObject.Interact(this);
		}
	}
    // ------------------------------------------------------------------------------------------------------------------------------
    void OnDestroy()
	{
        UnassignInputActions();
    }
    // ------------------------------------------------------------------------------------------------------------------------------
    public void KillFish()
    {
        _isDead = true;
        FishRigidBody.useGravity = false;
        FishRigidBody.velocity = Vector3.zero;
        FishRigidBody.AddForce(Vector3.up * DeathForce);
        FishRigidBody.AddForce(Vector3.back * DeathSideForce, ForceMode.Impulse);
        FishDeathEvent.Trigger();
    }
    // ------------------------------------------------------------------------------------------------------------------------------
    public void ReviveFish()
    {
        _isDead = false;
        FishRigidBody.useGravity = true;
    }
    // ------------------------------------------------------------------------------------------------------------------------------
    private void Jump()
	{
        if (_isDead)
		{
            return;
		}

        FishRigidBody.velocity = Vector3.zero;
        FishRigidBody.AddForce(Vector2.up * DefaultJumpStrength);
    }
    // ------------------------------------------------------------------------------------------------------------------------------
    private void AssignInputActions()
	{
        InputActions inputActions = Core.GetInputManager().GetInputActions();
        inputActions.FlappyFish.Jump.performed += ctx => Jump();
	}
    // ------------------------------------------------------------------------------------------------------------------------------
    private void UnassignInputActions()
    {
        InputActions inputActions = Core.GetInputManager().GetInputActions();
        inputActions.FlappyFish.Jump.performed -= ctx => Jump();
    }
    // ------------------------------------------------------------------------------------------------------------------------------
    private void AddGravity()
    {
        if (_isDead)
		{
            return;
		}

        if (FishRigidBody.velocity.y < 0)
		{
            FishRigidBody.AddForce(Vector2.down * GravityAcceleration);
        }
	}
    // ------------------------------------------------------------------------------------------------------------------------------
}
