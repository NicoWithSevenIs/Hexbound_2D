using Hexbound.Stats;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnemyInstance : UnitInstance<Enemy>
{
    #region events
    public UnityEvent<float,float,float> _ev;
    #endregion

    [SerializeField] private Enemy _enemy;

    public void Start()
    {
        Load(_enemy);
    }

    private void Load(Enemy _enemy)
    {
        unit = _enemy;
        current_stats = new(_enemy.base_stats);
    }

    public override void ReceiveDamage(float dmg, List<Damage_Tag> damage_tags, IDamageable source)
    {
        current_stats[StatType.HP] = Mathf.Max(0, current_stats[StatType.HP]-dmg);    
        _ev?.Invoke(dmg, current_stats[StatType.HP], unit.base_stats[StatType.HP]);
     
        if (current_stats[StatType.HP] == 0)
        {
            Destroy(gameObject);
        }
        
    }

}
