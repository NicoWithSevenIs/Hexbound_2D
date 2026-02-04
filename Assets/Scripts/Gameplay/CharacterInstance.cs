using Hexbound.Stats;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class Character_Instance : MonoBehaviour, IDamageable
{
    #region Debug
    [SerializeField] private Character debug_character;
    [SerializeField] private Character_Build debug_build;
    #endregion

    private Character_Build char_build;

    private Character_Stats base_stats;
    private Character_Stats build_stats;
    private Character_Stats current_stats;


    public void Load()
    {
        base_stats = new();
        base_stats.BasicStats = debug_character.base_stats;
        base_stats.AdvancedStats = debug_character.advanced_stats;

        char_build = debug_build;

        build_stats = base_stats + char_build.bonuses;
        current_stats = new(build_stats);
    }


    public void ReceiveDamage(float dmg, List<Damage_Tag> damage_tags, IDamageable source)
    {
        if (!gameObject.activeSelf || current_stats.BasicStats.HP <= 0)
            return;

        //mitigate here

        current_stats.BasicStats.HP = Mathf.Max(0,current_stats.BasicStats.HP - dmg);

        if (current_stats.BasicStats.HP > 0) 
            return;

        EventBroadcaster.InvokeEvent(Event_Names.Character_Instance.ON_CHARACTER_DEFEATED);
    }

    
    private void CalculateTotalStats()
    {
        OnBaseStatsUpdated?.Invoke(Stat_Utilities.Snapshot(base_stats));
        OnBuildStatsUpdated?.Invoke(Stat_Utilities.Snapshot(build_stats));
        OnBattleStatsUpdated?.Invoke(Stat_Utilities.Snapshot(current_stats), Stat_Utilities.Snapshot(build_stats));
    }


    #region lifetime_actions

    IntervalAction UpdateStats;

    private void Start()
    {
        Load();
        UpdateStats = new IntervalAction(0.1f, CalculateTotalStats);
    }

    private void Update()
    {
        UpdateStats.Tick();
    }

    #endregion

    #region Events
    [Header("Stat Calculation Events")]
    [Space]
    public UnityEvent<CharacterStat_Snapshot> OnBaseStatsUpdated;
    public UnityEvent<CharacterStat_Snapshot> OnBuildStatsUpdated;
    public UnityEvent<CharacterStat_Snapshot, CharacterStat_Snapshot> OnBattleStatsUpdated;
    #endregion
}


