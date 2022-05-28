using UnityEngine;
using UnityEngine.Events;

public class EventMessage {}
// ------------------------------------------------------------------------------------------------------------------------------
public class EventGameObjectMessage : EventMessage
{
    public GameObject MessageGameObject;
}
// ------------------------------------------------------------------------------------------------------------------------------
public class EventFloatMessage : EventMessage
{
    public float MessageFloat;
}
// ------------------------------------------------------------------------------------------------------------------------------
public class EventIntMessage : EventMessage
{
    public int MessageInt;
}
// ------------------------------------------------------------------------------------------------------------------------------
public class EventBoolMessage : EventMessage
{
    public bool Value;
}
// ------------------------------------------------------------------------------------------------------------------------------
[System.Serializable]
public class EventObject : UnityEvent<EventMessage>
{
    
}
// ------------------------------------------------------------------------------------------------------------------------------