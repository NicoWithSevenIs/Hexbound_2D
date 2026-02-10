using Hexbound.Stats;
using System.Data;
using UnityEngine;
using UnityEngine.Events;

public partial class CharacterManager : MonoBehaviour
{

    #region Debug
    [SerializeField] private Character debug_character_1;
    [SerializeField] private Character_Build debug_c1_build;
    #endregion

    [Header("References")]
    [SerializeField] private CharacterInstance character_1;
    [SerializeField] private CharacterInstance character_2;

    private CharacterInstance current_character;

    public void SwitchCharacters()
    {
        bool turn = current_character == character_1;
        current_character = turn ? character_2 : character_1;
        //character_1.gameObject.SetActive(turn);
        //character_2.gameObject.SetActive(!turn);
    }


}

#region Lifecycle Methods
public partial class CharacterManager
{
    IntervalAction UpdateStats;

    private void Start()
    {
        current_character = character_1;
        character_1.Load(debug_character_1, debug_c1_build);

        UpdateStats = new IntervalAction(0.1f, CalculateTotalStats);
    }

    private void Update()
    {
        UpdateStats.Tick();
    }

    private void CalculateTotalStats()
    {
        var base_snapshot = Stat_Utilities.Snapshot(current_character.BaseStats);
        var build_snapshot = Stat_Utilities.Snapshot(current_character.BuildStats);
        var current_snapshot = Stat_Utilities.Snapshot(current_character.CurrentStats);

        OnBaseStatsUpdated?.Invoke(base_snapshot);
        OnBuildStatsUpdated?.Invoke(build_snapshot);
        OnBattleStatsUpdated?.Invoke(current_snapshot, build_snapshot);
    }

}
#endregion

#region Events
public partial class CharacterManager
{
    [Header("Stat Calculation Events")]
    [Space]
    public UnityEvent<CharacterStat_Snapshot> OnBaseStatsUpdated;
    public UnityEvent<CharacterStat_Snapshot> OnBuildStatsUpdated;
    public UnityEvent<CharacterStat_Snapshot, CharacterStat_Snapshot> OnBattleStatsUpdated;
}
#endregion

#region Getters
public partial class CharacterManager
{
    public CharacterInstance CurrentCharacter { get => current_character; }
}
#endregion