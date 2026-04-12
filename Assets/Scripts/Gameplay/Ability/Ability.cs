using AYellowpaper.SerializedCollections;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


/*
    Future Case:
    -> Multiplier Modification
 
 */

public partial class Ability : MonoBehaviour
{
    [Header("Info")]
    [SerializeField] private string _ability_name;

}

public partial class Ability
{

    [Header("Combat Data")]
    [SerializeField]
    [SerializedDictionary("Action Name", "Multipliers")]
    private List<Multiplier> ability_multipliers;

    private List<AbilityComponent> ability_components;
    private List<AbilityComponent> actives = new();
    [SerializeField] private bool is_ability_active = true;

    public void Initialize(CharacterInstance character)
    {
        ability_components = new(GetComponentsInChildren<AbilityComponent>());

        foreach (var component in ability_components)
        {
            component.AbilityMultipliers = ability_multipliers;
            component.Initialize(character);
            if (component.Type == ComponentType.ACTIVE)
            {
                actives.Add(component);
            }
        }
    }

    public void TriggerActiveEffect()
    {
        foreach(var active in actives)
        {
            active.Trigger();
        }
    }

    public bool AbilityActive
    {
        get => is_ability_active;
        set {
            is_ability_active = value;
            gameObject.SetActive(value);
            foreach (var ability_component in ability_components)
            {
                ability_component.ComponentActive = value;
            }
        }
    }
}


public partial class Ability
{
    public string AbilityName { get => _ability_name; }
}