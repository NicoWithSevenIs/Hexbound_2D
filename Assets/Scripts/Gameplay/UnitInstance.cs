using UnityEngine;
using Hexbound.Stats;
public class Unit_Instance : MonoBehaviour, IDamageable
{
    protected Basic_Stat stats;
    protected Basic_Stat current_stats;
    protected Basic_Stat flat_bonus;
    protected Basic_Stat percent_bonus;

    public void Load(Unit u)
    {
        stats = u.base_stats;
        current_stats = u.base_stats;
        flat_bonus = new();
        percent_bonus = new();
    }

    public virtual void ReceiveDamage(float dmg)
    {

    }
}

