using UnityEngine;

public class Lucas_AetherActive : AbilityComponent
{
    protected override void Activate()
    {
        if (!gameObject.activeSelf || !Initialized)
            return;
        Debug.Log("Aether Active");
    }
}