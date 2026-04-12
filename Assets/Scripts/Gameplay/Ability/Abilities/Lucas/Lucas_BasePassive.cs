using UnityEngine;

public class Lucas_BasePassive : AbilityComponent, IOnCharacterSwitched
{

    [SerializeField] private int _switch_count = 0;
    [SerializeField] private int _max = 3;

    protected override void Activate()
    {
        if (!_character.IsActive || !Initialized)
        {
            Debug.Log("Character Not Active");
            return;
        }

        Debug.Log($"{_character.CharacterData.unit_name} :Switched {_max} times, activating passive");
    }

    public void OnCharacterSwitched(CharacterInstance entering, CharacterInstance departing)
    {
        _switch_count = ++_switch_count % _max;
        if (_switch_count == 0)
        {
            Activate();
        }
    }

    public int SwitchCount { get => _switch_count; }
 
}
