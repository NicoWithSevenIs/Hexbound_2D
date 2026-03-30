using AYellowpaper.SerializedCollections;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


/*
    Future Case:
    -> Multiplier Modification
 
 */
public class Ability: MonoBehaviour
{
    [SerializeField]
    [SerializedDictionary("Action Name", "Multipliers")]
    private readonly SerializedDictionary<string, List<Multiplier>> ability_multipliers;

    
    private List<AbilityComponent> passives;
    private List<AbilityComponent> actives;

    public void Initialize(IEventAdapter adapter)
    {
        var components = GetComponentsInChildren<AbilityComponent>();
        passives = components.Where(component => component.Type == AbilityComponent.ComponentType.PASSIVE).ToList();
        actives = components.Where(component => component.Type == AbilityComponent.ComponentType.ACTIVE).ToList();

        foreach (var component in components)
        {
            component.EventAdapter = adapter;
            component.Initialize(ability_multipliers);
        }
    }

    public void TriggerActiveEffect()
    {
        foreach(var active in actives)
        {
            active.Trigger();
        }
    }

}
