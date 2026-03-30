using UnityEngine;
using System;
using AYellowpaper.SerializedCollections;
using System.Collections.Generic;




public abstract partial class AbilityComponent : MonoBehaviour, IEventAdapterListener
{
    public IEventAdapter EventAdapter { get; set; }


    public enum ComponentType
    {
        ACTIVE, 
        PASSIVE,
    }

    #region attributes
    private Type[] _event_triggers;
    [SerializeField] private ComponentType _component;
    protected SerializedDictionary<string, List<Multiplier>> _ability_multipliers;
    private bool _initialized = false;
    #endregion

    public void Initialize(SerializedDictionary<string, List<Multiplier>> multipliers) 
    { 
        _ability_multipliers = multipliers;
        SetEventTriggers();
        EventAdapter.TryRegister(this, _event_triggers);
        _initialized = true;
    }

    public void OnEnable()
    {
        if (_initialized)
        {
            EventAdapter.TryRegister(this, _event_triggers);
        }
    }

    public void OnDisable()
    {
        if (_initialized)
        {
            EventAdapter.TryUnregister(this);
        }
    }

    public ComponentType Type { get => _component; }
}

#region abstract methods
public partial class AbilityComponent
{
    protected abstract void SetEventTriggers();
    public abstract void Trigger();
}
#endregion

#region event trigger helpers
public partial class AbilityComponent
{
    protected void _OnSetEventTriggers<T1>()
        where T1 : IEvent
     => _event_triggers = new Type[] { typeof(T1) };

    protected void _OnSetEventTriggers<T1, T2>()
        where T1 : IEvent where T2 : IEvent
        => _event_triggers = new Type[] { typeof(T1), typeof(T2) };

    protected void _OnSetEventTriggers<T1, T2, T3>()
        where T1 : IEvent where T2 : IEvent  where T3 : IEvent
        => _event_triggers = new Type[] { typeof(T1), typeof(T2), typeof(T3) };

    protected void _OnSetEventTriggers<T1, T2, T3, T4>()
        where T1 : IEvent where T2 : IEvent where T3 : IEvent 
        where T4 : IEvent
        => _event_triggers = new Type[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4) };

    protected void _OnSetEventTriggers<T1, T2, T3, T4, T5>()
        where T1 : IEvent where T2 : IEvent where T3 : IEvent 
        where T4 : IEvent where T5 : IEvent
        => _event_triggers = new Type[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5) };

    protected void _OnSetEventTriggers<T1, T2, T3, T4, T5, T6>()
        where T1 : IEvent where T2 : IEvent where T3 : IEvent 
        where T4 : IEvent where T5 : IEvent where T6 : IEvent
        => _event_triggers = new Type[] { typeof(T1), typeof(T2), typeof(T3), typeof(T4), typeof(T5), typeof(T6) };

}
#endregion