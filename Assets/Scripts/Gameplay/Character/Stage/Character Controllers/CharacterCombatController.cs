using UnityEngine;


public class CharacterCombatController : CharacterController
{
   
    [SerializeField] private float global_min_hold_duration = 0.6f;


    public void TriggerBasicAttack(float input_duration)
    {
        bool is_aerial = !IsGrounded();
        bool is_held = input_duration > global_min_hold_duration;

        if (!is_aerial && !is_held)
        {
            Debug.Log("Grounded Basic");

            var colls = Physics2D.OverlapBoxAll(transform.position + transform.right * Mathf.Sign(transform.localScale.x) * 3f, Vector3.one * 5f, 0);
            foreach(var coll in colls)
            {
                var damageable =  coll.GetComponent<IDamageable>();
                if(damageable != null)
                {
                    damageable.ReceiveDamage(ch.CurrentStats.BasicStats.ATK, null, ch);
                }
            }
            
        }
        else if (!is_aerial && is_held)
        {
            Debug.Log("Grounded Heavy");
        }
        else if(is_aerial && !is_held)
        {
            Debug.Log("Aerial Basic");
        }
        else if (is_aerial && is_held)
        {
            Debug.Log("Aerial Heavy");
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        var scale = transform.localScale;
        Gizmos.DrawWireCube(transform.position + transform.right * Mathf.Sign(transform.localScale.x) * 3f, Vector3.one * 5f);
    }


    //wip, plunge should be automatically triggered when global_min_hold_duration is met rather than on key up
    public void TriggerPlunge(float input_duration)
    {
        if (!IsGrounded() && input_duration > global_min_hold_duration)
        {
            Debug.Log("Plunging");
        }
    }
}
