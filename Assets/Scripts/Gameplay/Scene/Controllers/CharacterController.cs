using Hexbound.Stats;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    #region References
        //Character
        private CharacterState state;
        private CharacterInstance character;
        
        //Physics
        private Rigidbody2D body;
        private CapsuleCollider2D coll;

        //Rendering
        private SpriteRenderer sprite;
        private Animator anim;

        private void Awake()
        {
            state = GetComponent<CharacterState>();
            character = GetComponent<CharacterInstance>();

            body = GetComponentInParent<Rigidbody2D>();
            coll = GetComponentInParent<CapsuleCollider2D>();

            sprite = GetComponent<SpriteRenderer>();
            anim = GetComponent<Animator>();

            foreach (LayerMask m in exclude_list)
            {
                excluded |= m;
            }
        }
        private void Start()
        {
            anim.SetInteger("attack_count", attack_index); //replace 3 with actual attack loop count
        }
    #endregion

    #region Ground Checking

    private static float GROUND_CHECK_OFFSET = 0.5f;
    [SerializeField] private List<LayerMask> exclude_list;
    private LayerMask excluded = 0;
    public bool IsGrounded() => Physics2D.CapsuleCast(coll.bounds.center, coll.size, coll.direction, 0, Vector2.down, GROUND_CHECK_OFFSET, ~excluded);
    #endregion

    private float move_axis = 0;
    private bool is_grounded = false;
    
    private int attack_index = 0;
    private float last_attacked = 0;

    private void FixedUpdate()
    {
        if (state.can_move && move_axis != 0)
        {
            var move_spd = character.CurrentStats[StatType.MOVE_SPEED];
            body.AddForce(new Vector2(move_axis * move_spd, 0));
        }

        var vel = body.linearVelocity;
        if (state.is_hovering)
        {
            vel.y = 0;
            body.linearVelocity = vel;
            body.AddForce(-Physics.gravity * body.gravityScale * body.mass);
        }
    }

    private void Update()
    {
        var x_dir = Mathf.Abs(move_axis);
        is_grounded = IsGrounded(); //temp
        last_attacked += Time.deltaTime;

        anim.SetFloat("last_attacked", last_attacked);
        anim.SetFloat("x_dir", x_dir);
        anim.SetBool("is_grounded", is_grounded);
        anim.SetFloat("y_vel", body.linearVelocity.y);
        anim.SetBool("hovering", state.is_hovering);

        /*
        if (x_dir > 0)
            sprite.flipX = move_axis < 0;
        */

        if(x_dir != 0)
        {
            var scale = body.transform.localScale;
            scale.x = Mathf.Sign(move_axis) * x_dir;
            body.transform.localScale = scale;
        }
        

        //doesnt have to be in update this oul be calculated on aerial attack
        var mouse_pos = Mouse.current.position.ReadValue();
        var cursor_y_pos_diff = Camera.main.ScreenToWorldPoint(mouse_pos).y - transform.position.y;

        anim.SetFloat("cursor_y_pos_diff", cursor_y_pos_diff);
    }

    #region Buffered Action
    public void InitiateAction(PlayerActionType action_type)
    {
   
        switch (action_type)
        {
            case PlayerActionType.JUMP:
                if (state.can_jump)
                {
                    anim.SetTrigger("jump");
                }
                break;
            case PlayerActionType.ATTACK:
                if (state.can_attack)
                {
                    anim.SetTrigger("attack");    
                }
            break;
        }

    }

    public void Jump()
    {
        var jump_force = character.CurrentStats[StatType.JUMP_FORCE];
        body.AddForce(new Vector2(0, jump_force), ForceMode2D.Impulse);
        body.constraints &= ~RigidbodyConstraints2D.FreezePosition;
    }

    public void Attack()
    {
        attack_index = (attack_index + 1 ) % 2;
        anim.SetInteger("attack_count", attack_index); //replace 3 with actual attack loop count
        last_attacked = 0f;
    }
    public void AerialJumpWindup()
    {
        body.constraints |= RigidbodyConstraints2D.FreezePosition;
    }

    #endregion Buffered Action

    public float MovementAxis { get => move_axis; set => move_axis = value; }
    public bool IsHovering { get => state.is_hovering; set => state.is_hovering = value; }
}
