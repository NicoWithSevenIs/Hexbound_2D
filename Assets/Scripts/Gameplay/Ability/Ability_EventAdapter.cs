using UnityEngine;

public class Ability_EventAdapter : MonoBehaviour
{
    public void TryRegisterAbility(GameObject game_object)
    {
        var ability = game_object.GetComponent<Ability>();
        if(ability != null){
            ability.Initialize();

        }
    }
}
