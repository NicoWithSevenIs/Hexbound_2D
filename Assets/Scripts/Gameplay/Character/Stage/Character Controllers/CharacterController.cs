using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterInstance))]
public abstract class CharacterController : MonoBehaviour
{
    private static float GROUND_CHECK_OFFSET = 0.5f;

    protected Rigidbody2D body;
    protected CapsuleCollider2D coll;

    protected CharacterInstance ch;
    protected CharacterState state;

    [SerializeField] private List<LayerMask> exclude_list;
    private LayerMask excluded = 0;

    protected virtual void Awake()
    {
        body = GetComponentInParent<Rigidbody2D>();
        coll = GetComponentInParent<CapsuleCollider2D>();
        ch = GetComponent<CharacterInstance>();
        state = GetComponent<CharacterState>();

        foreach (LayerMask m in exclude_list)
        {
            excluded |= m;
        }

    }

    public bool IsGrounded()
    {        
        return Physics2D.CapsuleCast(coll.bounds.center, coll.size, coll.direction, 0, Vector2.down, GROUND_CHECK_OFFSET, ~excluded);
    }


}
