using UnityEngine;

public class EventListener : MonoBehaviour
{
	// ------------------------------------------------------------------------------------------------------------------------------
	public ObserverEvent ObserverEvent;
	public EventObject Response = new EventObject();
	public bool CreationBase = true;
	public bool ActivationBase = false;
	// ------------------------------------------------------------------------------------------------------------------------------
	void Awake()
	{
		if (CreationBase)
		{ 
			ObserverEvent.Register(this);
		}
	}
	// ------------------------------------------------------------------------------------------------------------------------------
	void OnEnable()
	{
		if (ActivationBase)
		{
			ObserverEvent.Register(this);
		}
	}
	// ------------------------------------------------------------------------------------------------------------------------------
	void OnDestroy()
	{
		if (CreationBase)
		{
			ObserverEvent.Unregister(this);
		}
	}
	// ------------------------------------------------------------------------------------------------------------------------------
	void OnDisable()
	{
		if (ActivationBase)
		{
			ObserverEvent.Unregister(this);
		}
	}
	// ------------------------------------------------------------------------------------------------------------------------------
	public void OnTrigger(EventMessage eventMessage = null)
	{
		Response.Invoke(eventMessage);
	}
	// ------------------------------------------------------------------------------------------------------------------------------
}
