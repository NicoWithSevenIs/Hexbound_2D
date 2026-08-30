using Hexbound.Stats;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterController : MonoBehaviour
{
    #region References
        //Character
        protected CharacterState state;
        protected CharacterInstance character;
        protected CharacterEvents events;

        //Physics
        protected Rigidbody2D body;
        protected CapsuleCollider2D coll;

        //Rendering
        protected SpriteRenderer sprite;
        protected Animator anim;

        protected virtual void Awake()
        {
            state = GetComponent<CharacterState>();
            character = GetComponent<CharacterInstance>();

            body = GetComponentInParent<Rigidbody2D>();
            coll = GetComponentInParent<CapsuleCollider2D>();
            events = coll.GetComponent<CharacterEvents>();

            sprite = GetComponent<SpriteRenderer>();
            anim = GetComponent<Animator>();

            foreach (LayerMask m in exclude_list)
            {
                excluded |= m;
            }
        }
        protected virtual void Start()
        {
            anim.SetInteger("attack_count", attack_index); //replace 3 with actual attack loop count
        }
    #endregion

    #region Ground Checking
        protected static float GROUND_CHECK_OFFSET = 0.5f;
        [SerializeField] private List<LayerMask> exclude_list;
        private LayerMask excluded = 0;
        public bool IsGrounded() => Physics2D.CapsuleCast(coll.bounds.center, coll.size, coll.direction, 0, Vector2.down, GROUND_CHECK_OFFSET, ~excluded);
    #endregion

    protected float move_axis = 0;
    protected bool is_grounded = false;

    protected int attack_index = 0;
    protected float last_attacked = 0;

    private void FixedUpdate()
    {
        Move();
        Hover();
    }

    protected virtual void Update()
    {
        UpdateAnimatorData();

        var x_dir = Mathf.Abs(move_axis);
        if (x_dir != 0)
        {
            var scale = body.transform.localScale;
            scale.x = Mathf.Sign(move_axis) * x_dir;
            body.transform.localScale = scale;
        }
    }

    protected virtual void Move()
    {
        if (state.can_move && move_axis != 0)
        {
            var move_spd = character.CurrentStats[StatType.MOVE_SPEED];
            body.AddForce(new Vector2(move_axis * move_spd, 0));
        }
    }

    protected virtual void Hover()
    {
        var vel = body.linearVelocity;
        if (state.is_hovering)
        {
            vel.y = 0;
            body.linearVelocity = vel;
            body.AddForce(-Physics.gravity * body.gravityScale * body.mass);
        }
    }

    protected virtual void UpdateAnimatorData()
    {

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

    public virtual void Jump()
    {
        var jump_force = character.CurrentStats[StatType.JUMP_FORCE];
        body.AddForce(new Vector2(0, jump_force), ForceMode2D.Impulse);
        body.constraints &= ~RigidbodyConstraints2D.FreezePosition;
    }

    public virtual void Attack()
    {
        
    }

    public virtual void AerialJumpWindup()
    {
        body.constraints |= RigidbodyConstraints2D.FreezePosition;
    }

    #endregion Buffered Action

    public float MovementAxis { get => move_axis; set => move_axis = value; }
    public bool IsHovering { get => state.is_hovering; set => state.is_hovering = value; }
}
