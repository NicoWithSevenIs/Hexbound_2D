using UnityEngine;
using Hexbound.Stats;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "Character", menuName = "Units/Character", order = 2)]
public class Character : Unit
{
    public Path main_path;
    public Path secondary_path;

    [Header("Action Multipliers")]
    [Space]
    public CharacterActionData grounded_basic;
    public CharacterActionData grounded_heavy;
    public CharacterActionData aerial_basic;
    public CharacterActionData aerial_heavy;
    public CharacterActionData plunge;

    [Header("Abilities")]
    [Space]
    public AbilitySet base_abilities;
    public AbilitySet somato_abilities;
    public AbilitySet aether_abilities;
    public AbilitySet onero_abilities;
    public Ability ultimate;
}


