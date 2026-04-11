using Hexbound.Stats;
using System.Data;
using System.Runtime.ConstrainedExecution;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.TextCore.Text;

public partial class CharacterManager : MonoBehaviour
{

    /*
        [TO-DO]: After Character Ability System
            -> Dynamic Party Modification (Character joins in the middle of the stage.
    */

    #region Debug
    [Header("Debug")]
    [SerializeField] private Character debug_character_1;
    [SerializeField] private Character_Build debug_c1_build;

    [SerializeField] private Character debug_character_2;
    [SerializeField] private Character_Build debug_c2_build;

    [SerializeField] private bool load_c1_only = false;
    #endregion

    [Header("References")]
    [SerializeField] private CharacterInstance character_1;
    [SerializeField] private CharacterInstance character_2;

    private bool has_character_2;
    private CharacterInstance current_character;
    private CharacterEvents char_events;

    public void SwitchCharacters()
    {
        if (!has_character_2)
        {
            return;
        }

        var departing = current_character;
        var entering = current_character == character_1 ? character_2 : character_1;
        current_character = entering;

        character_1.IsActive = character_1 == current_character;
        character_2.IsActive = character_2 == current_character;

        char_events.DoOnListeners<IOnCharacterSwitched>(i => i.OnCharacterSwitched(entering, departing));
    }

    private void InitializeCharacters()
    {
        current_character = character_1;
        character_1.IsActive = character_1 == current_character;
        character_1.Load(debug_character_1, debug_c1_build);
 
        if (has_character_2)
        {
            character_2.IsActive = character_2 == current_character;
            character_2.Load(debug_character_2, debug_c2_build);
        }

        void LoadCharacter(IOnCharacterLoaded i) => i.OnCharacterLoaded(character_1, character_2);
        char_events.DoOnListeners<IOnCharacterLoaded>(LoadCharacter);


        character_1.SwitchPaths(debug_character_1.main_path);
        character_2.SwitchPaths(debug_character_2.main_path);
    }
}

#region Lifecycle Methods
public partial class CharacterManager
{
    Timer UpdateStats;

    private void Awake()
    {
        char_events = GetComponent<CharacterEvents>();
        character_1.gameObject.SetActive(true);
        character_2.gameObject.SetActive(true);
    }

    private void Start()
    {
        has_character_2 = !load_c1_only;
        UpdateStats = new Timer(0.1f, CalculateTotalStats);
        InitializeCharacters();
    }

    private void Update()
    {
        UpdateStats.Tick();
    }

    private void CalculateTotalStats()
    {
        OnStatsUpdating?.Invoke(current_character.BaseStats, current_character.BuildStats, current_character.CurrentStats);
    }

}
#endregion

#region Events
public partial class CharacterManager
{
    [Header("Stat Calculation Events")]
    [Space]
    public UnityEvent<Stats, Stats, Stats> OnStatsUpdating;
}
#endregion

#region Getters
public partial class CharacterManager
{
    public CharacterInstance CurrentCharacter { get => current_character; }
}
#endregion