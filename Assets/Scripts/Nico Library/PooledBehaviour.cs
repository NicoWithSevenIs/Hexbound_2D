using System.Collections.Generic;
using UnityEngine;

public abstract class PooledBehaviour: MonoBehaviour 
{
    [SerializeField] private PoolableBehaviour poolable_prefab;
    [SerializeField] private Transform poolable_parent;

    protected List<PoolableBehaviour> pool = new();

    protected PoolableBehaviour RequestPoolable()
    {
        if(pool.Count == 0)
        {
            var new_poolable = Instantiate(poolable_prefab.gameObject, poolable_parent);
            return new_poolable.GetComponent<PoolableBehaviour>();
        }
        
        var poolable = pool[pool.Count - 1];
        pool.RemoveAt(pool.Count - 1);
        poolable.gameObject.SetActive(true);
        return poolable;
    }

    protected void ReleasePoolable(PoolableBehaviour poolable)
    {
        poolable.gameObject.SetActive(false);
        pool.Add(poolable);
    }

    protected List<PoolableBehaviour> RequestPoolableBatch(int count)
    {
        var poolables = new List<PoolableBehaviour>();
        for(int i = 0; i < count; i++)
        {
            poolables.Add(RequestPoolable());
        }
        return poolables;
    }
}
