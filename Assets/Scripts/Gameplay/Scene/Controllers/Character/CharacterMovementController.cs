using Hexbound.Stats;
using System.Collections.Generic;
using UnityEngine;

public partial class CharacterMovementController : CharacterControlHandler
{

    /*
         [TO-DO]: Do after Ability System
            -> Anti-gravity effects for jump and dash
            -> Dash cooldown 
    */

    private void FixedUpdate()
    {
        if (state.can_move)
        {
            var move_spd = ch.CurrentStats[StatType.MOVE_SPEED];
            if (is_walking)
                move_spd *= walk_multiplier;
            body.AddForce(new Vector2(movement_axis * move_spd, 0));
        }
        
        if (will_jump)
        {
            var jump_force = ch.CurrentStats[StatType.JUMP_FORCE];
            body.AddForce(new Vector2(0, jump_force), ForceMode2D.Impulse);
            jump_count++;
            will_jump = false;
        }

        if (will_dash)
        {
            var dash_force = ch.CurrentStats[StatType.DASH_FORCE];
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

        if(movement_axis != 0)
        {
            //switch this to sprite
            var scale = transform.localScale;
            scale.x = Mathf.Sign(movement_axis) * Mathf.Abs(scale.x);
            transform.localScale = scale;
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

        var max_jumps = ch.CurrentStats[StatType.MAX_JUMPS];
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

   
}
#endregion