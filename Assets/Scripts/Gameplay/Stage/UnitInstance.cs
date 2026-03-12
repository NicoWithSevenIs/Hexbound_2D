using Hexbound.Stats;
using System.Collections.Generic;
using UnityEngine;
public class Unit_Instance : MonoBehaviour, IDamageable
{
    protected Basic_Stat stats;
    protected Basic_Stat current_stats;

    public void Load(Unit u)
    {
        stats = u.base_stats;
        current_stats = u.base_stats;
    }

    public virtual void ReceiveDamage(float dmg, List<Damage_Tag> damage_tags, IDamageable source)
    {

    }
}

