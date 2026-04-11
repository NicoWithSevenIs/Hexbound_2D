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

    public abstract void Activate();
    public virtual void OnSetAbilityActive(bool value)
    {

    }

    #region getters & setters
    public List<Multiplier> AbilityMultipliers { set => _ability_multipliers = value; }
    public ComponentType Type { get => _type; }

    public bool Initialized { get => _character != null; }
    public bool ComponentActive
    {
        get => _is_ability_component_active;
        set
        {
            if(_is_ability_component_active == value)
            {
                return;
            }
            _is_ability_component_active= value;
            OnSetAbilityActive(value);
        }
    }
    #endregion
}

