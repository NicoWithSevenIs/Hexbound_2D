
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class MVP_ProximityAttack : MonoBehaviour
{
    [SerializeField] private Transform attack_origin;

    [SerializeField] private float range_radius;
    [SerializeField] private float chain_range;
    [SerializeField] private GameObject chain_lightning_prefab;

    private void OnChain(List<GameObject> chain_list)
    {
        foreach (var link in chain_list)
        {
            var damageable = link.GetComponent<IDamageable>();

            if (damageable == null)
            {
                return;
            }

            damageable.ReceiveDamage(100, null, null);
        }

        var vfx_chain_list = new List<GameObject>() { attack_origin.gameObject };
        vfx_chain_list.AddRange(chain_list);

        var vfx_chain_list_pos = new List<Vector3>();
        foreach (var link in vfx_chain_list)
        {
            vfx_chain_list_pos.Add(link.transform.position);
        }

        var chain_lightning = Instantiate(chain_lightning_prefab).GetComponent<ChainLightning>();
        chain_lightning.Initialize(vfx_chain_list_pos);

    }

    public void Attack()
    {
        var go = AttackUtilities.GetNearestUnit(attack_origin, range_radius);

        if (go == null)
        {
            var x_offset = attack_origin.position;
            x_offset.x += range_radius * Mathf.Sign(transform.localScale.x);

            var no_target_hit = new List<Vector3>() { attack_origin.position, x_offset };
            var chain_lightning = Instantiate(chain_lightning_prefab).GetComponent<ChainLightning>();
            chain_lightning.Initialize(no_target_hit);
            return;
        }

        //turn to face the target if facing away
        var scale = transform.localScale;
        scale.x = (transform.position.x < go.transform.position.x ? 1 : -1) * Math.Abs(scale.x);
        transform.localScale = scale;

        var chain_args = new AttackUtilities.ChainArgs
        {
            origin = go,
            chain_count = 4,
            chain_radius = 7,
            on_chain_formed = OnChain,
        };
        AttackUtilities.Chain(chain_args);
    }



}


/*
[SerializeField] private int attack_index;

#region attack
public void Initialize(CharacterInstance parent)
{
    source = parent;
}

public void Trigger()
{
    gameObject.SetActive(true);
}

public void DamageOverlap()
{
    var capsule = GetComponent<CapsuleCollider2D>();
    var collisions = new List<Collider2D>();
    Physics2D.OverlapCollider(capsule, collisions);




    foreach (var coll in collisions)
    {
                 var damageable = GetComponent<IDamageable>();
        damageable.ReceiveDamage()
         }
}

public void DestroyProjectile()
{
    gameObject.SetActive(false);
}

*/