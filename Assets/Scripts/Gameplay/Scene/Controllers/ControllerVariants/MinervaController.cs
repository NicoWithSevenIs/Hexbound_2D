using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Hexbound.Stats;
public class MinervaController : CharacterController
{
    [SerializeField] private Transform attack_origin;
    [SerializeField] private ChainLightning chain_lightning_prefab;

    public override void Attack()
    {
        attack_index = (attack_index + 1) % 2;
        var range_radius = character.CurrentStats[StatType.ATK_RANGE];

        var go = AttackUtilities.GetNearestUnit(attack_origin, range_radius);

        var x_offset = attack_origin.position;
        x_offset.x += range_radius * Mathf.Sign(transform.localScale.x);

        var origin_to_target = new List<Vector3>() { attack_origin.position, go == null ? x_offset : go.transform.position };
        var chain_lightning = Instantiate(chain_lightning_prefab).GetComponent<ChainLightning>();
        chain_lightning.Initialize(origin_to_target);

        anim.SetInteger("attack_count", attack_index); //replace 3 with actual attack loop count
        last_attacked = 0f;

        var damageable = go.GetComponent<IDamageable>();
        var hit = new List<IDamageable>() { };
        if(damageable != null)
            hit.Add(damageable);

        events.DoOnListeners<IOnBasicAttack>(e => e.OnBasicAttack(character, hit, false, false));
    }

    private void OnDrawGizmos()
    {
        if (Application.isPlaying)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, character.CurrentStats[StatType.ATK_RANGE]);
        }
    }

    protected override void UpdateAnimatorData()
    {
        var x_dir = Mathf.Abs(move_axis);
        is_grounded = IsGrounded(); //temp
        last_attacked += Time.deltaTime;

        anim.SetFloat("last_attacked", last_attacked);
        anim.SetFloat("x_dir", x_dir);
        anim.SetBool("is_grounded", is_grounded);
        anim.SetFloat("y_vel", body.linearVelocity.y);
        anim.SetBool("hovering", state.is_hovering);


        //doesnt have to be in update this oul be calculated on aerial attack
        var mouse_pos = Mouse.current.position.ReadValue();
        var cursor_y_pos_diff = Camera.main.ScreenToWorldPoint(mouse_pos).y - transform.position.y;

        anim.SetFloat("cursor_y_pos_diff", cursor_y_pos_diff);
    }

}
