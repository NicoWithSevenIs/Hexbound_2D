using System;
using UnityEngine;


//temp implementation in the interest of time, optimize
//this physically hurts me too
public class CharacterAnimationController : CharacterControlHandler
{

    [SerializeField] private Animator animator;
    [SerializeField] private Propagator propagator;


    public void TriggerJump(Action on_leap)
    {
        void callback()
        {
            on_leap?.Invoke();
            animator.SetBool("isJumping", false);
        }
        propagator.callback = callback;
        animator.SetBool("isJumping", true);
    }

    private void Update()
    {
        animator.SetBool("isGrounded", IsGrounded());
        animator.SetFloat("x_velocity", Mathf.Abs(body.linearVelocity.x));
        animator.SetFloat("y_velocity",body.linearVelocity.y);
    }
}
