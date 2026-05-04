using Hexbound.Stats;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public partial class CharacterInstance : UnitInstance<Character>
{
    private Character_Build char_build;
    private Stats build_stats;

    private Path current_path;

    public void Load(Character character, Character_Build build)
    {
        unit = character;
        char_build = build;

        build_stats = unit.base_stats + char_build.bonuses;
        current_stats = new(build_stats);
        Debug.Log(current_stats[StatType.MOVE_SPEED]);
    }

    public override void ReceiveDamage(float dmg, List<Damage_Tag> damage_tags, IDamageable source)
    {
        if (!gameObject.activeSelf || current_stats[StatType.HP] <= 0)
            return;

        current_stats[StatType.HP] = Mathf.Max(0,current_stats[StatType.HP] - dmg);

        if (current_stats[StatType.HP] > 0) 
            return;

        var events = GetComponentInParent<CharacterEvents>();
        events.DoOnListeners<IOnCharacterDefeated>(defeated => defeated.OnCharacterDefeated());
    }

    public void SwitchPaths(Path entry_path)
    {
        if(!is_active) return;
        var departing_path = current_path;
        current_path = entry_path;
        var events = GetComponentInParent<CharacterEvents>();
        events.DoOnListeners<IOnPathSwitched>(listener => listener.OnPathSwitched(this, entry_path, departing_path));
    }
}

#region Getters
public partial class CharacterInstance
{
    public Character CharacterData { get => unit; }
    public Character_Build Build { get => char_build; }
    public Stats BaseStats { get => unit.base_stats; }
    public Stats BuildStats { get => build_stats; }
    public Stats CurrentStats { get => current_stats; }

    public bool Loaded { get => unit != null;  }
    public Path CurrentPath { get => current_path; set => SwitchPaths(value); }

    private bool is_active;
    public bool IsActive 
    { 
        get => is_active; 
        set 
        { 
            is_active = value;
            gameObject.SetActive(value);
        } 
    }
}
#endregion