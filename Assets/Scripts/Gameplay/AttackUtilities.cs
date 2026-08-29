using UnityEngine;
using System.Collections.Generic;
using System;

public static class AttackUtilities
{
    public static GameObject GetNearestUnit(Transform origin_unit, float range_radius, List<GameObject> ignore_list = null)
    {

        var colls = Physics2D.OverlapCircleAll(origin_unit.position, range_radius);
        var targets_in_range = new List<GameObject>();

        foreach (var coll in colls)
        {
            var damageable = coll.GetComponent<IDamageable>();
            bool is_not_in_ignore_list = ignore_list == null || ignore_list != null && !ignore_list.Contains(coll.gameObject);

            if (is_not_in_ignore_list && damageable != null)
                targets_in_range.Add(coll.gameObject);
        }

        if (targets_in_range.Count == 0)
            return null;

        GameObject nearest = targets_in_range[0];
        float nearest_dist = Vector3.Distance(origin_unit.position, nearest.transform.position);

        for (int i = 1; i < targets_in_range.Count; i++)
        {
            float dist = Vector3.Distance(origin_unit.position, targets_in_range[i].transform.position);

            if (dist < nearest_dist)
            {
                nearest = targets_in_range[i];
                nearest_dist = dist;
            }
        }

        return nearest;
    }

    public struct ChainArgs
    {
        public GameObject origin;
        public int chain_count;
        public float chain_radius;
        public Action<List<GameObject>> on_chain_formed;
        public List<GameObject> ignore_list;
    }

    
    public static void Chain(ChainArgs args)
    {
        if(args.origin == null)
        {
            return;
        }

        GameObject current = args.origin;
        var chain_list = new List<GameObject>() { current };

        if (args.ignore_list == null)
        {
            args.ignore_list = new List<GameObject>();
        }
        args.ignore_list.Add(current);
  
        while (chain_list.Count <= args.chain_count)
        {
            GameObject next_target = GetNearestUnit(current.transform, args.chain_radius, args.ignore_list);

            if (next_target == null)
                break;

            current = next_target;
            args.ignore_list.Add(next_target);
            chain_list.Add(next_target);
        }
        args.on_chain_formed(chain_list);
    }

    public static void Bounce()
    {
        /*
   * 
   *   if (args.chain_count == 0)
      {
          return;
      }
 if (args.chain_count == 0)
 {
     return;
 }

 if (args.ignore_list == null)
 {
     args.ignore_list = new List<GameObject>();
     Debug.Log("is null, now creating");
 }



 GameObject next_target = GetNearestUnit(args.origin.transform, args.chain_radius, args.ignore_list);

 if (next_target == null)
 {
     return;
 }

 if (args.bounce_stack == null)
 {
     args.bounce_stack = new List<GameObject>();
 }

 args.bounce_stack.Push(next_target);
 args.effect_on_bounce?.Invoke(args.bounce_count, args.bounce_stack);

 if (!args.is_repeatable)
 {
     args.ignore_list.Add(next_target);
 }

 args.bounce_count--;
 Chain(args);
 */
    }


}
