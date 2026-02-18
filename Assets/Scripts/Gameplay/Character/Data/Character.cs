using UnityEngine;
using Hexbound.Stats;

[CreateAssetMenu(fileName = "Character", menuName = "Units/Character", order = 2)]
public class Character : Unit
{
    public Advanced_Stat advanced_stats;
    public Hidden_Stat hidden_stats;
}


public class Hidden_Stat
{
    public int MAX_JUMPS;
    public float JUMP_FORCE;
}