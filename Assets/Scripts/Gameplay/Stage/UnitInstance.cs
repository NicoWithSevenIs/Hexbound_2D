using Hexbound.Stats;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public partial class UnitInstance : MonoBehaviour, IDamageable
{
    protected Basic_Stat base_stats;
    protected Basic_Stat current_stats;

    public void Load(Unit u)
    {
        base_stats = u.base_stats;
        current_stats = new(u.base_stats);
    }

    public Unit debug_unit;

    private void Start()
    {
        Load(debug_unit);
    }

    public virtual void ReceiveDamage(float dmg, List<Damage_Tag> damage_tags, IDamageable source)
    {
        if (current_stats.HP == 0)
        {
            return;
        }

        current_stats.HP = Mathf.Max(current_stats.HP - dmg, 0);
        OnDamageTaken?.Invoke(dmg, current_stats.HP, base_stats.HP);

        if (current_stats.HP == 0)
        {
            Destroy(gameObject); //temp
        }
    }


    public UnityEvent<float, float, float> OnDamageTaken;
}

public partial class UnitInstance
{
    public Basic_Stat BaseStats { get => base_stats; }
    public Basic_Stat CurrentStats { get => current_stats; }
}