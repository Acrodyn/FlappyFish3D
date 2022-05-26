using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlappyFish : MonoBehaviour
{
    // ------------------------------------------------------------------------------------------------------------------------------
    // [Editor]
    [SerializeField] private float DefaultJumpStrength;
    [SerializeField] private Rigidbody FishRigidBody;
    // ------------------------------------------------------------------------------------------------------------------------------
    // [Code - private]
    private float _jumpStrength;
	// ------------------------------------------------------------------------------------------------------------------------------
	void Start()
	{
        _jumpStrength = DefaultJumpStrength;
        AssignInputActions();
    }
    // ------------------------------------------------------------------------------------------------------------------------------
    void OnDestroy()
	{
        UnassignInputActions();
    }
	// ------------------------------------------------------------------------------------------------------------------------------
	private void Jump()
	{
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
}
