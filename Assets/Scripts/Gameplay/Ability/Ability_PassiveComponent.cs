using UnityEngine;
using System;
using AYellowpaper.SerializedCollections;
using System.Collections.Generic;


public abstract partial class Ability_PassiveComponent : MonoBehaviour, IAbilityComponent
{
    private Type[] _event_triggers;
    protected SerializedDictionary<string, List<Multiplier>> ability_multipliers;

    protected abstract void OnInitialize();
    public abstract void TriggerEffect();

    public void Initialize(SerializedDictionary<string, List<Multiplier>> multipliers) 
    {
        ability_multipliers = multipliers;
        OnInitialize();
        foreach (var event_trigger in _event_triggers)
        {
            //Connect to dispatcher
        }
    }
}

public partial class Ability_PassiveComponent
{
    protected void SetEventTriggers<T1>()
        where T1 : IEvent
     => _event_triggers = new Type[] { typeof(T1) };

    protected void SetEventTriggers<T1, T2>()
        where T1 : IEvent where T2 : IEvent
        => _event_triggers = new Type[] { typeof(T1), typeof(T2) };

    protected void SetEventTriggers<T1, T2, T3>()
        where T1 : IEvent where T2 : IEvent  where T3 : IEvent
        => _event_triggers = new Type[] { typeof(T1), typeof(T2), typeof(T3) };

    protected void SetEventTriggers<T1, T2, T3, T4>()
        where T1 : IEvent where T2 : IEvent where T3 : IEvent 
        where T4 : IEvent
        => _event_triggers = new Type[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4) };

    protected void SetEventTriggers<T1, T2, T3, T4, T5>()
        where T1 : IEvent where T2 : IEvent where T3 : IEvent 
        where T4 : IEvent where T5 : IEvent
        => _event_triggers = new Type[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5) };

    protected void SetEventTriggers<T1, T2, T3, T4, T5, T6>()
        where T1 : IEvent where T2 : IEvent where T3 : IEvent 
        where T4 : IEvent where T5 : IEvent where T6 : IEvent
        => _event_triggers = new Type[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6) };

}