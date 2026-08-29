using UnityEngine;

public enum PlayerActionType
{
    JUMP, 
    ATTACK,
    BASE_ACTIVE,
    STRATUM_ACTIVE,
}

public class PlayerAction 
{
    public PlayerActionType type;
    public bool is_held;
    public float input_time;
    public float lifetime;
}
