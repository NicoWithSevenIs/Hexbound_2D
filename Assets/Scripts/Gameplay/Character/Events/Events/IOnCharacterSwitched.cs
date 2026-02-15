public interface IOnCharacterSwitched: ICharacterEvent
{
    public void OnCharacterSwitched(CharacterInstance entering, CharacterInstance departing);
}
