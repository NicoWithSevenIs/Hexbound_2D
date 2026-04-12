using UnityEngine;
using System;
using AYellowpaper.SerializedCollections;
using System.Collections.Generic;


public enum ComponentType
{
    ACTIVE,
    PASSIVE,
}

public abstract class AbilityComponent : MonoBehaviour
{
    [SerializeField] protected ComponentType _type;
    [SerializeField] protected List<Multiplier> _ability_multipliers;
    protected CharacterInstance _character;
    protected bool _is_ability_component_active = true;

    public virtual void Initialize(CharacterInstance character)
    {
        _character = character;
    }

    public virtual void Trigger()
    {
        if( _is_ability_component_active)
        {
            Activate();
        }
    }
    protected abstract void Activate();

    #region getters & setters
    public List<Multiplier> AbilityMultipliers { set => _ability_multipliers = value; }
    public ComponentType Type { get => _type; }

    public bool Initialized { get => _character != null; }
    public virtual bool ComponentActive
    {
        get => _is_ability_component_active;
        set
        {
            if(_is_ability_component_active != value)
            {
                _is_ability_component_active = value;
            }
        }
    }
    #endregion
}

