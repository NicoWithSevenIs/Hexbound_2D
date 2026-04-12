using UnityEngine;

public class Lucas_OneroPassive : AbilityComponent, IOnBasicAttack
{
    public void OnBasicAttack(CharacterInstance ch, bool is_heavy, bool is_aerial)
    {
        Activate();
    }

    protected override void Activate()
    {
        Debug.Log("Onero Passive: I'm persisistent between characters and paths");
    }
}
