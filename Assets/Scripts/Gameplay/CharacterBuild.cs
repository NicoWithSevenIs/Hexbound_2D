using UnityEngine;


[CreateAssetMenu(fileName = "TempBuild", menuName = "Debug/Temp Build", order = 1)]
public class Character_Build : ScriptableObject
{
    public int character_id;
    public int level;
    public float exp;
}