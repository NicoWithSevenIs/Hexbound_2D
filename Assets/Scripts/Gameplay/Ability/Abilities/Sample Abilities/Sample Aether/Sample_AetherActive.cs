using UnityEngine;

public class Sample_AetherActive : AbilityComponent
{
    public override void Activate()
    {
        if (!gameObject.activeSelf || !Initialized)
            return;
        Debug.Log("Aether Active");
    }
}