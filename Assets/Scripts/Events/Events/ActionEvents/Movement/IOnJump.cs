using UnityEngine;

public interface IOnJump : ICharacterEvent
{
    public void OnJump(CharacterInstance ch, float effective_jump_force);
}
