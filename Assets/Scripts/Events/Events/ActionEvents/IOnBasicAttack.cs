using UnityEngine;

public interface IOnBasicAttack : ICharacterEvent
{
    public void OnBasicAttack(CharacterInstance ch, bool is_heavy, bool is_aerial);
}
