using Hexbound.Stats;
using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;


public partial class CharacterCombatController : CharacterController, IOnCharacterLoaded
{
   
    [SerializeField] private float global_min_hold_duration = 0.6f;

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
}

public partial class CharacterCombatController
{
    #region Ability World Buckets
    [Header("Ability Instance Buckets")]
    [SerializeField] private Transform _base;
    [SerializeField] private Transform _somato;
    [SerializeField] private Transform _onero;
    [SerializeField] private Transform _aether;
    #endregion

    private Ability _base_passive;
    private Ability _base_active;

    private Ability _somato_passive;
    private Ability _somato_active;

    private Ability _onero_passive;
    private Ability _onero_active;

    private Ability _aether_passive;
    private Ability _aether_active;

    private Ability _ultimate;

    private Dictionary<Path, Ability> path_ability_lookup = new();

    public void SwitchPaths(int dir)
    {
        dir = math.sign(dir);
        if (dir == 0)
        {
            return;
        }
        var path_index = (int)ch.CurrentPath;
        var path_count = Enum.GetValues(typeof(Path)).Length;
        int next = path_index + dir;
        next = (next % path_count + path_count) % path_count; //wrap around with negative dir in mind
        ch.SwitchPaths((Path)next);
    }

    public void TriggerPathActive(float input_duration)
    {
        path_ability_lookup[ch.CurrentPath].TriggerActiveEffect();
    }

    public void OnCharacterLoaded(CharacterInstance character1, CharacterInstance character2)
    {
        if (!ch.Loaded)
        {
            return;
        }

        var data = ch.CharacterData;
        InitializeAbility(data.base_abilities.passive, out _base_passive, _base);
        InitializeAbility(data.base_abilities.active, out _base_active, _base);

        InitializeAbility(data.somato_abilities.passive, out _somato_passive, _somato);
        InitializeAbility(data.somato_abilities.active, out _somato_active, _somato);

        InitializeAbility(data.onero_abilities.passive, out _onero_passive, _onero);
        InitializeAbility(data.onero_abilities.active, out _onero_active, _onero);

        InitializeAbility(data.aether_abilities.passive, out _aether_passive, _aether);
        InitializeAbility(data.aether_abilities.active, out _aether_active, _aether);

        InitializeAbility(data.ultimate, out _ultimate, _base);

        InjectDependencies();

        path_ability_lookup[Path.SOMATO] = data.somato_abilities.active;
        path_ability_lookup[Path.ONERO] = data.onero_abilities.active;
        path_ability_lookup[Path.AETHER] = data.aether_abilities.active;
    }

    private void InitializeAbility(Ability ability_data, out Ability ability, Transform parent = null)
    {
        var instance = WorldManager.Create(ability_data.gameObject, parent == null ? transform : parent, Vector3.zero);
        ability = instance.GetComponent<Ability>();
        ability.Initialize(ch);
    }

    private void InjectDependencies()
    {

        var ability_list = new List<Ability>()
        {
            _base_passive,
            _base_active,
            _somato_passive,
            _somato_active,
            _onero_passive,
            _onero_active,
            _aether_passive,
            _aether_active,
            _ultimate
        };

        foreach (var ability in ability_list)
        {
            var dependency_receiver = ability.GetComponent<AbilityDependencyReceiver>();
            if (dependency_receiver == null)
            {
                continue;
            }
            foreach (var dependency in ability_list)
            {
                if (ability != dependency)
                {
                    dependency_receiver.SetDependency(dependency);
                }
            }
        }
    }


}