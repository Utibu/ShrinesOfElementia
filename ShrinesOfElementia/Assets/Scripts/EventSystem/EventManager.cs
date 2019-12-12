//Author: Joakim Ljung

using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }

    private void Awake()
    {
        // Prevents multiple instances
        if (Instance == null) { Instance = this; }
        else { Debug.Log("Warning: multiple " + this + " in scene!"); }
    }

    delegate void EventListener(EventClass e);
    Dictionary<System.Type, HashSet<EventHandler>> eventListeners;

    public void RegisterListener<T>(System.Action<T> listener) where T : EventClass
    {
        System.Type eventType = typeof(T);
        var target = listener.Target;
        var method = listener.Method;

        if (eventListeners == null)
        {
            eventListeners = new Dictionary<System.Type, HashSet<EventHandler>>();
        }

        if (!eventListeners.ContainsKey(eventType) || eventListeners[eventType] == null)
        {
            eventListeners[eventType] = new HashSet<EventHandler>();
        }


        eventListeners[eventType].Add(new EventHandler { Target = target, Method = method });
    }

    public void UnregisterListener<T>(System.Action<T> listener) where T : EventClass
    {
        System.Type eventType = typeof(T);
        if (eventListeners == null || !eventListeners.ContainsKey(eventType) || eventListeners[eventType] == null)
        {
            return;
        }
        eventListeners[eventType].RemoveWhere(item => item.Target == listener.Target && item.Method == listener.Method);
    }

    public void FireEvent(EventClass eventInfo)
    {
        System.Type trueEventType = eventInfo.GetType();
        if (eventListeners == null || !eventListeners.ContainsKey(trueEventType) || eventListeners[trueEventType] == null)
        {
            return;
        }

        foreach (EventHandler handler in eventListeners[trueEventType])
        {
            handler.Method.Invoke(handler.Target, new[] { eventInfo });
        }
    }

    public void ClearListener<T>() where T : EventClass
    {
        System.Type eventType = typeof(T);
        if (eventListeners == null || !eventListeners.ContainsKey(eventType))
        {
            return;
        }

        eventListeners.Remove(eventType);
    }

    public class EventHandler
    {
        public object Target { get; set; }
        public System.Reflection.MethodInfo Method { get; set; }
    }
}
