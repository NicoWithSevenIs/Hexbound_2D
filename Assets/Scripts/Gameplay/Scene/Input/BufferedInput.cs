using UnityEngine;

public enum BufferedInputType
{
    JUMP, 
    ATTACK,
    BASE_ACTIVE,
    STRATUM_ACTIVE,
}

public class BufferedInput 
{
    public BufferedInputType type;
    public bool is_held;
    public float input_time;
    public float lifetime;
}
