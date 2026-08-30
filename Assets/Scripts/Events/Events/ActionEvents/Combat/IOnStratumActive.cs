using UnityEngine;

public interface IOnStratumActive: ICharacterEvent
{
    public void OnStratumAbility(CharacterInstance ch, Stratum stratum);
}
