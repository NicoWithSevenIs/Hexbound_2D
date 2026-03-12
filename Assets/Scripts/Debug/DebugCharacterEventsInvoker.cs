using UnityEngine;

public class DebugCharacterEventsInvoker : MonoBehaviour, IOnCharacterLoaded, IOnCharacterSwitched
{

    public void OnCharacterLoaded(CharacterInstance character1, CharacterInstance character2)
    {
        Debug.Log($"loaded {character1.CharacterData.unit_name}");
        
        //Debug.Log($"loaded {character2.CharacterData.unit_name}");
    }

    public void OnCharacterSwitched(CharacterInstance entering, CharacterInstance departing)
    {
        Debug.Log($"switched to {entering.CharacterData.unit_name} from {departing.CharacterData.unit_name}");
    }
}
