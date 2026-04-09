using Hexbound.Stats;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public partial class CharacterInstance : MonoBehaviour, IDamageable
{
    private Character character_data;
    private Character_Build char_build;

    private Stats build_stats;
    private Stats current_stats;

    private Path current_path;
    private bool is_active;

    public void Load(Character character, Character_Build build)
    {
        character_data = character;
        char_build = build;

        build_stats = character.stats + char_build.bonuses;
        current_stats = new(build_stats);

        SwitchPaths(character.main_path);
    }

    public void ReceiveDamage(float dmg, List<Damage_Tag> damage_tags, IDamageable source)
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
    public Character CharacterData { get => character_data; }
    public Character_Build Build { get => char_build; }
    public Stats BaseStats { get => character_data.stats; }
    public Stats BuildStats { get => build_stats; }
    public Stats CurrentStats { get => current_stats; }
    public bool Loaded { get => character_data != null;  }
    public Path CurrentPath { get => current_path; set => SwitchPaths(value); }

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