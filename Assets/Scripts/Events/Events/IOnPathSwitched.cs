using UnityEngine;

public interface IOnPathSwitched : ICharacterEvent
{
    public void OnPathSwitched(CharacterInstance character, Path entry_path, Path departing_path);
}
