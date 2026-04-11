using UnityEngine;

public interface IOnBasicAttack : ICharacterEvent
{
    public void IOnBasicAttack(CharacterInstance ch, bool is_heavy, bool is_aerial);
}
