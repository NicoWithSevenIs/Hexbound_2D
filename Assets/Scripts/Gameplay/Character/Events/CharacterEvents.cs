using System;
using System.Collections.Generic;
using UnityEngine;


public interface ICharacterEvent {}

public class CharacterEvents : MonoBehaviour
{
    private Dictionary<Type, List<GameObject>> listeners = new();
    
    public void Register<TEvent>(GameObject listener) where TEvent : ICharacterEvent
    {
        Type Key = typeof(TEvent);

        if(listener.GetComponent(Key) == null)
        {
            return;
        }

        if (!listeners.ContainsKey(Key))
        {
            listeners[Key] = new();
        }

        if (!listeners[Key].Contains(listener))
        {
            listeners[Key].Add(listener);
        }
    }

    public void DoOnListeners<TEvent>(Action<TEvent> predicate) where TEvent : ICharacterEvent
    {
        Type Key = typeof(TEvent);

        bool contains_key = listeners.ContainsKey(Key);

        if (!contains_key)
        {
            return;
        }

        Utilities.RemoveAll(listeners[Key], gameobject => gameobject == null);

        foreach (var listener in listeners[Key])
        {
            var component = listener.GetComponent<TEvent>();
            predicate(component);
        }
    }

    public void ClearListeners<TEvent>() where TEvent: ICharacterEvent
    {
        Type Key = typeof(TEvent);

        if (listeners.ContainsKey(Key))
        {
            listeners[Key].Clear();
        }
    }
}
