using System.Collections.Generic;
using UnityEngine;

public partial class WorldManager : SingletonBehaviour<WorldManager>
{
    
    [SerializeField] private GameObject debug_prefab;

    [SerializeField] private CharacterEvents character_events;
    [SerializeField] private Ability_EventAdapter ability_events;

    [SerializeField] private List<UnitInstance> unit_list = new();

    protected override void Awake()
    {
        base.Awake();
        RegisterWorld();
    }

    public void CreateEntity(GameObject prefab, Vector2? pos = null)
    {
        var instance = Instantiate(prefab);
        instance.transform.position = pos == null ? Vector2.zero : pos.Value;
        character_events.TryRegisterEvents(instance);
    }

    public void RegisterWorld()
    {
        List<Transform> scene = new(FindObjectsByType<Transform>(FindObjectsInactive.Include, FindObjectsSortMode.None));
        foreach (Transform t in scene)
        {
            character_events.TryRegisterEvents(t.gameObject);
            ability_events.TryRegisterAbility(t.gameObject);
            var unit_instance = t.GetComponent<UnitInstance>();
            if (unit_instance)
            {
                unit_list.Add(unit_instance);
            }
        }
    }

}


