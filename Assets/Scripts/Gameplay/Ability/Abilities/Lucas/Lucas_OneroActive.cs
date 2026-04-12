using UnityEngine;

public class Lucas_OneroActive : AbilityComponent
{
    protected override void Activate()
    {
        if (!gameObject.activeSelf || !Initialized)
            return;
        Debug.Log("Onero Active");
    }
}
