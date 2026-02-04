using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//typedefs
using Event_Args = System.Collections.Generic.Dictionary<string, object>;
using Event = System.Action<System.Collections.Generic.Dictionary<string, object>>;

public class EventBroadcaster: Singleton<EventBroadcaster>
{

    private Dictionary<string, List<Event>> events = new();

    private EventBroadcaster()
    {
        SceneManager.sceneUnloaded += s => 
        {
            Debug.Log("Cleaning Up Listeners");
            events.Clear(); 
        };
        SceneManager.sceneLoaded += (s, t) => Debug.Log(events.Count);
    }

    public static void AddObserver(string event_name, Event observer)
    {
        var events = Instance.events;
        bool key_not_found = !events.ContainsKey(event_name);

        if (key_not_found)
            events[event_name] = new();

        events[event_name].Add(observer);
    }

    public static void RemoveObserver(string event_name, Event observer)
    {
        var events = Instance.events;

        if (events.ContainsKey(event_name))
            events[event_name].Remove(observer);
    }

    public static void InvokeEvent(string event_name, Event_Args args = null)
    {
        var events = Instance.events;
        bool key_not_found = !events.ContainsKey(event_name);

        if (key_not_found)
            return;

        foreach(var observer in events[event_name])
        {
            observer?.Invoke(args);
        }
            
    }
    public static void Clear() => Instance.events.Clear();

}
