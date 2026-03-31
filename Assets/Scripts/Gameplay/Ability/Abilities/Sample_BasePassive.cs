using UnityEngine;

public class Sample_BasePassive : AbilityComponent, IOnCharacterSwitched
{

    [SerializeField] private int _switch_count = 0;
    [SerializeField] private int _max = 3;

    public override void Activate()
    {
        if (!gameObject.activeSelf || !Initialzied)
            return;

        _switch_count = ++_switch_count % _max;
    }

    public void OnCharacterSwitched(CharacterInstance entering, CharacterInstance departing)
    {
        Activate();
    }

    public int SwitchCount { get => _switch_count; }
 
}
