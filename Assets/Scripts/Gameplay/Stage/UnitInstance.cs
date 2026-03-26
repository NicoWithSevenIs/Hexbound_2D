using Hexbound.Stats;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public partial class UnitInstance : MonoBehaviour, IDamageable
{
    private Unit unit;
    protected Stats current_stats;

    public void Load(Unit u)
    {
        unit = u;
        current_stats = new(u.stats);
    }

    public Unit debug_unit;

    private void Start()
    {
        Load(debug_unit);
    }

    public virtual void ReceiveDamage(float dmg, List<Damage_Tag> damage_tags, IDamageable source)
    {
        if (current_stats[StatType.HP] == 0)
        {
            return;
        }

        current_stats[StatType.HP] = Mathf.Max(current_stats[StatType.HP] - dmg, 0);
        OnDamageTaken?.Invoke(dmg, current_stats[StatType.HP], unit.stats[StatType.HP]);

        if (current_stats[StatType.HP] == 0)
        {
            Destroy(gameObject); //temp
        }
    }


    public UnityEvent<float, float, float> OnDamageTaken;
}

public partial class UnitInstance
{
    public Stats BaseStats { get => new(unit.stats); }
    public Stats CurrentStats { get => current_stats; }
}