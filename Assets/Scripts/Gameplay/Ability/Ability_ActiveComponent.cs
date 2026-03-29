using System.Collections.Generic;
using UnityEngine;
using AYellowpaper.SerializedCollections;


public abstract class Ability_ActiveComponent : MonoBehaviour, IAbilityComponent
{
    public abstract void Initialize(SerializedDictionary<string, List<Multiplier>> multipliers);
    public abstract void TriggerEffect();
}
