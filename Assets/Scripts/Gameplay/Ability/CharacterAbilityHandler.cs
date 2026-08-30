using UnityEngine;

public class CharacterAbilityHandler : MonoBehaviour
{
    [SerializeField] private Transform base_container;
    [SerializeField] private Transform somato_container;
    [SerializeField] private Transform onero_container;
    [SerializeField] private Transform aether_container;

    private Ability base_passive;
    private Ability somato_passive;
    private Ability onero_passive;
    private Ability aether_passive;

    private Ability base_active;
    private Ability somato_active;
    private Ability onero_active;
    private Ability aether_active;

    public void LoadCharacterAbilities(Character character)
    {
        
    }

    public void BaseActive()
    {
        if(base_active != null)
        {
            base_active.TriggerActiveEffect();
        }
        else
        {
            Debug.LogError("No Base Active Registered");
        }
    }

    public void SomatoActive()
    {
        if (somato_active != null)
        {
            somato_active.TriggerActiveEffect();
        }
        else
        {
            Debug.LogError("No Somato Active Registered");
        }
    }

    public void OneroActive()
    {
        if (onero_active != null)
        {
            onero_active.TriggerActiveEffect();
        }
        else
        {
            Debug.LogError("No Onero Active Registered");
        }
    }

    public void AetherActive()
    {
        if (aether_active != null)
        {
            aether_active.TriggerActiveEffect();
        }
        else
        {
            Debug.LogError("No Onero Active Registered");
        }
    }
}
