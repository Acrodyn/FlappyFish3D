using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Event", menuName = "FishObjects/Observer Event", order = 1)]
public class ObserverEvent : ScriptableObject
{
	// ------------------------------------------------------------------------------------------------------------------------------
	private List<EventListener> _listeners = new List<EventListener>();
	// ------------------------------------------------------------------------------------------------------------------------------
	public void Register(EventListener listener)
	{
		_listeners.Add(listener);
	}
	// ------------------------------------------------------------------------------------------------------------------------------
	public void Unregister(EventListener listener)
	{
		_listeners.Remove(listener);
	}
	// ------------------------------------------------------------------------------------------------------------------------------
	public void Trigger(EventMessage eventMessage = null)
	{
		for (int i = _listeners.Count - 1; i >= 0; --i)
		{
			_listeners[i].OnTrigger(eventMessage);
		}
	}
	// ------------------------------------------------------------------------------------------------------------------------------
}
