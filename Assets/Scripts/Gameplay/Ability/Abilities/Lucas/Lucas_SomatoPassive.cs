using UnityEngine;

public class Lucas_SomatoPassive : AbilityComponent, IOnBasicAttack
{
    public void OnBasicAttack(CharacterInstance ch, bool is_heavy, bool is_aerial)
    {
        Trigger();
    }

    protected override void Activate()
    {
        Debug.Log("Somato Passive: I can only activate if it's the current path");
    }
}
