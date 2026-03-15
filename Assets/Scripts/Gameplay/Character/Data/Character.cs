using UnityEngine;
using Hexbound.Stats;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "Character", menuName = "Units/Character", order = 2)]
public class Character : Unit
{
    


    public Advanced_Stat advanced_stats;
    public Hidden_Stat hidden_stats;

    [Serializable]
    public class ActionMultiplier
    {
        public string action_name;
        public List<Multiplier> multipliers;
    }
    [Serializable]
    public class Multiplier
    {
        public float value;
        public STAT stat;
    }

    [Header("Action Multipliers")]
    public List<ActionMultiplier> grounded_basic;
    public List<ActionMultiplier> grounded_heavy;
    public List<ActionMultiplier> aerial_basic;
    public List<ActionMultiplier> aerial_heavy;
    public List<ActionMultiplier> plunge;
}


