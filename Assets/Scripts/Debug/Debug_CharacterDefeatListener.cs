using UnityEngine;

public class Debug_CharacterDefeatListener : MonoBehaviour, IOnCharacterDefeated
{
    public void OnCharacterDefeated()
    {
        Debug.Log("Dead");
    }
}
