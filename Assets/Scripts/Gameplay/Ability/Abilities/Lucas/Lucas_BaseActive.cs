using UnityEngine;

public class Lucas_BaseActive : AbilityComponent
{
    [SerializeField] private string _base_passive_name;

    protected override void Activate()
    {
        var dependency_receiver = GetComponent<AbilityDependencyReceiver>();
        var passive = dependency_receiver[_base_passive_name].GetComponent<Lucas_BasePassive>();

        Debug.Log($"From Base Active, Switch Count {passive.SwitchCount}");

    }
}
