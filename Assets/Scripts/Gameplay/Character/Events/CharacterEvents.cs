using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public interface ICharacterEvent {}

//modify to get all components
public partial class CharacterEvents : MonoBehaviour
{
    private Dictionary<Type, List<GameObject>> listeners = new();
    
    private void Register<TEvent>(GameObject listener) where TEvent : ICharacterEvent
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
            Debug.Log($"Adding {gameObject.name} to {Key.Name}");
            listeners[Key].Add(listener);
        }
    }

    public void DoOnListeners<TEvent>(Action<TEvent> executor) where TEvent : ICharacterEvent
    {
        Type Key = typeof(TEvent);

        bool contains_key = listeners.ContainsKey(Key);

        if (!contains_key)
        {
            Debug.Log($"No key found for {Key.Name}");
            return;
        }

        Utilities.RemoveAll(listeners[Key], gameobject => gameobject == null);

       
        foreach (var listener in listeners[Key])
        {
            var components = listener.GetComponents<TEvent>();
            for(int i =0; i < components.Length; i++)
            {
                executor(components[i]);
            }
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


public partial class CharacterEvents
{
    public void HookUpCharacterEvents(GameObject instance)
    {
        Register<IOnCharacterDefeated>(instance);
        Register<IOnCharacterLoaded>(instance);
        Register<IOnCharacterSwitched>(instance);
    }
}