using UnityEngine;
using Hexbound.Stats;
using UnityEngine.Events;


public class Character_Instance : MonoBehaviour, IDamageable
{
    #region Debug
    [SerializeField] private Character debug_character;
    [SerializeField] private Character_Build debug_build;
    public UnityEvent<CharacterStat_Snapshot> OnStatsUpdated;
    #endregion

    private Character_Build char_build;
    [SerializeField] private Character_Stats stats;

    public void Load()
    {
        stats = new();
        stats.BasicStats = debug_character.base_stats;
        stats.AdvancedStats = debug_character.advanced_stats;
    }


    public void ReceiveDamage(float dmg)
    {
        
    }

    


    private void CalculateTotalStats()
    {
        var snapshot = Utilities.Snapshot(stats);
        OnStatsUpdated?.Invoke(snapshot);
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


