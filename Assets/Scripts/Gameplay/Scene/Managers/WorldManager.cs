using System.Collections.Generic;
using UnityEngine;

public partial class WorldManager : SingletonBehaviour<WorldManager>
{
    
    [SerializeField] private GameObject debug_prefab;

    [SerializeField] private CharacterEvents character_events;

    [SerializeField] private List<IUnitInstance> unit_list = new();

    protected override void Awake()
    {
        base.Awake();
        RegisterWorld();
    }

    public static GameObject Create(GameObject prefab, Transform parent = null, Vector2? pos = null)
    {
        var new_gameobject = GameObject.Instantiate(prefab);
        new_gameobject.transform.parent = parent;
        new_gameobject.transform.position = pos == null ? Vector2.zero : pos.Value;

        instance.RegisterInstance(new_gameobject);
        return new_gameobject;
    }

    public void RegisterWorld()
    {
        List<Transform> scene = new(FindObjectsByType<Transform>(FindObjectsInactive.Include, FindObjectsSortMode.None));
        foreach (Transform t in scene)
        {;
            RegisterInstance(t.gameObject);
        }
    }

    private void RegisterInstance(GameObject instance)
    {
        character_events.TryRegisterEvents(instance.gameObject);
       
        var unit_instance = instance.GetComponent<IUnitInstance>();
        if (unit_instance != null)
        {
            unit_list.Add(unit_instance);
        }
        
    }
}


