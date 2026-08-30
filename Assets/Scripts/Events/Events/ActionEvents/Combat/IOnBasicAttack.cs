using System.Collections.Generic;
using UnityEngine;

public interface IOnBasicAttack : ICharacterEvent
{
    public void OnBasicAttack(CharacterInstance ch, List<IDamageable> hit,  bool is_heavy, bool is_aerial);
}
