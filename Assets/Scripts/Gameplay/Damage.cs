
using System.Collections.Generic;

public enum Damage_Tag
{
    TRUE_DAMAGE,
    ADDITIONAL_DAMAGE,
}

public interface IDamageable
{
    public void ReceiveDamage(float dmg, List<Damage_Tag> damage_tags, IDamageable source);
}

public class Damage
{
    public float amount;
    public List<Damage_Tag> damage_tags;
    public IDamageable source;
    public IDamageable target;
}


public class DamageManager: SingletonBehaviour<DamageManager>
{
    public Queue<Damage> damage_queue = new();

    public static void ApplyDamage(float amt, IDamageable target, List<Damage_Tag> damage_tags = null, IDamageable source = null)
    {
        var dmg = new Damage()
        {
            amount = amt,
            damage_tags = damage_tags,
            source = source,
            target = target
        };
        Instance.damage_queue.Enqueue(dmg);
    }
    private void Update()
    {
        //wip
        while (damage_queue.Count > 0)
        {
            var dmg = damage_queue.Dequeue();
            dmg.target.ReceiveDamage(dmg.amount, dmg.damage_tags, dmg.source);
        }
    }

}