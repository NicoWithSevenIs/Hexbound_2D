using UnityEngine;
using Hexbound.Stats;
using UnityEngine.Events;


public class Character_Instance : MonoBehaviour, IDamageable
{
    #region Debug
    [SerializeField] private Character debug_character;
    [SerializeField] private Character_Build debug_build;

    public UnityEvent<CharacterStat_Snapshot> OnBaseStatsUpdated;
    public UnityEvent<CharacterStat_Snapshot> OnBuildStatsUpdated;
    public UnityEvent<CharacterStat_Snapshot, CharacterStat_Snapshot> OnBattleStatsUpdated;
    #endregion

    private Character_Build char_build;

    [SerializeField] private Character_Stats base_stats;
    private Character_Stats build_stats;
    private Character_Stats current_stats;


    public void Load()
    {
        base_stats = new();
        base_stats.BasicStats = debug_character.base_stats;
        base_stats.AdvancedStats = debug_character.advanced_stats;

        build_stats = Utilities.Copy(base_stats);
        current_stats = Utilities.Copy(build_stats);
    }


    public void ReceiveDamage(float dmg)
    {
        
    }

    


    private void CalculateTotalStats()
    {
        OnBaseStatsUpdated?.Invoke(Utilities.Snapshot(base_stats));
        OnBuildStatsUpdated?.Invoke(Utilities.Snapshot(build_stats));
        OnBattleStatsUpdated?.Invoke(Utilities.Snapshot(current_stats), Utilities.Snapshot(build_stats));
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

}


