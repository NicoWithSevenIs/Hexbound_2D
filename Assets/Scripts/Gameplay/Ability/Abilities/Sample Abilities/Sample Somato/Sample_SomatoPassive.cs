using UnityEngine;

public class Sample_SomatoPassive : AbilityComponent
{
    public override void Activate()
    {
        if (!gameObject.activeSelf || !Initialized)
            return;
        Debug.Log("Somato Passive");
    }
}
