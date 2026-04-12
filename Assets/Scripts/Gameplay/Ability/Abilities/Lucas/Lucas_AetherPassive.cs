using UnityEngine;

public class Lucas_AetherPassive : AbilityComponent, IOnBasicAttack
{
    public void OnBasicAttack(CharacterInstance ch, bool is_heavy, bool is_aerial)
    {
        Activate();
    }

    protected override void Activate()
    {
        if (_character.IsActive)
        {
            Debug.Log("Aether Passive: I'm persistent between Paths");
        }
    }
}
