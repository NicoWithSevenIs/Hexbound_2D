
using System.Collections.Generic;

public enum Damage_Tag
{
    TRUE_DAMAGE,
    ADDITIONAL_DAMAGE,
}

public interface IDamageable
{
    public void ReceiveDamage(float dmg);
}

public class Damage
{
    public float amount;
    public List<Damage_Tag> damage_tags;
    public Unit source;
    public IDamageable target;
}


public class DamageManager
{
    public Queue<Damage> damage_queue;

    public void ApplyDamage(float amt, List<Damage_Tag> damage_tags, IDamageable target, Unit source = null)
    {
        var dmg = new Damage()
        {
            amount = amt,
            damage_tags = damage_tags,
            source = source,
            target = target
        };
        damage_queue.Enqueue(dmg);
    }
}