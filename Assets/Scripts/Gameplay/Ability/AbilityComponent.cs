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


    public virtual void Initialize(CharacterInstance character)
    {
        _character = character;
    }

    public abstract void Activate();

    #region getters & setter
    public List<Multiplier> AbilityMultipliers { set => _ability_multipliers = value; }
    public ComponentType Type { get => _type; }

    public bool Initialzied { get => _character != null; }
    #endregion
}

