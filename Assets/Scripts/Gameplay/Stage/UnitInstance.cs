using Hexbound.Stats;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public abstract class UnitInstance<U> : MonoBehaviour, IDamageable, IUnitInstance
    where U : Unit
{
    protected U unit;
    protected Stats current_stats;

    public abstract void ReceiveDamage(float dmg, List<Damage_Tag> damage_tags, IDamageable source);
}
