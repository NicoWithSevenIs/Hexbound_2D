using Hexbound.Stats;
using System.Collections.Generic;
using UnityEngine;


public class CharacterCombatController : CharacterController, IOnCharacterLoaded
{
   
    [SerializeField] private float global_min_hold_duration = 0.6f;

    private Ability _base_passive;
    private Ability _base_active;
    

    public void TriggerBasicAttack(float input_duration)
    {
        bool is_aerial = !IsGrounded();
        bool is_held = input_duration > global_min_hold_duration;

        if (!is_aerial && !is_held)
        {
            Debug.Log("Grounded Basic");

            var colls = Physics2D.OverlapBoxAll(transform.position + transform.right * Mathf.Sign(transform.localScale.x) * 3f, Vector3.one * 5f, 0);
            foreach(var coll in colls)
            {
                var damageable =  coll.GetComponent<IDamageable>();
                if(damageable != null)
                {
                    damageable.ReceiveDamage(ch.CurrentStats[StatType.ATK], null, ch);
                }
            }
            
        }
        else if (!is_aerial && is_held)
        {
            Debug.Log("Grounded Heavy");
        }
        else if(is_aerial && !is_held)
        {
            Debug.Log("Aerial Basic");
        }
        else if (is_aerial && is_held)
        {
            Debug.Log("Aerial Heavy");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        var scale = transform.localScale;
        Gizmos.DrawWireCube(transform.position + transform.right * Mathf.Sign(transform.localScale.x) * 3f, Vector3.one * 5f);
    }


    //wip, plunge should be automatically triggered when global_min_hold_duration is met rather than on key up
    public void TriggerPlunge(float input_duration)
    {
        if (!IsGrounded() && input_duration > global_min_hold_duration)
        {
            Debug.Log("Plunging");
        }
    }

    public void TriggerBaseActive(float input_duration)
    {
        _base_active.TriggerActiveEffect();
    }

    protected override void Awake()
    {
        base.Awake();
        print("initialized");
        print(ch);
    }

    public void OnCharacterLoaded(CharacterInstance character1, CharacterInstance character2)
    {
        if (!ch.Loaded)
        {
            return;
        }
        var data = ch.CharacterData;
        InitializeAbility(data.base_abilities.passive, out _base_passive);
        InitializeAbility(data.base_abilities.active, out _base_active);
    }

    private void InitializeAbility(Ability ability_data, out Ability ability)
    {
        var instance = WorldManager.Create(ability_data.gameObject, transform);
        ability = instance.GetComponent<Ability>();
        ability.Initialize(ch);

        Debug.Log($"Loaded {ability.AbilityName} for {ch.CharacterData.unit_name}");

        var dependency_receiver = instance.GetComponent<AbilityDependencyReceiver>();
        if (dependency_receiver == null)
        {
            return;
        }

        var dependency_list = new List<Ability>()
        {
            _base_passive,
            _base_active
        };

        foreach(var dependency in dependency_list)
        {
            if(ability != dependency)
            {
                dependency_receiver.SetDependency(dependency);
            }
        }

    }

}
