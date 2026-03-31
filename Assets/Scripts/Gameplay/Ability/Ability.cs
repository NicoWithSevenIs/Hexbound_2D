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

    private List<AbilityComponent> actives = new();

    public void Initialize(CharacterInstance character)
    {
        var components = GetComponentsInChildren<AbilityComponent>();

        foreach (var component in components)
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
            active.Activate();
        }
    }
}


public partial class Ability
{
    public string AbilityName { get => _ability_name; }
}