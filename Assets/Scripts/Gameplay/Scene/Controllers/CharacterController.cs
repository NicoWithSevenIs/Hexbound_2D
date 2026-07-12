using Hexbound.Stats;
using System.Collections.Generic;
using UnityEngine;

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
    #endregion

    private static float GROUND_CHECK_OFFSET = 0.5f;
    private float move_axis = 0;
    private bool is_grounded = false;
    private bool is_attacking = false;
    private int attack_count = 0;

    #region Ground Checking
        [SerializeField] private List<LayerMask> exclude_list;
        private LayerMask excluded = 0;
        public bool IsGrounded() => Physics2D.CapsuleCast(coll.bounds.center, coll.size, coll.direction, 0, Vector2.down, GROUND_CHECK_OFFSET, ~excluded);   
    #endregion

    private void FixedUpdate()
    {
        if (state.can_move && move_axis != 0)
        {
            var move_spd = character.CurrentStats[StatType.MOVE_SPEED];
            body.AddForce(new Vector2(move_axis * move_spd, 0));
        }
    }

    private void Update()
    {
        var x_dir = Mathf.Abs(move_axis);
        is_grounded = IsGrounded(); //temp
        anim.SetFloat("x_dir", x_dir);
        anim.SetBool("is_grounded", is_grounded);
        anim.SetFloat("y_vel", body.linearVelocity.y);
        if (x_dir > 0)
            sprite.flipX = move_axis < 0;
    }

    #region Buffered Action
    public bool PollBufferedAction(BufferedInputType action_type)
    {
        bool success = false;
        switch (action_type)
        {
            case BufferedInputType.JUMP:
                if (state.can_jump)
                {
                    anim.SetBool("is_jumping", true);
                    success = true;
                }
                break;
            case BufferedInputType.ATTACK:
                if (state.can_attack && !is_attacking)
                {
                    anim.SetBool("is_attacking", true);
                    anim.SetInteger("attack_count", attack_count);
                    attack_count++;
                    success = true;
                }
                break;
        }
        return success;
    }

    public void Jump()
    {
        var jump_force = character.CurrentStats[StatType.JUMP_FORCE];
        body.AddForce(new Vector2(0, jump_force), ForceMode2D.Impulse);
        anim.SetBool("is_jumping", false);
        state.can_attack = true;
        state.can_jump = true;
    }

    public void EndAttackTemp()
    {
        anim.SetBool("is_attacking", false);
        state.can_jump = true;
        state.can_attack = true;
    }
    #endregion Buffered Action

    public float MovementAxis { get => move_axis; set => move_axis = value; }
}
