using System;
using System.Collections.Generic;
using UnityEngine;


public interface PassiveAbilityComponent 
{
    public void Initialize();
}
public interface ActiveAbilityComponent {
    public void TriggerEffect();
}

#region Effect Trigger

public partial class Ability: MonoBehaviour
{
    private List<PassiveAbilityComponent> passives;
    private List<ActiveAbilityComponent> actives;

    private void Start()
    {
        passives = new(GetComponentsInChildren<PassiveAbilityComponent>());
        actives = new(GetComponentsInChildren<ActiveAbilityComponent>());
        Initialize();
    }

    private void Initialize()
    {
        foreach (var passive in passives)
        {
            passive.Initialize();
        }
    }

    public void TriggerActiveEffect()
    {
        foreach(var active in actives)
        {
            active.TriggerEffect();
        }
    }
}
#endregion

public partial class Ability
{

}