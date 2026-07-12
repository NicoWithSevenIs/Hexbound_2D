using UnityEngine;

public interface IOnStratumSwitched : ICharacterEvent
{
    public void OnStratumSwitched(CharacterInstance character, Stratum entry_stratum, Stratum departing_stratum);
}
