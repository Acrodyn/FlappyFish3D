using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Event", menuName = "FishObjects/Observer Event", order = 1)]
public class ObserverEvent : ScriptableObject
{
	public enum ObserverPriority
	{
		Undefined,
		Low,
		Normal,
		High
	}
	// ------------------------------------------------------------------------------------------------------------------------------
	private Dictionary<ObserverPriority, List<EventListener>> _listeners = new Dictionary<ObserverPriority, List<EventListener>>();
	// ------------------------------------------------------------------------------------------------------------------------------
	void OnEnable()
	{
		foreach (ObserverPriority priority in Enum.GetValues(typeof(ObserverPriority)))
		{
			if (priority == ObserverPriority.Undefined)
			{
				continue;
			}

			_listeners[priority] = new List<EventListener>();
		}
	}
	// ------------------------------------------------------------------------------------------------------------------------------
	public void Register(EventListener listener, ObserverPriority priority = ObserverPriority.Low)
	{
		_listeners[priority].Add(listener);
	}
	// ------------------------------------------------------------------------------------------------------------------------------
	public void Unregister(EventListener listener)
	{
		ObserverPriority priority = GetListenerPriority(listener);
		_listeners[priority].Remove(listener);
	}
	// ------------------------------------------------------------------------------------------------------------------------------
	public void Trigger(EventMessage eventMessage = null)
	{
		TriggerListeners(ObserverPriority.High, eventMessage);
		TriggerListeners(ObserverPriority.Normal, eventMessage);
		TriggerListeners(ObserverPriority.Low, eventMessage);
	}
	// ------------------------------------------------------------------------------------------------------------------------------
	private ObserverPriority GetListenerPriority(EventListener listener)
	{
		foreach (var listenerGroup in _listeners)
		{
			foreach (var listenerList in listenerGroup.Value)
			{
				if (listenerList == listener)
				{
					return listenerGroup.Key;
				}
			}
		}

		return ObserverPriority.Undefined;
	}
	// ------------------------------------------------------------------------------------------------------------------------------
	private void TriggerListeners(ObserverPriority priority, EventMessage eventMessage)
	{
		List<EventListener> listenerListCache = _listeners[priority];
		if (listenerListCache.Count == 0)
		{
			return;
		}

		for (int i = listenerListCache.Count - 1; i >= 0; --i)
		{
			listenerListCache[i].OnTrigger(eventMessage);
		}
	}
	// ------------------------------------------------------------------------------------------------------------------------------
}
