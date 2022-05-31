using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyFish : MonoBehaviour
{
    // ------------------------------------------------------------------------------------------------------------------------------
    // [Editor]
    [SerializeField] private bool IsFishImmortal;
    [SerializeField] private float DefaultJumpStrength;
    [SerializeField] private float GravityAcceleration;
    [SerializeField] private float DeathForce;
    [SerializeField] private float DeathSideForce;
    [SerializeField] private AudioSource JumpSoundSource;
    [SerializeField] private AudioSource DeathSoundSource;
    [SerializeField] private ObserverEvent FishDeathEvent;
    [SerializeField] private Rigidbody FishRigidBody;
    // ------------------------------------------------------------------------------------------------------------------------------
    // [Properties]
    public bool IsDead => _isDead;
    // ------------------------------------------------------------------------------------------------------------------------------
    // [Code - private]
    private float _jumpStrength;
    private bool _isDead;
	// ------------------------------------------------------------------------------------------------------------------------------
	void Start()
	{
        _jumpStrength = DefaultJumpStrength;
        AssignInputActions();
        Core.SetActiveFish(this);
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
            MovingObject movingObject = Utils.GetComponentAtRoot<MovingObject>(collider.transform);
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
        if (IsFishImmortal || _isDead)
		{
            return;
		}

        _isDead = true;
        FishRigidBody.useGravity = false;
        FishRigidBody.velocity = Vector3.zero;
        FishRigidBody.AddForce(Vector3.up * DeathForce);
        FishRigidBody.AddForce(Vector3.back * DeathSideForce, ForceMode.Impulse);
        FishDeathEvent.Trigger();
        DeathSoundSource.Play();
    }
    // ------------------------------------------------------------------------------------------------------------------------------
    public void ReviveFish()
    {
        _isDead = false;
        FishRigidBody.useGravity = true;
        FishRigidBody.velocity = Vector3.zero;
        transform.position = Vector3.zero;
    }
    // ------------------------------------------------------------------------------------------------------------------------------
    public void Jump()
	{
        if (_isDead)
		{
            return;
		}

        FishRigidBody.velocity = Vector3.zero;
        FishRigidBody.AddForce(Vector2.up * DefaultJumpStrength);
        JumpSoundSource.Play();
    }
    // ------------------------------------------------------------------------------------------------------------------------------
    public bool IsFalling()
	{
        return FishRigidBody.velocity.y <= 0f;
	}
    // ------------------------------------------------------------------------------------------------------------------------------
    private void AssignInputActions()
	{
        InputActions inputActions = Core.InputManager.GetInputActions();
        inputActions.FlappyFish.Jump.performed += ctx => Jump();
	}
    // ------------------------------------------------------------------------------------------------------------------------------
    private void UnassignInputActions()
    {
        InputActions inputActions = Core.InputManager.GetInputActions();
        inputActions.FlappyFish.Jump.performed -= ctx => Jump();
    }
    // ------------------------------------------------------------------------------------------------------------------------------
    private void AddGravity()
    {
        if (_isDead)
		{
            return;
		}

        if (FishRigidBody.velocity.y < 0f)
		{
            FishRigidBody.AddForce(Vector2.down * GravityAcceleration);
        }
	}
    // ------------------------------------------------------------------------------------------------------------------------------
}
