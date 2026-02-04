using Hexbound.Stats;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Character_Instance : MonoBehaviour, IDamageable
{
    private Character_Build char_build;

    private Character_Stats base_stats;
    private Character_Stats build_stats;
    private Character_Stats current_stats;

    public void Load(Character character, Character_Build build)
    {
        base_stats = new();
        base_stats.BasicStats = character.base_stats;
        base_stats.AdvancedStats = character.advanced_stats;

        char_build = build;

        build_stats = base_stats + char_build.bonuses;
        current_stats = new(build_stats);
    }

    public void ReceiveDamage(float dmg, List<Damage_Tag> damage_tags, IDamageable source)
    {
        if (!gameObject.activeSelf || current_stats.BasicStats.HP <= 0)
            return;

        current_stats.BasicStats.HP = Mathf.Max(0,current_stats.BasicStats.HP - dmg);

        if (current_stats.BasicStats.HP > 0) 
            return;

        EventBroadcaster.InvokeEvent(Event_Names.Character_Instance.ON_CHARACTER_DEFEATED);
    }

    #region Getters

    public Character_Build Build { get => char_build; }
    public Character_Stats BaseStats { get => base_stats; }
    public Character_Stats BuildStats { get => build_stats; }
    public Character_Stats CurrentStats { get => current_stats; }

    #endregion
}


