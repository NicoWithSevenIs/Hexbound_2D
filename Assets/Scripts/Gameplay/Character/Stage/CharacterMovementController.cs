using Hexbound.Stats;
using System.Collections.Generic;
using UnityEngine;

public partial class CharacterMovementController : MonoBehaviour
{

    /*
         [TO-DO]: Do after Ability System
            -> Anti-gravity effects for jump and dash
            -> Dash cooldown 
    */

    private Rigidbody2D body;
    private CapsuleCollider2D coll;

    private CharacterInstance ch;
    private CharacterState state;

    [SerializeField] private List<LayerMask> exclude_list;

    private void Awake()
    {
        body = GetComponentInParent<Rigidbody2D>();
        coll = GetComponentInParent<CapsuleCollider2D>();
        ch = GetComponent<CharacterInstance>();
        state = GetComponent<CharacterState>();
    }

    private void FixedUpdate()
    {
        if (state.can_move)
        {
            var move_spd = ch.CurrentStats.BasicStats.MOV_SPD;
            if (is_walking)
                move_spd *= walk_multiplier;
            body.AddForce(new Vector2(movement_axis * move_spd, 0));
        }
        
        if (will_jump)
        {
            var jump_force = ch.CharacterData.hidden_stats.JUMP_FORCE;
            body.AddForce(new Vector2(0, jump_force), ForceMode2D.Impulse);
            jump_count++;
            will_jump = false;
        }

        if (will_dash)
        {
            var dash_force = ch.CharacterData.hidden_stats.DASH_FORCE;
            if (body.linearVelocity.x != 0 && IsGrounded())
            {
                dash_direction.x = Mathf.Sign(body.linearVelocity.x);
            }
            body.AddForce(dash_force * dash_direction, ForceMode2D.Impulse);
            will_dash = false;
        }
    }

    private void Update()
    {
        if(body.linearVelocity.y >= 0 && IsGrounded())
        {
            jump_count = 0;
        }
    }


}

#region Input Receiver
public partial class CharacterMovementController
{



    #region attributes

    //Walk & Run
    private float movement_axis = 0f;
    private float walk_multiplier = 0.4f;
    private bool is_walking = false;

    //Jump
    [SerializeField] private bool will_jump = false;
    [SerializeField] private int jump_count = 0;
    [SerializeField] private float ground_check_offset = 0.5f;

    //Dash
    private bool will_dash = false;
    private Vector2 dash_direction = Vector2.zero;
    #endregion

    public void SetMovementAxis(float dir)
    {
        movement_axis = dir;
    }

    public void SetJumpFlag()
    {
        if (!state.can_jump)
        {
            return;
        }

        bool grounded = IsGrounded();

        var max_jumps = ch.CharacterData.hidden_stats.MAX_JUMPS;
        if (grounded || !grounded && jump_count < max_jumps)
        {
            will_jump = true;
        }
    }

    public void SetDashFlag(Vector2 dash_direction)
    {
        if (state.can_dash) // add timer here
        {
            if (IsGrounded())
            {
                dash_direction.y = 0f;
            }
            this.dash_direction = dash_direction;
            will_dash = true;
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