using System.Collections.Generic;
using UnityEngine;

public partial class WorldManager : SingletonBehaviour<WorldManager>
{
    
    [SerializeField] private GameObject debug_prefab;
    [SerializeField] private CharacterEvents character;


    private void Start()
    {
        RegisterWorld();
    }

    public void CreateEntity(GameObject prefab, Vector2? pos = null)
    {
        var instance = Instantiate(prefab);
        instance.transform.position = pos == null ? Vector2.zero : pos.Value;
        character.HookUpCharacterEvents(instance);
    }

    public void RegisterWorld()
    {
        List<Transform> scene = new(FindObjectsByType<Transform>(FindObjectsInactive.Include, FindObjectsSortMode.None));
        Debug.Log(scene.Count);
        foreach (Transform t in scene)
        {
            Debug.Log(t.gameObject.name);
            character.HookUpCharacterEvents(t.gameObject);
        }
    }



}


