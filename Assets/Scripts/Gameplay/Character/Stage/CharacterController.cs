using System.Collections.Generic;
using UnityEngine;

public partial class CharacterController : MonoBehaviour
{

    private Rigidbody2D body;
    private CapsuleCollider2D coll;

    private CharacterInstance ch;
    private CharacterState state;

    [SerializeField] private List<LayerMask> exclude_list;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        coll = GetComponent<CapsuleCollider2D>();
        ch = GetComponent<CharacterInstance>();
        state = GetComponent<CharacterState>();
    }

    private void FixedUpdate()
    {
        if (state.can_move)
        {
            var move_spd = ch.CurrentStats.BasicStats.MOV_SPD;
            body.AddForce(new Vector2(movement_axis * move_spd, 0));
        }
        
        if (state.can_jump && will_jump)
        {
            var jump_force = ch.CharacterData.hidden_stats.JUMP_FORCE;
            if (is_walking)
                jump_force *= walk_multiplier;
            body.AddForce(new Vector2(0, jump_force), ForceMode2D.Impulse);
            will_jump = false;
        }
    }

    private void Update()
    {
        Debug.Log(IsGrounded());
    }

    private void OnDrawGizmos()
    {
        
    }

}

#region Input Receiver
public partial class CharacterController
{

    #region attributes

    //Walk & Run
    private float movement_axis = 0f;
    private float walk_multiplier = 0.4f;
    private bool is_walking = false;

    //Jump
    private bool will_jump = false;
    private int jump_count = 0;
    private float ground_check_offset = 0.5f;

    //Dash
    private bool will_dash = false;
    private float fixed_dash_cooldown = 1;

    #endregion

    public void SetMovementAxis(float dir)
    {
        movement_axis = dir;
    }

    public void SetJumpFlag()
    {
        if (IsGrounded())
        {
            jump_count++;
        }
    }

    public bool IsGrounded()
    {
        LayerMask excluded = 0;

        foreach(LayerMask m in exclude_list)
        {
            excluded |= m;
        }

        return Physics2D.CapsuleCast(coll.bounds.center, coll.size, coll.direction, 0, Vector2.down, ground_check_offset, ~excluded);
    }



}
#endregion