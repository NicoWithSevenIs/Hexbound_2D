using Hexbound.Stats;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;



public class CharacterInstance : UnitInstance<Character>
{
    private Character_Build char_build;
    private Stats build_stats;

    private Stratum current_stratum;

    public void Load(Character character, Character_Build build)
    {
        unit = character;
        char_build = build;

        build_stats = unit.base_stats + char_build.bonuses;
        current_stats = new(build_stats);
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

    public void SwitchStrata(Stratum entry_stratum)
    {
        if(!IsActive) return;
        var departing_stratum = current_stratum;
        current_stratum = entry_stratum;
        var events = GetComponentInParent<CharacterEvents>();
        events.DoOnListeners<IOnStratumSwitched>(listener => listener.OnStratumSwitched(this, entry_stratum, departing_stratum));
    }

    #region Getters
        public Character CharacterData { get => unit; }
        public Character_Build Build { get => char_build; }
        public Stats BaseStats { get => unit.base_stats; }
        public Stats BuildStats { get => build_stats; }
        public Stats CurrentStats { get => current_stats; }

        public bool Loaded { get => unit != null; }
        public Stratum CurrentStratum { get => current_stratum; set => SwitchStrata(value); }

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
    #endregion
}


