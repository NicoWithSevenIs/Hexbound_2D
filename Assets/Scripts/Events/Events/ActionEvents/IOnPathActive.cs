using UnityEngine;

public interface IOnPathAbility: ICharacterEvent
{
    public void OnPathAbility(CharacterInstance ch, Path path);
}
