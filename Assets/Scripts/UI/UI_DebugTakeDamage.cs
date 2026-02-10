using UnityEngine;

public class UI_DebugTakeDamage : MonoBehaviour
{
    [Header("Reference")]
    [SerializeField] private CharacterInstance target;

    [SerializeField] private float amount;
    public void DeclareDamage()
    {
        DamageManager.ApplyDamage(amount, target);
    }
}
