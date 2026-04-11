using UnityEngine;

public interface IOnDash : ICharacterEvent
{
    public void OnDash(CharacterInstance ch, float effective_dash_force);
}
