using Hexbound.Stats;
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Multiplier
{
    public StatType stat;
    public float value;
}

[Serializable]
public class ActionMultipliers
{
    public List<Multiplier> grounded_basic;
    public List<Multiplier> grounded_heavy;
    public List<Multiplier> aerial_basic;
    public List<Multiplier> aerial_heavy;
    public List<Multiplier> plunge;
}
