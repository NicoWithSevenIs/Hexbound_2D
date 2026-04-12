using System;

[Serializable]
public class AbilitySet
{
    public Ability passive;
    public Ability active;

    public void SetActive(bool value)
    {
        if (passive != null)
        {
            passive.AbilityActive = value;
        }

        if (active != null)
        {
            active.AbilityActive = value;
        }
    }
}