using System.Collections.Generic;
using UnityEngine;
using AYellowpaper.SerializedCollections;

/*
    Notes: 
        Trying to keep it flexible right now. Rn there's virtually no distinction between
        this class and Passive components minus the set even trigger functionalities.
 */


public abstract class Ability_ActiveComponent : MonoBehaviour
{
    protected SerializedDictionary<string, List<Multiplier>> ability_multipliers;

    public void Initialize(SerializedDictionary<string, List<Multiplier>> multipliers)
    {
        ability_multipliers = multipliers;
        OnInitialize();
    }

    public abstract void OnInitialize();
    public abstract void Activate();
}
