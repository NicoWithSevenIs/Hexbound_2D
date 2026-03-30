using AYellowpaper.SerializedCollections;
using System;
using System.Collections.Generic;
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

    private List<Ability_PassiveComponent> passives;
    private List<Ability_ActiveComponent> actives;

    public void Initialize(IEventAdapter adapter)
    {
        passives = new(GetComponentsInChildren<Ability_PassiveComponent>());
        actives = new(GetComponentsInChildren<Ability_ActiveComponent>());
        foreach (var passive in passives)
        {
            passive.EventAdapter = adapter;
            passive.Initialize(ability_multipliers);
        }
        foreach (var active in actives)
        {
            active.Initialize(ability_multipliers);
        }
    }

    public void TriggerActiveEffect()
    {
        foreach(var active in actives)
        {
            active.Activate();
        }
    }


    public List<Ability_PassiveComponent> PassiveComponents { get => passives; }
    public List<Ability_ActiveComponent> ActiveComponents { get => actives; }
}
