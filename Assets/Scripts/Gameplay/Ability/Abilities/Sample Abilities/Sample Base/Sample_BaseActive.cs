using UnityEngine;

public class Sample_BaseActive : AbilityComponent
{
    [SerializeField] private string _base_passive_name;

    public override void Activate()
    {
        var dependency_receiver = GetComponent<AbilityDependencyReceiver>();
        var passive = dependency_receiver[_base_passive_name].GetComponent<Sample_BasePassive>();

        Debug.Log($"From Base Active, Switch Count {passive.SwitchCount}");

    }
}
