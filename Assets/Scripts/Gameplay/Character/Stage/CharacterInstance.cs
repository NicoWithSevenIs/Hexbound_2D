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

    public void Load(Character character, Character_Build build)
    {
        character_data = character;
        char_build = build;

        build_stats = character.stats + char_build.bonuses;
        current_stats = new(build_stats);
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
}
#endregion