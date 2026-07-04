using System;
using UnityEngine;
using Hexbound.Stats;

public abstract class Unit: ScriptableObject
{
    [Header("Attributes")]
    public uint id;
    public string unit_name;

    [Space]
    [Header("Stats")]
    public Stats base_stats;
}

