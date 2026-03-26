using System;
using UnityEngine;
using Hexbound.Stats;

[CreateAssetMenu(fileName ="Unit", menuName = "Units/Base Unit", order = 1)]
public class Unit: ScriptableObject
{
    [Header("Attributes")]
    public uint id;
    public string unit_name;

    [Space]
    [Header("Stats")]
    public Stats stats;
}

