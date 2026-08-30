using UnityEngine;

public interface IOnPlungingAttack: ICharacterEvent
{
    public void OnPlungingAttack(CharacterInstance ch, int level);
}
