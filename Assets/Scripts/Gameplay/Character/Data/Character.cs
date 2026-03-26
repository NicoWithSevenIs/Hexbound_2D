using UnityEngine;
using Hexbound.Stats;
using System.Collections.Generic;
using System;

[CreateAssetMenu(fileName = "Character", menuName = "Units/Character", order = 2)]
public class Character : Unit
{
    [SerializeField] ActionMultipliers action_multipliers;
}


