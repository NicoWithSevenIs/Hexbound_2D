using Hexbound.Stats;
using System;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;


public partial class CharacterCombatController : CharacterControlHandler, IOnCharacterLoaded, IOnPathSwitched, IOnCharacterSwitched
{

    [SerializeField] private float global_min_hold_duration = 0.6f;
    [SerializeField] private float global_attack_loop_reset = 0.3f;

    [Header("Debug")]
    [SerializeField] private int basic_grounded_loop_count = 0;
    [SerializeField] private int basic_aerial_loop_count = 0;
    [SerializeField] private int heavy_grounded_loop_count = 0;
    [SerializeField] private int heavy_aerial_loop_count = 0;


    private Timer attack_look_falloff;

    private void Start()
    {
        void Reset()
        {
            basic_grounded_loop_count = 0;
            basic_aerial_loop_count = 0;
            heavy_grounded_loop_count = 0;
            heavy_aerial_loop_count = 0;

        }
        attack_look_falloff = new(global_attack_loop_reset, Reset, false);
        attack_look_falloff.Stop();
    }

    private void Update()
    {
        attack_look_falloff.Tick();
    }

    public void TriggerBasicAttack(float input_duration)
    {
        bool is_aerial = !IsGrounded();
        bool is_heavy = input_duration > global_min_hold_duration;


        if (!is_aerial && !is_heavy)
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
        else if (!is_aerial && is_heavy)
        {
            Debug.Log("Grounded Heavy");
        }
        else if(is_aerial && !is_heavy)
        {
            Debug.Log("Aerial Basic");
        }
        else if (is_aerial && is_heavy)
        {
            Debug.Log("Aerial Heavy");
        }


        HandleAttackLoopCount(is_heavy, is_aerial);
        attack_look_falloff.Restart();
        ev.DoOnListeners<IOnBasicAttack>(listener => listener.OnBasicAttack(ch, is_heavy, is_aerial));

    }

    private void HandleAttackLoopCount(bool is_heavy, bool is_aerial)
    {
        basic_grounded_loop_count = !is_heavy && !is_aerial? basic_grounded_loop_count + 1 : 0;
        basic_grounded_loop_count %= ch.CharacterData.grounded_basic.hit_count + 1;

        basic_aerial_loop_count = !is_heavy && is_aerial ? basic_aerial_loop_count + 1 : 0;
        basic_aerial_loop_count %= ch.CharacterData.aerial_basic.hit_count + 1;

        heavy_grounded_loop_count = is_heavy && !is_aerial ? heavy_grounded_loop_count + 1 : 0;
        heavy_grounded_loop_count %= ch.CharacterData.grounded_heavy.hit_count + 1;

        heavy_aerial_loop_count = is_heavy && is_aerial ? heavy_aerial_loop_count + 1 : 0;
        heavy_aerial_loop_count  %= ch.CharacterData.aerial_heavy.hit_count + 1;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
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

    private AbilitySet _base_set = new();
    private AbilitySet _somato_set = new();
    private AbilitySet _onero_set = new();
    private AbilitySet _aether_set = new();

    private Ability _ultimate;

    private Dictionary<Path, AbilitySet> path_ability_lookup = new();

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

    public void TriggerBaseActive(float input_duration)
    {
        _base_set.active.TriggerActiveEffect();
    }

    public void TriggerPathActive(float input_duration)
    {
        path_ability_lookup[ch.CurrentPath].active.TriggerActiveEffect();
    }

    #region character events
    public void OnCharacterLoaded(CharacterInstance character1, CharacterInstance character2)
    {
        if (!ch.Loaded)
        {
            return;
        }

        var data = ch.CharacterData;
        InitializeAbility(data.base_abilities.passive, out _base_set.passive, _base);
        InitializeAbility(data.base_abilities.active, out _base_set.active, _base);

        InitializeAbility(data.somato_abilities.passive, out _somato_set.passive, _somato);
        InitializeAbility(data.somato_abilities.active, out _somato_set.active, _somato);

        InitializeAbility(data.onero_abilities.passive, out _onero_set.passive, _onero);
        InitializeAbility(data.onero_abilities.active, out _onero_set.active, _onero);

        InitializeAbility(data.aether_abilities.passive, out _aether_set.passive, _aether);
        InitializeAbility(data.aether_abilities.active, out _aether_set.active, _aether);

        InitializeAbility(data.ultimate, out _ultimate, _base);

        InjectDependencies();

        path_ability_lookup[Path.SOMATO] = new AbilitySet{
           active =  _somato_set.active,
           passive = _somato_set.passive,
        };
        path_ability_lookup[Path.ONERO] = new AbilitySet
        {
            active = _onero_set.active,
            passive = _onero_set.passive,
        };
        path_ability_lookup[Path.AETHER] = new AbilitySet
        {
            active = _aether_set.active,
            passive = _aether_set.passive,
        };

        //InitializeAbilityInstances();
    }

    public void OnCharacterSwitched(CharacterInstance entering, CharacterInstance departing)
    {
        InitializeAbilityInstances();
    }
    
    public void OnPathSwitched(CharacterInstance character, Path entry_path, Path departing_path)
    {
        if (!ch.IsActive)
        {
            return;
        }
        foreach (var path in Paths.Values)
        {
            path_ability_lookup[path].SetActive(path == entry_path);
        }
    }
    #endregion
    #region helper methods

    private void InitializeAbilityInstances()
    {
        _base_set.SetActive(ch.IsActive);
        _ultimate.AbilityActive = ch.IsActive;
        foreach (var path in Paths.Values)
        {
            path_ability_lookup[path].SetActive(ch.IsActive && path == ch.CurrentPath);
            path_ability_lookup[path].SetActive(ch.IsActive && path == ch.CurrentPath);
        }
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
            _base_set.passive,
            _base_set.active,
            _somato_set.passive,
            _somato_set.active,
            _onero_set.passive,
            _onero_set.active,
            _aether_set.passive,
            _aether_set.active,
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

    #endregion
}